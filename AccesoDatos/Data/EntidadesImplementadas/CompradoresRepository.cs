using AccesoDatos.BlogCore.Models;
using AccesoDatos.Data.Helpers;
using AccesoDatos.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Modelos.Entidades;
using Modelos.ViewModels.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccesoDatos.Data.EntidadesImplementadas
{
    public class CompradoresRepository : Repository<TbClienteComprador>, ICompradoresRepository
    {
        private readonly SolyLunaDbContext context;
        private readonly IBitacoraRepository bitacora;
        private readonly IUserHelper userHelper;

        public CompradoresRepository(SolyLunaDbContext context, IBitacoraRepository bitacora, IUserHelper userHelper) : base(context)
        {
            this.context = context;
            this.bitacora = bitacora;
            this.userHelper = userHelper;
        }

        public async Task<List<TbPersonaJuridica>> ObtenerTodosTiposPersonaJuridica()
        {
            return await context.TbPersonaJuridicas.ToListAsync();
        }

        public async Task<List<TbTipoIntermediario>> ObtenerTodosLosTiposIntermediarios()
        {
            return await context.TbTipoIntermediarios.ToListAsync();
        }
        public async Task<List<TbDocumento>> ObtenerTodosLosTiposDocumentos()
        {
            return await context.TbDocumentos.ToListAsync();
        }

        public async Task<Response> RegistrarComprador(RegistrarCompradorViewModel model, string usuario)
        {
            Response response = null;

            if (model != null)
            {

                TbPersona comprador = await context.TbPersonas.FirstOrDefaultAsync(
                    v => v.Identificacion.Equals(model.Identificacion)
                    || v.Email.Equals(model.CorreoElectronico));

                /*Verificar si el comprador ya existe con dicha cedula*/
                if (comprador != null)
                {
                    return response = new Response { EsCorrecto = false, Mensaje = "Ya existe" };
                }

                IExecutionStrategy strategy = context.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    using IDbContextTransaction transaction = context.Database.BeginTransaction();

                    try
                    {
                        TbPersonaJuridica personaJuridica = null;
                        if (!string.IsNullOrEmpty(model.NombreEntidad))
                        {
                            TbPersonaJuridica existe = await context.TbPersonaJuridicas.FirstOrDefaultAsync(x =>
                            x.NombreEntidad.Equals(model.Cedula));

                            if (existe != null)
                            {
                                personaJuridica = existe;
                            }
                            else
                            {
                                TbPersonaJuridica personaJuridicaNueva = new TbPersonaJuridica()
                                {
                                    NombreEntidad = model.NombreEntidad,
                                    RazonSocial = model.RazonSocial,
                                    Cedula = model.Cedula,
                                    Correo = model.Correo
                                };

                                await context.TbPersonaJuridicas.AddAsync(personaJuridicaNueva);
                                await context.SaveChangesAsync();

                                personaJuridica = await context.TbPersonaJuridicas.FirstOrDefaultAsync(x
                                    => x.Cedula.Equals(model.Cedula));

                            }
                        }

                        TbPersona objetoEnviar = new TbPersona()
                        {
                            IdTipoIdentificacion = model.TipoPersonaId,
                            IdPersonaJ = personaJuridica == null ? 1 : personaJuridica.IdPersonaJ,
                            Identificacion = model.Identificacion,
                            Nombre = model.Nombre,
                            Ape1 = model.Apellido1,
                            Ape2 = model.Apellido2,
                            TelPer = model.TelefonoMovil,
                            TelCas = model.TelefonoCasa,
                            TelOfi = model.TelefonoOficina,
                            Email = model.CorreoElectronico,
                            IdUbicacion = model.DistritoId,
                            Direccion = model.DireccionExacta
                        };

                        await context.TbPersonas.AddAsync(objetoEnviar);
                        await context.SaveChangesAsync();

                        /*Obtener la persona recien ingresada de la base de datos*/
                        TbPersona nuevaPersona = await context.TbPersonas.FirstOrDefaultAsync(
                            p => p.Identificacion.Equals(model.Identificacion)
                            && p.Email.Equals(model.CorreoElectronico));

                        /*Agregar a esta persona como Cliente Comprador*/
                        await context.TbClienteCompradors.AddAsync(new TbClienteComprador
                        {
                            IdPersona = nuevaPersona.IdPersona,
                            Estado = "A",
                            FechaRegis = DateTime.UtcNow
                        });

                        await context.SaveChangesAsync();
                        /*Si todo transcurrio correctamente, se cierra la transaccion de manera exitosa*/
                        transaction.Commit();
                        response = new Response { EsCorrecto = true, Mensaje = "OK" };

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        response = new Response { EsCorrecto = false, Mensaje = ex.Message };
                    }

                });

            }

            return response;
        }

        public List<ReferenciasViewModel> ObtenerReferenciasComprador(int idClienteComprador)
        {
            try
            {

                Task<TbReferenciasComprador> existe = context.TbReferenciasCompradors.FirstOrDefaultAsync(
                        x => x.IdClienteC.Equals(idClienteComprador));

                if (existe.Result == null)
                {
                    string[] refes = { "Facebook", "Instagram", "WhatsApp", "Rótulo", "Persona", "YouTube" };

                    for (int i = 0; i < refes.Length; i++)
                    {

                        TbReferenciasComprador regisReferencias = new TbReferenciasComprador()
                        {
                            IdClienteC = idClienteComprador,
                            Descripcion = refes[i],
                            Estado = "I"
                        };
                        context.TbReferenciasCompradors.AddAsync(regisReferencias);
                        context.SaveChangesAsync();

                    }

                    List<ReferenciasViewModel> consulta = (from refe in context.TbReferenciasCompradors
                                                           where idClienteComprador == refe.IdClienteC
                                                           select new ReferenciasViewModel
                                                           {
                                                               IdReferencias = refe.IdReferencia,
                                                               Descripcion = refe.Descripcion,
                                                               Estado = Convert.ToChar(refe.Estado),
                                                           }).ToList();

                    return consulta;
                }
                else
                {
                    List<ReferenciasViewModel> consulta = (from refe in context.TbReferenciasCompradors
                                                           where idClienteComprador == refe.IdClienteC
                                                           select new ReferenciasViewModel
                                                           {
                                                               IdReferencias = refe.IdReferencia,
                                                               Descripcion = refe.Descripcion,
                                                               Estado = Convert.ToChar(refe.Estado),
                                                           }).ToList();

                    return consulta;
                }

            }
            catch (Exception)
            {
                return null;
            }

        }

        public List<CompradorTabla> ObtenerListaCompradores()
        {
            try
            {
                List<CompradorTabla> consulta = (from compradores in context.TbClienteCompradors
                                                 join personas in context.TbPersonas
                                                 on compradores.IdPersona equals personas.IdPersona
                                                 select new CompradorTabla
                                                 {
                                                     Id = compradores.IdClienteC,
                                                     Estado = compradores.Estado.Equals("A") ? "Activo" : "Inactivo",
                                                     FechaRegistra = compradores.FechaRegis,
                                                     IdPersona = compradores.IdPersona,
                                                     Identificacion = personas.Identificacion,
                                                     Comprador = $"{personas.Nombre} {personas.Ape1} {personas.Ape2}",
                                                     Correo = personas.Email
                                                 }).ToList();

                return consulta;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public CompradorViewModel ObtenerComprador(int idClienteComprador)
        {
            try
            {
                List<CompradorViewModel> consulta = (from personas in context.TbPersonas
                                                     join tipoIdentificacion in context.TbTipoIdentificacions
                                                     on personas.IdTipoIdentificacion equals tipoIdentificacion.IdTipoIdentificacion
                                                     join ubicacion in context.Ubicacions
                                                     on personas.IdUbicacion equals ubicacion.IdUbicacion
                                                     join clienteComprador in context.TbClienteCompradors
                                                     on personas.IdPersona equals clienteComprador.IdPersona
                                                     join personaJuridica in context.TbPersonaJuridicas
                                                     on personas.IdPersonaJ equals personaJuridica.IdPersonaJ

                                                     where clienteComprador.IdClienteC == idClienteComprador

                                                     group personas by new
                                                     {
                                                         tipoIdentificacion.Descripcion,
                                                         ubicacion.Provincia,
                                                         ubicacion.Canton,
                                                         ubicacion.Distrito,
                                                         clienteComprador.FechaRegis,
                                                         clienteComprador.Estado,
                                                         clienteComprador.IdClienteC,
                                                         personas.Identificacion,
                                                         personas.Nombre,
                                                         personas.Ape1,
                                                         personas.Ape2,
                                                         personas.Direccion,
                                                         personas.Email,
                                                         personas.TelCas,
                                                         personas.TelOfi,
                                                         personas.TelPer,
                                                         personas.IdPersona,
                                                         personaJuridica.IdPersonaJ,
                                                         personaJuridica.NombreEntidad,
                                                         personaJuridica.RazonSocial,
                                                         personaJuridica.Correo,
                                                         personaJuridica.Cedula
                                                     } into p

                                                     select new CompradorViewModel
                                                     {
                                                         TipoIdentificacion = p.Key.Descripcion,
                                                         Provincia = p.Key.Provincia,
                                                         Canton = p.Key.Canton,
                                                         Distrito = p.Key.Distrito.ToUpper(),
                                                         FechaRegistra = p.Key.FechaRegis,
                                                         Estado = p.Key.Estado.Equals("A") ? "Activo" : "Inactivo",
                                                         IdClienteComprador = p.Key.IdClienteC,
                                                         Identificacion = p.Key.Identificacion,
                                                         Nombre = p.Key.Nombre,
                                                         Apellido1 = p.Key.Ape1,
                                                         Apellido2 = p.Key.Ape2,
                                                         DireccionExacta = p.Key.Direccion,
                                                         NombreCompleto = $"{p.Key.Nombre} {p.Key.Ape1} {p.Key.Ape2}",
                                                         CorreoElectronico = p.Key.Email,
                                                         TelefonoCasa = !string.IsNullOrEmpty(p.Key.TelCas) ? p.Key.TelCas : "No dispone",
                                                         TelefonoMovil = p.Key.TelPer,
                                                         TelefonoOficina = !string.IsNullOrEmpty(p.Key.TelOfi) ? p.Key.TelOfi : "No dispone",
                                                         IdPersona = p.Key.IdPersona,
                                                         PersonaJuridica = string.IsNullOrEmpty(p.Key.NombreEntidad) ? string.Empty : p.Key.NombreEntidad,
                                                         CedulaPersonaJuridica = string.IsNullOrEmpty(p.Key.Cedula) ? string.Empty : p.Key.Cedula,
                                                         CorreoJuridica = string.IsNullOrEmpty(p.Key.Correo) ? string.Empty : p.Key.Correo,
                                                         RazonSocial = string.IsNullOrEmpty(p.Key.RazonSocial) ? string.Empty : p.Key.RazonSocial
                                                     }).ToList();

                return consulta[0]; /* Como devuelve una lista de IQueryable, y la castea a lista, entonces obtengo el unico elemento*/

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Ubicacion>> ObtenerTodasLasUbicaciones()
        {
            return await context.Ubicacions.ToListAsync();
        }

        public async Task<Tuple<int, int, int>> ObtenerIDsUbicacion(Ubicacion ubicacion)
        {
            List<Ubicacion> listadoUbicaciones = await ObtenerTodasLasUbicaciones();

            int contadorProvincia = 0; int contadorCanton = 0;

            List<Provincias> agruparProvincias = (from item in listadoUbicaciones
                                                  group item by new { item.Provincia } into p
                                                  select new Provincias()
                                                  {
                                                      Id = ++contadorProvincia,
                                                      Nombre = p.Key.Provincia
                                                  }).ToList();

            Provincias provinciaObtenida = agruparProvincias.FirstOrDefault(x => x.Nombre.Equals(ubicacion.Provincia));

            IEnumerable<Ubicacion> obtenidos1 = listadoUbicaciones.Where(c => c.Provincia.Equals(ubicacion.Provincia));

            List<Cantones> agruparCantones = (from item in obtenidos1
                                              group item by new { item.Canton } into p
                                              select new Cantones()
                                              {
                                                  Id = ++contadorCanton,
                                                  Nombre = p.Key.Canton,
                                              }).ToList();

            Cantones cantonObtenido = agruparCantones.FirstOrDefault(x => x.Nombre.Equals(ubicacion.Canton));

            IEnumerable<Ubicacion> obtenidos2 = listadoUbicaciones.Where(c => c.Canton.Equals(ubicacion.Canton));

            Ubicacion distritoObtenido = obtenidos2.FirstOrDefault(x => x.Distrito.Equals(ubicacion.Distrito));

            return new Tuple<int, int, int>(provinciaObtenida.Id, cantonObtenido.Id, distritoObtenido.IdUbicacion);
        }

        public async Task<EditarCompradorViewModel> ObtenerCompradorParaEditar(int idClienteComprador)
        {
            try
            {
                List<EditarCompradorViewModel> consulta = (from personas in context.TbPersonas
                                                           join tipoIdentificacion in context.TbTipoIdentificacions
                                                           on personas.IdTipoIdentificacion equals tipoIdentificacion.IdTipoIdentificacion
                                                           join ubicacion in context.Ubicacions
                                                           on personas.IdUbicacion equals ubicacion.IdUbicacion
                                                           join clienteComprador in context.TbClienteCompradors
                                                           on personas.IdPersona equals clienteComprador.IdPersona
                                                           join personaJuridica in context.TbPersonaJuridicas
                                                           on personas.IdPersonaJ equals personaJuridica.IdPersonaJ

                                                           where clienteComprador.IdClienteC == idClienteComprador

                                                           group personas by new
                                                           {
                                                               tipoIdentificacion.Descripcion,
                                                               ubicacion.Provincia,
                                                               ubicacion.Canton,
                                                               ubicacion.Distrito,
                                                               clienteComprador.FechaRegis,
                                                               clienteComprador.Estado,
                                                               clienteComprador.IdClienteC,
                                                               personas.Identificacion,
                                                               personas.Nombre,
                                                               personas.Ape1,
                                                               personas.Ape2,
                                                               personas.Direccion,
                                                               personas.Email,
                                                               personas.TelCas,
                                                               personas.TelOfi,
                                                               personas.TelPer,
                                                               personas.IdPersona,
                                                               personaJuridica.IdPersonaJ,
                                                               personaJuridica.NombreEntidad,
                                                               personaJuridica.Cedula,
                                                               personaJuridica.Correo,
                                                               personaJuridica.RazonSocial,
                                                               personas.IdUbicacion,
                                                               personas.IdTipoIdentificacion,
                                                           } into p

                                                           select new EditarCompradorViewModel
                                                           {
                                                               IdPersona = p.Key.IdPersona,
                                                               IdComprador = p.Key.IdClienteC,
                                                               Identificacion = p.Key.Identificacion,
                                                               Nombre = p.Key.Nombre,
                                                               Apellido1 = p.Key.Ape1,
                                                               Apellido2 = p.Key.Ape2,
                                                               DireccionExacta = p.Key.Direccion,
                                                               CorreoElectronico = p.Key.Email,
                                                               TelefonoCasa = !string.IsNullOrEmpty(p.Key.TelCas) ? p.Key.TelCas : string.Empty,
                                                               TelefonoMovil = p.Key.TelPer.ToString(),
                                                               TelefonoOficina = !string.IsNullOrEmpty(p.Key.TelOfi) ? p.Key.TelOfi : string.Empty,
                                                               DistritoId = p.Key.IdUbicacion,
                                                               TipoPersonaId = p.Key.IdTipoIdentificacion,
                                                               NombreEntidad = p.Key.NombreEntidad.Equals("na") ? string.Empty : p.Key.NombreEntidad,
                                                               RazonSocial = p.Key.RazonSocial.Equals("na") ? string.Empty : p.Key.RazonSocial,
                                                               Cedula = p.Key.Cedula.Equals("na") ? string.Empty : p.Key.Cedula,
                                                               Correo = p.Key.Correo.Equals("na") ? string.Empty : p.Key.Correo,
                                                               PoseePersonaJuridica = !p.Key.NombreEntidad.Equals("na")
                                                           }).ToList();

                if (consulta[0] != null)
                {
                    Ubicacion ubicacion = await context.Ubicacions.FindAsync(consulta[0].DistritoId);

                    Tuple<int, int, int> tupla = await ObtenerIDsUbicacion(ubicacion);

                    consulta[0].ProvinciaId = tupla.Item1;
                    consulta[0].CantonId = tupla.Item2;
                    consulta[0].DistritoId = tupla.Item3;
                    consulta[0].NombreProvicia = ubicacion.Provincia;
                    consulta[0].NombreCanton = ubicacion.Canton;
                }

                return consulta[0];

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Response> EditarRegistroComprador(EditarCompradorViewModel model, string usuario)
        {
            Response response = null;

            try
            {
                IExecutionStrategy strategy = context.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    using IDbContextTransaction transaction = context.Database.BeginTransaction();

                    try
                    {
                        TbPersonaJuridica personaJuridica = null;
                        if (!string.IsNullOrEmpty(model.NombreEntidad)) /*valida que el modelo tenga indicada la entidad*/
                        {
                            TbPersonaJuridica existe = await context.TbPersonaJuridicas.FirstOrDefaultAsync(x => x.NombreEntidad.Equals(model.Cedula));
                            /*si ya anteriormente tenia una la actualiza*/
                            if (existe != null)
                            {
                                existe.NombreEntidad = model.NombreEntidad;
                                existe.RazonSocial = model.RazonSocial;
                                existe.Cedula = model.Cedula;
                                existe.Correo = model.Correo;

                                context.TbPersonaJuridicas.Update(existe);
                                await context.SaveChangesAsync();

                                /*envia el objeto persona juridica para seguirlo asociando a la misma persona*/
                                personaJuridica = existe;
                            }
                            else  /*agrega una nueva*/
                            {
                                TbPersonaJuridica personaJuridicaNueva = new TbPersonaJuridica()
                                {
                                    NombreEntidad = model.NombreEntidad,
                                    RazonSocial = model.RazonSocial,
                                    Cedula = model.Cedula,
                                    Correo = model.Correo
                                };

                                await context.TbPersonaJuridicas.AddAsync(personaJuridicaNueva);
                                await context.SaveChangesAsync();

                                personaJuridica = await context.TbPersonaJuridicas.FirstOrDefaultAsync(x
                                    => x.Cedula.Equals(model.Cedula));

                            }
                        }

                        TbPersona objetoActualizar = await context.TbPersonas.FindAsync(model.IdPersona);

                        objetoActualizar.IdTipoIdentificacion = model.TipoPersonaId;
                        objetoActualizar.IdPersonaJ = personaJuridica == null ? 8 : personaJuridica.IdPersonaJ;
                        objetoActualizar.Identificacion = model.Identificacion;
                        objetoActualizar.Nombre = model.Nombre;
                        objetoActualizar.Ape1 = model.Apellido1;
                        objetoActualizar.Ape2 = model.Apellido2;
                        objetoActualizar.TelPer = model.TelefonoMovil;
                        objetoActualizar.TelCas = model.TelefonoCasa;
                        objetoActualizar.TelOfi = model.TelefonoOficina;
                        objetoActualizar.Email = model.CorreoElectronico;
                        objetoActualizar.IdUbicacion = model.DistritoId;
                        objetoActualizar.Direccion = model.DireccionExacta;

                        context.TbPersonas.Update(objetoActualizar);
                        await context.SaveChangesAsync();
                        /*Si todo transcurrio correctamente, se cierra la transaccion de manera exitosa*/
                        transaction.Commit();
                        response = new Response { EsCorrecto = true, Mensaje = "OK" };

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        response = new Response { EsCorrecto = false, Mensaje = ex.Message };
                    }

                });

                return response;
            }
            catch (Exception ex)
            {
                return response = new Response { EsCorrecto = false, Mensaje = ex.Message };
            }

        }

        public async Task<Response> AgregarDatosPropiedadElegida(CaracteristicasPropiedadElegida caracteristicas, string usuario)
        {
            try
            {
                TbCaractRequeridasCompradorPropiedad nuevoRegistro = new TbCaractRequeridasCompradorPropiedad()
                {
                    IdTipoPropiedad = caracteristicas.IdTipoPropiedad,
                    TienePropiedadEspecifica = caracteristicas.PoseePropiedadEspecifica,
                    IdPropiedad = Convert.ToInt32(caracteristicas.CodigoPropiedad),
                    IdClienteComprador = caracteristicas.IdComprador,
                    LugarCompra = caracteristicas.Lugar.Trim(),
                    Presupuesto = caracteristicas.Presupuesto
                };

                await context.TbCaractRequeridasCompradorPropiedads.AddAsync(nuevoRegistro);
                await context.SaveChangesAsync();

                ApplicationUser usuarioObtenido = await userHelper.GetUserByEmailAsync(usuario);

                await bitacora.InformesModificacionBitacora(usuario, $"Ha agregado la siguiente característica en la sección de Compradores para el comprador con Id {caracteristicas.IdComprador}." +
                    $" Código : {caracteristicas.CodigoPropiedad}," +
                    $" Tiene propiedad : {caracteristicas.PoseePropiedadEspecifica}" +
                    $" Lugar :  {caracteristicas.Lugar}," +
                    $" Presupuesto {caracteristicas.Presupuesto}", nameof(TbCaractRequeridasCompradorPropiedad).ToString(), OperacionBitacora.INSERTAR.ToString(), 0);

                return new Response { EsCorrecto = true, Mensaje = "OK" };
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }
        }

        public List<CaracteristicasPropiedadElegida> ObtenerCaracteristicasPropiedadObtenida(int idClienteComprador)
        {
            try
            {
                List<CaracteristicasPropiedadElegida> consulta = (from caracteristicas in context.TbCaractRequeridasCompradorPropiedads
                                                                  join tipoPropiedad in context.TbTipoPropiedades
                                                                  on caracteristicas.IdTipoPropiedad equals tipoPropiedad.IdTipoPro

                                                                  where caracteristicas.IdClienteComprador == idClienteComprador

                                                                  select new CaracteristicasPropiedadElegida
                                                                  {
                                                                      IdComprador = idClienteComprador,
                                                                      CodigoPropiedad = caracteristicas.IdPropiedad.ToString(),
                                                                      Id = caracteristicas.IdCaracteristica,
                                                                      IdTipoPropiedad = caracteristicas.IdTipoPropiedad,
                                                                      NompreTipoPropiedad = tipoPropiedad.Descripcion,
                                                                      Lugar = caracteristicas.LugarCompra,
                                                                      PoseePropiedadEspecifica = caracteristicas.TienePropiedadEspecifica.Equals("S") ? "Si" : "No",
                                                                      Presupuesto = caracteristicas.Presupuesto
                                                                  }).ToList();

                return consulta;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Response> EliminarCaracteristicaPropiedadAdquirida(int id, string usuario)
        {
            try
            {
                TbCaractRequeridasCompradorPropiedad obtenido = await context.TbCaractRequeridasCompradorPropiedads.FindAsync(id);

                if (obtenido != null)
                {
                    context.TbCaractRequeridasCompradorPropiedads.Remove(obtenido);
                    await context.SaveChangesAsync();

                    await bitacora.InformesModificacionBitacora(usuario, $"Ha eliminado la característica en la " +
                        $"sección de Compradores con código {id}",
                        nameof(TbCaractRequeridasCompradorPropiedad).ToString(), OperacionBitacora.ELIMINAR.ToString(), 0);

                    return new Response { EsCorrecto = true, Mensaje = "OK" };
                }

                return new Response { EsCorrecto = false, Mensaje = "No existe" };
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }

        }

        public Response ObtenerCaracteristicaPropiedadAdquirida(int id)
        {
            try
            {
                List<CaracteristicasPropiedadElegida> consulta = (from caracteristicas in context.TbCaractRequeridasCompradorPropiedads
                                                                  join tipoPropiedad in context.TbTipoPropiedades
                                                                  on caracteristicas.IdTipoPropiedad equals tipoPropiedad.IdTipoPro

                                                                  where caracteristicas.IdCaracteristica == id

                                                                  select new CaracteristicasPropiedadElegida
                                                                  {
                                                                      IdComprador = caracteristicas.IdClienteComprador,
                                                                      CodigoPropiedad = caracteristicas.IdPropiedad.ToString(),
                                                                      Id = caracteristicas.IdCaracteristica,
                                                                      IdTipoPropiedad = caracteristicas.IdTipoPropiedad,
                                                                      NompreTipoPropiedad = tipoPropiedad.Descripcion,
                                                                      Lugar = caracteristicas.LugarCompra,
                                                                      PoseePropiedadEspecifica = caracteristicas.TienePropiedadEspecifica.Equals("S") ? "Si" : "No",
                                                                      Presupuesto = caracteristicas.Presupuesto
                                                                  }).ToList();

                return new Response { EsCorrecto = true, Resultado = consulta[0] };
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }
        }

        public async Task<Response> EditarCaracteristicasPropiedadElegida(CaracteristicasPropiedadElegida model, string usuario)
        {
            try
            {
                TbCaractRequeridasCompradorPropiedad obtenido = await context.TbCaractRequeridasCompradorPropiedads.FindAsync(model.Id);

                if (obtenido != null)
                {
                    obtenido.TienePropiedadEspecifica = model.PoseePropiedadEspecifica.Substring(0, 1);
                    obtenido.Presupuesto = model.Presupuesto;
                    obtenido.LugarCompra = model.Lugar;
                    obtenido.IdPropiedad = Convert.ToInt32(model.CodigoPropiedad);
                    obtenido.IdTipoPropiedad = model.IdTipoPropiedad;

                    context.TbCaractRequeridasCompradorPropiedads.Update(obtenido);
                    await context.SaveChangesAsync();

                    await bitacora.InformesModificacionBitacora(usuario, $"Ha editado la siguiente característica en la sección de Compradores para el comprador con Id {model.IdComprador}." +
                    $" Código : {model.CodigoPropiedad}," +
                    $" Tiene propiedad : {model.PoseePropiedadEspecifica}" +
                    $" Lugar :  {model.Lugar}," +
                    $" Presupuesto {model.Presupuesto}", nameof(TbCaractRequeridasCompradorPropiedad).ToString(), OperacionBitacora.ACTUALIZAR.ToString(), 0);

                    return new Response { EsCorrecto = true, Mensaje = "OK" };
                }
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }

            return new Response { EsCorrecto = false };
        }

        public async Task<Response> AgregarDatosGestion(GestionCompra gestion, string usuario)
        {
            try
            {

                DateTime fechaEntrega = new DateTime();

                if (!string.IsNullOrEmpty(gestion.FechaEntregaString))
                {
                    string[] array = gestion.FechaEntregaString.Split("/");
                    int dia = Convert.ToInt32(array[0]);
                    int mes = Convert.ToInt32(array[1]);
                    int anio = Convert.ToInt32(array[2]);

                    fechaEntrega = new DateTime(anio, mes, dia);
                }

                TbGestionesCompra nuevoRegistro = new TbGestionesCompra()
                {
                    IdComprador = gestion.IdComprador,
                    Descripcion = gestion.Descripcion,
                    Estado = gestion.Activo,
                    FechaEntrega = fechaEntrega,
                    FechaSolicitud = DateTime.UtcNow
                };

                await context.TbGestionesCompras.AddAsync(nuevoRegistro);
                await context.SaveChangesAsync();

                await bitacora.InformesModificacionBitacora(usuario, $"Ha agregado la siguiente gestión en la sección de Compradores para el comprador con Id {gestion.IdComprador}." +
                    $" Descripción : {gestion.Descripcion}," +
                    $" Estado : {gestion.Activo}" +
                    $" Fecha entrega :  {fechaEntrega}," +
                    $" Fecha Solicitud {DateTime.Now}", nameof(TbGestionesCompra).ToString().ToUpper(), OperacionBitacora.ACTUALIZAR.ToString(), 0);

                return new Response { EsCorrecto = true, Mensaje = "OK" };
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }
        }

        public List<GestionCompra> ObtenerGestiones(int idClienteComprador)
        {
            try
            {
                List<GestionCompra> consulta = (from gestiones in context.TbGestionesCompras

                                                where gestiones.IdComprador == idClienteComprador

                                                select new GestionCompra
                                                {
                                                    IdComprador = idClienteComprador,
                                                    Id = gestiones.IdGestion,
                                                    Descripcion = gestiones.Descripcion,
                                                    FechaEntregaString = gestiones.FechaEntrega.ToString("dd/MM/yyyy"),
                                                    FechaSolicitaString = gestiones.FechaSolicitud.ToLocalTime().ToString("dd/MM/yyyy"),
                                                    Activo = gestiones.Estado.Equals("E") ? "Entregado" : "Pendiente"
                                                }).ToList();

                return consulta;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Response> EliminarGestion(int id, string usuario)
        {
            try
            {
                TbGestionesCompra obtenido = await context.TbGestionesCompras.FindAsync(id);

                if (obtenido != null)
                {
                    context.TbGestionesCompras.Remove(obtenido);
                    await context.SaveChangesAsync();

                    await bitacora.InformesModificacionBitacora(usuario, $"Ha eliminado la gestión en la " +
                        $"sección de Compradores con código {id} para el comprador con Id {obtenido.IdComprador}.",
                        nameof(TbGestionesCompra).ToString().ToUpper(), OperacionBitacora.ELIMINAR.ToString(), 0);

                    return new Response { EsCorrecto = true, Mensaje = "OK" };
                }

                return new Response { EsCorrecto = false, Mensaje = "No existe" };
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }

        }

        public Response ObtenerGestion(int id)
        {
            try
            {
                List<GestionCompra> consulta = (from gestiones in context.TbGestionesCompras

                                                where gestiones.IdGestion == id

                                                select new GestionCompra
                                                {
                                                    IdComprador = id,
                                                    Id = gestiones.IdGestion,
                                                    Descripcion = gestiones.Descripcion,
                                                    FechaEntregaString = gestiones.FechaEntrega.ToString("dd/MM/yyyy"),
                                                    FechaSolicitaString = gestiones.FechaSolicitud.ToLocalTime().ToString("dd/MM/yyyy"),
                                                    Activo = gestiones.Estado.Equals("E") ? "Entregado" : "Pendiente"
                                                }).ToList();

                return new Response { EsCorrecto = true, Resultado = consulta[0] };
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }
        }

        public async Task<Response> EditarGestion(GestionCompra model, string usuario)
        {
            try
            {
                TbGestionesCompra obtenido = await context.TbGestionesCompras.FindAsync(model.Id);

                if (obtenido != null)
                {
                    DateTime fechaEntrega = new DateTime();

                    if (!string.IsNullOrEmpty(model.FechaEntregaString))
                    {
                        string[] array = model.FechaEntregaString.Split("/");
                        int dia = Convert.ToInt32(array[0]);
                        int mes = Convert.ToInt32(array[1]);
                        int anio = Convert.ToInt32(array[2]);

                        fechaEntrega = new DateTime(anio, mes, dia);
                    }

                    obtenido.Estado = model.Activo.Substring(0, 1);
                    obtenido.Descripcion = model.Descripcion.Trim();
                    obtenido.FechaEntrega = fechaEntrega;

                    context.TbGestionesCompras.Update(obtenido);
                    await context.SaveChangesAsync();

                    await bitacora.InformesModificacionBitacora(usuario, $"Ha editado la siguiente gestión en la sección de Compradores para el comprador con Id {model.IdComprador}." +
                           $" Descripción : {model.Descripcion}," +
                           $" Estado : {model.Activo}" +
                           $" Fecha entrega :  {fechaEntrega},",
                           nameof(TbGestionesCompra).ToString().ToUpper(), OperacionBitacora.ACTUALIZAR.ToString(), 0);

                    return new Response { EsCorrecto = true, Mensaje = "OK" };
                }
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }

            return new Response { EsCorrecto = false };
        }

        public async Task<Response> AgregarDatosResultadoSugef(ResultadoSugef resultado, string usuario)
        {
            try
            {

                TbResultadoSugef nuevoRegistro = new TbResultadoSugef()
                {
                    IdComprador = resultado.IdComprador,
                    Estado = resultado.Estado,
                    Fecha = DateTime.UtcNow,
                    Observacion = string.IsNullOrEmpty(resultado.Observacion) ? string.Empty : resultado.Observacion
                };

                await context.TbResultadoSugefs.AddAsync(nuevoRegistro);
                await context.SaveChangesAsync();

                await bitacora.InformesModificacionBitacora(usuario, $"Ha agregado el resultado de SUGEF en la sección de Compradores para el comprador con Id {resultado.IdComprador}." +
                   $" Descripción : {resultado.Estado}," +
                   $" Estado : {DateTime.Now}" +
                   $" Observación :  {resultado.Observacion}," +
                   $" Fecha Solicitud {DateTime.Now}", nameof(TbResultadoSugef).ToString().ToUpper(), OperacionBitacora.INSERTAR.ToString(), 0);

                return new Response { EsCorrecto = true, Mensaje = "OK" };
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }
        }

        public List<ResultadoSugef> ObtenerResultadosSugef(int idClienteComprador)
        {
            try
            {
                List<ResultadoSugef> consulta = (from resultados in context.TbResultadoSugefs

                                                 where resultados.IdComprador == idClienteComprador

                                                 select new ResultadoSugef
                                                 {
                                                     IdComprador = idClienteComprador,
                                                     Id = resultados.IdResultado,
                                                     Observacion = resultados.Observacion,
                                                     FechaRegistroString = resultados.Fecha.ToLocalTime().ToString("dd/MM/yyyy"),
                                                     Estado = resultados.Estado.Equals("C") ? "Califica" : "No califica"
                                                 }).ToList();

                return consulta;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Response> EliminarResultadosSugef(int id, string usuario)
        {
            try
            {
                TbResultadoSugef obtenido = await context.TbResultadoSugefs.FindAsync(id);

                if (obtenido != null)
                {
                    context.TbResultadoSugefs.Remove(obtenido);
                    await context.SaveChangesAsync();

                    await bitacora.InformesModificacionBitacora(usuario, $"Ha eliminado el resultado de SUGEF en la " +
                       $"sección de Compradores con código {id} para el comprador con Id {obtenido.IdComprador}.",
                       nameof(TbResultadoSugef).ToString().ToUpper(), OperacionBitacora.ELIMINAR.ToString(), 0);

                    return new Response { EsCorrecto = true, Mensaje = "OK" };
                }

                return new Response { EsCorrecto = false, Mensaje = "No existe" };
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }

        }

        public Response ObtenerResultadoSugef(int id)
        {
            try
            {
                List<ResultadoSugef> consulta = (from resultados in context.TbResultadoSugefs

                                                 where resultados.IdResultado == id

                                                 select new ResultadoSugef
                                                 {
                                                     IdComprador = id,
                                                     Id = resultados.IdResultado,
                                                     Observacion = resultados.Observacion,
                                                     FechaRegistroString = resultados.Fecha.ToLocalTime().ToString("dd/MM/yyyy"),
                                                     Estado = resultados.Estado.Equals("C") ? "Califica" : "No califica"
                                                 }).ToList();

                return new Response { EsCorrecto = true, Resultado = consulta[0] };
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }
        }

        public async Task<Response> EditarResultadoSugef(ResultadoSugef model, string usuario)
        {
            try
            {
                TbResultadoSugef obtenido = await context.TbResultadoSugefs.FindAsync(model.Id);

                if (obtenido != null)
                {
                    obtenido.Estado = model.Estado;
                    obtenido.Observacion = string.IsNullOrEmpty(model.Observacion) ? string.Empty : model.Observacion;

                    context.TbResultadoSugefs.Update(obtenido);
                    await context.SaveChangesAsync();

                    await bitacora.InformesModificacionBitacora(usuario, $"Ha editado la siguiente gestión en la sección de Compradores para el comprador con Id {model.IdComprador}." +
                         $" Estado : {model.Estado}" +
                         $" Observación :  {model.Observacion},",
                         nameof(ResultadoSugef).ToString().ToUpper(), OperacionBitacora.ACTUALIZAR.ToString(), 0);

                    return new Response { EsCorrecto = true, Mensaje = "OK" };
                }
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }

            return new Response { EsCorrecto = false };
        }

        public async Task<Response> AgregarDatosResultadoSolicitante(ResultadoSolicitante resultado, string usuario)
        {
            try
            {

                TbResultadoSolicitante nuevoRegistro = new TbResultadoSolicitante()
                {
                    IdComprador = resultado.IdComprador,
                    Estado = resultado.Estado,
                    Fecha = DateTime.UtcNow,
                    Observacion = string.IsNullOrEmpty(resultado.Observacion) ? string.Empty : resultado.Observacion
                };

                await context.TbResultadoSolicitantes.AddAsync(nuevoRegistro);
                await context.SaveChangesAsync();

                await bitacora.InformesModificacionBitacora(usuario, $"Ha agregado Resultados de Solicitante en la sección de Compradores para el comprador con Id {resultado.IdComprador}." +
                   $" Descripción : {resultado.Estado}," +
                   $" Estado : {resultado.Estado}" +
                   $" Fecha : {DateTime.Now}" +
                   $" Observación :  {resultado.Observacion},"
                   , nameof(TbResultadoSolicitante).ToString().ToUpper(), OperacionBitacora.INSERTAR.ToString(), 0);

                return new Response { EsCorrecto = true, Mensaje = "OK" };
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }
        }

        public List<ResultadoSolicitante> ObtenerResultadosSolicitante(int idClienteComprador)
        {
            try
            {
                List<ResultadoSolicitante> consulta = (from resultados in context.TbResultadoSolicitantes

                                                       where resultados.IdComprador == idClienteComprador

                                                       select new ResultadoSolicitante
                                                       {
                                                           IdComprador = idClienteComprador,
                                                           Id = resultados.IdResultado,
                                                           Observacion = resultados.Observacion,
                                                           FechaRegistroString = resultados.Fecha.ToLocalTime().ToString("dd/MM/yyyy"),
                                                           Estado = resultados.Estado.Equals("P") ? "Positivo" : "Negativo"
                                                       }).ToList();

                return consulta;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Response> EliminarResultadosSolicitante(int id, string usuario)
        {
            try
            {
                TbResultadoSolicitante obtenido = await context.TbResultadoSolicitantes.FindAsync(id);

                if (obtenido != null)
                {
                    context.TbResultadoSolicitantes.Remove(obtenido);
                    await context.SaveChangesAsync();

                    await bitacora.InformesModificacionBitacora(usuario, $"Ha eliminado el Resultado Solicitante en la " +
                       $"sección de Compradores con código {id} para el comprador con Id {obtenido.IdComprador}.",
                       nameof(TbResultadoSolicitante).ToString().ToUpper(), OperacionBitacora.ELIMINAR.ToString(), 0);

                    return new Response { EsCorrecto = true, Mensaje = "OK" };
                }

                return new Response { EsCorrecto = false, Mensaje = "No existe" };
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }

        }

        public Response ObtenerResultadoSolicitante(int id)
        {
            try
            {
                List<ResultadoSolicitante> consulta = (from resultados in context.TbResultadoSolicitantes

                                                       where resultados.IdResultado == id

                                                       select new ResultadoSolicitante
                                                       {
                                                           IdComprador = id,
                                                           Id = resultados.IdResultado,
                                                           Observacion = resultados.Observacion,
                                                           FechaRegistroString = resultados.Fecha.ToLocalTime().ToString("dd/MM/yyyy"),
                                                           Estado = resultados.Estado.Equals("P") ? "Positivo" : "Negativo"
                                                       }).ToList();

                return new Response { EsCorrecto = true, Resultado = consulta[0] };
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }
        }

        public async Task<Response> EditarResultadoSolicitante(ResultadoSolicitante model, string usuario)
        {
            try
            {
                TbResultadoSolicitante obtenido = await context.TbResultadoSolicitantes.FindAsync(model.Id);

                if (obtenido != null)
                {
                    obtenido.Estado = model.Estado;
                    obtenido.Observacion = string.IsNullOrEmpty(model.Observacion) ? string.Empty : model.Observacion;

                    context.TbResultadoSolicitantes.Update(obtenido);
                    await context.SaveChangesAsync();

                    await bitacora.InformesModificacionBitacora(usuario, $"Ha editado Resultados de Solicitante en la sección de Compradores para el comprador con Id {model.IdComprador}." +
                         $" Estado : {model.Estado}" +
                         $" Observación :  {model.Observacion},",
                         nameof(ResultadoSolicitante).ToString().ToUpper(), OperacionBitacora.ACTUALIZAR.ToString(), 0);

                    return new Response { EsCorrecto = true, Mensaje = "OK" };
                }
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }

            return new Response { EsCorrecto = false };
        }

        public async Task<Response> AgregarDatosDocumentoComprador(DocumentoComprador documento, string usuario)
        {
            try
            {
                DateTime? fechaVenc = null;

                if (!string.IsNullOrEmpty(documento.FechaVencimientoString))
                {
                    string[] array = documento.FechaVencimientoString.Split("/");
                    int dia = Convert.ToInt32(array[0]);
                    int mes = Convert.ToInt32(array[1]);
                    int anio = Convert.ToInt32(array[2]);

                    fechaVenc = new DateTime(anio, mes, dia);
                }

                TbDocumentosComprador nuevoDocumento = new TbDocumentosComprador()
                {
                    IdComprador = documento.IdComprador,
                    IdDocumento = documento.IdTipoDocumento,
                    Notas = string.IsNullOrEmpty(documento.Notas) ? string.Empty : documento.Notas,
                    FechaRegis = DateTime.Now.ToUniversalTime(),
                    Vencimiento = fechaVenc,
                    EstadoRecepcion = documento.Estado
                };

                await context.TbDocumentosCompradors.AddAsync(nuevoDocumento);
                await context.SaveChangesAsync();

                await bitacora.InformesModificacionBitacora(usuario, $"Ha agregado Documento de Comprador en la sección de Compradores para el comprador con Id {documento.IdComprador}." +
                   $" Id Documento : {documento.IdDocumentoComprador}," +
                   $" Notas : {documento.Notas}" +
                   $" Fecha Registra :  {DateTime.Now}," +
                   $" Vencimiento : {fechaVenc}" +
                   $" Estado Recepción : {documento.Estado}", nameof(TbDocumentosComprador).ToString().ToUpper(), OperacionBitacora.INSERTAR.ToString(), 0);

                return new Response { EsCorrecto = true, Mensaje = "OK" };
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }
        }

        public List<DocumentoComprador> ObtenerDocumentosComprador(int idComprador)
        {
            try
            {
                List<DocumentoComprador> consulta = (from documento in context.TbDocumentosCompradors
                                                     join tipoDocumento in context.TbDocumentos
                                                     on documento.IdDocumento equals tipoDocumento.IdDocumento

                                                     where documento.IdComprador == idComprador

                                                     select new DocumentoComprador
                                                     {
                                                         IdDocumentoComprador = documento.IdDocComp,
                                                         DescripcionDocumento = tipoDocumento.Descripcion,
                                                         IdTipoDocumento = documento.IdDocumento,
                                                         Estado = documento.EstadoRecepcion.Equals("S") ? "Si" : "No",
                                                         FechaVencimientoString = documento.Vencimiento.Value.ToString("dd/MM/yyyy"),
                                                         Notas = documento.Notas,
                                                         FechaRegistroString = documento.FechaRegis.ToString("dd/MM/yyyy"),
                                                     }).ToList();

                return consulta;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Response> EliminarDocumentoComprador(int idDocumento, string usuario)
        {
            try
            {
                TbDocumentosComprador objetoObtenido = await context.TbDocumentosCompradors.FindAsync(idDocumento);

                if (objetoObtenido != null)
                {
                    context.TbDocumentosCompradors.Remove(objetoObtenido);
                    await context.SaveChangesAsync();

                    await bitacora.InformesModificacionBitacora(usuario, $"Ha eliminado el Documento Comprador en la " +
                       $"sección de Compradores con código {idDocumento} para el comprador con Id {objetoObtenido.IdComprador}.",
                       nameof(TbDocumentosComprador).ToString().ToUpper(), OperacionBitacora.ELIMINAR.ToString(), 0);

                    return new Response { EsCorrecto = true };
                }

                return new Response { EsCorrecto = false, Mensaje = "No existe" };
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = $"ERROR {ex.Message}" };
            }
        }

        public async Task<Response> ObtenerDocumentoComprador(int idDocumento)
        {
            try
            {
                TbDocumentosComprador existe = await context.TbDocumentosCompradors.FindAsync(idDocumento);

                if (existe == null)
                {
                    return new Response { EsCorrecto = false, Mensaje = "No existe" };
                }

                List<DocumentoComprador> consulta = (from documento in context.TbDocumentosCompradors
                                                     join tipoDocumento in context.TbDocumentos
                                                     on documento.IdDocumento equals tipoDocumento.IdDocumento

                                                     where documento.IdDocComp == idDocumento

                                                     select new DocumentoComprador
                                                     {
                                                         IdDocumentoComprador = documento.IdDocComp,
                                                         DescripcionDocumento = tipoDocumento.Descripcion,
                                                         IdTipoDocumento = documento.IdDocumento,
                                                         Estado = documento.EstadoRecepcion.Equals("S") ? "Si" : "No",
                                                         FechaVencimientoString = documento.Vencimiento.Value.ToString("dd/MM/yyyy"),
                                                         Notas = documento.Notas,
                                                         FechaRegistroString = documento.FechaRegis.ToString("dd/MM/yyyy"),
                                                         FechaVencimiento = documento.Vencimiento
                                                     }).ToList();

                return new Response { EsCorrecto = true, Resultado = consulta[0] };
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }
        }

        public async Task<Response> AgregarDatosEditadosDocumentosComprador(DocumentoComprador documento, string usuario)
        {
            try
            {
                TbDocumentosComprador documentoObtenido = await context.TbDocumentosCompradors.FindAsync(documento.IdDocumentoComprador);

                if (documentoObtenido != null)
                {
                    DateTime? fechaVenc = null;

                    if (!string.IsNullOrEmpty(documento.FechaVencimientoString))
                    {
                        string[] array = documento.FechaVencimientoString.Split("/");
                        int dia = Convert.ToInt32(array[0]);
                        int mes = Convert.ToInt32(array[1]);
                        int anio = Convert.ToInt32(array[2]);

                        fechaVenc = new DateTime(anio, mes, dia);
                    }

                    documentoObtenido.Notas = documento.Notas;
                    documentoObtenido.Vencimiento = fechaVenc;
                    documentoObtenido.IdDocumento = documento.IdTipoDocumento;
                    documentoObtenido.EstadoRecepcion = documento.Estado;

                    context.TbDocumentosCompradors.Update(documentoObtenido);
                    await context.SaveChangesAsync();

                    await bitacora.InformesModificacionBitacora(usuario, $"Ha editado el Documento Comprador en la sección de Compradores para el comprador con Id {documento.IdComprador}." +
                         $" Notas : {documento.Notas}" +
                         $" Vencimiento :  {fechaVenc}," +
                         $" Id Documento : {documento.IdDocumentoComprador}" +
                         $"Estado Recepción : {documento.Estado}",
                         nameof(TbDocumentosComprador).ToString().ToUpper(), OperacionBitacora.ACTUALIZAR.ToString(), 0);

                    return new Response { EsCorrecto = true, Mensaje = "OK" };
                }

                return new Response { EsCorrecto = false, Mensaje = "No existe" };

            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }
        }

        public async Task<Response> CambiarEstadoReferencias(ReferenciasViewModel referencias, string usuario)
        {
            try
            {
                TbReferenciasComprador obtenerRefe = await context.TbReferenciasCompradors.FindAsync(referencias.IdReferencias);

                if (obtenerRefe != null)
                {
                    obtenerRefe.Estado = Convert.ToString(referencias.Estado);

                    context.TbReferenciasCompradors.Update(obtenerRefe);
                    await context.SaveChangesAsync();

                    await bitacora.InformesModificacionBitacora(usuario, $"Ha editado el Estado Referencia en la sección de Compradores para el comprador con Id {referencias.IdCliente}." +
                         $"Estado Referencia : {obtenerRefe.Estado}",
                         nameof(TbDocumentosComprador).ToString().ToUpper(), OperacionBitacora.ACTUALIZAR.ToString(), 0);

                    return new Response { EsCorrecto = true, Mensaje = "OK" };
                }
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }

            return new Response { EsCorrecto = false };
        }

    }

}

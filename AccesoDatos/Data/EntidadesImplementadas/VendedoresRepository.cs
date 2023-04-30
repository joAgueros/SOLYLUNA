using AccesoDatos.BlogCore.Models;
using AccesoDatos.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Modelos.Entidades;
using Modelos.ViewModels.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccesoDatos.Data.EntidadesImplementadas
{
    public class VendedoresRepository : Repository<TbClienteVendedor>, IVendedoresRepository
    {
        private readonly SolyLunaDbContext context;
        private readonly IBitacoraRepository bitacora;

        public VendedoresRepository(SolyLunaDbContext context, IBitacoraRepository bitacora) : base(context)
        {
            this.context = context;
            this.bitacora = bitacora;
        }

        public async Task<List<TbPersonaJuridica>> ObtenerTodosTiposPersonaJuridica()
        {
            return await context.TbPersonaJuridicas.ToListAsync();
        }

        public async Task<List<TbTipoIntermediario>> ObtenerTodosLosTiposIntermediarios()
        {
            return await context.TbTipoIntermediarios.ToListAsync();
        }

        public async Task<TbPersonaJuridica> Registrar_PersonaJuridica(RegistrarVendedorViewModel model)
        {

            try
            {
                TbPersonaJuridica entidad = await context.TbPersonaJuridicas.FirstOrDefaultAsync(
                  v => v.Cedula.Equals(model.CedulaJu)
                  || v.Correo.Equals(model.CorreoJu));

                /*Verificar si el vendedor ya existe con dicha cedula*/
                if (entidad != null)
                {
                    return null;
                }


                TbPersonaJuridica nuevoRegistro = new TbPersonaJuridica()
                {
                    NombreEntidad = model.NombreEntidad,
                    RazonSocial = model.RazonSocial,
                    Cedula = model.CedulaJu,
                    Correo = model.CorreoJu
                };

                await context.TbPersonaJuridicas.AddAsync(nuevoRegistro);
                await context.SaveChangesAsync();

                TbPersonaJuridica objetoObtenido = context.TbPersonaJuridicas.FirstOrDefault(
                    x => x.Cedula == model.CedulaJu && x.Correo == model.CorreoJu);

                return objetoObtenido;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Response> RegistrarVendedor(RegistrarVendedorViewModel model, string usuario)
        {

            Response response = null;

            if (model != null)
            {

                if (model.CedulaJu != "na")
                {
                    /*Verifica si la persona juridica existe*/
                    TbPersonaJuridica personeria = await context.TbPersonaJuridicas.FirstOrDefaultAsync(
                        x => x.Cedula.Equals(model.CedulaJu) && x.Correo.Equals(model.CorreoJu));

                    if (personeria != null)
                    {
                        return response = new Response { EsCorrecto = false, Mensaje = "Existe" };
                    }
                }

                TbPersona vendedor = await context.TbPersonas.FirstOrDefaultAsync(
                    v => v.Identificacion.Equals(model.Identificacion)
                    || v.Email.Equals(model.CorreoElectronico));

                /*Verificar si el vendedor ya existe con dicha cedula*/
                if (vendedor != null)
                {
                    return response = new Response { EsCorrecto = true, Mensaje = "Ya existe" };
                }

                Microsoft.EntityFrameworkCore.Storage.IExecutionStrategy strategy = context.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = context.Database.BeginTransaction();

                    try
                    {

                        if (model.TipoPersonaId != 2)
                        {

                            TbPersona objetoEnviar = new TbPersona()
                            {
                                IdTipoIdentificacion = model.TipoPersonaId,
                                IdPersonaJ = 8,
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

                            await bitacora.InsertarVendedor(usuario, objetoEnviar.Identificacion);


                        }
                        else
                        {
                            TbPersonaJuridica personaJuridica = await Registrar_PersonaJuridica(model);

                            TbPersona objetoEnviar = new TbPersona()
                            {
                                IdTipoIdentificacion = model.TipoPersonaId,
                                IdPersonaJ = personaJuridica.IdPersonaJ,
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

                            await bitacora.InsertarVendedor(usuario, objetoEnviar.Identificacion);

                        }

                        /*Obtener la persona recien ingresada de la base de datos*/
                        TbPersona nuevaPersona = await context.TbPersonas.FirstOrDefaultAsync(
                            p => p.Identificacion.Equals(model.Identificacion)
                            && p.Email.Equals(model.CorreoElectronico));

                        /*Agregar a esta persona como Cliente Vendedor*/
                        await context.TbClienteVendedors.AddAsync(new TbClienteVendedor
                        {
                            IdPersona = nuevaPersona.IdPersona,
                            Estado = "A",
                            FechaRegis = DateTime.UtcNow
                        });
                        await context.SaveChangesAsync();

                        /*Registro de referencias*/

                        TbClienteVendedor vendedorReg = await context.TbClienteVendedors.FirstOrDefaultAsync(
                            v => v.IdPersona.Equals(nuevaPersona.IdPersona));

                        string[] refes = { "Facebook", "Instagram", "WhatsApp", "Rótulo", "Persona", "YouTube" };

                        for (int i = 0; i < refes.Length; i++)
                        {

                            TbReferenciasVendedor regisReferencias = new TbReferenciasVendedor()
                            {
                                IdClienteV = vendedorReg.IdClienteV,
                                Descripcion = refes[i],
                                Estado = "I"
                            };
                            await context.TbReferenciasVendedors.AddAsync(regisReferencias);
                            await context.SaveChangesAsync();

                        }
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

        public PersonaJuridicaViewModel ObtenerPersonaJuridicas(int idPersonaJuridica)
        {

            List<PersonaJuridicaViewModel> consulta = (from juridicas in context.TbPersonaJuridicas
                                                       where idPersonaJuridica == juridicas.IdPersonaJ
                                                       select new PersonaJuridicaViewModel
                                                       {
                                                           IdPersonaJuridica = juridicas.IdPersonaJ,
                                                           nombreEntidad = juridicas.NombreEntidad,
                                                           RazonSocial = juridicas.RazonSocial,
                                                           Cedula = juridicas.Cedula,
                                                           Correo = juridicas.Correo,
                                                       }).ToList();

            return consulta[0];

        }

        public List<ReferenciasViewModel> ObtenerReferenciasVendedor(int idClienteVendedor)
        {
            try
            {
                List<ReferenciasViewModel> consulta = (from refe in context.TbReferenciasVendedors
                                                       where idClienteVendedor == refe.IdClienteV
                                                       select new ReferenciasViewModel
                                                       {
                                                           IdReferencias = refe.IdReferencia,
                                                           Descripcion = refe.Descripcion,
                                                           Estado = Convert.ToChar(refe.Estado),
                                                       }).ToList();

                return consulta;
            }
            catch (Exception)
            {
                return null;
            }

        }


        public List<VendedorTabla> ObtenerListaVendedores()
        {
            try
            {
                List<VendedorTabla> consulta = (from vendedores in context.TbClienteVendedors
                                                join personas in context.TbPersonas
                                                on vendedores.IdPersona equals personas.IdPersona
                                                select new VendedorTabla
                                                {
                                                    Id = vendedores.IdClienteV,
                                                    Estado = vendedores.Estado.Equals("A") ? "Activo" : "Inactivo",
                                                    FechaRegistra = vendedores.FechaRegis.ToLocalTime(),
                                                    IdPersona = vendedores.IdPersona,
                                                    Identificacion = personas.Identificacion,
                                                    Vendedor = $"{personas.Nombre} {personas.Ape1} {personas.Ape2}",
                                                    Correo = personas.Email
                                                }).ToList();

                return consulta;


            }
            catch (Exception)
            {
                return null;
            }
        }

        public VendedorViewModel ObtenerVendedor(int idClienteVendedor)
        {
            try
            {
                List<VendedorViewModel> consulta = (from personas in context.TbPersonas
                                                    join tipoIdentificacion in context.TbTipoIdentificacions
                                                    on personas.IdTipoIdentificacion equals tipoIdentificacion.IdTipoIdentificacion
                                                    join ubicacion in context.Ubicacions
                                                    on personas.IdUbicacion equals ubicacion.IdUbicacion
                                                    join clienteVendedor in context.TbClienteVendedors
                                                    on personas.IdPersona equals clienteVendedor.IdPersona
                                                    join personaJuridica in context.TbPersonaJuridicas
                                                    on personas.IdPersonaJ equals personaJuridica.IdPersonaJ

                                                    where clienteVendedor.IdClienteV == idClienteVendedor

                                                    group personas by new
                                                    {
                                                        tipoIdentificacion.Descripcion,
                                                        ubicacion.Provincia,
                                                        ubicacion.Canton,
                                                        ubicacion.Distrito,
                                                        clienteVendedor.FechaRegis,
                                                        clienteVendedor.Estado,
                                                        clienteVendedor.IdClienteV,
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
                                                        personaJuridica.NombreEntidad
                                                    } into p

                                                    select new VendedorViewModel
                                                    {
                                                        TipoIdentificacion = p.Key.Descripcion,
                                                        Provincia = p.Key.Provincia,
                                                        Canton = p.Key.Canton,
                                                        Distrito = p.Key.Distrito.ToUpper(),
                                                        FechaRegistra = p.Key.FechaRegis,
                                                        Estado = p.Key.Estado.Equals("A") ? "Activo" : "Inactivo",
                                                        IdClienteVendedor = p.Key.IdClienteV,
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
                                                        PersonaJuridica = p.Key.NombreEntidad,
                                                        IdentificacionPersonaJuridica = p.Key.IdPersonaJ
                                                    }).ToList();

                return consulta[0]; /*como devuelve una lista de IQueryable, y la castea a lista, entonces obtengo el unico elemento*/

            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<MostrarPropiedadTabla> ObtenerListaPropiedadesPorClienteVendedor(int idClienteVendedor)
        {
            try
            {
                List<MostrarPropiedadTabla> consulta = (from propiedad in context.TbPropiedades
                                                        join usoTipoPropiedad in context.TbUsoTipopropiedades
                                                        on propiedad.IdUsoTipo equals usoTipoPropiedad.IdUsoTipo
                                                        join tipoPropiedad in context.TbTipoPropiedades
                                                        on usoTipoPropiedad.IdTipoPro equals tipoPropiedad.IdTipoPro
                                                        join usoPropiedad in context.TbUsoPropiedads
                                                        on usoTipoPropiedad.IdUsoPro equals usoPropiedad.IdUsoPro
                                                        join ubicacion in context.Ubicacions
                                                        on propiedad.IdUbicacion equals ubicacion.IdUbicacion
                                                        join medidaPropiedad in context.TbMedidaPropiedads
                                                        on propiedad.IdMedidaPro equals medidaPropiedad.IdMedidaPro
                                                        join tipoMedidas in context.TbTipoMedidas
                                                        on medidaPropiedad.IdTipoMedida equals tipoMedidas.IdTipoMedida

                                                        where propiedad.IdClienteV == idClienteVendedor && propiedad.Eliminado == "N"

                                                        group propiedad by new
                                                        {
                                                            propiedad.IdPropiedad,
                                                            d1 = tipoPropiedad.Descripcion,
                                                            d2 = usoPropiedad.Descripcion,
                                                            ubicacion.Provincia,
                                                            ubicacion.Canton,
                                                            ubicacion.Distrito,
                                                            medidaPropiedad.Medida,
                                                            d3 = tipoMedidas.Descripcion,
                                                            tipoMedidas.Siglas,
                                                            propiedad.PrecioMax,
                                                            propiedad.PrecioMin,
                                                            propiedad.Topografia,
                                                            propiedad.Publicado,
                                                            propiedad.Moneda,
                                                            propiedad.CodigoTipoUsoPropiedad
                                                        } into p

                                                        select new MostrarPropiedadTabla
                                                        {
                                                            Id = p.Key.IdPropiedad,
                                                            TipoPropiedad = p.Key.d1,
                                                            UsoPropiedad = p.Key.d2,
                                                            Ubicacion = $"{p.Key.Provincia}, {p.Key.Canton}, {p.Key.Distrito}".ToUpper(),
                                                            MedidaPropiedad = $"{p.Key.Medida:N2} {p.Key.Siglas.Trim()} ({p.Key.d3.Trim()})",
                                                            PrecioMaximo = p.Key.PrecioMax,
                                                            PrecioMinimo = p.Key.PrecioMin,
                                                            Topografia = p.Key.Topografia,
                                                            Publicado = p.Key.Publicado.Equals("N") ? "No publicado" : "Publicado",
                                                            Moneda = p.Key.Moneda,
                                                            CodigoTipoUsoPropiedad = p.Key.CodigoTipoUsoPropiedad
                                                        }).ToList();

                return consulta;

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

        public async Task<EditarVendedorViewModel> ObtenerVendedorParaEditar(int idClienteVendedor)
        {
            try
            {
                List<EditarVendedorViewModel> consulta = (from personas in context.TbPersonas
                                                          join tipoIdentificacion in context.TbTipoIdentificacions
                                                          on personas.IdTipoIdentificacion equals tipoIdentificacion.IdTipoIdentificacion
                                                          join ubicacion in context.Ubicacions
                                                          on personas.IdUbicacion equals ubicacion.IdUbicacion
                                                          join clienteVendedor in context.TbClienteVendedors
                                                          on personas.IdPersona equals clienteVendedor.IdPersona
                                                          join personaJuridica in context.TbPersonaJuridicas
                                                          on personas.IdPersonaJ equals personaJuridica.IdPersonaJ

                                                          where clienteVendedor.IdClienteV == idClienteVendedor

                                                          group personas by new
                                                          {
                                                              tipoIdentificacion.Descripcion,
                                                              ubicacion.Provincia,
                                                              ubicacion.Canton,
                                                              ubicacion.Distrito,
                                                              clienteVendedor.FechaRegis,
                                                              clienteVendedor.Estado,
                                                              clienteVendedor.IdClienteV,
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
                                                              personas.IdTipoIdentificacion
                                                          } into p

                                                          select new EditarVendedorViewModel
                                                          {
                                                              IdPersona = p.Key.IdPersona,
                                                              IdClienteVendedor = p.Key.IdClienteV,
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

        public async Task<Response> EditarRegistroVendedor(EditarVendedorViewModel model, string usuario)
        {
            Response response = null;

            try
            {
                Microsoft.EntityFrameworkCore.Storage.IExecutionStrategy strategy = context.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = context.Database.BeginTransaction();

                    try
                    {
                        TbPersonaJuridica personaJuridica = null;
                        if (!string.IsNullOrEmpty(model.NombreEntidad)) /*valida que el modelo tenga indicada la entidad*/
                        {

                            TbPersonaJuridica existe = await context.TbPersonaJuridicas.FirstOrDefaultAsync(x =>
                                               x.NombreEntidad.Equals(model.Cedula));
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

                        await bitacora.EditarVendedor(usuario, objetoActualizar.Identificacion);

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

        public async Task<Response> CambiarEstadoReferencias(ReferenciasViewModel referencias)
        {
            try
            {
                TbReferenciasVendedor obtenerRefe = await context.TbReferenciasVendedors.FindAsync(referencias.IdReferencias);

                if (obtenerRefe != null)
                {
                    obtenerRefe.Estado = Convert.ToString(referencias.Estado);

                    context.TbReferenciasVendedors.Update(obtenerRefe);
                    await context.SaveChangesAsync();
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


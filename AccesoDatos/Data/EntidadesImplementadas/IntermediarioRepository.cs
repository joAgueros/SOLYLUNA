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
    public class IntermediarioRepository : Repository<TbIntermediario>, IIntermedarioRepository
    {
        private readonly SolyLunaDbContext context;

        public IntermediarioRepository(SolyLunaDbContext context) : base(context)
        {
            this.context = context;
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

        public async Task<List<TbTipoIntermediario>> ObtenerTodosLosTiposIntermediarios()
        {
            return await context.TbTipoIntermediarios.ToListAsync();
        }

        public async Task<List<TbPersonaJuridica>> ObtenerTodosTiposPersonaJuridica()
        {
            return await context.TbPersonaJuridicas.ToListAsync();
        }

        public async Task<Response> EditarRegistroIntermediario(EditarIntermediarioViewModel model)
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

                        TbPersona intermediario = await context.TbPersonas.FindAsync(model.IdPersona);

                        /*Obtener la persona recien ingresada de la base de datos*/
                        TbIntermediario intermediarioBD = await context.TbIntermediarios.FirstOrDefaultAsync(
                            p => p.IdPersona == model.IdPersona && p.IdIntermediario == model.IntermediarioId);

                        /*actualizar el tipo de intermediario*/
                        intermediarioBD.IdTipoInter = model.IdTipoIntermediario;

                        context.TbIntermediarios.Update(intermediarioBD);
                        await context.SaveChangesAsync();

                        if (intermediario == null)
                        {
                            throw new Exception();
                        }

                        intermediario.IdTipoIdentificacion = model.TipoPersonaId;
                        intermediario.Identificacion = model.Identificacion;
                        intermediario.Nombre = model.Nombre;
                        intermediario.Ape1 = model.Apellido1;
                        intermediario.Ape2 = model.Apellido2;
                        intermediario.TelPer = model.TelefonoMovil;
                        intermediario.TelCas = model.TelefonoCasa;
                        intermediario.TelOfi = model.TelefonoOficina;
                        intermediario.Email = model.CorreoElectronico;
                        intermediario.IdUbicacion = model.DistritoId;
                        intermediario.Direccion = model.DireccionExacta;

                        context.TbPersonas.Update(intermediario);
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
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }

            return response;
        }

        public List<MostrarPropiedadTabla> ObtenerListaPropiedadesPorIntermediario(int idIntermediario)
        {
            try
            {
                List<MostrarPropiedadTabla> consulta = (from propInter in context.TbIntermediarioPropiedads
                                                        join propiedad in context.TbPropiedades
                                                        on propInter.IdPropiedad equals propiedad.IdPropiedad
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

                                                        where propInter.IdIntermediario == idIntermediario

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
                                                            propiedad.Publicado
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
                                                            Publicado = p.Key.Publicado.Equals("N") ? "No publicado" : "Publicado"
                                                        }).ToList();

                return consulta;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public IntermediarioViewModel ObtenerIntermediario(int idIntermediario)
        {
            try
            {
                List<IntermediarioViewModel> consulta = (from personas in context.TbPersonas
                                                         join tipoIdentificacion in context.TbTipoIdentificacions
                                                         on personas.IdTipoIdentificacion equals tipoIdentificacion.IdTipoIdentificacion
                                                         join ubicacion in context.Ubicacions
                                                         on personas.IdUbicacion equals ubicacion.IdUbicacion
                                                         join intermediario in context.TbIntermediarios
                                                         on personas.IdPersona equals intermediario.IdPersona
                                                         join personaJuridica in context.TbPersonaJuridicas
                                                         on personas.IdPersonaJ equals personaJuridica.IdPersonaJ
                                                         join tipoInter in context.TbTipoIntermediarios
                                                         on intermediario.IdTipoInter equals tipoInter.IdTipoInter

                                                         where intermediario.IdIntermediario == idIntermediario

                                                         group personas by new
                                                         {
                                                             tipoIdentificacion.Descripcion,
                                                             ubicacion.Provincia,
                                                             ubicacion.Canton,
                                                             ubicacion.Distrito,
                                                             intermediario.Estado,
                                                             intermediario.IdIntermediario,
                                                             tipoInter.Detalle,
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

                                                         select new IntermediarioViewModel
                                                         {
                                                             TipoIdentificacion = p.Key.Descripcion,
                                                             Provincia = p.Key.Provincia,
                                                             Canton = p.Key.Canton,
                                                             Distrito = p.Key.Distrito.ToUpper(),
                                                             Estado = p.Key.Estado.Equals("A") ? "Activo" : "Inactivo",
                                                             IdIntermediario = p.Key.IdIntermediario,
                                                             DetalleIntermediario = p.Key.Detalle,
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
                                                         }).ToList();

                return consulta[0]; /*como devuelve una lista de IQueryable, y la castea a lista, entonces obtengo el unico elemento*/

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<EditarIntermediarioViewModel> ObtenerIntermediarioParaEditar(int idIntermediario)
        {
            try
            {
                List<EditarIntermediarioViewModel> consulta = (from personas in context.TbPersonas
                                                               join tipoIdentificacion in context.TbTipoIdentificacions
                                                               on personas.IdTipoIdentificacion equals tipoIdentificacion.IdTipoIdentificacion
                                                               join ubicacion in context.Ubicacions
                                                               on personas.IdUbicacion equals ubicacion.IdUbicacion
                                                               join intermediario in context.TbIntermediarios
                                                               on personas.IdPersona equals intermediario.IdPersona
                                                               join personaJuridica in context.TbPersonaJuridicas
                                                               on personas.IdPersonaJ equals personaJuridica.IdPersonaJ

                                                               where intermediario.IdIntermediario == idIntermediario

                                                               group personas by new
                                                               {
                                                                   tipoIdentificacion.Descripcion,
                                                                   ubicacion.Provincia,
                                                                   ubicacion.Canton,
                                                                   ubicacion.Distrito,
                                                                   intermediario.IdTipoInter,
                                                                   intermediario.Estado,
                                                                   intermediario.IdIntermediario,
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
                                                                   personas.IdUbicacion,
                                                                   personas.IdTipoIdentificacion,
                                                               } into p

                                                               select new EditarIntermediarioViewModel
                                                               {
                                                                   IdPersona = p.Key.IdPersona,
                                                                   IntermediarioId = p.Key.IdIntermediario,
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
                                                                   IdTipoIntermediario = p.Key.IdTipoInter
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

        public List<IntermediarioTabla> ObtenerListaIntermediarios()
        {
            try
            {
                List<IntermediarioTabla> consulta = (from intermediario in context.TbIntermediarios
                                                     join personas in context.TbPersonas
                                                     on intermediario.IdPersona equals personas.IdPersona
                                                     join tipoInter in context.TbTipoIntermediarios
                                                     on intermediario.IdTipoInter equals tipoInter.IdTipoInter
                                                     select new IntermediarioTabla
                                                     {
                                                         Id = intermediario.IdIntermediario,
                                                         Estado = intermediario.Estado.Equals("A") ? "Activo" : "Inactivo",
                                                         IdPersona = personas.IdPersona,
                                                         Identificacion = personas.Identificacion,
                                                         Intermediario = $"{personas.Nombre} {personas.Ape1} {personas.Ape2}",
                                                         TipoInter = tipoInter.Detalle,
                                                         Correo = personas.Email
                                                     }).ToList();

                return consulta;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Response> RegistrarIntermediario(RegistrarIntermediarioViewModel model)
        {
            Response response = null;

            if (model != null)
            {

                TbPersona persona = await context.TbPersonas.FirstOrDefaultAsync(
                    p => p.Identificacion.Equals(model.Identificacion)
                    || p.Email.Equals(model.CorreoElectronico));

                /*Verificar si el vendedor ya existe con dicha cedula*/
                if (persona != null)
                {
                    return new Response { EsCorrecto = false, Mensaje = "Ya existe" };
                }

                Microsoft.EntityFrameworkCore.Storage.IExecutionStrategy strategy = context.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = context.Database.BeginTransaction();

                    try
                    {

                        TbPersona objetoPersona = new TbPersona()
                        {
                            IdTipoIdentificacion = 1,
                            IdPersonaJ = 1,
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

                        await context.TbPersonas.AddAsync(objetoPersona);
                        await context.SaveChangesAsync();

                        /*Obtener la persona recien ingresada de la base de datos*/
                        TbPersona nuevaPersona = await context.TbPersonas.FirstOrDefaultAsync(
                            p => p.Identificacion.Equals(model.Identificacion)
                            && p.Email.Equals(model.CorreoElectronico));

                        /*Agregar a esta persona como Intermediario*/
                        await context.TbIntermediarios.AddAsync(new TbIntermediario
                        {
                            IdTipoInter = model.IdTipoIntermediario,
                            IdPersona = nuevaPersona.IdPersona,
                            Estado = "A"
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

        public async Task<Response> AgregarIntermediarioPropiedad(RegistrarIntermediarioViewModel intermediario)
        {
            try
            {
                TbIntermediarioPropiedad existe = await context.TbIntermediarioPropiedads.FirstOrDefaultAsync(x =>
                x.IdIntermediario == intermediario.IdIntermediario && x.IdPropiedad == intermediario.IdPropiedad);

                if (existe != null)
                {
                    return new Response { EsCorrecto = false, Mensaje = "Ya existe" };
                }

                TbIntermediarioPropiedad nuevoRegistro = new TbIntermediarioPropiedad
                {
                    IdIntermediario = intermediario.IdIntermediario,
                    IdPropiedad = intermediario.IdPropiedad,
                    FechaRegis = DateTime.UtcNow
                };

                await context.TbIntermediarioPropiedads.AddAsync(nuevoRegistro);
                await context.SaveChangesAsync();
                return new Response { EsCorrecto = true, Mensaje = "OK" };
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }
        }

        public List<IntermediarioTabla> ObtenerListaIntermediariosPropiedad(int idPropiedad)
        {
            try
            {
                List<IntermediarioTabla> consulta = (from intermediario in context.TbIntermediarios
                                                     join personas in context.TbPersonas
                                                     on intermediario.IdPersona equals personas.IdPersona
                                                     join tipoInter in context.TbTipoIntermediarios
                                                     on intermediario.IdTipoInter equals tipoInter.IdTipoInter
                                                     join intermediarioPropiedad in context.TbIntermediarioPropiedads
                                                     on intermediario.IdIntermediario equals intermediarioPropiedad.IdIntermediario

                                                     where intermediarioPropiedad.IdPropiedad == idPropiedad

                                                     select new IntermediarioTabla
                                                     {
                                                         Id = intermediario.IdIntermediario,
                                                         Estado = intermediario.Estado.Equals("A") ? "Activo" : "Inactivo",
                                                         IdPersona = personas.IdPersona,
                                                         Identificacion = personas.Identificacion,
                                                         Intermediario = $"{personas.Nombre} {personas.Ape1} {personas.Ape2}",
                                                         TipoInter = tipoInter.Detalle,
                                                         Correo = personas.Email
                                                     }).ToList();

                return consulta;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Response> EliminarIntermediarioPropiedad(int idIntermediario)
        {
            try
            {
                TbIntermediario objetoObtenido = await context.TbIntermediarios.FirstOrDefaultAsync(x => x.IdIntermediario == idIntermediario);

                TbIntermediarioPropiedad intermediarioPropiedad = await context.TbIntermediarioPropiedads.FirstOrDefaultAsync(x => x.IdIntermediario == objetoObtenido.IdIntermediario);

                if (intermediarioPropiedad == null)
                {
                    return new Response { EsCorrecto = false, Mensaje = "No existe" };
                }

                context.TbIntermediarioPropiedads.Remove(intermediarioPropiedad);
                await context.SaveChangesAsync();
                return new Response { EsCorrecto = true };

            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = $"ERROR {ex.Message}" };
            }
        }
    }
}

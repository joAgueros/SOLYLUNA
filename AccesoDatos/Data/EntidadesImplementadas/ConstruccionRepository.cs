using AccesoDatos.BlogCore.Models;
using AccesoDatos.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccesoDatos.Data.EntidadesImplementadas
{
    public class ConstruccionRepository : Repository<TbConstruccion>, IConstruccionRepository
    {
        private readonly SolyLunaDbContext context;

        public ConstruccionRepository(SolyLunaDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<List<TbCaracteristica>> ObtenerTodasLasCaracteristicas()
        {
            return await context.TbCaracteristicas.ToListAsync();
        }

        public async Task<List<TbDivisionesObra>> ObtenerTodasLasDivisionesObra()
        {
            return await context.TbDivisionesObras.ToListAsync();
        }

        public async Task<List<TbPersVisual>> ObtenerTodasLasVistas()
        {
            return await context.TbPersVisuals.ToListAsync();
        }

        public async Task<List<TbEquipamiento>> ObtenerTodosLosEquipamientos()
        {
            return await context.TbEquipamientos.ToListAsync();
        }

        public async Task<List<TbMaterialesObra>> ObtenerTodosLosMaterialesObra()
        {
            return await context.TbMaterialesObras.ToListAsync();
        }

        public async Task<List<TbTipocableado>> ObtenerTodosLosTiposCableado()
        {
            return await context.TbTipocableados.ToListAsync();
        }

        public async Task<List<TbTipoMedida>> ObtenerTodosLosTiposMedidas()
        {
            return await context.TbTipoMedidas.ToListAsync();
        }

        public async Task<Response> AumentarEquipamiento(int id)
        {
            try
            {
                TbConstruccionEquipamiento objetoObtenido = await context.TbConstruccionEquipamientos.FindAsync(id);

                if (objetoObtenido != null)
                {
                    objetoObtenido.Cantidad += 1;
                    context.TbConstruccionEquipamientos.Update(objetoObtenido);
                    await context.SaveChangesAsync();
                    return new Response { EsCorrecto = true };
                }

                return new Response { EsCorrecto = false, Mensaje = "No existe" };
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = $"ERROR {ex.Message}" };
            }
        }

        public async Task<Response> DisminuirEquipamiento(int id)
        {
            try
            {
                TbConstruccionEquipamiento objetoObtenido = await context.TbConstruccionEquipamientos.FindAsync(id);

                if (objetoObtenido != null)
                {
                    if (objetoObtenido.Cantidad != 1)
                    {
                        objetoObtenido.Cantidad -= 1;
                        context.TbConstruccionEquipamientos.Update(objetoObtenido);
                        await context.SaveChangesAsync();
                        return new Response { EsCorrecto = true, Mensaje = "OK" };
                    }
                    else
                    {
                        return new Response { EsCorrecto = true, Mensaje = "No rebaja mas" };
                    }
                }

                return new Response { EsCorrecto = false, Mensaje = "No existe" };

            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = $"ERROR {ex.Message}" };
            }
        }

        public async Task<Response> EliminarEquipamiento(int id)
        {
            try
            {
                TbConstruccionEquipamiento objetoObtenido = await context.TbConstruccionEquipamientos.FindAsync(id);

                if (objetoObtenido != null)
                {
                    context.TbConstruccionEquipamientos.Remove(objetoObtenido);
                    await context.SaveChangesAsync();
                    return new Response { EsCorrecto = true };
                }

                return new Response { EsCorrecto = false, Mensaje = "No existe" };
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = $"ERROR {ex.Message}" };
            }
        }

        public async Task<Construccion> ObtenerConstruccion(int idConstruccion)
        {
            try
            {
                /*este caso aplica para construcciones que ya poseen toda la informacion cargada*/
                List<Construccion> consulta = (from construccion in context.TbConstruccions
                                               join medidadConstruccion in context.TbMedidaContruccions
                                               on construccion.IdMedidaCon equals medidadConstruccion.IdMedidaCon
                                               join tipoMedida in context.TbTipoMedidas
                                               on medidadConstruccion.IdTipoMedida equals tipoMedida.IdTipoMedida
                                               join visualizacion in context.TbPersVisuals
                                               on construccion.IdPerVisual equals visualizacion.IdPerVisual
                                               join propiedadConstruccion in context.TbPropiedadConstruccions
                                               on construccion.IdConstruccion equals propiedadConstruccion.IdConstruccion

                                               where construccion.IdConstruccion == idConstruccion

                                               group construccion by new
                                               {
                                                   medidadConstruccion.Medida,
                                                   d1 = tipoMedida.Descripcion,
                                                   tipoMedida.Siglas,
                                                   d2 = visualizacion.Descripcion,
                                                   d4 = construccion.Descripcion,
                                                   construccion.Antiguedad,
                                                   construccion.Estado,
                                                   construccion.Estadofisico,
                                                   construccion.FechaRegis,
                                                   construccion.IdConstruccion,
                                                   propiedadConstruccion.IdPropiedad,
                                                   construccion.IdPerVisual,
                                                   construccion.IdMedidaCon
                                               } into c

                                               select new Construccion
                                               {
                                                   IdConstruccion = c.Key.IdConstruccion,
                                                   Estado = c.Key.Estado.Equals("A") ? "Activo" : "Inactivo",
                                                   FechaRegistra = c.Key.FechaRegis.ToShortDateString(),
                                                   IdPropiedad = c.Key.IdPropiedad,
                                                   Medida = c.Key.Medida,
                                                   TipoMedida = c.Key.d1,
                                                   Siglas = c.Key.Siglas,
                                                   Vistada = c.Key.d2,
                                                   Descripcion = c.Key.d4,
                                                   Antiguedad = c.Key.Antiguedad,
                                                   EstadoFisico = c.Key.Estadofisico,
                                                   IdVisualizacion = c.Key.IdPerVisual,
                                                   IdMedida = c.Key.IdMedidaCon,
                                               }).ToList();

                if (consulta.Count == 0)
                {
                    /*Este caso aplica para construccion que son nuevas y tiene algunos datos sin completar, ya que en 
                     la tabla de la base de datos algunas llaves que hacen referencia a otras tablas aun no tiene ligue*/
                    TbConstruccion construccionSinDatosAgregados = await context.TbConstruccions.FindAsync(idConstruccion);

                    /*para obtener el id de la propiedad a la que esta ligado*/
                    TbPropiedadConstruccion construccionPropiedad = await context.TbPropiedadConstruccions.FirstOrDefaultAsync(
                        x => x.IdConstruccion == idConstruccion);

                    return new Construccion()
                    {
                        IdConstruccion = idConstruccion,
                        FechaRegistra = construccionSinDatosAgregados.FechaRegis.ToShortDateString(),
                        Estado = construccionSinDatosAgregados.Estado.Equals("A") ? "Activo" : "Inactivo",
                        Descripcion = construccionSinDatosAgregados.Descripcion,
                        TipoMedida = string.Empty,
                        Siglas = string.Empty,
                        Antiguedad = string.Empty,
                        TipoCableado = string.Empty,
                        EstadoFisico = string.Empty,
                        IdPropiedad = construccionPropiedad.IdPropiedad,
                        Medida = 0.0M,
                        TipoEntubado = string.Empty,
                        Vistada = string.Empty
                    };

                }

                /*la idea es obtener en base al id de medida asociado, el id del tipo de medida, aca se hace el cambio,
                  esto para poder pintar en el combo box de medidad la correspondiente, y tambien pintar la cantidad de 
                dicha medida*/
                TbMedidaContruccion tipoMedidaAsociada = await context.TbMedidaContruccions.FirstOrDefaultAsync(
                    x => x.IdMedidaCon == consulta[0].IdMedida);
                consulta[0].IdMedida = tipoMedidaAsociada.IdTipoMedida;
                consulta[0].TotalMedida = tipoMedidaAsociada.Medida;

                consulta[0].EstadoFisico = consulta[0].EstadoFisico switch
                {
                    "A" => "Aceptable",
                    "D" => "Deficiente",
                    "R" => "Regular",
                    "B" => "Bueno",
                    "M" => "Muy bueno",
                    _ => "Sin definir",
                };

                return consulta[0];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ConstruccionDivision> ObtenerDivisionesAdquiridas(int idConstruccion)
        {
            try
            {
                List<ConstruccionDivision> consulta = (from construccionDivisiones in context.TbConstruccionDiviciones
                                                       join divisiones in context.TbDivisionesObras
                                                       on construccionDivisiones.IdDivision equals divisiones.IdDivision

                                                       where construccionDivisiones.IdConstruccion == idConstruccion

                                                       select new ConstruccionDivision
                                                       {
                                                           IdConstruccion = idConstruccion,
                                                           IdDivision = construccionDivisiones.IdDivision,
                                                           IdConstruccionDivision = construccionDivisiones.IdConsDivisiones,
                                                           Descripcion = divisiones.Descripcion,
                                                           NombreDescriptivo = string.IsNullOrEmpty(construccionDivisiones.NombreDescriptivo) ? string.Empty : construccionDivisiones.NombreDescriptivo,
                                                           Observacion = string.IsNullOrEmpty(construccionDivisiones.Observacion) ? string.Empty : construccionDivisiones.Observacion
                                                       }).ToList();

                return consulta;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ConstruccionEquipamiento> ObtenerEquipamientosAdquiridos(int idConstruccion)
        {
            try
            {
                List<ConstruccionEquipamiento> consulta = (from construccionEquipamiento in context.TbConstruccionEquipamientos
                                                           join equipamiento in context.TbEquipamientos
                                                           on construccionEquipamiento.IdEquipamiento equals equipamiento.IdEquipamiento

                                                           where construccionEquipamiento.IdConstruccion == idConstruccion

                                                           select new ConstruccionEquipamiento
                                                           {
                                                               IdConstruccion = idConstruccion,
                                                               IdEquipamiento = equipamiento.IdEquipamiento,
                                                               Descripcion = equipamiento.Descripcion,
                                                               Cantidad = construccionEquipamiento.Cantidad,
                                                               IdConstruccionEquipamiento = construccionEquipamiento.IdConstruccionEquipamiento
                                                           }).ToList();

                return consulta;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ConstruccionCaracteristica> ObtenerCaracteristicasConstruccionAdquiridas(int idConstruccion)
        {
            try
            {
                List<ConstruccionCaracteristica> consulta = (from construccionCaracteristica in context.TbContruccionCaracteristicas
                                                             join caracteristica in context.TbCaracteristicas
                                                             on construccionCaracteristica.IdCaracteristica equals caracteristica.IdCaracteristica

                                                             where construccionCaracteristica.IdConstruccion == idConstruccion

                                                             select new ConstruccionCaracteristica
                                                             {
                                                                 IdConstruccion = idConstruccion,
                                                                 IdCaracteristica = caracteristica.IdCaracteristica,
                                                                 Descripcion = caracteristica.Descripcion,
                                                                 Cantidad = construccionCaracteristica.Cantidad,
                                                                 IdConstruccionCaracteristica = construccionCaracteristica.IdConstruccionCaracteristica
                                                             }).ToList();

                return consulta;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Construccion> ObtenerListaConstrucciones(int idPropiedad)
        {
            try
            {
                List<Construccion> consulta = (from propiedadConstruccion in context.TbPropiedadConstruccions
                                               join construccion in context.TbConstruccions
                                               on propiedadConstruccion.IdConstruccion equals construccion.IdConstruccion

                                               where propiedadConstruccion.IdPropiedad == idPropiedad

                                               select new Construccion
                                               {
                                                   IdConstruccion = construccion.IdConstruccion,
                                                   Descripcion = construccion.Descripcion,
                                                   Estado = construccion.Estado.Equals("A") ? "Activo" : "Inactivo",
                                                   FechaRegistra = construccion.FechaRegis.ToShortDateString(),
                                                   IdPropiedad = propiedadConstruccion.IdPropiedad
                                               }).ToList();

                return consulta;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Response> AgregarDivision(Division division)
        {
            Response response = new Response();

            try
            {

                Microsoft.EntityFrameworkCore.Storage.IExecutionStrategy strategy = context.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = context.Database.BeginTransaction();

                    try
                    {

                        int idDivision = division.IdDivision;

                        TbConstruccionDivicione nuevaConstruccionDivision = new TbConstruccionDivicione()
                        {
                            IdConstruccion = division.IdConstruccion,
                            IdDivision = division.IdDivision,
                            Observacion = string.IsNullOrEmpty(division.Descripcion) ? string.Empty : division.Descripcion,
                            NombreDescriptivo = division.NombreDescriptivo
                        };

                        await context.TbConstruccionDiviciones.AddAsync(nuevaConstruccionDivision);
                        await context.SaveChangesAsync();

                        TbConstruccionDivicione divisionObtenida = await context.TbConstruccionDiviciones.FirstOrDefaultAsync(
                            x => x.IdConstruccion == division.IdConstruccion && x.NombreDescriptivo.Equals(division.NombreDescriptivo)
                            && x.IdDivision == division.IdDivision);

                        List<TbDivisionMateriale> listadoMateriales = division.Materiales.Select(x => new TbDivisionMateriale()
                        {
                            IdMaterial = x.IdMaterial,
                            Descripcion = string.IsNullOrEmpty(x.Descripcion) ? string.Empty : x.Descripcion,
                            IdDivision = idDivision,
                            IdConsDivisiones = divisionObtenida.IdConsDivisiones
                        }).ToList();

                        await context.TbDivisionMateriales.AddRangeAsync(listadoMateriales);
                        await context.SaveChangesAsync();

                        /*Si todo transcurrio correctamente, se cierra la transaccion de manera exitosa*/
                        transaction.Commit();
                        response = new Response { EsCorrecto = true, Mensaje = "OK" };

                    }
                    catch (Exception ex)
                    {
                        response = new Response { EsCorrecto = false, Mensaje = $"ERROR: {ex.Message}" };
                        transaction.Rollback();
                    }

                });
            }
            catch (Exception ex)
            {
                return response = new Response { EsCorrecto = false, Mensaje = $"ERROR: {ex.Message}" };
            }

            return response;
        }

        public async Task<bool> AgregarEquipamiento(int idEquipamiento, int idConstruccion)
        {
            try
            {
                TbEquipamiento equipamiento = await context.TbEquipamientos.FindAsync(idEquipamiento);

                if (equipamiento != null)
                {
                    TbConstruccionEquipamiento equipamientoExistente = await context.TbConstruccionEquipamientos.FirstOrDefaultAsync(
                        x => x.IdEquipamiento == idEquipamiento && x.IdConstruccion == idConstruccion);

                    if (equipamientoExistente != null)
                    {
                        equipamientoExistente.Cantidad += 1;
                        context.TbConstruccionEquipamientos.Update(equipamientoExistente);
                        await context.SaveChangesAsync();
                        return true;
                    }

                    TbConstruccionEquipamiento nuevoEquipamiento = new TbConstruccionEquipamiento()
                    {
                        IdConstruccion = idConstruccion,
                        IdEquipamiento = equipamiento.IdEquipamiento,
                        Cantidad = 1
                    };

                    await context.TbConstruccionEquipamientos.AddAsync(nuevoEquipamiento);
                    await context.SaveChangesAsync();

                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AgregarCaracteristicaConstruccion(int idCaracteristica, int idConstruccion)
        {
            try
            {
                TbCaracteristica caracteristica = await context.TbCaracteristicas.FindAsync(idCaracteristica);

                if (caracteristica != null)
                {
                    TbContruccionCaracteristica caracteristicaExistente = await context.TbContruccionCaracteristicas.FirstOrDefaultAsync(
                        x => x.IdCaracteristica == idCaracteristica && x.IdConstruccion == idConstruccion);

                    if (caracteristicaExistente != null)
                    {
                        caracteristicaExistente.Cantidad += 1;
                        context.TbContruccionCaracteristicas.Update(caracteristicaExistente);
                        await context.SaveChangesAsync();
                        return true;
                    }

                    TbContruccionCaracteristica nuevaCaracteristica = new TbContruccionCaracteristica()
                    {
                        IdConstruccion = idConstruccion,
                        IdCaracteristica = caracteristica.IdCaracteristica,
                        Cantidad = 1
                    };

                    await context.TbContruccionCaracteristicas.AddAsync(nuevaCaracteristica);
                    await context.SaveChangesAsync();

                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Response> DisminuirCaracteristicaConstruccion(int idConstruccionCaracteristica)
        {
            try
            {
                TbContruccionCaracteristica objetoObtenido = await context.TbContruccionCaracteristicas.FindAsync(idConstruccionCaracteristica);

                if (objetoObtenido != null)
                {
                    if (objetoObtenido.Cantidad != 1)
                    {
                        objetoObtenido.Cantidad -= 1;
                        context.TbContruccionCaracteristicas.Update(objetoObtenido);
                        await context.SaveChangesAsync();
                        return new Response { EsCorrecto = true, Mensaje = "OK" };
                    }
                    else
                    {
                        return new Response { EsCorrecto = true, Mensaje = "No rebaja mas" };
                    }

                }

                return new Response { EsCorrecto = false, Mensaje = "No existe" };

            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = $"ERROR {ex.Message}" };
            }
        }

        public async Task<Response> AumentarCaracteristicaConstruccion(int idConstruccionCaracteristica)
        {
            try
            {
                TbContruccionCaracteristica objetoObtenido = await context.TbContruccionCaracteristicas.FindAsync(idConstruccionCaracteristica);

                if (objetoObtenido != null)
                {
                    objetoObtenido.Cantidad += 1;
                    context.TbContruccionCaracteristicas.Update(objetoObtenido);
                    await context.SaveChangesAsync();
                    return new Response { EsCorrecto = true };
                }

                return new Response { EsCorrecto = false, Mensaje = "No existe" };
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = $"ERROR {ex.Message}" };
            }
        }

        public async Task<Response> EliminarCaracteristicaConstruccion(int idConstruccionCaracteristica)
        {
            try
            {
                TbContruccionCaracteristica objetoObtenido = await context.TbContruccionCaracteristicas.FindAsync(idConstruccionCaracteristica);

                if (objetoObtenido != null)
                {
                    context.TbContruccionCaracteristicas.Remove(objetoObtenido);
                    await context.SaveChangesAsync();
                    return new Response { EsCorrecto = true };
                }

                return new Response { EsCorrecto = false, Mensaje = "No existe" };
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = $"ERROR {ex.Message}" };
            }
        }

        public async Task<Response> EditarDivision(ConstruccionDivision division)
        {
            try
            {
                TbConstruccionDivicione divisionActualizar = await context.TbConstruccionDiviciones.FirstOrDefaultAsync(
                    x => x.IdConstruccion == division.IdConstruccion && x.IdConsDivisiones == division.IdConstruccionDivision
                    && x.IdDivision == division.IdDivision);

                if (divisionActualizar != null)
                {
                    divisionActualizar.NombreDescriptivo = string.IsNullOrEmpty(division.NombreDescriptivo) ? string.Empty : division.NombreDescriptivo;
                    divisionActualizar.Observacion = string.IsNullOrEmpty(division.Observacion) ? string.Empty : division.Observacion;

                    context.TbConstruccionDiviciones.Update(divisionActualizar);
                    await context.SaveChangesAsync();
                    return new Response { EsCorrecto = true, Mensaje = "OK" };
                }

                return new Response { EsCorrecto = false, Mensaje = "No existe" };
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }

        }

        public async Task<Response> EliminarDivisionAgregada(Division division)
        {

            Response response = new Response();
            try
            {

                Microsoft.EntityFrameworkCore.Storage.IExecutionStrategy strategy = context.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = context.Database.BeginTransaction();

                    try
                    {

                        /*obtener la division ligada a la construccion*/
                        TbConstruccionDivicione divisionObtenida = await context.TbConstruccionDiviciones.FindAsync(division.IdConstruccionDivision);

                        if (divisionObtenida != null)
                        {
                            /*obtener los materiales pertenecientes a dicha division*/
                            List<TbDivisionMateriale> listadoMateriales = await context.TbDivisionMateriales.Where(
                                x => x.IdConsDivisiones == divisionObtenida.IdConsDivisiones
                                ).ToListAsync();

                            context.TbDivisionMateriales.RemoveRange(listadoMateriales);
                            await context.SaveChangesAsync();

                            context.TbConstruccionDiviciones.Remove(divisionObtenida);
                            await context.SaveChangesAsync();

                            transaction.Commit();
                            response = new Response { EsCorrecto = true, Mensaje = "OK" };
                        }
                        else
                        {
                            response = new Response { EsCorrecto = false, Mensaje = "No existe" };
                        }

                    }
                    catch (Exception ex)
                    {
                        response = new Response { EsCorrecto = false, Mensaje = $"ERROR: {ex.Message}" };
                        transaction.Rollback();
                    }

                });
            }
            catch (Exception ex)
            {
                return response = new Response { EsCorrecto = false, Mensaje = $"ERROR: {ex.Message}" };
            }

            return response;

        }

        public async Task<Response> ObtenerDivisionAdquirida(int idConstruccion, int idConstruccionDivision)
        {
            try
            {
                TbConstruccionDivicione existe = await context.TbConstruccionDiviciones.FindAsync(idConstruccionDivision);

                if (existe == null)
                {
                    return new Response { EsCorrecto = false, Mensaje = "No existe" };
                }

                List<ConstruccionDivision> consulta = (from construccionDivisiones in context.TbConstruccionDiviciones
                                                       join divisiones in context.TbDivisionesObras
                                                       on construccionDivisiones.IdDivision equals divisiones.IdDivision

                                                       where construccionDivisiones.IdConstruccion == idConstruccion
                                                             && construccionDivisiones.IdConsDivisiones == idConstruccionDivision

                                                       select new ConstruccionDivision
                                                       {
                                                           IdConstruccion = idConstruccion,
                                                           IdDivision = construccionDivisiones.IdDivision,
                                                           IdConstruccionDivision = construccionDivisiones.IdConsDivisiones,
                                                           Descripcion = divisiones.Descripcion,
                                                           NombreDescriptivo = construccionDivisiones.NombreDescriptivo,
                                                           Observacion = construccionDivisiones.Observacion

                                                       }).ToList();

                List<MaterialesDivision> materiales = (from construccionDivisiones in context.TbConstruccionDiviciones
                                                       join divisionMateriales in context.TbDivisionMateriales
                                                       on construccionDivisiones.IdConsDivisiones equals divisionMateriales.IdConsDivisiones
                                                       join materialesObra in context.TbMaterialesObras
                                                       on divisionMateriales.IdMaterial equals materialesObra.IdMaterial

                                                       where construccionDivisiones.IdConstruccion == idConstruccion
                                                             && construccionDivisiones.IdConsDivisiones == idConstruccionDivision

                                                       select new MaterialesDivision
                                                       {
                                                           IdDivisionMateriales = divisionMateriales.IdDivisionMateriales,
                                                           IdMaterial = divisionMateriales.IdMaterial,
                                                           IdDivision = construccionDivisiones.IdDivision,
                                                           IdConstruccionDivision = divisionMateriales.IdConsDivisiones,
                                                           Descripcion = divisionMateriales.Descripcion,
                                                           Nombre = materialesObra.Descripcion,

                                                       }).ToList();

                consulta[0].Materiales = materiales;

                return new Response { EsCorrecto = true, Resultado = consulta[0] };
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }
        }

        public async Task<Response> AgregarMaterialDivision(Material division)
        {
            try
            {
                TbDivisionMateriale nuevoMaterialDivision = new TbDivisionMateriale()
                {
                    IdMaterial = division.IdMaterial,
                    IdDivision = division.IdDivision,
                    IdConsDivisiones = division.IdConstruccionDivisiones,
                    Descripcion = string.IsNullOrEmpty(division.Descripcion) ? string.Empty : division.Descripcion
                };

                await context.TbDivisionMateriales.AddAsync(nuevoMaterialDivision);
                await context.SaveChangesAsync();
                return new Response() { EsCorrecto = true, Mensaje = "OK" };
            }
            catch (Exception ex)
            {
                return new Response() { EsCorrecto = false, Mensaje = ex.Message };
            }
        }

        public async Task<Response> EliminarMaterialDivision(Material division)
        {
            try
            {
                if (string.IsNullOrEmpty(division.Descripcion))
                {
                    division.Descripcion = string.Empty;
                }

                TbConstruccionDivicione existeDivision = await context.TbConstruccionDiviciones.FindAsync(division.IdDivision);

                if (existeDivision == null)
                {
                    return new Response { EsCorrecto = false, Mensaje = "No existe division" };
                }

                TbDivisionMateriale existenteMaterialDivision = await context.TbDivisionMateriales.FirstOrDefaultAsync(
                    x => x.IdConsDivisiones == division.IdConstruccionDivisiones
                    && x.IdDivision == division.IdDivision
                    && x.IdMaterial == division.IdMaterial
                    && x.Descripcion == division.Descripcion);

                if (existenteMaterialDivision != null)
                {
                    context.TbDivisionMateriales.Remove(existenteMaterialDivision);
                    await context.SaveChangesAsync();
                    return new Response() { EsCorrecto = true, Mensaje = "OK" };
                }

                return new Response() { EsCorrecto = false, Mensaje = "No existe material" };

            }
            catch (Exception ex)
            {
                return new Response() { EsCorrecto = false, Mensaje = ex.Message };
            }

        }

        public async Task<Response> AgregarDatosBasicosConstruccion(EditarConstruccion construccion)
        {
            Response response = new Response();

            try
            {

                Microsoft.EntityFrameworkCore.Storage.IExecutionStrategy strategy = context.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = context.Database.BeginTransaction();

                    try
                    {

                        TbConstruccion construccionObtenida = await context.TbConstruccions.FindAsync(construccion.IdConstruccion);

                        /*se recupera el tipo de medida agregado para asociarla a la construccion*/
                        TbMedidaContruccion obtenerMedidaConstruccion = await context.TbMedidaContruccions.FirstOrDefaultAsync(
                            x => x.CodigoIdentificador.Equals(construccionObtenida.CodigoIdentificador));

                        /*este caso aplica unicamente para la primera vez que se agregan los datos de la construccion*/
                        TbMedidaContruccion nuevaMedida = null;
                        if (obtenerMedidaConstruccion == null)
                        {
                            nuevaMedida = new TbMedidaContruccion()
                            {
                                IdTipoMedida = construccion.IdTipoMedida,
                                Medida = construccion.TotalMedida,
                                CodigoIdentificador = construccionObtenida.CodigoIdentificador
                            };

                            /*almaceno el tipo de medida*/
                            await context.TbMedidaContruccions.AddAsync(nuevaMedida);
                            await context.SaveChangesAsync();

                        }
                        else /*actualiza los nuevos valores para la medida de la construccion*/
                        {
                            obtenerMedidaConstruccion.IdTipoMedida = construccion.IdTipoMedida;
                            obtenerMedidaConstruccion.Medida = construccion.TotalMedida;

                            context.TbMedidaContruccions.Update(obtenerMedidaConstruccion);
                            await context.SaveChangesAsync();
                        }

                        if (construccionObtenida != null)
                        {
                            construccionObtenida.IdMedidaCon = obtenerMedidaConstruccion == null ? nuevaMedida.IdMedidaCon : obtenerMedidaConstruccion.IdMedidaCon;
                            construccionObtenida.Antiguedad = $"{construccion.TotalPeriodo}-{construccion.Periodo}";
                            construccionObtenida.Estadofisico = construccion.EstadoFisico.Substring(0, 1);
                            construccionObtenida.IdPerVisual = construccion.IdTipoVisualizacion;

                            context.TbConstruccions.Update(construccionObtenida);
                            await context.SaveChangesAsync();

                            /*Si todo transcurrio correctamente, se cierra la transaccion de manera exitosa*/
                            transaction.Commit();
                            response = new Response { EsCorrecto = true, Mensaje = "OK" };
                        }

                    }
                    catch (Exception ex)
                    {
                        response = new Response { EsCorrecto = false, Mensaje = $"ERROR: {ex.Message}" };
                        transaction.Rollback();
                    }

                });
            }
            catch (Exception ex)
            {
                return response = new Response { EsCorrecto = false, Mensaje = $"ERROR: {ex.Message}" };
            }

            return response;
        }

        public async Task<Response> AgregarTiposDeCableadoConstruccion(Cableado cableado)
        {
            try
            {
                List<TbConstruccionCableado> listadoCableados = cableado.Cableados.Select(x => new TbConstruccionCableado()
                {
                    IdTipoCableado = x.IdTipoCableado,
                    Entubado = x.EsEntubado.Equals("Si") ? "S" : "N",
                    IdConstruccion = cableado.IdConstruccion,
                    Observacion = string.IsNullOrEmpty(x.Observacion) ? string.Empty : x.Observacion
                }).ToList();

                await context.TbConstruccionCableados.AddRangeAsync(listadoCableados);
                await context.SaveChangesAsync();
                return new Response { EsCorrecto = true, Mensaje = "OK" };
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }
        }

        public List<ConstruccionCableado> ObtenerListadoTiposCableadoObtenidos(int idConstruccion)
        {
            try
            {
                List<ConstruccionCableado> consulta = (from cableadoConstruccion in context.TbConstruccionCableados
                                                       join tiposCableado in context.TbTipocableados
                                                       on cableadoConstruccion.IdTipoCableado equals tiposCableado.IdTipoCableado

                                                       where cableadoConstruccion.IdConstruccion == idConstruccion

                                                       select new ConstruccionCableado
                                                       {
                                                           IdConstruccionCableado = cableadoConstruccion.IdConstruccionCableado,
                                                           Observacion = cableadoConstruccion.Observacion,
                                                           Descripcion = tiposCableado.Descripcion,
                                                           Entubado = cableadoConstruccion.Entubado.Equals("S") ? "Si" : "No"
                                                       }).ToList();

                return consulta;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Response> EliminarCableadoConstruccion(Cableado cableado)
        {
            try
            {
                TbConstruccionCableado cableadoObtenido = await context.TbConstruccionCableados.FindAsync(cableado.IdConstruccionCableado);

                if (cableadoObtenido != null)
                {
                    context.TbConstruccionCableados.Remove(cableadoObtenido);
                    await context.SaveChangesAsync();
                    return new Response { EsCorrecto = true, Mensaje = "OK" };
                }

                return new Response { EsCorrecto = false, Mensaje = "No existe" };
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = true, Mensaje = ex.Message };
            }

        }

        public async Task<Response> ObtenerTipoCableadoAdquirido(Cableado cableado)
        {
            try
            {
                TbConstruccionCableado existe = await context.TbConstruccionCableados.FindAsync(cableado.IdConstruccionCableado);

                if (existe == null)
                {
                    return new Response { EsCorrecto = false, Mensaje = "No existe" };
                }

                List<TiposCableado> consulta = (from construccionCableado in context.TbConstruccionCableados
                                                join tipoCableado in context.TbTipocableados
                                                on construccionCableado.IdTipoCableado equals tipoCableado.IdTipoCableado

                                                where construccionCableado.IdConstruccionCableado == cableado.IdConstruccionCableado
                                                      && construccionCableado.IdConstruccion == cableado.IdConstruccion

                                                select new TiposCableado
                                                {
                                                    Nombre = tipoCableado.Descripcion,
                                                    IdConstruccionCableado = construccionCableado.IdConstruccionCableado,
                                                    EsEntubado = construccionCableado.Entubado.Equals("S") ? "Si" : "No",
                                                    Observacion = construccionCableado.Observacion
                                                }).ToList();

                return new Response { EsCorrecto = true, Resultado = consulta[0] };
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }
        }

        public async Task<Response> EditarCableadoConstruccion(TiposCableado cableado)
        {
            try
            {
                TbConstruccionCableado cableadoObtenido = await context.TbConstruccionCableados.FindAsync(cableado.IdConstruccionCableado);

                if (cableadoObtenido != null)
                {
                    cableadoObtenido.Entubado = cableado.EsEntubado;
                    cableadoObtenido.Observacion = string.IsNullOrEmpty(cableado.Observacion) ? string.Empty : cableado.Observacion;

                    context.TbConstruccionCableados.Update(cableadoObtenido);
                    await context.SaveChangesAsync();
                    return new Response { EsCorrecto = true, Mensaje = "OK" };
                }

                return new Response { EsCorrecto = false, Mensaje = "No existe" };
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }

        }

        public async Task<Response> CambiarEstadoConstruccion(Construccion construccion)
        {
            try
            {
                TbConstruccion obtenerConstruccion = await context.TbConstruccions.FindAsync(construccion.IdConstruccion);

                if (obtenerConstruccion != null)
                {
                    obtenerConstruccion.Estado = construccion.Estado;

                    context.TbConstruccions.Update(obtenerConstruccion);
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

        public async Task<Response> AgregarImagenConstruccion(string rutaImagen, int idConstruccion)
        {
            try
            {
                TbRutaImgconst nuevaImagen = new TbRutaImgconst
                {
                    FechaIns = DateTime.Now.ToUniversalTime(),
                    IdConstruccion = idConstruccion,
                    Ruta = rutaImagen
                };

                await context.TbRutaImgconsts.AddAsync(nuevaImagen);
                await context.SaveChangesAsync();
                return new Response { EsCorrecto = true, Mensaje = "OK" };
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }
        }

        public async Task<Response> ObtenerImagenesConstruccion(int idConstruccion)
        {
            try
            {
                List<TbRutaImgconst> listaImagenes = await context.TbRutaImgconsts.Where(x => x.IdConstruccion == idConstruccion).ToListAsync();
                if (listaImagenes != null)
                {
                    List<Imagen> lista = listaImagenes.Select(x => new Imagen
                    {
                        Ruta = x.Ruta,
                        Titulo = Guid.NewGuid().ToString(),
                        IdImagen = x.IdRuta
                    }).ToList();

                    return new Response { EsCorrecto = true, Mensaje = "OK", Resultado = lista };
                }
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }

            return new Response { EsCorrecto = false };
        }

        public async Task<Response> EliminarImagenGaleria(Imagen imagen)
        {
            try
            {
                if (imagen != null)
                {
                    TbRutaImgconst imagenEliminar = await context.TbRutaImgconsts.FindAsync(imagen.IdImagen);
                    context.TbRutaImgconsts.Remove(imagenEliminar);
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

        public async Task<Response> ObtenerImagen(int idImagen)
        {
            try
            {
                TbRutaImgconst imagenObtenida = await context.TbRutaImgconsts.FindAsync(idImagen);

                if (imagenObtenida != null)
                {
                    Imagen imagenEnvia = new Imagen
                    {
                        Ruta = imagenObtenida.Ruta,
                        Titulo = Guid.NewGuid().ToString(),
                        IdImagen = imagenObtenida.IdRuta
                    };

                    return new Response { EsCorrecto = true, Mensaje = "OK", Resultado = imagenEnvia };
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

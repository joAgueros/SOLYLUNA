using AccesoDatos.BlogCore.Models;
using AccesoDatos.Data.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Modelos.Entidades;
using Modelos.ViewModels.Propiedades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AccesoDatos.Data.EntidadesImplementadas
{
    public class PropiedadRepository : Repository<TbPropiedade>, IPropiedadRepository
    {
        private readonly SolyLunaDbContext context;
        private readonly IBitacoraRepository bitacora;

        public PropiedadRepository(SolyLunaDbContext context,
                                IBitacoraRepository bitacora) : base(context)
        {
            this.context = context;
            this.bitacora = bitacora;
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

        public List<TiposPozo> ObtenerTipoPozo()
        {
            return new List<TiposPozo>()
            {
                new TiposPozo(){ IdTipoPozo = 1, Descripcion = "Pozos hincados"},
                new TiposPozo(){ IdTipoPozo = 2, Descripcion = "Pozos excavados (Artesanal)"},
                new TiposPozo(){ IdTipoPozo = 3, Descripcion = "Pozos aforados o perforaciones"},
            };
        }

        public List<EstadosPozo> ObtenerEstadosPozo()
        {
            return new List<EstadosPozo>()
            {
                new EstadosPozo(){ IdEstadoPozo = 1, Descripcion = "Inscrito"},
                new EstadosPozo(){ IdEstadoPozo = 2, Descripcion = "Sin inscribir"},
                new EstadosPozo(){ IdEstadoPozo = 3, Descripcion = "En trámite"},
            };
        }

        public List<NivelCalle> ObtenerNivelesCalle()
        {
            return new List<NivelCalle>()
            {
                new NivelCalle(){ IdNivelCalle = 1, Descripcion = "A nivel de la calle"},
                new NivelCalle(){ IdNivelCalle = 2, Descripcion = "Sobre el nivel de la calle"},
                new NivelCalle(){ IdNivelCalle = 3, Descripcion = "Bajo el nivel de la calle"}
            };
        }

        public List<Topografia> ObtenerTopografia()
        {
            return new List<Topografia>()
            {
                new Topografia(){ IdTopografia = 1, Descripcion = "Plana"},
                new Topografia(){ IdTopografia = 2, Descripcion = "Ondulada"},
                new Topografia(){ IdTopografia = 3, Descripcion = "Quebrada"},
                new Topografia(){ IdTopografia = 4, Descripcion = "Mixta"}
            };
        }

        public List<TiposAcceso> ObtenerTiposAcceso()
        {
            return new List<TiposAcceso>() {

                new TiposAcceso(){ Descripcion = "Pavimento", IdTipoAcceso = 1},
                new TiposAcceso(){ Descripcion = "Lastre", IdTipoAcceso = 2},
                new TiposAcceso(){ Descripcion = "Adoquín", IdTipoAcceso = 3},

            };
        }

        public async Task<List<TbCaracteristica>> ObtenerTodasLasCaracteristicas()
        {
            return await context.TbCaracteristicas.ToListAsync();
        }

        public async Task<List<TbEstadosPozo>> ObtenerTodosLosEstadosPozo()
        {
            return await context.TbEstadosPozos.ToListAsync();
        }

        public async Task<List<TbTiposerMunicipal>> ObtenerTodosLosServiciosMunicipales()
        {
            return await context.TbTiposerMunicipals.ToListAsync();
        }

        public async Task<List<TbServiciosPub>> ObtenerTodosLosServiciosPublicos()
        {
            return await context.TbServiciosPubs.ToListAsync();
        }

        public async Task<List<TbAcceso>> ObtenerTodosLosTiposAccesoPropiedad()
        {
            return await context.TbAccesos.ToListAsync();
        }

        public async Task<List<TbComisione>> ObtenerTodosLosTiposComision()
        {
            return await context.TbComisiones.ToListAsync();
        }

        public async Task<List<TbTipoCuota>> ObtenerTodosLosTiposCuotas()
        {
            return await context.TbTipoCuotas.ToListAsync();
        }

        public async Task<List<TbDocumento>> ObtenerTodosLosTiposDocumentos()
        {
            return await context.TbDocumentos.ToListAsync();
        }

        public async Task<List<TbTipoIntermediario>> ObtenerTodosLosTiposIntermediarios()
        {
            return await context.TbTipoIntermediarios.ToListAsync();
        }

        public async Task<List<TbTipoMedida>> ObtenerTodosLosTiposMedidas()
        {
            return await context.TbTipoMedidas.ToListAsync();
        }

        public async Task<List<TbTipoPropiedade>> ObtenerTodosLosTiposPropiedades()
        {
            return await context.TbTipoPropiedades.ToListAsync();
        }

        public async Task<List<TbTipoSituacion>> ObtenerTodosLosTiposSituacionPropiedad()
        {
            return await context.TbTipoSituacions.ToListAsync();
        }

        public async Task<List<TbUsoSuelo>> ObtenerTodosLosTiposUsoSuelo()
        {
            return await context.TbUsoSuelos.ToListAsync();
        }

        public async Task<List<TbUsoPropiedad>> ObtenerTodosLosUsosPropiedad()
        {
            return await context.TbUsoPropiedads.ToListAsync();
        }

        public async Task<List<TbPersVisual>> ObtenerTodasLasVistas()
        {
            return await context.TbPersVisuals.ToListAsync();
        }

        public List<EstadosCalificacion> EstadosDeCalificacion()
        {
            return new List<EstadosCalificacion>()
            {
                new EstadosCalificacion(){ IdEstadoCalificacion = 1, Descripcion = "Deficiente" },
                new EstadosCalificacion(){ IdEstadoCalificacion = 2, Descripcion = "Aceptable" },
                new EstadosCalificacion(){ IdEstadoCalificacion = 3, Descripcion = "Regular" },
                new EstadosCalificacion(){ IdEstadoCalificacion = 4, Descripcion = "Bueno" },
                new EstadosCalificacion(){ IdEstadoCalificacion = 5, Descripcion = "Muy bueno" },
            };
        }

        public async Task<TbUsoTipopropiedade> RegistrarUso_TipoPropiedad(RegistrarPropiedadViewModel propiedad, string codigoIdentificador)
        {
            try
            {
                TbTipoPropiedade tipoPropiedad = context.TbTipoPropiedades.Find(propiedad.TipoPropiedadId);

                TbUsoTipopropiedade nuevoRegistro = new TbUsoTipopropiedade()
                {
                    IdUsoPro = propiedad.UsoPropiedadId,
                    IdTipoPro = propiedad.TipoPropiedadId,
                    CodigoIdentificador = codigoIdentificador
                };

                await context.TbUsoTipopropiedades.AddAsync(nuevoRegistro);
                await context.SaveChangesAsync();

                TbUsoTipopropiedade objetoObtenido = context.TbUsoTipopropiedades.FirstOrDefault(
                    x => x.IdTipoPro == propiedad.TipoPropiedadId && x.IdUsoPro == propiedad.UsoPropiedadId);

                return objetoObtenido;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<TbAcceso> RegistrarAccesoPropiedad(RegistrarPropiedadViewModel propiedad, string codigoIdentificador)
        {
            try
            {

                TbAcceso nuevoRegistro = new TbAcceso()
                {
                    TipoAcceso = propiedad.TipoAcceso,
                    Estado = propiedad.EstadoAcceso,
                    CodigoIdentificador = codigoIdentificador
                };

                await context.TbAccesos.AddAsync(nuevoRegistro);
                await context.SaveChangesAsync();

                TbAcceso objetoObtenido = context.TbAccesos.FirstOrDefault(
                    x => x.TipoAcceso == propiedad.TipoAcceso && x.Estado == propiedad.EstadoAcceso);

                return objetoObtenido;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<TbMedidaPropiedad> RegistrarMedidaPropiedad(RegistrarPropiedadViewModel propiedad, string codigoIdentificador)
        {
            try
            {

                TbMedidaPropiedad nuevoRegistro = new TbMedidaPropiedad()
                {
                    CodigoIdentificador = codigoIdentificador,
                    IdTipoMedida = propiedad.MedidasPropiedadId,
                    Medida = Convert.ToDecimal(propiedad.TotalMedida)
                };

                await context.TbMedidaPropiedads.AddAsync(nuevoRegistro);
                await context.SaveChangesAsync();

                TbMedidaPropiedad objetoObtenido = context.TbMedidaPropiedads.FirstOrDefault(
                    x => x.IdTipoMedida == propiedad.MedidasPropiedadId && x.Medida == propiedad.TotalMedida);

                return objetoObtenido;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<TbEstadosPozo> RegistrarEstatusPozoPropiedad(RegistrarPropiedadViewModel propiedad, string codigoIdentificador)
        {
            try
            {
                TiposPozo tipoPozo = ObtenerTipoPozo().FirstOrDefault(tp => tp.Descripcion.Equals(propiedad.Pozo));
                EstadosPozo estatusPozo = ObtenerEstadosPozo().FirstOrDefault(ep => ep.Descripcion.Equals(propiedad.EstatusPozo));

                if (tipoPozo == null && estatusPozo == null)
                {
                    return null;
                }

                TbEstadosPozo nuevoRegistro = new TbEstadosPozo()
                {
                    CodigoIdentificador = codigoIdentificador,
                    Pozo = propiedad.Pozo,
                    EstadoLegal = propiedad.EstatusPozo.Substring(0, 1)
                };

                await context.TbEstadosPozos.AddAsync(nuevoRegistro);
                await context.SaveChangesAsync();

                TbEstadosPozo objetoObtenido = context.TbEstadosPozos.FirstOrDefault(
                    x => x.Pozo == propiedad.Pozo && x.EstadoLegal == propiedad.EstatusPozo.Substring(0, 1));

                return objetoObtenido;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool ValidarInformacionTopografia(string tipoTopografia)
        {

            Topografia topografiaObtenida = ObtenerTopografia().FirstOrDefault(x => x.Descripcion.Equals(tipoTopografia));

            if (topografiaObtenida == null)
            {
                return false;
            }

            return true;
        }

        public bool ValidarInformacionNiveCalle(string nivelCalle)
        {

            NivelCalle nivelCalleObtenido = ObtenerNivelesCalle().FirstOrDefault(x => x.Descripcion.Equals(nivelCalle));

            if (nivelCalleObtenido == null)
            {
                return false;
            }

            return true;
        }

        public async Task<string> AgregarDatosInicialesPropiedad(RegistrarPropiedadViewModel propiedad, string usuario)
        {
            try
            {
                string mensaje = string.Empty;

                Microsoft.EntityFrameworkCore.Storage.IExecutionStrategy strategy = context.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = context.Database.BeginTransaction();

                    try
                    {
                        int? idSinPozo = null;
                        int? idUsoSuelo = null;
                        string codigoIdentificadorPropiedad = Guid.NewGuid().ToString();

                        Ubicacion ubicacion = await context.Ubicacions.FindAsync(propiedad.DistritoId);
                        TbUsoTipopropiedade usoTipoPropiedadObtenido = await RegistrarUso_TipoPropiedad(propiedad, codigoIdentificadorPropiedad);
                        TbAcceso accesoPropiedadObtenido = await RegistrarAccesoPropiedad(propiedad, codigoIdentificadorPropiedad);
                        TbMedidaPropiedad medidaPropiedadObtenido = await RegistrarMedidaPropiedad(propiedad, codigoIdentificadorPropiedad);
                        TbEstadosPozo pozoPropiedadObtenido = await RegistrarEstatusPozoPropiedad(propiedad, codigoIdentificadorPropiedad);

                        TbTipoPropiedade tipoPropiedad = context.TbTipoPropiedades.Find(propiedad.TipoPropiedadId);

                        string barrioPoblado = string.Empty;
                        if (!string.IsNullOrEmpty(propiedad.BarrioPoblado))
                        {
                            barrioPoblado = $"{propiedad.BarrioPoblado.Trim()}, ";
                        }

                        TbPropiedade nuevoRegistro = new TbPropiedade()
                        {
                            IdUsoTipo = usoTipoPropiedadObtenido.IdUsoTipo,
                            IdUbicacion = propiedad.DistritoId,
                            Direccion = propiedad.DireccionExacta,
                            IdMedidaPro = medidaPropiedadObtenido.IdMedidaPro,
                            IdAcceso = accesoPropiedadObtenido.IdAcceso,
                            IdEstadosPozo = pozoPropiedadObtenido == null ? idSinPozo : pozoPropiedadObtenido.IdEstadosPozo,
                            IdUsoSuelo = propiedad.UsoSueloId == 0 ? idUsoSuelo : propiedad.UsoSueloId,
                            IdClienteV = propiedad.IdClienteVendedor,
                            FechaRegis = DateTime.UtcNow,
                            Publicado = "N",
                            Megusta = 0,
                            Intencion = propiedad.Intencion,
                            CodigoIdentificador = codigoIdentificadorPropiedad,
                            PrecioMax = propiedad.PrecioMaximo,
                            PrecioMin = propiedad.PrecioMinimo,
                            DisAgua = propiedad.DisponeAgua.ToString(),
                            CuotaMante = propiedad.CuotaMantenimiento,
                            NumFinca = propiedad.NumeroFinca,
                            NumPlano = propiedad.NumeroPlano,
                            NivelCalle = !ValidarInformacionNiveCalle(propiedad.NivelCalleSeleccionada) ? string.Empty : propiedad.NivelCalleSeleccionada,
                            Topografia = !ValidarInformacionTopografia(propiedad.TopografiaSeleccionada) ? string.Empty : propiedad.TopografiaSeleccionada,
                            Estado = "A",
                            Descripcion = string.IsNullOrEmpty(propiedad.DescripcionPropiedad) ? string.Empty : propiedad.DescripcionPropiedad.Trim(),
                            LinkVideo = string.IsNullOrEmpty(propiedad.LinkVideo) ? string.Empty : propiedad.LinkVideo.Trim(),
                            Moneda = string.IsNullOrEmpty(propiedad.Moneda) ? string.Empty : propiedad.Moneda.Trim(),
                            CodigoTipoUsoPropiedad = string.Empty,
                            BarrioPoblado = string.IsNullOrEmpty(barrioPoblado) ? string.Empty : barrioPoblado.Replace(",", string.Empty).Trim(),
                            PoseeVistaMar = propiedad.TipoVistada.Equals("Vista al Mar") ? "S" : "N",
                            PoseeVistaMontania = propiedad.TipoVistada.Equals("Vista a la montaña") ? "S" : "N",
                            PoseeVistaValle = propiedad.TipoVistada.Equals("Vista al Valle") ? "S" : "N",
                            SinVista = propiedad.TipoVistada.Equals("Sin vista") ? "S" : "N",
                            Eliminado = "N",
                            DireccionCompleta = $"{barrioPoblado}{ubicacion.Distrito.ToUpper()}, {ubicacion.Canton}, {ubicacion.Provincia}"
                        };

                        await context.TbPropiedades.AddAsync(nuevoRegistro);
                        await context.SaveChangesAsync();

                        /*Ingresar el codigo de propiedad*/
                        TbPropiedade propiedadEnBD = await context.TbPropiedades.FirstOrDefaultAsync(x => x.CodigoIdentificador.Equals(codigoIdentificadorPropiedad));
                        propiedadEnBD.CodigoTipoUsoPropiedad = $"{tipoPropiedad.Descripcion.Substring(0, 2)}-{propiedadEnBD.IdPropiedad}";
                        context.TbPropiedades.Update(propiedadEnBD);
                        await context.SaveChangesAsync();

                        /*Registro en la bitacora de la propiedad*/
                        await bitacora.InsertarPropiedad(usuario, codigoIdentificadorPropiedad);

                        /*Si todo transcurrio correctamente, se cierra la transaccion de manera exitosa*/
                        transaction.Commit();
                        mensaje = "OK";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        mensaje = $"ERROR {ex.Message}";
                    }
                });

                return mensaje;
            }
            catch (Exception ex)
            {
                return $"ERROR {ex.Message}";
            }
        }

        public List<MostrarPropiedadTabla> ObtenerListaPropiedades(string status)
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
                                                        join persona in context.TbClienteVendedors
                                                        on propiedad.IdClienteV equals persona.IdClienteV
                                                        join personaCliente in context.TbPersonas
                                                        on persona.IdPersona equals personaCliente.IdPersona

                                                        where propiedad.Eliminado == status

                                                        group propiedad by new
                                                        {
                                                            propiedad.IdPropiedad,
                                                            propiedad.IdClienteV,
                                                            personaCliente.Nombre,
                                                            propiedad.CodigoTipoUsoPropiedad,
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
                                                            propiedad.BarrioPoblado
                                                        } into p

                                                        select new MostrarPropiedadTabla
                                                        {
                                                            Id = p.Key.IdPropiedad,
                                                            IdClienteVenta = p.Key.IdClienteV,
                                                            NombreClienteV = p.Key.Nombre,
                                                            CodigoTipoUsoPropiedad = p.Key.CodigoTipoUsoPropiedad,
                                                            TipoPropiedad = p.Key.d1,
                                                            UsoPropiedad = p.Key.d2,
                                                            Ubicacion = $"{p.Key.Provincia.Trim()}, {p.Key.Canton.Trim()}, {p.Key.Distrito.Trim()}".ToUpper(),
                                                            MedidaPropiedad = $"{p.Key.Medida:N2} {p.Key.Siglas.Trim()} ({p.Key.d3.Trim()})",
                                                            PrecioMaximo = p.Key.PrecioMax,
                                                            PrecioMinimo = p.Key.PrecioMin,
                                                            Topografia = p.Key.Topografia,
                                                            Publicado = p.Key.Publicado.Equals("N") ? "No publicado" : "Publicado",
                                                            Moneda = p.Key.Moneda,
                                                            BarrioPoblado = p.Key.BarrioPoblado
                                                        }).ToList();

                return consulta;

            }
            catch (Exception)
            {
                return null;
            }
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

                    await context.AddAsync(nuevoEquipamiento);
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

        public async Task<Response> AgregarNuevaConstruccion(string descripcionConstruccion, int idPropiedad, string usuario)
        {
            try
            {

                Microsoft.EntityFrameworkCore.Storage.IExecutionStrategy strategy = context.Database.CreateExecutionStrategy();
                Response response = new Response();

                await strategy.ExecuteAsync(async () =>
                {
                    using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = context.Database.BeginTransaction();

                    try
                    {
                        string codigoIdentificador = Guid.NewGuid().ToString();

                        TbConstruccion nuevaConstruccion = new TbConstruccion()
                        {
                            FechaRegis = DateTime.Now,
                            Descripcion = descripcionConstruccion,
                            Estado = "A",
                            CodigoIdentificador = codigoIdentificador
                        };

                        /*Proceso para crear una nueva construccion*/
                        await context.TbConstruccions.AddAsync(nuevaConstruccion);
                        await context.SaveChangesAsync();

                        /*Ahora con la construccion obtenida, se crea la relacion propiedad-construccion*/
                        TbConstruccion construccionObtenida = await context.TbConstruccions.FirstOrDefaultAsync(
                            x => x.CodigoIdentificador.Equals(codigoIdentificador));

                        TbPropiedadConstruccion construccionPropiedad = new TbPropiedadConstruccion()
                        {
                            IdConstruccion = construccionObtenida.IdConstruccion,
                            IdPropiedad = idPropiedad
                        };

                        await context.AddAsync(construccionPropiedad);
                        await context.SaveChangesAsync();

                        /*Registro en la bitacora de la construccion*/
                        await bitacora.InsertarConstruccion(usuario, codigoIdentificador);

                        /*Si todo transcurrio correctamente, se cierra la transaccion de manera exitosa*/
                        transaction.Commit();

                        response.Resultado = construccionObtenida.IdConstruccion;
                        response.Mensaje = "OK";
                        response.EsCorrecto = true;
                    }
                    catch (Exception ex)
                    {
                        response.EsCorrecto = false;
                        response.Mensaje = ex.Message;
                        transaction.Rollback();
                    }

                });

                return response;
            }
            catch (Exception ex)
            {
                return new Response() { EsCorrecto = false, Mensaje = ex.Message };
            }
        }

        public async Task<Response> ObtenerConstruccion(int idPropiedad)
        {
            try
            {
                TbPropiedadConstruccion objetoObtenido = await context.TbPropiedadConstruccions.FirstOrDefaultAsync(
                                                    x => x.IdPropiedad == idPropiedad);

                TbConstruccion construccion = await context.TbConstruccions.FirstOrDefaultAsync(
                    x => x.IdConstruccion == objetoObtenido.IdConstruccion && x.Estado.Equals("E"));

                if (construccion != null)
                {
                    return new Response
                    {
                        EsCorrecto = true,
                        Mensaje = "OK",
                        Resultado = construccion.IdConstruccion
                    };
                }
                else
                {
                    return new Response
                    {
                        EsCorrecto = true,
                        Mensaje = "OK",
                        Resultado = construccion.IdConstruccion
                    };
                }
            }
            catch (Exception)
            {
                return new Response() { EsCorrecto = false };
            }
        }

        public List<PropiedadCaracteristica> ObtenerCaracteristicasPropiedadAdquiridas(int idPropiedad)
        {
            try
            {
                List<PropiedadCaracteristica> consulta = (from propiedadCaracteristica in context.TbPropiedadCaracteristicas
                                                          join caracteristica in context.TbCaracteristicas
                                                          on propiedadCaracteristica.IdCaracteristica equals caracteristica.IdCaracteristica

                                                          where propiedadCaracteristica.IdPropiedad == idPropiedad

                                                          select new PropiedadCaracteristica
                                                          {
                                                              IdPropiedad = idPropiedad,
                                                              IdCaracteristica = caracteristica.IdCaracteristica,
                                                              Descripcion = caracteristica.Descripcion,
                                                              Cantidad = propiedadCaracteristica.Cantidad,
                                                              IdPropiedadCaracteristica = propiedadCaracteristica.IdPropiedadCaracteristica
                                                          }).ToList();

                return consulta;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> AgregarCaracteristicasPropiedad(int idCaracteristica, int idPropiedad)
        {
            try
            {
                TbCaracteristica caracteristica = await context.TbCaracteristicas.FindAsync(idCaracteristica);

                if (caracteristica != null)
                {
                    TbPropiedadCaracteristica caracteristicaExistente = await context.TbPropiedadCaracteristicas.FirstOrDefaultAsync(
                        x => x.IdCaracteristica == idCaracteristica && x.IdPropiedad == idPropiedad);

                    if (caracteristicaExistente != null)
                    {
                        caracteristicaExistente.Cantidad += 1;
                        context.TbPropiedadCaracteristicas.Update(caracteristicaExistente);
                        await context.SaveChangesAsync();
                        return true;
                    }

                    TbPropiedadCaracteristica nuevaCaracteristica = new TbPropiedadCaracteristica()
                    {
                        IdPropiedad = idPropiedad,
                        IdCaracteristica = caracteristica.IdCaracteristica,
                        Cantidad = 1
                    };

                    await context.TbPropiedadCaracteristicas.AddAsync(nuevaCaracteristica);
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

        public async Task<Response> DisminuirCaracteristicaPropiedad(int idPropiedadCaracteristica)
        {
            try
            {
                TbPropiedadCaracteristica objetoObtenido = await context.TbPropiedadCaracteristicas.FindAsync(idPropiedadCaracteristica);

                if (objetoObtenido != null)
                {
                    if (objetoObtenido.Cantidad != 1)
                    {
                        objetoObtenido.Cantidad -= 1;
                        context.TbPropiedadCaracteristicas.Update(objetoObtenido);
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

        public async Task<Response> EliminarCaracteristicaPropiedad(int idPropiedadCaracteristica)
        {
            try
            {
                TbPropiedadCaracteristica objetoObtenido = await context.TbPropiedadCaracteristicas.FindAsync(idPropiedadCaracteristica);

                if (objetoObtenido != null)
                {
                    context.TbPropiedadCaracteristicas.Remove(objetoObtenido);
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

        public async Task<Response> AumentarCaracteristicasPropiedad(int idPropiedadCaracteristica)
        {
            try
            {
                TbPropiedadCaracteristica objetoObtenido = await context.TbPropiedadCaracteristicas.FindAsync(idPropiedadCaracteristica);

                if (objetoObtenido != null)
                {
                    objetoObtenido.Cantidad += 1;
                    context.TbPropiedadCaracteristicas.Update(objetoObtenido);
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

        public async Task<Response> AgregarDatosServiciosMunicipales(ServicioMunicipal servicio)
        {
            try
            {
                TbServiciosMunicipale nuevoServicioMunicipal = new TbServiciosMunicipale()
                {
                    IdPropiedad = servicio.IdPropiedad,
                    IdCuota = servicio.IdCuota,
                    IdTipoSer = servicio.IdTipoServicio,
                    Costo = servicio.Costo,
                    Estado = servicio.Estado,
                    Observacion = string.IsNullOrEmpty(servicio.Observacion) ? string.Empty : servicio.Observacion
                };

                await context.TbServiciosMunicipales.AddAsync(nuevoServicioMunicipal);
                await context.SaveChangesAsync();
                return new Response { EsCorrecto = true, Mensaje = "OK" };
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }
        }

        public List<ServicioMunicipal> ObtenerServiciosMunicipales(int idPropiedad)
        {
            try
            {
                List<ServicioMunicipal> consulta = (from servicioMunicipal in context.TbServiciosMunicipales
                                                    join tipoServicio in context.TbTiposerMunicipals
                                                    on servicioMunicipal.IdTipoSer equals tipoServicio.IdTipoSer
                                                    join tipoCuota in context.TbTipoCuotas
                                                    on servicioMunicipal.IdCuota equals tipoCuota.IdCuota

                                                    where servicioMunicipal.IdPropiedad == idPropiedad

                                                    select new ServicioMunicipal
                                                    {
                                                        IdServicioMunicipal = servicioMunicipal.IdSerMuni,
                                                        DescripcionServicio = tipoServicio.Descripcion,
                                                        DescripcionCuota = tipoCuota.Cuota,
                                                        Costo = servicioMunicipal.Costo,
                                                        Observacion = servicioMunicipal.Observacion,
                                                        Estado = servicioMunicipal.Estado.Equals("A") ? "Activo" : "Inactivo"
                                                    }).ToList();

                return consulta;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<AccesoPropiedad> ObtenerRecorridosPropiedad(int idPropiedad)
        {
            try
            {
                List<AccesoPropiedad> consulta = (from recorrido in context.TbRecorridos
                                                  join tipoAccesibilidad in context.TbAccesibilidads
                                                  on recorrido.IdAccesibilidad equals tipoAccesibilidad.IdAccesibilidad

                                                  where recorrido.IdPropiedad == idPropiedad

                                                  select new AccesoPropiedad
                                                  {
                                                      IdRecorrido = recorrido.IdRecorrido,
                                                      IdPropiedad = recorrido.IdPropiedad,
                                                      IdTipoAccesibilidad = recorrido.IdAccesibilidad,
                                                      Descripcion = recorrido.Descripcion,
                                                      Recorrido = recorrido.RecorridoKm,
                                                      DescripcionAccesibilidad = tipoAccesibilidad.Descripcion
                                                  }).ToList();

                return consulta;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Response> EliminarTipoServicioMunicipal(int idServicioMunicipal)
        {
            try
            {
                TbServiciosMunicipale servMunicipalObtenido = await context.TbServiciosMunicipales.FindAsync(idServicioMunicipal);

                if (servMunicipalObtenido != null)
                {
                    context.TbServiciosMunicipales.Remove(servMunicipalObtenido);
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

        public async Task<Response> ObtenerServicioMunicipal(int idServicio)
        {
            try
            {
                TbServiciosMunicipale existe = await context.TbServiciosMunicipales.FindAsync(idServicio);
                if (existe == null)
                {
                    return new Response { EsCorrecto = false, Mensaje = "No existe" };
                }

                List<ServicioMunicipal> consulta = (from servicioMunicipal in context.TbServiciosMunicipales
                                                    join tipoServicio in context.TbTiposerMunicipals
                                                    on servicioMunicipal.IdTipoSer equals tipoServicio.IdTipoSer
                                                    join tipoCuota in context.TbTipoCuotas
                                                    on servicioMunicipal.IdCuota equals tipoCuota.IdCuota

                                                    where servicioMunicipal.IdSerMuni == idServicio

                                                    select new ServicioMunicipal
                                                    {
                                                        IdServicioMunicipal = servicioMunicipal.IdSerMuni,
                                                        DescripcionServicio = tipoServicio.Descripcion,
                                                        DescripcionCuota = tipoCuota.Cuota,
                                                        Costo = servicioMunicipal.Costo,
                                                        Observacion = servicioMunicipal.Observacion,
                                                        Estado = servicioMunicipal.Estado.Equals("A") ? "Activo" : "Inactivo",
                                                        IdCuota = tipoCuota.IdCuota,
                                                        IdPropiedad = servicioMunicipal.IdPropiedad,
                                                        IdTipoServicio = tipoServicio.IdTipoSer
                                                    }).ToList();

                return new Response { EsCorrecto = true, Resultado = consulta[0] };

            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }
        }

        public async Task<Response> AgregarDatosEditadosServiciosMunicipales(ServicioMunicipal servicio)
        {
            try
            {
                TbServiciosMunicipale servicioMunicipalObtenido = await context.TbServiciosMunicipales.FindAsync(servicio.IdServicioMunicipal);

                if (servicioMunicipalObtenido != null)
                {
                    servicioMunicipalObtenido.Observacion = servicio.Observacion;
                    servicioMunicipalObtenido.Costo = servicio.Costo;
                    servicioMunicipalObtenido.Estado = servicio.Estado;
                    servicioMunicipalObtenido.IdCuota = servicio.IdCuota;
                    servicioMunicipalObtenido.IdTipoSer = servicio.IdTipoServicio;

                    context.TbServiciosMunicipales.Update(servicioMunicipalObtenido);
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

        public async Task<Response> AgregarDatosSituacionPropiedad(SituacionLegalPropiedad situacion)
        {
            try
            {
                TbLegalPropiedad nuevoServicioMunicipal = new TbLegalPropiedad()
                {
                    IdPropiedad = situacion.IdPropiedad,
                    IdCuota = situacion.IdCuota,
                    IdTipoSituacion = situacion.IdTipoSituacion,
                    Monto = situacion.Monto,
                    Estado = situacion.Estado,
                    Observacion = string.IsNullOrEmpty(situacion.Observacion) ? string.Empty : situacion.Observacion,
                    NombreEntidad = situacion.NombreEntidad
                };

                await context.TbLegalPropiedads.AddAsync(nuevoServicioMunicipal);
                await context.SaveChangesAsync();
                return new Response { EsCorrecto = true, Mensaje = "OK" };
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }
        }

        public List<SituacionLegalPropiedad> ObtenerSituacionesPropiedad(int idPropiedad)
        {
            try
            {
                List<SituacionLegalPropiedad> consulta = (from tipoSituacionLegal in context.TbLegalPropiedads
                                                          join tipoSituacion in context.TbTipoSituacions
                                                          on tipoSituacionLegal.IdTipoSituacion equals tipoSituacion.IdTipoSituacion
                                                          join tipoCuota in context.TbTipoCuotas
                                                          on tipoSituacionLegal.IdCuota equals tipoCuota.IdCuota

                                                          where tipoSituacionLegal.IdPropiedad == idPropiedad

                                                          select new SituacionLegalPropiedad
                                                          {
                                                              IdSituacionLegalPropiedad = tipoSituacionLegal.IdLegal,
                                                              DescripcionSituacion = tipoSituacion.Descripcion,
                                                              DescripcionCuota = tipoCuota.Cuota,
                                                              Monto = tipoSituacionLegal.Monto,
                                                              Observacion = tipoSituacionLegal.Observacion,
                                                              NombreEntidad = tipoSituacionLegal.NombreEntidad,
                                                              Estado = tipoSituacionLegal.Estado.Equals("A") ? "Activo" : "Inactivo"
                                                          }).ToList();

                return consulta;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Response> EliminarTipoSituacionPropiedad(int idSituacion)
        {
            try
            {
                TbLegalPropiedad situacionLegalObtenida = await context.TbLegalPropiedads.FindAsync(idSituacion);

                if (situacionLegalObtenida != null)
                {
                    context.TbLegalPropiedads.Remove(situacionLegalObtenida);
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

        public async Task<Response> ObtenerSituacionLegalPropiedad(int idSituacion)
        {
            try
            {
                TbLegalPropiedad existe = await context.TbLegalPropiedads.FindAsync(idSituacion);

                if (existe == null)
                {
                    return new Response { EsCorrecto = false, Mensaje = "No existe" };
                }

                List<SituacionLegalPropiedad> consulta = (from tipoSituacionLegal in context.TbLegalPropiedads
                                                          join tipoSituacion in context.TbTipoSituacions
                                                          on tipoSituacionLegal.IdTipoSituacion equals tipoSituacion.IdTipoSituacion
                                                          join tipoCuota in context.TbTipoCuotas
                                                          on tipoSituacionLegal.IdCuota equals tipoCuota.IdCuota

                                                          where tipoSituacionLegal.IdLegal == idSituacion

                                                          select new SituacionLegalPropiedad
                                                          {
                                                              IdSituacionLegalPropiedad = tipoSituacionLegal.IdLegal,
                                                              DescripcionSituacion = tipoSituacion.Descripcion,
                                                              DescripcionCuota = tipoCuota.Cuota,
                                                              Monto = tipoSituacionLegal.Monto,
                                                              Observacion = tipoSituacionLegal.Observacion,
                                                              NombreEntidad = tipoSituacionLegal.NombreEntidad,
                                                              Estado = tipoSituacionLegal.Estado.Equals("A") ? "Activo" : "Inactivo",
                                                              IdCuota = tipoCuota.IdCuota,
                                                              IdPropiedad = tipoSituacionLegal.IdPropiedad,
                                                              IdTipoSituacion = tipoSituacionLegal.IdTipoSituacion
                                                          }).ToList();

                return new Response { EsCorrecto = true, Resultado = consulta[0] };
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }
        }

        public async Task<Response> AgregarDatosEditadosSituacionPropiedad(SituacionLegalPropiedad situacion)
        {
            try
            {
                TbLegalPropiedad situacionLegalPropiedad = await context.TbLegalPropiedads.FindAsync(situacion.IdSituacionLegalPropiedad);

                if (situacionLegalPropiedad != null)
                {
                    situacionLegalPropiedad.Observacion = situacion.Observacion;
                    situacionLegalPropiedad.Monto = situacion.Monto;
                    situacionLegalPropiedad.Estado = situacion.Estado;
                    situacionLegalPropiedad.IdCuota = situacion.IdCuota;
                    situacionLegalPropiedad.IdTipoSituacion = situacion.IdTipoSituacion;
                    situacionLegalPropiedad.NombreEntidad = situacion.NombreEntidad;

                    context.TbLegalPropiedads.Update(situacionLegalPropiedad);
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

        public async Task<Response> AgregarDatosServiciosPublicos(ServicioPublico servicio)
        {
            try
            {
                TbServiciosPubPropiedad nuevoServicioPublico = new TbServiciosPubPropiedad()
                {
                    IdPropiedad = servicio.IdPropiedad,
                    IdServicioPublico = servicio.IdTipoServicio,
                    Costo = servicio.Costo,
                    Estado = servicio.Estado,
                    Observacion = string.IsNullOrEmpty(servicio.Observacion) ? string.Empty : servicio.Observacion,
                    Empresa = servicio.Empresa,
                    Distancia = servicio.Distancia
                };

                await context.TbServiciosPubPropiedads.AddAsync(nuevoServicioPublico);
                await context.SaveChangesAsync();
                return new Response { EsCorrecto = true, Mensaje = "OK" };
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }
        }

        public List<ServicioPublico> ObtenerServiciosPublicos(int idPropiedad)
        {
            try
            {
                List<ServicioPublico> consulta = (from servicioPublico in context.TbServiciosPubPropiedads
                                                  join tipoServicio in context.TbServiciosPubs
                                                  on servicioPublico.IdServicioPublico equals tipoServicio.IdServicioPublico

                                                  where servicioPublico.IdPropiedad == idPropiedad

                                                  select new ServicioPublico
                                                  {
                                                      IdServicioPublico = servicioPublico.IdServicioPubPropiedad,
                                                      DescripcionServicio = tipoServicio.Descripcion,
                                                      Distancia = servicioPublico.Distancia,
                                                      Costo = servicioPublico.Costo,
                                                      Empresa = servicioPublico.Empresa,
                                                      IdTipoServicio = tipoServicio.IdServicioPublico,
                                                      Observacion = servicioPublico.Observacion,
                                                      Estado = servicioPublico.Estado.Equals("A") ? "Activo" : "Inactivo"
                                                  }).ToList();

                return consulta;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Response> EliminarTipoServicioPublico(int idServicio)
        {
            try
            {
                TbServiciosPubPropiedad servPublicoObtenido = await context.TbServiciosPubPropiedads.FindAsync(idServicio);

                if (servPublicoObtenido != null)
                {
                    context.TbServiciosPubPropiedads.Remove(servPublicoObtenido);
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

        public async Task<Response> ObtenerServicioPublico(int idServicio)
        {
            try
            {
                TbServiciosPubPropiedad existe = await context.TbServiciosPubPropiedads.FindAsync(idServicio);

                if (existe == null)
                {
                    return new Response { EsCorrecto = false, Mensaje = "No existe" };
                }

                List<ServicioPublico> consulta = (from servicioPublico in context.TbServiciosPubPropiedads
                                                  join tipoServicio in context.TbServiciosPubs
                                                  on servicioPublico.IdServicioPublico equals tipoServicio.IdServicioPublico

                                                  where servicioPublico.IdServicioPubPropiedad == idServicio

                                                  select new ServicioPublico
                                                  {
                                                      IdServicioPublico = servicioPublico.IdServicioPubPropiedad,
                                                      DescripcionServicio = tipoServicio.Descripcion,
                                                      Distancia = servicioPublico.Distancia,
                                                      Costo = servicioPublico.Costo,
                                                      Empresa = servicioPublico.Empresa,
                                                      IdTipoServicio = tipoServicio.IdServicioPublico,
                                                      Observacion = servicioPublico.Observacion,
                                                      Estado = servicioPublico.Estado.Equals("A") ? "Activo" : "Inactivo",
                                                      IdPropiedad = servicioPublico.IdPropiedad
                                                  }).ToList();

                return new Response { EsCorrecto = true, Resultado = consulta[0] };
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }
        }

        public async Task<Response> AgregarDatosEditadosServiciosPublicos(ServicioPublico servicio)
        {
            try
            {
                TbServiciosPubPropiedad servicioPublicoObtenido = await context.TbServiciosPubPropiedads.FindAsync(servicio.IdServicioPublico);

                if (servicioPublicoObtenido != null)
                {
                    servicioPublicoObtenido.Observacion = servicio.Observacion;
                    servicioPublicoObtenido.Costo = servicio.Costo;
                    servicioPublicoObtenido.Estado = servicio.Estado;
                    servicioPublicoObtenido.Empresa = servicio.Empresa;
                    servicioPublicoObtenido.Distancia = servicio.Distancia;
                    servicioPublicoObtenido.IdServicioPublico = servicio.IdTipoServicio;

                    context.TbServiciosPubPropiedads.Update(servicioPublicoObtenido);
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

        public async Task<Response> AgregarDatosDocumentoPropiedad(DocumentoPropiedad documento)
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

                TbDocumentosPropiedad nuevoDocumentoPropiedad = new TbDocumentosPropiedad()
                {
                    IdPropiedad = documento.IdPropiedad,
                    IdDocumento = documento.IdTipoDocumento,
                    Notas = string.IsNullOrEmpty(documento.Notas) ? string.Empty : documento.Notas,
                    FechaRegis = DateTime.Now.ToUniversalTime(),
                    Vencimiento = fechaVenc,
                    EstadoRecepcion = documento.Estado
                };

                await context.TbDocumentosPropiedads.AddAsync(nuevoDocumentoPropiedad);
                await context.SaveChangesAsync();
                return new Response { EsCorrecto = true, Mensaje = "OK" };
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }
        }

        public List<DocumentoPropiedad> ObtenerDocumentosPropiedad(int idPropiedad)
        {
            try
            {
                List<DocumentoPropiedad> consulta = (from documentoPropiedad in context.TbDocumentosPropiedads
                                                     join tipoDocumento in context.TbDocumentos
                                                     on documentoPropiedad.IdDocumento equals tipoDocumento.IdDocumento

                                                     where documentoPropiedad.IdPropiedad == idPropiedad

                                                     select new DocumentoPropiedad
                                                     {
                                                         IdDocumentoPropiedad = documentoPropiedad.IdDocPro,
                                                         DescripcionDocumento = tipoDocumento.Descripcion,
                                                         IdTipoDocumento = documentoPropiedad.IdDocumento,
                                                         Estado = documentoPropiedad.EstadoRecepcion.Equals("S") ? "Si" : "No",
                                                         FechaVencimientoString = documentoPropiedad.Vencimiento.Value.ToString("dd/MM/yyyy"),
                                                         Notas = documentoPropiedad.Notas,
                                                         FechaRegistroString = documentoPropiedad.FechaRegis.ToString("dd/MM/yyyy"),
                                                     }).ToList();

                return consulta;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Response> EliminarDocumentoPropiedad(int idDocumento)
        {
            try
            {
                TbDocumentosPropiedad objetoObtenido = await context.TbDocumentosPropiedads.FindAsync(idDocumento);

                if (objetoObtenido != null)
                {
                    context.TbDocumentosPropiedads.Remove(objetoObtenido);
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

        public async Task<Response> ObtenerDocumentoPropiedad(int idDocumento)
        {
            try
            {
                TbDocumentosPropiedad existe = await context.TbDocumentosPropiedads.FindAsync(idDocumento);

                if (existe == null)
                {
                    return new Response { EsCorrecto = false, Mensaje = "No existe" };
                }

                List<DocumentoPropiedad> consulta = (from documentoPropiedad in context.TbDocumentosPropiedads
                                                     join tipoDocumento in context.TbDocumentos
                                                     on documentoPropiedad.IdDocumento equals tipoDocumento.IdDocumento

                                                     where documentoPropiedad.IdDocPro == idDocumento

                                                     select new DocumentoPropiedad
                                                     {
                                                         IdDocumentoPropiedad = documentoPropiedad.IdDocPro,
                                                         DescripcionDocumento = tipoDocumento.Descripcion,
                                                         IdTipoDocumento = documentoPropiedad.IdDocumento,
                                                         Estado = documentoPropiedad.EstadoRecepcion.Equals("S") ? "Si" : "No",
                                                         FechaVencimientoString = documentoPropiedad.Vencimiento.Value.ToString("dd/MM/yyyy"),
                                                         Notas = documentoPropiedad.Notas,
                                                         FechaRegistroString = documentoPropiedad.FechaRegis.ToString("dd/MM/yyyy"),
                                                         FechaVencimiento = documentoPropiedad.Vencimiento
                                                     }).ToList();

                return new Response { EsCorrecto = true, Resultado = consulta[0] };
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }
        }

        public async Task<Response> AgregarDatosEditadosDocumentosPropiedad(DocumentoPropiedad documento)
        {
            try
            {
                TbDocumentosPropiedad documentoObtenido = await context.TbDocumentosPropiedads.FindAsync(documento.IdDocumentoPropiedad);

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

                    context.TbDocumentosPropiedads.Update(documentoObtenido);
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

        public async Task<VerPropiedadViewModel> ObtenerDatosPropiedad(int idPropiedad)
        {
            try
            {
                TbPropiedade datosPropiedad = await context.TbPropiedades.FindAsync(idPropiedad);

                if (datosPropiedad == null)
                {
                    return null;
                }

                TbEstadosPozo estadosPozo = null;
                TiposPozo tipoPozo = null;
                EstadosPozo estadoPozo = null;
                if (datosPropiedad.IdEstadosPozo != null)
                {
                    estadosPozo = await context.TbEstadosPozos.FindAsync(datosPropiedad.IdEstadosPozo);

                    if (estadosPozo != null)
                    {
                        tipoPozo = ObtenerTipoPozo().FirstOrDefault(x => x.Descripcion.Equals(estadosPozo.Pozo));
                        estadoPozo = ObtenerEstadosPozo().FirstOrDefault(x => x.Descripcion.StartsWith(estadosPozo.EstadoLegal));
                    }
                }

                TbAcceso acceso = await context.TbAccesos.FindAsync(datosPropiedad.IdAcceso);
                TbMedidaPropiedad medida = await context.TbMedidaPropiedads.FindAsync(datosPropiedad.IdMedidaPro);
                TbUsoSuelo usoSuelo = await context.TbUsoSuelos.FindAsync(datosPropiedad.IdUsoSuelo);
                Ubicacion ubicacion = await context.Ubicacions.FindAsync(datosPropiedad.IdUbicacion);

                TbUsoTipopropiedade usoTipo = await context.TbUsoTipopropiedades.FindAsync(datosPropiedad.IdUsoTipo);
                TbUsoPropiedad tbUsoPropiedad = null;
                TbTipoPropiedade tbTipoPropiedad = null;

                if (usoTipo != null)
                {
                    tbUsoPropiedad = await context.TbUsoPropiedads.FirstOrDefaultAsync(x => x.IdUsoPro == usoTipo.IdUsoPro);
                    tbTipoPropiedad = await context.TbTipoPropiedades.FirstOrDefaultAsync(x => x.IdTipoPro == usoTipo.IdTipoPro);
                }

                TbTipoMedida tbTipoMedida = null;

                if (medida != null)
                {
                    tbTipoMedida = await context.TbTipoMedidas.FirstOrDefaultAsync(x => x.IdTipoMedida == medida.IdTipoMedida);
                }

                VerPropiedadViewModel objetoPropiedad = new VerPropiedadViewModel()
                {

                    IdPropiedad = datosPropiedad.IdPropiedad,
                    IdClienteVendedor = (int)datosPropiedad.IdClienteV,
                    CodigoTipoUsoPropiedad = datosPropiedad.CodigoTipoUsoPropiedad,
                    TotalMeGusta = datosPropiedad.Megusta,
                    Estado = datosPropiedad.Estado.Equals("A"),
                    Publicado = datosPropiedad.Publicado.Equals("S"),
                    FechaRegistra = datosPropiedad.FechaRegis.ToLocalTime(),
                    NumeroFinca = datosPropiedad.NumFinca ?? "No indicado",
                    NumeroPlano = datosPropiedad.NumPlano ?? "No indicado",
                    CuotaMantenimiento = datosPropiedad.CuotaMante == null ? 0.0M : Convert.ToDecimal(Math.Round(datosPropiedad.CuotaMante.Value, 2)),
                    DisponeAgua = datosPropiedad.DisAgua.Equals("S") ? "Si" : "No",
                    NivelCalleSeleccionada = datosPropiedad.NivelCalle,
                    TopografiaSeleccionada = datosPropiedad.Topografia,
                    PrecioMaximo = datosPropiedad.PrecioMax == null ? 0.0M : Convert.ToDecimal(Math.Round(datosPropiedad.PrecioMax.Value, 2)),
                    PrecioMinimo = datosPropiedad.PrecioMin == null ? 0.0M : Convert.ToDecimal(Math.Round(datosPropiedad.PrecioMin.Value, 2)),
                    Pozo = estadosPozo == null ? "No indicado" : estadosPozo.Pozo,
                    EstatusPozo = estadosPozo == null ? "No indicado" : estadoPozo.Descripcion,
                    TipoAcceso = acceso == null ? "No indicado" : acceso.TipoAcceso,
                    EstadoAcceso = acceso == null ? "No indicado" : acceso.Estado,
                    TotalMedida = medida == null ? 0 : medida.Medida,
                    Siglas = tbTipoMedida == null ? "N/A" : tbTipoMedida.Siglas,
                    DescripcionMedida = tbTipoMedida == null ? string.Empty : tbTipoMedida.Descripcion,
                    UsoSuelo = usoSuelo == null ? "No indicado" : usoSuelo.Descripcion,
                    DireccionExacta = datosPropiedad.Direccion,
                    Provincia = ubicacion.Provincia,
                    Canton = ubicacion.Canton,
                    Distrito = ubicacion.Distrito.ToUpper(),
                    TipoPropiedad = tbTipoPropiedad == null ? "No indicado" : tbTipoPropiedad.Descripcion,
                    UsoPropiedad = tbUsoPropiedad == null ? "No indicado" : tbUsoPropiedad.Descripcion,
                    Intencion = datosPropiedad.Intencion,
                    LinkVideo = string.IsNullOrEmpty(datosPropiedad.LinkVideo) ? string.Empty : datosPropiedad.LinkVideo,
                    Comentario = string.IsNullOrEmpty(datosPropiedad.Descripcion) ? string.Empty : datosPropiedad.Descripcion,
                    BarrioPoblado = string.IsNullOrEmpty(datosPropiedad.BarrioPoblado) ? string.Empty : datosPropiedad.BarrioPoblado
                };

                return objetoPropiedad;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<EditarPropiedadViewModel> ObtenerDatosEditarPropiedad(int idPropiedad)
        {
            try
            {
                TbPropiedade datosPropiedad = await context.TbPropiedades.FindAsync(idPropiedad);

                if (datosPropiedad == null)
                {
                    return null;
                }

                TbEstadosPozo estadosPozo = null;
                TiposPozo tipoPozo = null;
                EstadosPozo estadoPozo = null;

                if (datosPropiedad.IdEstadosPozo != null)
                {
                    estadosPozo = await context.TbEstadosPozos.FindAsync(datosPropiedad.IdEstadosPozo);

                    if (estadosPozo != null)
                    {
                        tipoPozo = ObtenerTipoPozo().FirstOrDefault(x => x.Descripcion.Equals(estadosPozo.Pozo));
                        estadoPozo = ObtenerEstadosPozo().FirstOrDefault(x => x.Descripcion.StartsWith(estadosPozo.EstadoLegal));
                    }
                }

                TbAcceso acceso = await context.TbAccesos.FindAsync(datosPropiedad.IdAcceso);
                TbMedidaPropiedad medida = await context.TbMedidaPropiedads.FindAsync(datosPropiedad.IdMedidaPro);
                TbUsoSuelo usoSuelo = await context.TbUsoSuelos.FindAsync(datosPropiedad.IdUsoSuelo);
                Ubicacion ubicacion = await context.Ubicacions.FindAsync(datosPropiedad.IdUbicacion);

                TbUsoTipopropiedade usoTipo = await context.TbUsoTipopropiedades.FindAsync(datosPropiedad.IdUsoTipo);
                TbUsoPropiedad tbUsoPropiedad = null;
                TbTipoPropiedade tbTipoPropiedad = null;

                if (usoTipo != null)
                {
                    tbUsoPropiedad = await context.TbUsoPropiedads.FirstOrDefaultAsync(x => x.IdUsoPro == usoTipo.IdUsoPro);
                    tbTipoPropiedad = await context.TbTipoPropiedades.FirstOrDefaultAsync(x => x.IdTipoPro == usoTipo.IdTipoPro);
                }

                TbTipoMedida tbTipoMedida = null;

                if (medida != null)
                {
                    tbTipoMedida = await context.TbTipoMedidas.FirstOrDefaultAsync(x => x.IdTipoMedida == medida.IdTipoMedida);
                }

                TiposAcceso tiposAcceso = ObtenerTiposAcceso().FirstOrDefault(x => x.Descripcion.Equals(acceso.TipoAcceso));
                EstadosCalificacion estadoAcceso = EstadosDeCalificacion().FirstOrDefault(x => x.Descripcion.Equals(acceso.Estado));
                Topografia topografiaObtenida = ObtenerTopografia().FirstOrDefault(x => x.Descripcion.Equals(datosPropiedad.Topografia));
                NivelCalle nivelCalleObtenido = ObtenerNivelesCalle().FirstOrDefault(x => x.Descripcion.Equals(datosPropiedad.NivelCalle));

                Tuple<int, int, int> IDsUbicacion = await ObtenerIDsUbicacion(ubicacion);

                string tipoVista = string.Empty;

                if (datosPropiedad.PoseeVistaMar.Equals("S"))
                {
                    tipoVista = "Vista al Mar";
                }
                else if (datosPropiedad.PoseeVistaMontania.Equals("S"))
                {
                    tipoVista = "Vista a la montaña";
                }
                else if (datosPropiedad.PoseeVistaValle.Equals("S"))
                {
                    tipoVista = "Vista al Valle";
                }
                else if (datosPropiedad.SinVista.Equals("S"))
                {
                    tipoVista = "Sin vista";
                }

                EditarPropiedadViewModel objetoPropiedad = new EditarPropiedadViewModel()
                {

                    IdPropiedad = datosPropiedad.IdPropiedad,
                    UsoPropiedadId = tbUsoPropiedad == null ? 0 : tbUsoPropiedad.IdUsoPro,
                    TipoPropiedadId = tbTipoPropiedad == null ? 0 : tbTipoPropiedad.IdTipoPro,
                    IngresoPropiedadId = Convert.ToInt32(tiposAcceso.IdTipoAcceso),
                    EstadoAccesoPropiedadId = Convert.ToInt32(estadoAcceso.IdEstadoCalificacion),
                    ProvinciaId = IDsUbicacion.Item1,
                    CantonId = IDsUbicacion.Item2,
                    DistritoId = IDsUbicacion.Item3,
                    DireccionExacta = datosPropiedad.Direccion,
                    MedidasPropiedadId = medida == null ? 0 : tbTipoMedida.IdTipoMedida,
                    TotalMedida = medida.Medida == 0 ? 0.0M : medida.Medida,
                    DisponePozo = estadosPozo != null,
                    TipoPozosPropiedadId = tipoPozo == null ? 0 : tipoPozo.IdTipoPozo,
                    EstatusPozoPropiedadId = estadoPozo == null ? 0 : estadoPozo.IdEstadoPozo,
                    DisponeAgua = datosPropiedad.DisAgua.Equals("S"),
                    UsoSueloId = usoSuelo == null ? 0 : usoSuelo.IdUsoSuelo,
                    PrecioMaximo = datosPropiedad.PrecioMax == 0 ? 0.0M : datosPropiedad.PrecioMax,
                    PrecioMinimo = datosPropiedad.PrecioMin == 0 ? 0.0M : datosPropiedad.PrecioMin,
                    TopografiaId = topografiaObtenida == null ? 0 : topografiaObtenida.IdTopografia,
                    NivelCalleId = nivelCalleObtenido == null ? 0 : nivelCalleObtenido.IdNivelCalle,
                    NumeroFinca = datosPropiedad.NumFinca ?? "No indicado",
                    NumeroPlano = datosPropiedad.NumPlano ?? "No indicado",
                    Intencion = datosPropiedad.Intencion,
                    CuotaMantenimiento = datosPropiedad.CuotaMante == 0 ? 0 : datosPropiedad.CuotaMante,
                    Provincia = ubicacion.Provincia,
                    Canton = ubicacion.Canton,
                    DescripcionPropiedad = string.IsNullOrEmpty(datosPropiedad.Descripcion) ? string.Empty : datosPropiedad.Descripcion,
                    LinkVideo = string.IsNullOrEmpty(datosPropiedad.LinkVideo) ? string.Empty : datosPropiedad.LinkVideo,
                    Moneda = string.IsNullOrEmpty(datosPropiedad.Moneda) ? string.Empty : datosPropiedad.Moneda,
                    BarrioPoblado = string.IsNullOrEmpty(datosPropiedad.BarrioPoblado) ? string.Empty : datosPropiedad.BarrioPoblado,
                    TipoVista = tipoVista
                };

                return objetoPropiedad;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Response> ObtenerIntencionPropiedad(int idPropiedad)
        {
            try
            {
                TbPropiedade propiedad = await context.TbPropiedades.FindAsync(idPropiedad);

                if (propiedad != null)
                {
                    return new Response { EsCorrecto = true, Mensaje = "OK", Resultado = propiedad.Intencion };
                }
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }

            return new Response { EsCorrecto = false };
        }

        public async Task<Response> ObtenerVistaElegida(int idPropiedad)
        {
            try
            {
                TbPropiedade propiedad = await context.TbPropiedades.FindAsync(idPropiedad);

                string valor = string.Empty;

                if (propiedad != null)
                {
                    if (propiedad.PoseeVistaValle.Equals("S"))
                    {
                        valor = "Vista al Valle";
                    }
                    else if (propiedad.PoseeVistaMar.Equals("S"))
                    {
                        valor = "Vista al Mar";
                    }
                    else if (propiedad.PoseeVistaMontania.Equals("S"))
                    {
                        valor = "Vista a la montaña";
                    }
                    else if (propiedad.SinVista.Equals("S"))
                    {
                        valor = "Sin vista";
                    }

                    return new Response { EsCorrecto = true, Mensaje = "OK", Resultado = valor };
                }
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }

            return new Response { EsCorrecto = false };
        }

        public async Task<Response> ValidarPoseePozoPropiedad(int idPropiedad)
        {
            try
            {
                TbPropiedade propiedad = await context.TbPropiedades.FindAsync(idPropiedad);

                if (propiedad != null)
                {
                    int? poseePozo = propiedad.IdEstadosPozo;

                    if (poseePozo == null)
                    {
                        return new Response { EsCorrecto = true, Mensaje = "No", Resultado = null };
                    }
                    else
                    {
                        return new Response { EsCorrecto = true, Mensaje = "Si", Resultado = poseePozo.Value };
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }

            return new Response { EsCorrecto = false };
        }

        public async Task<TbUsoTipopropiedade> EditarUso_TipoPropiedad(EditarPropiedadViewModel propiedad, string codigoIdentificador)
        {
            try
            {

                TbUsoTipopropiedade actualizaRegistro = await context.TbUsoTipopropiedades.FirstOrDefaultAsync(
                    x => x.CodigoIdentificador.Equals(codigoIdentificador));

                actualizaRegistro.IdUsoPro = propiedad.UsoPropiedadId;
                actualizaRegistro.IdTipoPro = propiedad.TipoPropiedadId;

                context.TbUsoTipopropiedades.Update(actualizaRegistro);
                await context.SaveChangesAsync();

                TbUsoTipopropiedade objetoObtenido = context.TbUsoTipopropiedades.FirstOrDefault(x => x.CodigoIdentificador.Equals(codigoIdentificador));

                return objetoObtenido;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<TbAcceso> EditarAccesoPropiedad(EditarPropiedadViewModel propiedad, string codigoIdentificador)
        {
            try
            {

                TbAcceso actualizaRegistro = await context.TbAccesos.FirstOrDefaultAsync(
                    x => x.CodigoIdentificador.Equals(codigoIdentificador));

                actualizaRegistro.TipoAcceso = propiedad.TipoAcceso;
                actualizaRegistro.Estado = propiedad.EstadoAcceso;

                context.TbAccesos.Update(actualizaRegistro);
                await context.SaveChangesAsync();

                TbAcceso objetoObtenido = context.TbAccesos.FirstOrDefault(
                    x => x.CodigoIdentificador.Equals(codigoIdentificador));

                return objetoObtenido;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<TbMedidaPropiedad> EditarMedidaPropiedad(EditarPropiedadViewModel propiedad, string codigoIdentificador)
        {
            try
            {

                TbMedidaPropiedad actualizaRegistro = await context.TbMedidaPropiedads.FirstOrDefaultAsync(
                    x => x.CodigoIdentificador.Equals(codigoIdentificador));

                actualizaRegistro.IdTipoMedida = propiedad.MedidasPropiedadId;
                actualizaRegistro.Medida = propiedad.TotalMedida;

                context.TbMedidaPropiedads.Update(actualizaRegistro);
                await context.SaveChangesAsync();

                TbMedidaPropiedad objetoObtenido = context.TbMedidaPropiedads.FirstOrDefault(
                    x => x.CodigoIdentificador.Equals(codigoIdentificador));

                return objetoObtenido;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<TbEstadosPozo> EditarEstatusPozoPropiedad(EditarPropiedadViewModel propiedad, string codigoIdentificador)
        {
            try
            {
                TiposPozo tipoPozo = ObtenerTipoPozo().FirstOrDefault(tp => tp.Descripcion.Equals(propiedad.Pozo));
                EstadosPozo estatusPozo = ObtenerEstadosPozo().FirstOrDefault(ep => ep.Descripcion.Equals(propiedad.EstatusPozo));

                if (tipoPozo == null && estatusPozo == null)
                {
                    return null;
                }

                TbEstadosPozo actualizaRegistro = await context.TbEstadosPozos.FirstOrDefaultAsync(
                    x => x.CodigoIdentificador.Equals(codigoIdentificador));

                if (actualizaRegistro == null)
                {
                    TbEstadosPozo tbEstadosPozo = new TbEstadosPozo()
                    {
                        CodigoIdentificador = codigoIdentificador,
                        EstadoLegal = estatusPozo.Descripcion.Substring(0, 1),
                        Pozo = tipoPozo.Descripcion
                    };

                    await context.TbEstadosPozos.AddAsync(tbEstadosPozo);
                    await context.SaveChangesAsync();
                }
                else
                {
                    actualizaRegistro.Pozo = propiedad.Pozo;
                    actualizaRegistro.EstadoLegal = propiedad.EstatusPozo.Substring(0, 1);

                    context.TbEstadosPozos.Update(actualizaRegistro);
                    await context.SaveChangesAsync();
                }

                TbEstadosPozo objetoObtenido = context.TbEstadosPozos.FirstOrDefault(
                    x => x.CodigoIdentificador.Equals(codigoIdentificador));

                return objetoObtenido;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Response> EditarDatosInicialesPropiedad(EditarPropiedadViewModel propiedad, string usuario)
        {
            Response response = new Response();

            try
            {
                string mensaje = string.Empty;

                Microsoft.EntityFrameworkCore.Storage.IExecutionStrategy strategy = context.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = context.Database.BeginTransaction();

                    try
                    {
                        int? idSinPozo = null;
                        int? idUsoSuelo = null;

                        TbPropiedade propiedadEditar = await context.TbPropiedades.FindAsync(propiedad.IdPropiedad);
                        string codigoIdentificadorPropiedad = propiedadEditar.CodigoIdentificador;

                        Ubicacion ubicacion = await context.Ubicacions.FindAsync(propiedad.DistritoId);
                        TbUsoTipopropiedade usoTipoPropiedadObtenido = await EditarUso_TipoPropiedad(propiedad, codigoIdentificadorPropiedad);
                        TbAcceso accesoPropiedadObtenido = await EditarAccesoPropiedad(propiedad, codigoIdentificadorPropiedad);
                        TbMedidaPropiedad medidaPropiedadObtenido = await EditarMedidaPropiedad(propiedad, codigoIdentificadorPropiedad);
                        TbEstadosPozo pozoPropiedadObtenido = await EditarEstatusPozoPropiedad(propiedad, codigoIdentificadorPropiedad);

                        string barrioPoblado = string.Empty;
                        if (!string.IsNullOrEmpty(propiedad.BarrioPoblado))
                        {
                            barrioPoblado = $"{propiedad.BarrioPoblado.Trim()}, ";
                        }

                        TbTipoPropiedade tipoPropiedad = context.TbTipoPropiedades.Find(propiedad.TipoPropiedadId);
                        propiedadEditar.Moneda = string.IsNullOrEmpty(propiedad.Moneda) ? string.Empty : propiedad.Moneda;
                        propiedadEditar.LinkVideo = string.IsNullOrEmpty(propiedad.LinkVideo) ? string.Empty : propiedad.LinkVideo;
                        propiedadEditar.Descripcion = string.IsNullOrEmpty(propiedad.DescripcionPropiedad) ? string.Empty : propiedad.DescripcionPropiedad;
                        propiedadEditar.IdUsoTipo = usoTipoPropiedadObtenido.IdUsoTipo;
                        propiedadEditar.IdUbicacion = propiedad.DistritoId;
                        propiedadEditar.Direccion = propiedad.DireccionExacta;
                        propiedadEditar.IdMedidaPro = medidaPropiedadObtenido.IdMedidaPro;
                        propiedadEditar.IdAcceso = accesoPropiedadObtenido.IdAcceso;
                        propiedadEditar.IdEstadosPozo = pozoPropiedadObtenido == null ? idSinPozo : pozoPropiedadObtenido.IdEstadosPozo;
                        propiedadEditar.IdUsoSuelo = propiedad.UsoSueloId == 0 ? idUsoSuelo : propiedad.UsoSueloId;
                        propiedadEditar.CodigoTipoUsoPropiedad = $"{tipoPropiedad.Descripcion.Substring(0, 2)}-{usoTipoPropiedadObtenido.IdUsoTipo}";
                        propiedadEditar.Intencion = propiedad.Intencion;
                        propiedadEditar.PrecioMax = propiedad.PrecioMaximo;
                        propiedadEditar.PrecioMin = propiedad.PrecioMinimo;
                        propiedadEditar.DisAgua = propiedad.DisponeAgua ? "S" : "N";
                        propiedadEditar.CuotaMante = propiedad.CuotaMantenimiento;
                        propiedadEditar.NumFinca = propiedad.NumeroFinca;
                        propiedadEditar.NumPlano = propiedad.NumeroPlano;
                        propiedadEditar.NivelCalle = !ValidarInformacionNiveCalle(propiedad.NivelCalleSeleccionada) ? string.Empty : propiedad.NivelCalleSeleccionada;
                        propiedadEditar.Topografia = !ValidarInformacionTopografia(propiedad.TopografiaSeleccionada) ? string.Empty : propiedad.TopografiaSeleccionada;
                        propiedadEditar.BarrioPoblado = string.IsNullOrEmpty(barrioPoblado) ? string.Empty : barrioPoblado.Replace(",", string.Empty);
                        propiedadEditar.PoseeVistaMar = propiedad.TipoVista.Equals("Vista al Mar") ? "S" : "N";
                        propiedadEditar.PoseeVistaMontania = propiedad.TipoVista.Equals("Vista a la montaña") ? "S" : "N";
                        propiedadEditar.PoseeVistaValle = propiedad.TipoVista.Equals("Vista al Valle") ? "S" : "N";
                        propiedadEditar.SinVista = propiedad.TipoVista.Equals("Sin vista") ? "S" : "N";
                        propiedadEditar.DireccionCompleta = $"{barrioPoblado}{ubicacion.Distrito.ToUpper()}, {ubicacion.Canton}, {ubicacion.Provincia}";

                        context.TbPropiedades.Update(propiedadEditar);
                        await context.SaveChangesAsync();

                        /*Registro de edicion en la bitacora de la propiedad*/
                        await bitacora.EditarPropiedad(usuario, codigoIdentificadorPropiedad);

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
                return response = new Response { EsCorrecto = false, Mensaje = ex.Message };
            }

            return response;
        }

        public async Task<Response> AgregarImagenPropiedad(string rutaImagen, int idPropiedad)
        {
            try
            {
                TbRutaImgprop nuevaImagen = new TbRutaImgprop
                {
                    FechaIns = DateTime.Now.ToUniversalTime(),
                    IdPropiedad = idPropiedad,
                    Ruta = rutaImagen
                };

                await context.TbRutaImgprops.AddAsync(nuevaImagen);
                await context.SaveChangesAsync();
                return new Response { EsCorrecto = true, Mensaje = "OK" };
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }
        }

        public async Task<Response> ObtenerImagenesPropiedad(int idPropiedad)
        {
            try
            {
                List<TbRutaImgprop> listaImagenes = await context.TbRutaImgprops.Where(x => x.IdPropiedad == idPropiedad).ToListAsync();
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

        public async Task<Response> CambiarEstadoPropiedad(MostrarPropiedadTabla propiedad)
        {
            try
            {
                TbPropiedade obtenerPropiedad = await context.TbPropiedades.FindAsync(propiedad.Id);

                if (obtenerPropiedad != null)
                {
                    obtenerPropiedad.Estado = propiedad.Estado.Substring(0, 1);

                    context.TbPropiedades.Update(obtenerPropiedad);
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

        public async Task<Response> CambiarPublicadoPropiedad(MostrarPropiedadTabla propiedad)
        {
            try
            {
                TbPropiedade obtenerPropiedad = await context.TbPropiedades.FindAsync(propiedad.Id);

                if (obtenerPropiedad != null)
                {
                    obtenerPropiedad.Publicado = propiedad.Publicado.Substring(0, 1);

                    context.TbPropiedades.Update(obtenerPropiedad);
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

        public async Task<Response> EliminarImagenGaleria(Imagen imagen)
        {
            try
            {
                if (imagen != null)
                {
                    TbRutaImgprop imagenEliminar = await context.TbRutaImgprops.FindAsync(imagen.IdImagen);
                    context.TbRutaImgprops.Remove(imagenEliminar);
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
                TbRutaImgprop imagenObtenida = await context.TbRutaImgprops.FindAsync(idImagen);

                if (imagenObtenida != null)
                {
                    Imagen imagenEnvia = new Imagen { Ruta = imagenObtenida.Ruta, Titulo = Guid.NewGuid().ToString(), IdImagen = imagenObtenida.IdRuta };
                    return new Response { EsCorrecto = true, Mensaje = "OK", Resultado = imagenEnvia };
                }

                return new Response { EsCorrecto = false, Mensaje = "No existe" };
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }

        }

        public async Task<Response> AgregarDatosAccesoPropiedad(AccesoPropiedad acceso)
        {
            try
            {
                TbRecorrido nuevoAcceso = new TbRecorrido()
                {
                    IdPropiedad = acceso.IdPropiedad,
                    IdAccesibilidad = acceso.IdTipoAccesibilidad,
                    Descripcion = string.IsNullOrEmpty(acceso.Descripcion) ? string.Empty : acceso.Descripcion.Trim(),
                    RecorridoKm = acceso.Recorrido
                };

                await context.TbRecorridos.AddAsync(nuevoAcceso);
                await context.SaveChangesAsync();
                return new Response { EsCorrecto = true, Mensaje = "OK" };
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = ex.Message };
            }
        }

        public async Task<Response> EliminarRecorridoPropiedad(int idRecorrido)
        {
            try
            {
                TbRecorrido recorrido = await context.TbRecorridos.FindAsync(idRecorrido);

                if (recorrido != null)
                {
                    context.TbRecorridos.Remove(recorrido);
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

        public async Task<Response> ObtenerRecorridoPropiedad(int idRecorrido)
        {
            try
            {
                TbRecorrido existe = await context.TbRecorridos.FindAsync(idRecorrido);

                if (existe == null)
                {
                    return new Response { EsCorrecto = false, Mensaje = "No existe" };
                }

                List<AccesoPropiedad> consulta = (from recorrido in context.TbRecorridos
                                                  join tipoAccesibilidad in context.TbAccesibilidads
                                                  on recorrido.IdAccesibilidad equals tipoAccesibilidad.IdAccesibilidad

                                                  where recorrido.IdRecorrido == idRecorrido

                                                  select new AccesoPropiedad
                                                  {
                                                      IdRecorrido = recorrido.IdRecorrido,
                                                      IdPropiedad = recorrido.IdPropiedad,
                                                      IdTipoAccesibilidad = recorrido.IdAccesibilidad,
                                                      Descripcion = recorrido.Descripcion,
                                                      Recorrido = recorrido.RecorridoKm,
                                                      DescripcionAccesibilidad = tipoAccesibilidad.Descripcion
                                                  }).ToList();

                return new Response { EsCorrecto = true, Resultado = consulta[0] };
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = true, Resultado = ex.Message };
            }
        }

        public async Task<Response> AgregarDatosEditadosRecorridoPropiedad(AccesoPropiedad acceso)
        {
            try
            {
                TbRecorrido recorridoObtenido = await context.TbRecorridos.FindAsync(acceso.IdRecorrido);

                if (recorridoObtenido != null)
                {
                    recorridoObtenido.IdAccesibilidad = acceso.IdTipoAccesibilidad;
                    recorridoObtenido.RecorridoKm = acceso.Recorrido;
                    recorridoObtenido.Descripcion = acceso.Descripcion;
                    context.TbRecorridos.Update(recorridoObtenido);
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

        public async Task<List<VerPropiedadViewModel>> ObtenerPropiedadPorFiltrado(BuscarPropiedad propiedad)
        {
            SqlConnection connection = (SqlConnection)context.Database.GetDbConnection(); /*para obtener la conexion con la BD*/

            try
            {
                List<TbPropiedade> datosPropiedad = new List<TbPropiedade>();

                try
                {
                    string procedimiento = propiedad.Proc switch
                    {
                        "Proc1" => "SP_ObtenerPropiedadTodosLosTipos_TodasLasProvincias",
                        "Proc2" => "SP_ObtenerPropiedadTodasLasProvincias_Precio_Tipo",
                        "Proc3" => "SP_ObtenerPropiedadTodasLosTipos_Precio_Provincia",
                        "Proc4" => "SP_ObtenerPropiedadPorProvincia_Precio_Tipo",
                        _ => "Sin definir",
                    };

                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    command.CommandText = procedimiento;

                    if (propiedad.Moneda.Equals("₡ Colones"))
                    {
                        propiedad.Moneda = "Colones";
                    }
                    else
                    {
                        propiedad.Moneda = "Dolares";
                    }

                    if (propiedad.Proc.Equals("Proc1"))
                    {
                        command.Parameters.AddWithValue("@precioMinimo", propiedad.PrecioMinimo);
                        command.Parameters.AddWithValue("@precioMaximo", propiedad.PrecioMaximo);
                        //command.Parameters.AddWithValue("@moneda", propiedad.Moneda);
                        //command.Parameters.AddWithValue("@vistaMontania", propiedad.Vista.Equals("Vista a la montaña") ? "S" : "N");
                        //command.Parameters.AddWithValue("@vistaValle", propiedad.Vista.Equals("Vista al valle") ? "S" : "N");
                        //command.Parameters.AddWithValue("@vistaMar", propiedad.Vista.Equals("Vista a la playa") ? "S" : "N");
                        //command.Parameters.AddWithValue("@sinVista", propiedad.Vista.Equals("Sin vista") ? "S" : "N");
                    }
                    else if (propiedad.Proc.Equals("Proc2"))
                    {
                        command.Parameters.AddWithValue("@tipo", propiedad.Tipo);
                        command.Parameters.AddWithValue("@precioMinimo", propiedad.PrecioMinimo);
                        command.Parameters.AddWithValue("@precioMaximo", propiedad.PrecioMaximo);
                        //command.Parameters.AddWithValue("@moneda", propiedad.Moneda);
                        //command.Parameters.AddWithValue("@vistaMontania", propiedad.Vista.Equals("Vista a la montaña") ? "S" : "N");
                        //command.Parameters.AddWithValue("@vistaValle", propiedad.Vista.Equals("Vista al valle") ? "S" : "N");
                        //command.Parameters.AddWithValue("@vistaMar", propiedad.Vista.Equals("Vista al mar") ? "S" : "N");
                        //command.Parameters.AddWithValue("@sinVista", propiedad.Vista.Equals("Sin vista") ? "S" : "N");
                    }
                    else if (propiedad.Proc.Equals("Proc3"))
                    {
                        command.Parameters.AddWithValue("@provincia", propiedad.Provincia);
                        command.Parameters.AddWithValue("@precioMinimo", propiedad.PrecioMinimo);
                        command.Parameters.AddWithValue("@precioMaximo", propiedad.PrecioMaximo);
                        //command.Parameters.AddWithValue("@moneda", propiedad.Moneda);
                        //command.Parameters.AddWithValue("@vistaMontania", propiedad.Vista.Equals("Vista a la montaña") ? "S" : "N");
                        //command.Parameters.AddWithValue("@vistaValle", propiedad.Vista.Equals("Vista al valle") ? "S" : "N");
                        //command.Parameters.AddWithValue("@vistaMar", propiedad.Vista.Equals("Vista al mar") ? "S" : "N");
                        //command.Parameters.AddWithValue("@sinVista", propiedad.Vista.Equals("Sin vista") ? "S" : "N");
                    }
                    else if (propiedad.Proc.Equals("Proc4"))
                    {
                        command.Parameters.AddWithValue("@provincia", propiedad.Provincia);
                        command.Parameters.AddWithValue("@tipo", propiedad.Tipo);
                        command.Parameters.AddWithValue("@precioMinimo", propiedad.PrecioMinimo);
                        command.Parameters.AddWithValue("@precioMaximo", propiedad.PrecioMaximo);
                        //command.Parameters.AddWithValue("@moneda", propiedad.Moneda);
                        //command.Parameters.AddWithValue("@vistaMontania", propiedad.Vista.Equals("Vista a la montaña") ? "S" : "N");
                        //command.Parameters.AddWithValue("@vistaValle", propiedad.Vista.Equals("Vista al valle") ? "S" : "N");
                        //command.Parameters.AddWithValue("@vistaMar", propiedad.Vista.Equals("Vista al mar") ? "S" : "N");
                        //command.Parameters.AddWithValue("@sinVista", propiedad.Vista.Equals("Sin vista") ? "S" : "N");
                    }

                    using SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        datosPropiedad.Add(new TbPropiedade
                        {
                            IdPropiedad = Convert.ToInt32(dr["idPropiedad"]),
                            IdUsoTipo = Convert.ToInt32(dr["idUsoTipo"]),
                            IdUbicacion = Convert.ToInt32(dr["idUbicacion"]),
                            Direccion = Convert.ToString(dr["direccion"]),
                            IdUsoSuelo = dr["idUsoSuelo"] is DBNull ? default(int?) : (int)dr["idUsoSuelo"],
                            IdMedidaPro = dr["idMedidaPro"] is DBNull ? default(int?) : (int)dr["idMedidaPro"],
                            PrecioMax = dr["precioMax"] is DBNull ? default(decimal?) : (decimal)dr["precioMax"],
                            PrecioMin = dr["precioMin"] is DBNull ? default(decimal?) : (decimal)dr["precioMin"],
                            Topografia = Convert.ToString(dr["topografia"]),
                            NivelCalle = Convert.ToString(dr["nivelCalle"]),
                            IdAcceso = dr["idAcceso"] is DBNull ? default(int?) : (int)dr["idAcceso"],
                            CuotaMante = dr["cuotaMante"] is DBNull ? default(decimal?) : (decimal)dr["cuotaMante"],
                            DisAgua = Convert.ToString(dr["disAgua"]),
                            IdEstadosPozo = dr["idEstadosPozo"] is DBNull ? default(int?) : (int)dr["idEstadosPozo"],
                            NumFinca = Convert.ToString(dr["numFinca"]),
                            NumPlano = Convert.ToString(dr["numPlano"]),
                            Publicado = Convert.ToString(dr["publicado"]),
                            Megusta = Convert.ToInt32(dr["megusta"]),
                            Intencion = Convert.ToString(dr["intencion"]),
                            Descripcion = Convert.ToString(dr["descripcion"]),
                            LinkVideo = Convert.ToString(dr["linkVideo"]),
                            Moneda = Convert.ToString(dr["Moneda"]),
                            BarrioPoblado = Convert.ToString(dr["BarrioPoblado"]),
                            PoseeVistaMar = Convert.ToString(dr["PoseeVistaMar"]),
                            PoseeVistaMontania = Convert.ToString(dr["PoseeVistaMontania"]),
                            PoseeVistaValle = Convert.ToString(dr["PoseeVistaValle"]),
                            SinVista = Convert.ToString(dr["SinVista"]),
                            /*PARA QUE OBTENGA LOS DATOS SOLICITADOS SE DEBE AGREGAR EN ESTE ESPACIO PARA QUE EJECUTEN EL SP*/
                        }); ;
                    }

                }
                catch (Exception)
                {
                    return null;
                }

                List<VerPropiedadViewModel> listPropiedad = new List<VerPropiedadViewModel>();

                for (int i = 0; i < datosPropiedad.Count; i++)
                {
                    TbEstadosPozo estadosPozo = null;
                    TiposPozo tipoPozo = null;
                    EstadosPozo estadoPozo = null;

                    if (datosPropiedad[i].IdEstadosPozo != null)
                    {
                        estadosPozo = await context.TbEstadosPozos.FindAsync(datosPropiedad[i].IdEstadosPozo);

                        if (estadosPozo != null)
                        {
                            tipoPozo = ObtenerTipoPozo().FirstOrDefault(x => x.Descripcion.Equals(estadosPozo.Pozo));
                            estadoPozo = ObtenerEstadosPozo().FirstOrDefault(x => x.Descripcion.StartsWith(estadosPozo.EstadoLegal));
                        }
                    }

                    TbAcceso acceso = await context.TbAccesos.FindAsync(datosPropiedad[i].IdAcceso);
                    TbMedidaPropiedad medida = await context.TbMedidaPropiedads.FindAsync(datosPropiedad[i].IdMedidaPro);
                    TbUsoSuelo usoSuelo = await context.TbUsoSuelos.FindAsync(datosPropiedad[i].IdUsoSuelo);
                    Ubicacion ubicacion = await context.Ubicacions.FindAsync(datosPropiedad[i].IdUbicacion);

                    TbUsoTipopropiedade usoTipo = await context.TbUsoTipopropiedades.FindAsync(datosPropiedad[i].IdUsoTipo);
                    TbUsoPropiedad tbUsoPropiedad = null;
                    TbTipoPropiedade tbTipoPropiedad = null;

                    if (usoTipo != null)
                    {
                        tbUsoPropiedad = await context.TbUsoPropiedads.FirstOrDefaultAsync(x => x.IdUsoPro == usoTipo.IdUsoPro);
                        tbTipoPropiedad = await context.TbTipoPropiedades.FirstOrDefaultAsync(x => x.IdTipoPro == usoTipo.IdTipoPro);
                    }

                    TbTipoMedida tbTipoMedida = null;

                    if (medida != null)
                    {
                        tbTipoMedida = await context.TbTipoMedidas.FirstOrDefaultAsync(x => x.IdTipoMedida == medida.IdTipoMedida);
                    }

                    TiposAcceso tiposAcceso = ObtenerTiposAcceso().FirstOrDefault(x => x.Descripcion.Equals(acceso.TipoAcceso));
                    EstadosCalificacion estadoAcceso = EstadosDeCalificacion().FirstOrDefault(x => x.Descripcion.Equals(acceso.Estado));
                    Topografia topografiaObtenida = ObtenerTopografia().FirstOrDefault(x => x.Descripcion.Equals(datosPropiedad[i].Topografia));
                    NivelCalle nivelCalleObtenido = ObtenerNivelesCalle().FirstOrDefault(x => x.Descripcion.Equals(datosPropiedad[i].NivelCalle));

                    Tuple<int, int, int> IDsUbicacion = await ObtenerIDsUbicacion(ubicacion);

                    VerPropiedadViewModel objetoPropiedad = new VerPropiedadViewModel()
                    {
                        IdPropiedad = datosPropiedad[i].IdPropiedad,
                        TotalMeGusta = datosPropiedad[i].Megusta,
                        Publicado = datosPropiedad[i].Publicado.Equals("S"),
                        NumeroFinca = datosPropiedad[i].NumFinca ?? "No Indicado",
                        NumeroPlano = datosPropiedad[i].NumPlano ?? "No indicado",
                        CuotaMantenimiento = datosPropiedad[i].CuotaMante == null ? 0.0M : datosPropiedad[i].CuotaMante.Value,
                        DisponeAgua = datosPropiedad[i].DisAgua,
                        NivelCalleSeleccionada = datosPropiedad[i].NivelCalle,
                        TopografiaSeleccionada = datosPropiedad[i].Topografia,
                        PrecioMaximo = datosPropiedad[i].PrecioMax == null ? 0.0M : datosPropiedad[i].PrecioMax.Value,
                        PrecioMinimo = datosPropiedad[i].PrecioMin == null ? 0.0M : datosPropiedad[i].PrecioMin.Value,
                        Pozo = estadosPozo == null ? "No indicado" : estadosPozo.Pozo,
                        EstatusPozo = estadosPozo == null ? "No indicado" : estadosPozo.EstadoLegal,
                        TipoAcceso = acceso == null ? "No indicado" : acceso.TipoAcceso,
                        EstadoAcceso = acceso == null ? "No indicado" : acceso.Estado,
                        TotalMedida = medida == null ? 0.0M : medida.Medida,
                        Siglas = tbTipoMedida == null ? "N/A" : tbTipoMedida.Siglas,
                        DescripcionMedida = tbTipoMedida == null ? string.Empty : tbTipoMedida.Descripcion,
                        UsoSuelo = usoSuelo == null ? "No indicado" : usoSuelo.Descripcion,
                        DireccionExacta = datosPropiedad[i].Direccion,
                        Provincia = ubicacion.Provincia,
                        Canton = ubicacion.Canton,
                        Distrito = ubicacion.Distrito,
                        TipoPropiedad = tbTipoPropiedad == null ? "No indicado" : tbTipoPropiedad.Descripcion,
                        UsoPropiedad = tbUsoPropiedad == null ? "No indicado" : tbUsoPropiedad.Descripcion,
                        Intencion = datosPropiedad[i].Intencion,
                        DescripcionPropiedad = datosPropiedad[i].Descripcion,
                        LinkVideo = datosPropiedad[i].LinkVideo,
                        Moneda = datosPropiedad[i].Moneda,
                        BarrioPoblado = datosPropiedad[i].BarrioPoblado,

                        /*PARA QUE OBTENGA LOS DATOS SOLICITADOS SE DEBE AGREGAR EN ESTE ESPACIO PARA QUE EJECUTEN EL SP*/
                    };

                    List<TbRutaImgprop> imagenesPropiedad = await context.TbRutaImgprops.Where(x => x.IdPropiedad == datosPropiedad[i].IdPropiedad).ToListAsync();

                    List<Imagen> imagenes = imagenesPropiedad.Select(x => new Imagen()
                    {
                        Ruta = x.Ruta,
                        Titulo = Guid.NewGuid().ToString()
                    }).ToList();

                    objetoPropiedad.ImagenesPropiedad = imagenes;
                    listPropiedad.Add(objetoPropiedad);
                }
                return listPropiedad;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

        }

        public async Task<List<VerPropiedadViewModel>> ObtenerDatosPropiedadesRecientes()
        {

            List<TbPropiedade> datosPropiedad = new List<TbPropiedade>();

            try
            {
                SqlConnection connection = (SqlConnection)context.Database.GetDbConnection(); /*para obtener la conexion con la BD*/

                try
                {

                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    command.CommandText = "SP_ObtenerUltimasPropiedadesAgregadas";

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        datosPropiedad.Add(new TbPropiedade
                        {
                            IdPropiedad = Convert.ToInt32(reader["idPropiedad"]),
                            IdUsoTipo = Convert.ToInt32(reader["idUsoTipo"]),
                            IdUbicacion = Convert.ToInt32(reader["idUbicacion"]),
                            Direccion = Convert.ToString(reader["direccion"]),
                            IdUsoSuelo = reader["idUsoSuelo"] is DBNull ? default(int?) : (int)reader["idUsoSuelo"],
                            IdMedidaPro = reader["idMedidaPro"] is DBNull ? default(int?) : (int)reader["idMedidaPro"],
                            PrecioMax = reader["precioMax"] is DBNull ? default(decimal?) : (decimal)reader["precioMax"],
                            PrecioMin = reader["precioMin"] is DBNull ? default(decimal?) : (decimal)reader["precioMin"],
                            Topografia = Convert.ToString(reader["topografia"]),
                            NivelCalle = Convert.ToString(reader["nivelCalle"]),
                            IdAcceso = reader["idAcceso"] is DBNull ? default(int?) : (int)reader["idAcceso"],
                            CuotaMante = reader["cuotaMante"] is DBNull ? default(decimal?) : (decimal)reader["cuotaMante"],
                            DisAgua = Convert.ToString(reader["disAgua"]),
                            IdEstadosPozo = reader["idEstadosPozo"] is DBNull ? default(int?) : (int)reader["idEstadosPozo"],
                            NumFinca = Convert.ToString(reader["numFinca"]),
                            NumPlano = Convert.ToString(reader["numPlano"]),
                            Publicado = Convert.ToString(reader["publicado"]),
                            Megusta = Convert.ToInt32(reader["megusta"]),
                            Intencion = Convert.ToString(reader["intencion"]),
                            Descripcion = Convert.ToString(reader["descripcion"]),
                            LinkVideo = Convert.ToString(reader["linkVideo"]),
                            Moneda = Convert.ToString(reader["moneda"]),
                            BarrioPoblado = Convert.ToString(reader["barrioPoblado"])
                        });
                    }
                }
                catch (Exception)
                {
                    return null;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }

                List<VerPropiedadViewModel> listPropiedad = new List<VerPropiedadViewModel>();

                for (int i = 0; i < datosPropiedad.Count; i++)
                {
                    TbEstadosPozo estadosPozo = null;
                    TiposPozo tipoPozo = null;
                    EstadosPozo estadoPozo = null;

                    if (datosPropiedad[i].IdEstadosPozo != null)
                    {
                        estadosPozo = await context.TbEstadosPozos.FindAsync(datosPropiedad[i].IdEstadosPozo);

                        if (estadosPozo != null)
                        {
                            tipoPozo = ObtenerTipoPozo().FirstOrDefault(x => x.Descripcion.Equals(estadosPozo.Pozo));
                            estadoPozo = ObtenerEstadosPozo().FirstOrDefault(x => x.Descripcion.StartsWith(estadosPozo.EstadoLegal));
                        }
                    }

                    TbAcceso acceso = await context.TbAccesos.FindAsync(datosPropiedad[i].IdAcceso);
                    TbMedidaPropiedad medida = await context.TbMedidaPropiedads.FindAsync(datosPropiedad[i].IdMedidaPro);
                    TbUsoSuelo usoSuelo = await context.TbUsoSuelos.FindAsync(datosPropiedad[i].IdUsoSuelo);
                    Ubicacion ubicacion = await context.Ubicacions.FindAsync(datosPropiedad[i].IdUbicacion);

                    TbUsoTipopropiedade usoTipo = await context.TbUsoTipopropiedades.FindAsync(datosPropiedad[i].IdUsoTipo);
                    TbUsoPropiedad tbUsoPropiedad = null;
                    TbTipoPropiedade tbTipoPropiedad = null;

                    if (usoTipo != null)
                    {
                        tbUsoPropiedad = await context.TbUsoPropiedads.FirstOrDefaultAsync(x => x.IdUsoPro == usoTipo.IdUsoPro);
                        tbTipoPropiedad = await context.TbTipoPropiedades.FirstOrDefaultAsync(x => x.IdTipoPro == usoTipo.IdTipoPro);
                    }

                    TbTipoMedida tbTipoMedida = null;

                    if (medida != null)
                    {
                        tbTipoMedida = await context.TbTipoMedidas.FirstOrDefaultAsync(x => x.IdTipoMedida == medida.IdTipoMedida);
                    }

                    TiposAcceso tiposAcceso = ObtenerTiposAcceso().FirstOrDefault(x => x.Descripcion.Equals(acceso.TipoAcceso));
                    EstadosCalificacion estadoAcceso = EstadosDeCalificacion().FirstOrDefault(x => x.Descripcion.Equals(acceso.Estado));
                    Topografia topografiaObtenida = ObtenerTopografia().FirstOrDefault(x => x.Descripcion.Equals(datosPropiedad[i].Topografia));
                    NivelCalle nivelCalleObtenido = ObtenerNivelesCalle().FirstOrDefault(x => x.Descripcion.Equals(datosPropiedad[i].NivelCalle));

                    Tuple<int, int, int> IDsUbicacion = await ObtenerIDsUbicacion(ubicacion);

                    VerPropiedadViewModel objetoPropiedad = new VerPropiedadViewModel()
                    {

                        IdPropiedad = datosPropiedad[i].IdPropiedad,
                        TotalMeGusta = datosPropiedad[i].Megusta,
                        Publicado = datosPropiedad[i].Publicado.Equals("S"),
                        NumeroFinca = datosPropiedad[i].NumFinca ?? "No indicado",
                        NumeroPlano = datosPropiedad[i].NumPlano ?? "No indicado",
                        CuotaMantenimiento = datosPropiedad[i].CuotaMante == null ? 0.0M : datosPropiedad[i].CuotaMante.Value,
                        DisponeAgua = datosPropiedad[i].DisAgua,
                        NivelCalleSeleccionada = datosPropiedad[i].NivelCalle,
                        TopografiaSeleccionada = datosPropiedad[i].Topografia,
                        PrecioMaximo = datosPropiedad[i].PrecioMax == null ? 0.0M : datosPropiedad[i].PrecioMax.Value,
                        PrecioMinimo = datosPropiedad[i].PrecioMin == null ? 0.0M : datosPropiedad[i].PrecioMin.Value,
                        Pozo = estadosPozo == null ? "No indicado" : estadosPozo.Pozo,
                        EstatusPozo = estadosPozo == null ? "No indicado" : estadosPozo.EstadoLegal,
                        TipoAcceso = acceso == null ? "No indicado" : acceso.TipoAcceso,
                        EstadoAcceso = acceso == null ? "No indicado" : acceso.Estado,
                        TotalMedida = medida == null ? 0.0M : medida.Medida,
                        Siglas = tbTipoMedida == null ? "N/A" : tbTipoMedida.Siglas,
                        DescripcionMedida = tbTipoMedida == null ? string.Empty : tbTipoMedida.Descripcion,
                        UsoSuelo = usoSuelo == null ? "No indicado" : usoSuelo.Descripcion,
                        DireccionExacta = datosPropiedad[i].Direccion,
                        Provincia = ubicacion.Provincia,
                        Canton = ubicacion.Canton,
                        Distrito = ubicacion.Distrito,
                        TipoPropiedad = tbTipoPropiedad == null ? "No indicado" : tbTipoPropiedad.Descripcion,
                        UsoPropiedad = tbUsoPropiedad == null ? "No indicado" : tbUsoPropiedad.Descripcion,
                        Intencion = datosPropiedad[i].Intencion,
                        Moneda = datosPropiedad[i].Moneda,
                        DescripcionPropiedad = datosPropiedad[i].Descripcion,
                        LinkVideo = datosPropiedad[i].LinkVideo,
                        BarrioPoblado = datosPropiedad[i].BarrioPoblado
                    };

                    List<TbRutaImgprop> imagenesPropiedad = await context.TbRutaImgprops.Where(x => x.IdPropiedad == datosPropiedad[i].IdPropiedad).ToListAsync();

                    List<Imagen> imagenes = imagenesPropiedad.Select(x => new Imagen()
                    {
                        Ruta = x.Ruta,
                        Titulo = Guid.NewGuid().ToString()
                    }).ToList();

                    objetoPropiedad.ImagenesPropiedad = imagenes;
                    listPropiedad.Add(objetoPropiedad);
                }

                return listPropiedad;

            }
            catch (Exception)
            {
                return null;
            }

        }

        public List<Bitacora> ObtenerListaBitacoraPropiedades(Bitacora bitacora)
        {
            SqlConnection connection = (SqlConnection)context.Database.GetDbConnection(); /*para obtener la conexion con la BD*/

            List<Bitacora> list = new List<Bitacora>();

            string fechaInicio = string.Empty;
            string fechaFin = string.Empty;

            if (!string.IsNullOrEmpty(bitacora.FechaInicio))
            {
                string[] array = bitacora.FechaInicio.Split('/');

                string dia = array[0];
                string mes = array[1];
                string anio = array[2];

                //if (Convert.ToInt32(dia) < 10)
                //{
                //    dia = $"0{dia}";
                //}

                //if (Convert.ToInt32(mes) < 10)
                //{
                //    mes = $"0{mes}";
                //}

                fechaInicio = $"{anio}{mes}{dia}";
            }

            if (!string.IsNullOrEmpty(bitacora.FechaFin))
            {
                string[] array = bitacora.FechaFin.Split('/');

                string dia = array[0];
                string mes = array[1];
                string anio = array[2];

                //if (Convert.ToInt32(dia) < 10)
                //{
                //    dia = $"0{dia}";
                //}

                //if (Convert.ToInt32(mes) < 10)
                //{
                //    mes = $"0{mes}";
                //}

                fechaFin = $"{anio}{mes}{dia}";
            }

            try
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                command.CommandText = "SP_ObtenerBitacoraPropiedades";

                command.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                command.Parameters.AddWithValue("@fechaFin", fechaFin);

                using SqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    list.Add(new Bitacora
                    {
                        IdBitacora = Convert.ToInt32(dr["id_bitacora"]),
                        Usuario = Convert.ToString(dr["id_usuario"]),
                        TipoOperacion = Convert.ToString(dr["DescripcionOperacion"]),
                        FechaString = Convert.ToDateTime(dr["fecha"]).ToLocalTime().ToShortDateString(),
                        HoraString = Convert.ToDateTime(dr["fecha"]).ToLocalTime().ToShortTimeString(),
                        FechaConcantenada = $"{Convert.ToDateTime(dr["fecha"]).ToLocalTime().ToShortDateString()} {Convert.ToDateTime(dr["fecha"]).ToLocalTime().ToShortTimeString()}",
                        Descripcion = Convert.ToString(dr["DescripcionBitacora"]),
                        IdRegistroAfectado = Convert.ToString(dr["id_tablaAfectada"]),
                        RegistroAfectado = Convert.ToString(dr["tablaAfectada"])
                    });
                }

            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return list;
        }

        public List<Bitacora> ListaBitacoraPropiedades()
        {
            SqlConnection connection = (SqlConnection)context.Database.GetDbConnection(); /*para obtener la conexion con la BD*/

            List<Bitacora> list = new List<Bitacora>();

            try
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                command.CommandText = "SP_ObtenerBitacoraPropiedadesDiaActual";

                using SqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    list.Add(new Bitacora
                    {
                        IdBitacora = Convert.ToInt32(dr["id_bitacora"]),
                        Usuario = Convert.ToString(dr["id_usuario"]),
                        TipoOperacion = Convert.ToString(dr["DescripcionOperacion"]),
                        FechaString = Convert.ToDateTime(dr["fecha"]).ToLocalTime().ToShortDateString(),
                        HoraString = Convert.ToDateTime(dr["fecha"]).ToLocalTime().ToShortTimeString(),
                        FechaConcantenada = $"{Convert.ToDateTime(dr["fecha"]).ToLocalTime().ToShortDateString()} {Convert.ToDateTime(dr["fecha"]).ToLocalTime().ToShortTimeString()}",
                        Descripcion = Convert.ToString(dr["DescripcionBitacora"]),
                        IdRegistroAfectado = Convert.ToString(dr["id_tablaAfectada"]),
                        RegistroAfectado = Convert.ToString(dr["tablaAfectada"])
                    });
                }

            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return list;
        }

        public async Task<List<VerPropiedadViewModel>> ObtenerDatosPropiedadesVenta(int id)
        {

            List<TbPropiedade> datosPropiedad = new List<TbPropiedade>();

            try
            {
                SqlConnection connection = (SqlConnection)context.Database.GetDbConnection(); /*para obtener la conexion con la BD*/

                try
                {

                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    command.CommandText = "SP_ObtenerPropiedadesVenta";
                    command.Parameters.AddWithValue("@idUsoProp", id);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        datosPropiedad.Add(new TbPropiedade
                        {
                            IdPropiedad = Convert.ToInt32(reader["idPropiedad"]),
                            IdUsoTipo = Convert.ToInt32(reader["idUsoTipo"]),
                            IdUbicacion = Convert.ToInt32(reader["idUbicacion"]),
                            Direccion = Convert.ToString(reader["direccion"]),
                            IdUsoSuelo = reader["idUsoSuelo"] is DBNull ? default(int?) : (int)reader["idUsoSuelo"],
                            IdMedidaPro = reader["idMedidaPro"] is DBNull ? default(int?) : (int)reader["idMedidaPro"],
                            PrecioMax = reader["precioMax"] is DBNull ? default(decimal?) : (decimal)reader["precioMax"],
                            PrecioMin = reader["precioMin"] is DBNull ? default(decimal?) : (decimal)reader["precioMin"],
                            Topografia = Convert.ToString(reader["topografia"]),
                            NivelCalle = Convert.ToString(reader["nivelCalle"]),
                            IdAcceso = reader["idAcceso"] is DBNull ? default(int?) : (int)reader["idAcceso"],
                            CuotaMante = reader["cuotaMante"] is DBNull ? default(decimal?) : (decimal)reader["cuotaMante"],
                            DisAgua = Convert.ToString(reader["disAgua"]),
                            IdEstadosPozo = reader["idEstadosPozo"] is DBNull ? default(int?) : (int)reader["idEstadosPozo"],
                            NumFinca = Convert.ToString(reader["numFinca"]),
                            NumPlano = Convert.ToString(reader["numPlano"]),
                            Publicado = Convert.ToString(reader["publicado"]),
                            Megusta = Convert.ToInt32(reader["megusta"]),
                            Intencion = Convert.ToString(reader["intencion"]),
                            Descripcion = Convert.ToString(reader["descripcion"]),
                            LinkVideo = Convert.ToString(reader["linkVideo"]),
                            Moneda = Convert.ToString(reader["moneda"])
                        });
                    }
                }
                catch (Exception)
                {
                    return null;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }

                List<VerPropiedadViewModel> listPropiedad = new List<VerPropiedadViewModel>();

                for (int i = 0; i < datosPropiedad.Count; i++)
                {
                    TbEstadosPozo estadosPozo = null;
                    TiposPozo tipoPozo = null;
                    EstadosPozo estadoPozo = null;

                    if (datosPropiedad[i].IdEstadosPozo != null)
                    {
                        estadosPozo = await context.TbEstadosPozos.FindAsync(datosPropiedad[i].IdEstadosPozo);

                        if (estadosPozo != null)
                        {
                            tipoPozo = ObtenerTipoPozo().FirstOrDefault(x => x.Descripcion.Equals(estadosPozo.Pozo));
                            estadoPozo = ObtenerEstadosPozo().FirstOrDefault(x => x.Descripcion.StartsWith(estadosPozo.EstadoLegal));
                        }
                    }

                    TbAcceso acceso = await context.TbAccesos.FindAsync(datosPropiedad[i].IdAcceso);
                    TbMedidaPropiedad medida = await context.TbMedidaPropiedads.FindAsync(datosPropiedad[i].IdMedidaPro);
                    TbUsoSuelo usoSuelo = await context.TbUsoSuelos.FindAsync(datosPropiedad[i].IdUsoSuelo);
                    Ubicacion ubicacion = await context.Ubicacions.FindAsync(datosPropiedad[i].IdUbicacion);

                    TbUsoTipopropiedade usoTipo = await context.TbUsoTipopropiedades.FindAsync(datosPropiedad[i].IdUsoTipo);
                    TbUsoPropiedad tbUsoPropiedad = null;
                    TbTipoPropiedade tbTipoPropiedad = null;

                    if (usoTipo != null)
                    {
                        tbUsoPropiedad = await context.TbUsoPropiedads.FirstOrDefaultAsync(x => x.IdUsoPro == usoTipo.IdUsoPro);
                        tbTipoPropiedad = await context.TbTipoPropiedades.FirstOrDefaultAsync(x => x.IdTipoPro == usoTipo.IdTipoPro);
                    }

                    TbTipoMedida tbTipoMedida = null;

                    if (medida != null)
                    {
                        tbTipoMedida = await context.TbTipoMedidas.FirstOrDefaultAsync(x => x.IdTipoMedida == medida.IdTipoMedida);
                    }

                    TiposAcceso tiposAcceso = ObtenerTiposAcceso().FirstOrDefault(x => x.Descripcion.Equals(acceso.TipoAcceso));
                    EstadosCalificacion estadoAcceso = EstadosDeCalificacion().FirstOrDefault(x => x.Descripcion.Equals(acceso.Estado));
                    Topografia topografiaObtenida = ObtenerTopografia().FirstOrDefault(x => x.Descripcion.Equals(datosPropiedad[i].Topografia));
                    NivelCalle nivelCalleObtenido = ObtenerNivelesCalle().FirstOrDefault(x => x.Descripcion.Equals(datosPropiedad[i].NivelCalle));

                    Tuple<int, int, int> IDsUbicacion = await ObtenerIDsUbicacion(ubicacion);

                    VerPropiedadViewModel objetoPropiedad = new VerPropiedadViewModel()
                    {
                        IdPropiedad = datosPropiedad[i].IdPropiedad,
                        TotalMeGusta = datosPropiedad[i].Megusta,
                        Publicado = datosPropiedad[i].Publicado.Equals("S"),
                        NumeroFinca = datosPropiedad[i].NumFinca ?? "No indicado",
                        NumeroPlano = datosPropiedad[i].NumPlano ?? "No indicado",
                        CuotaMantenimiento = datosPropiedad[i].CuotaMante == null ? 0.0M : datosPropiedad[i].CuotaMante.Value,
                        DisponeAgua = datosPropiedad[i].DisAgua,
                        NivelCalleSeleccionada = datosPropiedad[i].NivelCalle,
                        TopografiaSeleccionada = datosPropiedad[i].Topografia,
                        PrecioMaximo = datosPropiedad[i].PrecioMax == null ? 0.0M : datosPropiedad[i].PrecioMax.Value,
                        PrecioMinimo = datosPropiedad[i].PrecioMin == null ? 0.0M : datosPropiedad[i].PrecioMin.Value,
                        Pozo = estadosPozo == null ? "No indicado" : estadosPozo.Pozo,
                        EstatusPozo = estadosPozo == null ? "No indicado" : estadosPozo.EstadoLegal,
                        TipoAcceso = acceso == null ? "No indicado" : acceso.TipoAcceso,
                        EstadoAcceso = acceso == null ? "No indicado" : acceso.Estado,
                        TotalMedida = medida == null ? 0.0M : medida.Medida,
                        Siglas = tbTipoMedida == null ? "N/A" : tbTipoMedida.Siglas,
                        DescripcionMedida = tbTipoMedida == null ? string.Empty : tbTipoMedida.Descripcion,
                        UsoSuelo = usoSuelo == null ? "No indicado" : usoSuelo.Descripcion,
                        DireccionExacta = datosPropiedad[i].Direccion,
                        Provincia = ubicacion.Provincia,
                        Canton = ubicacion.Canton,
                        Distrito = ubicacion.Distrito,
                        TipoPropiedad = tbTipoPropiedad == null ? "No indicado" : tbTipoPropiedad.Descripcion,
                        UsoPropiedad = tbUsoPropiedad == null ? "No indicado" : tbUsoPropiedad.Descripcion,
                        Intencion = datosPropiedad[i].Intencion,
                        Moneda = datosPropiedad[i].Moneda,
                        DescripcionPropiedad = datosPropiedad[i].Descripcion,
                        LinkVideo = datosPropiedad[i].LinkVideo,
                        BarrioPoblado = datosPropiedad[i].BarrioPoblado
                    };

                    List<TbRutaImgprop> imagenesPropiedad = await context.TbRutaImgprops.Where(x => x.IdPropiedad == datosPropiedad[i].IdPropiedad).Take(2).ToListAsync();

                    List<Imagen> imagenes = imagenesPropiedad.Select(x => new Imagen()
                    {
                        Ruta = x.Ruta,
                        Titulo = Guid.NewGuid().ToString()
                    }).ToList();

                    objetoPropiedad.ImagenesPropiedad = imagenes;
                    listPropiedad.Add(objetoPropiedad);
                }
                return listPropiedad;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<Response> EliminarPropiedad(int idPropiedad)
        {
            try
            {
                TbPropiedade objetoObtenido = await context.TbPropiedades.FindAsync(idPropiedad);

                if (objetoObtenido != null)
                {
                    objetoObtenido.Eliminado = "S";
                    objetoObtenido.Estado = "I";
                    objetoObtenido.Publicado = "N";

                    context.TbPropiedades.Update(objetoObtenido);
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

        public async Task<Response> ActivarPropiedadEnListaPrincipal(int idPropiedad)
        {
            try
            {
                TbPropiedade objetoObtenido = await context.TbPropiedades.FindAsync(idPropiedad);

                if (objetoObtenido != null)
                {
                    objetoObtenido.Eliminado = "N";

                    context.TbPropiedades.Update(objetoObtenido);
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
    }
}

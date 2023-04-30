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
    public class AlquilerRepository : Repository<TbPropiedade>, IAlquilerRepository
    {
        private readonly SolyLunaDbContext context;
        private readonly IPropiedadRepository propiedadRepository;

        public AlquilerRepository(SolyLunaDbContext context, IPropiedadRepository propiedadRepository) : base(context)
        {
            this.context = context;
            this.propiedadRepository = propiedadRepository;
        }

        public async Task<VerPropiedadViewModel> ObtenerInformacionAlquilerPropiedad(int id)
        {
            try
            {
                TbUsoPropiedad datosUso = await context.TbUsoPropiedads.FindAsync(id);
                TbUsoTipopropiedade datosUsoTipo = await context.TbUsoTipopropiedades.FindAsync(datosUso.IdUsoPro);

                TbPropiedade datosPropiedad = await context.TbPropiedades.FindAsync(datosUsoTipo.IdUsoPro);

                TbEstadosPozo estadosPozo = null;
                TiposPozo tipoPozo = null;
                EstadosPozo estadoPozo = null;

                if (datosPropiedad.IdEstadosPozo != null)
                {
                    estadosPozo = await context.TbEstadosPozos.FindAsync(datosPropiedad.IdEstadosPozo);

                    if (estadosPozo != null)
                    {
                        tipoPozo = propiedadRepository.ObtenerTipoPozo().FirstOrDefault(x => x.Descripcion.Equals(estadosPozo.Pozo));
                        estadoPozo = propiedadRepository.ObtenerEstadosPozo().FirstOrDefault(x => x.Descripcion.StartsWith(estadosPozo.EstadoLegal));
                    }
                }

                TbAvaluo avaluo = await context.TbAvaluos.FindAsync(datosPropiedad.IdPropiedad);


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

                TiposAcceso tiposAcceso = propiedadRepository.ObtenerTiposAcceso().FirstOrDefault(x => x.Descripcion.Equals(acceso.TipoAcceso));
                EstadosCalificacion estadoAcceso = propiedadRepository.EstadosDeCalificacion().FirstOrDefault(x => x.Descripcion.Equals(acceso.Estado));
                Topografia topografiaObtenida = propiedadRepository.ObtenerTopografia().FirstOrDefault(x => x.Descripcion.Equals(datosPropiedad.Topografia));
                NivelCalle nivelCalleObtenido = propiedadRepository.ObtenerNivelesCalle().FirstOrDefault(x => x.Descripcion.Equals(datosPropiedad.NivelCalle));

                Tuple<int, int, int> IDsUbicacion = await propiedadRepository.ObtenerIDsUbicacion(ubicacion);

                /*Consultar si tiene construcciones*/
                List<TbPropiedadConstruccion> listadoConstruccionPropiedad = await context.TbPropiedadConstruccions.Where(x => x.IdPropiedad == datosPropiedad.IdPropiedad).ToListAsync();

                /************************************/

                /*Obtener servicios municipales relacionados a la propiedad*/
                List<ServicioMunicipal> listadoServiciosMunicipales = propiedadRepository.ObtenerServiciosMunicipales(datosPropiedad.IdPropiedad);
                /*************************************/

                /*Obtener servicios municipales relacionados a la propiedad*/
                List<ServicioPublico> listadoServiciosPublicos = propiedadRepository.ObtenerServiciosPublicos(datosPropiedad.IdPropiedad);
                /*************************************/

                /*Obtener caracteristicas relacionados a la propiedad*/
                List<PropiedadCaracteristica> listadoCaracteristicasPropiedad = propiedadRepository.ObtenerCaracteristicasPropiedadAdquiridas(datosPropiedad.IdPropiedad);
                /*************************************/

                /*Obtener recorridos relacionados a la propiedad*/
                List<AccesoPropiedad> listadoRecorridosPropiedad = propiedadRepository.ObtenerRecorridosPropiedad(datosPropiedad.IdPropiedad);
                /*************************************/



                VerPropiedadViewModel objetoPropiedad = new VerPropiedadViewModel()
                {
                    IdPropiedad = datosPropiedad.IdPropiedad,
                    TotalMeGusta = datosPropiedad.Megusta,
                    Publicado = datosPropiedad.Publicado.Equals("S"),
                    NumeroFinca = datosPropiedad.NumFinca ?? "No indicado",
                    NumeroPlano = datosPropiedad.NumPlano ?? "No indicado",
                    CuotaMantenimiento = datosPropiedad.CuotaMante == null ? 0.0M : datosPropiedad.CuotaMante.Value,
                    DisponeAgua = datosPropiedad.DisAgua,
                    NivelCalleSeleccionada = datosPropiedad.NivelCalle,
                    TopografiaSeleccionada = datosPropiedad.Topografia,
                    PrecioMaximo = datosPropiedad.PrecioMax == null ? 0.0M : datosPropiedad.PrecioMax.Value,
                    PrecioMinimo = datosPropiedad.PrecioMin == null ? 0.0M : datosPropiedad.PrecioMin.Value,
                    Pozo = estadosPozo == null ? "No indicado" : estadosPozo.Pozo,
                    EstatusPozo = estadosPozo == null ? "No indicado" : estadosPozo.EstadoLegal,
                    TipoAcceso = acceso == null ? "No indicado" : acceso.TipoAcceso,
                    EstadoAcceso = acceso == null ? "No indicado" : acceso.Estado,
                    TotalMedida = medida == null ? 0.0M : medida.Medida,
                    Siglas = tbTipoMedida == null ? "N/A" : tbTipoMedida.Siglas,
                    DescripcionMedida = tbTipoMedida == null ? string.Empty : tbTipoMedida.Descripcion,
                    UsoSuelo = usoSuelo == null ? "No indicado" : usoSuelo.Descripcion,
                    DireccionExacta = datosPropiedad.Direccion,
                    Provincia = ubicacion.Provincia,
                    Canton = ubicacion.Canton,
                    Distrito = ubicacion.Distrito,
                    TipoPropiedad = tbTipoPropiedad == null ? "No indicado" : tbTipoPropiedad.Descripcion,
                    UsoPropiedad = tbUsoPropiedad == null ? "No indicado" : tbUsoPropiedad.Descripcion,
                    Intencion = datosPropiedad.Intencion,
                    DescripcionPropiedad = datosPropiedad.Descripcion,
                    LinkVideo = datosPropiedad.LinkVideo,
                    Moneda = datosPropiedad.Moneda,
                    FechaRegistra = datosPropiedad.FechaRegis,
                    CodigoTipoUsoPropiedad = datosPropiedad.CodigoTipoUsoPropiedad

                };

                List<TbRutaImgprop> imagenesPropiedad = await context.TbRutaImgprops.Where(x => x.IdPropiedad == datosPropiedad.IdPropiedad).ToListAsync();

                List<Imagen> imagenes = imagenesPropiedad.Select(x => new Imagen()
                {
                    Ruta = x.Ruta,
                    Titulo = Guid.NewGuid().ToString()
                }).ToList();
                objetoPropiedad.ImagenesPropiedad = imagenes;
                return objetoPropiedad;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<VerPropiedadViewModel>> ObtenerDatosPropiedadesAlquiler(int id)
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
                    command.CommandText = "SP_ObtenerPropiedadesAlquiler";
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
                            tipoPozo = propiedadRepository.ObtenerTipoPozo().FirstOrDefault(x => x.Descripcion.Equals(estadosPozo.Pozo));
                            estadoPozo = propiedadRepository.ObtenerEstadosPozo().FirstOrDefault(x => x.Descripcion.StartsWith(estadosPozo.EstadoLegal));
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

                    TiposAcceso tiposAcceso = propiedadRepository.ObtenerTiposAcceso().FirstOrDefault(x => x.Descripcion.Equals(acceso.TipoAcceso));
                    EstadosCalificacion estadoAcceso = propiedadRepository.EstadosDeCalificacion().FirstOrDefault(x => x.Descripcion.Equals(acceso.Estado));
                    Topografia topografiaObtenida = propiedadRepository.ObtenerTopografia().FirstOrDefault(x => x.Descripcion.Equals(datosPropiedad[i].Topografia));
                    NivelCalle nivelCalleObtenido = propiedadRepository.ObtenerNivelesCalle().FirstOrDefault(x => x.Descripcion.Equals(datosPropiedad[i].NivelCalle));

                    Tuple<int, int, int> IDsUbicacion = await propiedadRepository.ObtenerIDsUbicacion(ubicacion);

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
                        LinkVideo = datosPropiedad[i].LinkVideo
                    };

                    List<TbRutaImgprop> imagenesPropiedad = await context.TbRutaImgprops.Where(x => x.IdPropiedad == datosPropiedad[i].IdPropiedad).Take(1).ToListAsync();

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



    }
}

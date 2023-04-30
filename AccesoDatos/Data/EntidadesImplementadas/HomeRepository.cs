using AccesoDatos.BlogCore.Models;
using AccesoDatos.Data.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Modelos.Entidades;
using Modelos.ViewModels.FrontEnd.Home;
using Modelos.ViewModels.Propiedades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AccesoDatos.Data.EntidadesImplementadas
{
    public class HomeRepository : IHomeRepository
    {
        private readonly SolyLunaDbContext context;
        private readonly IPropiedadRepository propiedadRepository;
        private readonly IConstruccionRepository construccionRepository;

        public HomeRepository(SolyLunaDbContext context, IPropiedadRepository propiedadRepository,
            IConstruccionRepository construccionRepository)
        {
            this.context = context;
            this.propiedadRepository = propiedadRepository;
            this.construccionRepository = construccionRepository;
        }

        public async Task<List<VerPropiedadViewModel>> ObtenerDatosPropiedadesRecientes(string intencion, string proc)
        {

            List<TbPropiedade> datosPropiedad = new List<TbPropiedade>();

            try
            {
                SqlConnection connection = (SqlConnection)context.Database.GetDbConnection(); /*para obtener la conexion con la BD*/

                try
                {
                    string procedimiento = proc switch
                    {
                        "Proc1" => "SP_ObtenerUltimasPropiedadesAgregadasPorIntencion",
                        "Proc2" => "SP_ObtenerTodasLaSPropiedadesAgregadas",
                        _ => "Sin definir",
                    };

                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    command.CommandText = procedimiento;

                    if (proc.Equals("Proc1"))
                    {
                        command.Parameters.AddWithValue("@intencion", intencion);
                    }

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

        public async Task<List<VerPropiedadViewModel>> ObtenerPropiedadesBusquedaLugarIntencion(string buscar, string intencion, string seleccionado, string proc)
        {
            try
            {
                List<TbPropiedade> datosPropiedad = new List<TbPropiedade>();

                SqlConnection connection = (SqlConnection)context.Database.GetDbConnection(); /*para obtener la conexion con la BD*/

                try
                {
                    string procedimiento = proc switch
                    {
                        "Proc1" => "SP_ObtenerPropiedadesBusquedaAlquiler_Lugar_Intencion",
                        "Proc2" => "SP_ObtenerPropiedadesPorProvincia",
                        "Proc3" => "SP_ObtenerPropiedadesBusqueda",
                        "Proc4" => "SP_ObtenerPropiedadesPorTipo",
                        _ => "Sin definir",
                    };

                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    command.CommandText = procedimiento;

                    if (proc.Equals("Proc1") )
                    {
                        command.Parameters.AddWithValue("@buscar", buscar);
                        command.Parameters.AddWithValue("@intencion", intencion);
                        command.Parameters.AddWithValue("@seleccionado", Convert.ToInt32(seleccionado));
                    }
                    if (proc.Equals("Proc3"))
                    {
                        command.Parameters.AddWithValue("@buscar", buscar);
                        command.Parameters.AddWithValue("@intencion", intencion);
                    }
                    if (proc.Equals("Proc4"))
                    {
                        command.Parameters.AddWithValue("@intencion", intencion);
                        command.Parameters.AddWithValue("@seleccionado", Convert.ToInt32(seleccionado));
                    }

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
                            BarrioPoblado = Convert.ToString(reader["barrioPoblado"]),
                            Megusta = Convert.ToInt32(reader["megusta"]),
                            Intencion = Convert.ToString(reader["intencion"]),
                            Descripcion = Convert.ToString(reader["descripcion"]),
                            LinkVideo = Convert.ToString(reader["linkVideo"]),
                            Moneda = Convert.ToString(reader["Moneda"]),
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
                        DescripcionPropiedad = datosPropiedad[i].Descripcion,
                        LinkVideo = datosPropiedad[i].LinkVideo,
                        Moneda = datosPropiedad[i].Moneda,
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

        public async Task<InformacionPropiedadViewModel> ObtenerInformacionPropiedad(int id)
        {
            try
            {

                TbPropiedade datosPropiedad = await context.TbPropiedades.FindAsync(id);

                if (datosPropiedad.Publicado != "N")
                {


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

                    List<Construccion> listadoConstrucciones = await ObtenerConstrucciones(listadoConstruccionPropiedad);
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

                    InformacionPropiedadViewModel objetoPropiedad = new InformacionPropiedadViewModel()
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
                        CodigoTipoUsoPropiedad = datosPropiedad.CodigoTipoUsoPropiedad,
                        BarrioPoblado = datosPropiedad.BarrioPoblado
                    };

                    List<TbRutaImgprop> imagenesPropiedad = await context.TbRutaImgprops.Where(x => x.IdPropiedad == datosPropiedad.IdPropiedad).ToListAsync();

                    List<Imagen> imagenes = imagenesPropiedad.Select(x => new Imagen()
                    {
                        Ruta = x.Ruta,
                        Titulo = Guid.NewGuid().ToString()
                    }).ToList();

                    objetoPropiedad.ImagenesPropiedad = imagenes;
                    objetoPropiedad.Construcciones = listadoConstrucciones;
                    objetoPropiedad.PropiedadCaracteristicas = listadoCaracteristicasPropiedad;
                    objetoPropiedad.ServicioMunicipales = listadoServiciosMunicipales;
                    objetoPropiedad.ServiciosPublicos = listadoServiciosPublicos;
                    objetoPropiedad.RecorridosAcceso = listadoRecorridosPropiedad;
                    return objetoPropiedad;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async Task<List<Construccion>> ObtenerConstrucciones(List<TbPropiedadConstruccion> listadoConstruccionPropiedad)
        {
            List<Construccion> construcciones = new List<Construccion>();

            if (listadoConstruccionPropiedad != null)
            {
                foreach (TbPropiedadConstruccion item in listadoConstruccionPropiedad)
                {
                    Construccion construccion = await construccionRepository.ObtenerConstruccion(item.IdConstruccion);

                    construcciones.Add(construccion);
                }
            }

            return construcciones;
        }

        public async Task<InformacionConstruccionViewModel> ObtenerInformacionConstruccion(int id)
        {
            try
            {
                TbConstruccion construccionObtenida = await context.TbConstruccions.FindAsync(id);

                if (construccionObtenida != null)
                {
                    /*Obtener los equipamientos*/
                    List<ConstruccionEquipamiento> listadoEquipamientos = construccionRepository.ObtenerEquipamientosAdquiridos(id);
                    /***************************/

                    /*Obtener los divisiones y sus materiales*/
                    List<ConstruccionDivision> listadoDivisiones = construccionRepository.ObtenerDivisionesAdquiridas(id);
                    List<ConstruccionDivision> divisionesConMateriales = await ObtenerDivisionConMaterialesDivision(listadoDivisiones, id);
                    /***************************/

                    /*Obtener las caracteristicas de la construccion*/
                    List<ConstruccionCaracteristica> listadoCaracteristicas = construccionRepository.ObtenerCaracteristicasConstruccionAdquiridas(id);
                    /***************************/

                    /*Obtener los tipos de cableado*/
                    List<ConstruccionCableado> listadoCableados = construccionRepository.ObtenerListadoTiposCableadoObtenidos(id);
                    /***************************/

                    /*datos principales de la construccion*/
                    Construccion datosPrincipales = await construccionRepository.ObtenerConstruccion(id);
                    /**************************************/
                    List<TbRutaImgconst> imagenesContruc = await context.TbRutaImgconsts.Where(x => x.IdConstruccion == id).ToListAsync();

                    List<Imagen> imagenes = imagenesContruc.Select(x => new Imagen()
                    {
                        Ruta = x.Ruta,
                        Titulo = Guid.NewGuid().ToString()
                    }).ToList();

                    return new InformacionConstruccionViewModel
                    {
                        Caracteristicas = listadoCaracteristicas,
                        Divisiones = listadoDivisiones,
                        Equipamientos = listadoEquipamientos,
                        Cableados = listadoCableados,
                        Construccion = datosPrincipales,
                        Imagenes = imagenes
                    };
                }
            }
            catch (Exception)
            {
                return null;
            }

            return null;
        }

        public async Task<List<ConstruccionDivision>> ObtenerDivisionConMaterialesDivision(List<ConstruccionDivision> listadoDivisiones, int idConstruccion)
        {

            if (listadoDivisiones != null)
            {
                foreach (ConstruccionDivision item in listadoDivisiones)
                {
                    Response elementoObtenido = await construccionRepository.ObtenerDivisionAdquirida(idConstruccion, item.IdConstruccionDivision);
                    ConstruccionDivision listado = elementoObtenido.Resultado as ConstruccionDivision;

                    item.Materiales = listado.Materiales;
                }

                return listadoDivisiones;
            }

            return null;
        }

        public TotalPorProvincia TotalPorProvincia()
        {
            SqlConnection connection = (SqlConnection)context.Database.GetDbConnection(); /*para obtener la conexion con la BD*/

            try
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                command.CommandText = "SP_ObtenerPropiedadesPorTotalProvincia";

                command.Parameters.Add("@totalAlajuela", SqlDbType.Int).Direction = ParameterDirection.Output;
                command.Parameters.Add("@totalHeredia", SqlDbType.Int).Direction = ParameterDirection.Output;
                command.Parameters.Add("@totalSanJose", SqlDbType.Int).Direction = ParameterDirection.Output;
                command.Parameters.Add("@totalGuanacaste", SqlDbType.Int).Direction = ParameterDirection.Output;
                command.Parameters.Add("@totalPuntarenas", SqlDbType.Int).Direction = ParameterDirection.Output;
                command.Parameters.Add("@totalLimon", SqlDbType.Int).Direction = ParameterDirection.Output;
                command.Parameters.Add("@totalCartago", SqlDbType.Int).Direction = ParameterDirection.Output;

                command.ExecuteNonQuery();

                return new TotalPorProvincia
                {
                    TotalAlajuela = command.Parameters["@totalAlajuela"].Value is DBNull ? 0 : (int)command.Parameters["@totalAlajuela"].Value,
                    TotalHeredia = command.Parameters["@totalHeredia"].Value is DBNull ? 0 : (int)command.Parameters["@totalHeredia"].Value,
                    TotalSanJose = command.Parameters["@totalSanJose"].Value is DBNull ? 0 : (int)command.Parameters["@totalSanJose"].Value,
                    TotalGuanacaste = command.Parameters["@totalGuanacaste"].Value is DBNull ? 0 : (int)command.Parameters["@totalGuanacaste"].Value,
                    TotalPuntarenas = command.Parameters["@totalPuntarenas"].Value is DBNull ? 0 : (int)command.Parameters["@totalPuntarenas"].Value,
                    TotalCartago = command.Parameters["@totalCartago"].Value is DBNull ? 0 : (int)command.Parameters["@totalCartago"].Value,
                    TotalLimon = command.Parameters["@totalLimon"].Value is DBNull ? 0 : (int)command.Parameters["@totalLimon"].Value,
                };

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

        public List<MostrarPropiedadTabla> ObtenerListaPropiedadesHome()
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
                                                        where propiedad.Estado == "A" && propiedad.Publicado == "S"
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
                                                            propiedad.Intencion,
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
                                                            MedidaPropiedad = $"{p.Key.Medida:N2} {p.Key.Siglas.Trim()}",
                                                            PrecioMaximo = p.Key.PrecioMax,
                                                            PrecioMinimo = p.Key.PrecioMin,
                                                            Intencion = p.Key.Intencion,
                                                            Topografia = p.Key.Topografia,
                                                            Publicado = p.Key.Publicado.Equals("N") ? "No publicado" : "Publicado",
                                                            Moneda = p.Key.Moneda.Equals("Colones") ? "₡" : "$",
                                                            BarrioPoblado = p.Key.BarrioPoblado
                                                        }).ToList();
                return consulta;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListaUsosPropiedades> ObtenerListaUsosPropiedadesAlquiler()
        {
            try
            {
                List<ListaUsosPropiedades> consulta = (from usopropiedad in context.TbUsoPropiedads
                                                       join usoTipoPropiedad in context.TbUsoTipopropiedades
                                                       on usopropiedad.IdUsoPro equals usoTipoPropiedad.IdUsoPro
                                                       join prop in context.TbPropiedades
                                                       on usoTipoPropiedad.IdUsoTipo equals prop.IdUsoTipo

                                                       where prop.Estado == "A" && prop.Publicado == "S" && prop.Intencion != "venta"
                                                       group usopropiedad by new
                                                       {
                                                           usopropiedad.IdUsoPro,
                                                           usopropiedad.Descripcion

                                                       } into p

                                                       select new ListaUsosPropiedades
                                                       {
                                                           IdUsoProp = p.Key.IdUsoPro,
                                                           Descripcion = p.Key.Descripcion
                                                       }).ToList();
                return consulta;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListaUsosPropiedades> ObtenerListaUsosPropiedadesVenta()
        {
            try
            {
                List<ListaUsosPropiedades> consulta = (from usopropiedad in context.TbUsoPropiedads
                                                       join usoTipoPropiedad in context.TbUsoTipopropiedades
                                                       on usopropiedad.IdUsoPro equals usoTipoPropiedad.IdUsoPro
                                                       join prop in context.TbPropiedades
                                                       on usoTipoPropiedad.IdUsoTipo equals prop.IdUsoTipo

                                                       where prop.Estado == "A" && prop.Publicado == "S" && prop.Intencion != "renta"
                                                       group usopropiedad by new
                                                       {
                                                           usopropiedad.IdUsoPro,
                                                           usopropiedad.Descripcion

                                                       } into p

                                                       select new ListaUsosPropiedades
                                                       {
                                                           IdUsoProp = p.Key.IdUsoPro,
                                                           Descripcion = p.Key.Descripcion
                                                       }).ToList();
                return consulta;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListaUsosPropiedades> Test(string intencion)
        {
            SqlConnection connection = (SqlConnection)context.Database.GetDbConnection(); /*para obtener la conexion con la BD*/

            List<ListaUsosPropiedades> datosUsos = new List<ListaUsosPropiedades>();
            try
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                command.CommandText = "SP_ObtenerUsosPropiedades";
                command.Parameters.AddWithValue("@intencion", intencion);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    datosUsos.Add(new ListaUsosPropiedades
                    {
                        IdUsoProp = Convert.ToInt32(reader["idUsoProp"]),
                        Descripcion = Convert.ToString(reader["descripcion"]),
                        Cantidad = Convert.ToInt32(reader["cantidad"])
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

            return datosUsos;
        }

        public List<string> ObtenerFiltroBusqueda(string busqueda, string intencion)
        {
            SqlConnection connection = (SqlConnection)context.Database.GetDbConnection(); /*para obtener la conexion con la BD*/

            List<string> empResult = new List<string>();
            try
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                connection.Open();
                command.CommandText = $"Select distinct Top 10 direccionCompleta from TB_PROPIEDADES where " +
                                      $"direccionCompleta LIKE '%'+@direccionCompleta+'%' and publicado = 'S' and estado = 'A' and intencion != '{intencion}'";
                command.Parameters.AddWithValue("@direccionCompleta", busqueda);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    empResult.Add(reader["direccionCompleta"].ToString());
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

            return empResult;
        }
    }
}

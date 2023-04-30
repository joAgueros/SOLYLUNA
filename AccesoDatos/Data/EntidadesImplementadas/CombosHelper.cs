using AccesoDatos.BlogCore.Models;
using AccesoDatos.Data.Helpers;
using AccesoDatos.Data.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccesoDatos.Data.EntidadesImplementadas
{
    public class CombosHelper : ICombosHelper
    {
        private readonly SolyLunaDbContext context;
        private readonly IPropiedadRepository propiedadRepository;
        private readonly IConstruccionRepository construccionRepository;

        public CombosHelper(SolyLunaDbContext context, IPropiedadRepository propiedadRepository,
            IConstruccionRepository construccionRepository)
        {
            this.context = context;
            this.propiedadRepository = propiedadRepository;
            this.construccionRepository = construccionRepository;
        }

        public List<TiposPeriodo> ObtenerPeriodos()
        {
            return new List<TiposPeriodo>()
            {
                new TiposPeriodo(){ IdPeriodo = 1, Descripcion = "Días" },
                new TiposPeriodo(){ IdPeriodo = 2, Descripcion = "Semanas" },
                new TiposPeriodo(){ IdPeriodo = 3, Descripcion = "Meses" },
                new TiposPeriodo(){ IdPeriodo = 4, Descripcion = "Años" },
            };
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

        public List<TipoMoneda> TiposMoneda()
        {
            return new List<TipoMoneda>()
            {
                new TipoMoneda(){ Id = 1, Descripcion = "Colones", Simbolo = '₡'},
                new TipoMoneda(){ Id = 2, Descripcion = "Dólares", Simbolo = '$'}
            };
        }

        public List<RangosDolares> RangosDolares()
        {
            return new List<RangosDolares>()
            {   new RangosDolares(){ Id = 1, Monto = 0  },
                new RangosDolares(){ Id = 2, Monto = 50000  },
                new RangosDolares(){ Id = 3, Monto = 100000 },
                new RangosDolares(){ Id = 4, Monto = 150000 },
                new RangosDolares(){ Id = 5, Monto = 200000 },
            };
        }

        public List<RangosColones> RangosColones()
        {
            return new List<RangosColones>()
            {
                new RangosColones(){ Id = 1, Monto = 5000000 },
                new RangosColones(){ Id = 2, Monto = 10000000 },
                new RangosColones(){ Id = 3, Monto = 20000000 },
                new RangosColones(){ Id = 4, Monto = 30000000 },
                new RangosColones(){ Id = 5, Monto = 50000000 },
                new RangosColones(){ Id = 6, Monto = 100000000 },
                new RangosColones(){ Id = 7, Monto = 200000000 },
                new RangosColones(){ Id = 8, Monto = 300000000 },
                new RangosColones(){ Id = 9, Monto = 500000000 },
                new RangosColones(){ Id = 10, Monto = 1000000000 },
                new RangosColones(){ Id = 11, Monto = 2000000000 },
            };
        }

        public SelectListItem ValorPorDefecto(string texto)
        {
            return new SelectListItem()
            {
                Text = texto,
                Value = "0"
            };
        }

        public async Task<List<Ubicacion>> ObtenerTodasLasUbicaciones()
        {
            return await context.Ubicacions.ToListAsync();
        }

        public async Task<List<TbTipoIdentificacion>> ObtenerTodosTiposIdentificacion()
        {
            return await context.TbTipoIdentificacions.ToListAsync();
        }

        public async Task<List<SelectListItem>> ObtenerTiposIdentificacion()
        {

            List<TbTipoIdentificacion> tiposIdentificacion = await ObtenerTodosTiposIdentificacion();

            List<SelectListItem> listadoIdentificaciones = tiposIdentificacion.Select(p => new SelectListItem
            {
                Text = p.Descripcion.Trim(),
                Value = p.IdTipoIdentificacion.ToString(),


            }).OrderBy(p => p.Text).ToList();

            listadoIdentificaciones.Insert(0, ValorPorDefecto("(Seleccione tipo identificación)"));

            return listadoIdentificaciones;
        }

        public async Task<IEnumerable<SelectListItem>> ObtenerTodasLasProvincias()
        {
            List<Ubicacion> listadoUbicaciones = await ObtenerTodasLasUbicaciones();

            List<Provincias> agrupar = (from item in listadoUbicaciones
                                        group item by new { item.Provincia } into p
                                        select new Provincias()
                                        {
                                            Nombre = p.Key.Provincia,
                                        }).ToList();

            int valor = 0;

            List<SelectListItem> listadoProvincias = agrupar.Select(p => new SelectListItem
            {
                Text = p.Nombre.Trim(),
                Value = Convert.ToString(++valor)

            }).OrderBy(p => p.Text).ToList();

            listadoProvincias.Insert(0, ValorPorDefecto("(Seleccione una provincia)"));

            return listadoProvincias;
        }

        public async Task<IEnumerable<SelectListItem>> ObtenerTodosLosCantones(string nombreProvincia)
        {
            List<Ubicacion> listadoUbicaciones = await ObtenerTodasLasUbicaciones();

            IEnumerable<Ubicacion> obtenidos = listadoUbicaciones.Where(c => c.Provincia.Trim().Equals(nombreProvincia));

            List<Cantones> agrupar = (from item in obtenidos
                                      group item by new { item.Canton } into p
                                      select new Cantones()
                                      {
                                          Nombre = p.Key.Canton,
                                      }).ToList();

            int valor = 0;

            List<SelectListItem> listadoCantones = agrupar.Select(p => new SelectListItem
            {
                Text = p.Nombre.Trim(),
                Value = Convert.ToString(++valor)

            }).OrderBy(c => c.Text).ToList();

            listadoCantones.Insert(0, ValorPorDefecto("(Seleccione un cantón)"));

            return listadoCantones;
        }

        public async Task<IEnumerable<SelectListItem>> ObtenerTodosLosDistritos(string nombreCanton)
        {
            List<Ubicacion> listadoUbicaciones = await ObtenerTodasLasUbicaciones();

            IEnumerable<Ubicacion> obtenidos = listadoUbicaciones.Where(c => c.Canton.Trim().Equals(nombreCanton));

            List<SelectListItem> listadoDistritos = obtenidos.Select(p => new SelectListItem
            {
                Text = p.Distrito.Trim().ToUpper(),
                Value = p.IdUbicacion.ToString()

            }).OrderBy(c => c.Text).ToList();

            listadoDistritos.Insert(0, ValorPorDefecto("(Seleccione un distrito)"));

            return listadoDistritos;
        }

        public async Task<List<SelectListItem>> ObtenerTiposPropiedad()
        {
            List<TbTipoPropiedade> list = await propiedadRepository.ObtenerTodosLosTiposPropiedades();

            List<SelectListItem> listadoTiposPropiedades = list.Select(p => new SelectListItem()
            {
                Text = p.Descripcion,
                Value = p.IdTipoPro.ToString()
            }).OrderBy(c => c.Text).ToList();

            listadoTiposPropiedades.Insert(0, ValorPorDefecto("(Seleccione el tipo de propiedad)"));

            return listadoTiposPropiedades;
        }

        public async Task<List<SelectListItem>> ObtenerTiposUsoPropiedad()
        {
            List<TbUsoPropiedad> list = await propiedadRepository.ObtenerTodosLosUsosPropiedad();

            List<SelectListItem> listadoUsoPropiedades = list.Select(p => new SelectListItem()
            {
                Text = p.Descripcion,
                Value = p.IdUsoPro.ToString()
            }).OrderByDescending(c => c.Value).ToList();

            listadoUsoPropiedades.Insert(0, ValorPorDefecto("(Seleccione el tipo de uso)"));

            return listadoUsoPropiedades;
        }

        public async Task<List<SelectListItem>> ObtenerTiposUsoSueloPropiedad()
        {
            List<TbUsoSuelo> list = await propiedadRepository.ObtenerTodosLosTiposUsoSuelo();

            List<SelectListItem> listadoUsoSueloPropiedades = list.Select(p => new SelectListItem()
            {
                Text = p.Descripcion,
                Value = p.IdUsoSuelo.ToString()
            }).OrderByDescending(c => c.Value).ToList();

            listadoUsoSueloPropiedades.Insert(0, ValorPorDefecto("(Seleccione el tipo de uso del suelo)"));

            return listadoUsoSueloPropiedades;
        }

        public List<SelectListItem> ObtenerTiposIngresoPropiedad()
        {
            List<TiposAcceso> list = new List<TiposAcceso>() {

                new TiposAcceso(){ Descripcion = "Pavimento", IdTipoAcceso = 1},
                new TiposAcceso(){ Descripcion = "Lastre", IdTipoAcceso = 2},
                new TiposAcceso(){ Descripcion = "Adoquín", IdTipoAcceso = 3},

            };

            List<SelectListItem> listadoTiposIngresoPropiedades = list.Select(p => new SelectListItem()
            {
                Text = p.Descripcion,
                Value = p.IdTipoAcceso.ToString()
            }).OrderBy(c => c.Text).ToList();

            listadoTiposIngresoPropiedades.Insert(0, ValorPorDefecto("(Seleccione el tipo de ingreso)"));

            return listadoTiposIngresoPropiedades;
        }

        public List<SelectListItem> ObtenerTiposEstadoIngresoPropiedad()
        {
            List<EstadosCalificacion> list = EstadosDeCalificacion();

            List<SelectListItem> listadoEstadosCalificacion = list.Select(p => new SelectListItem()
            {
                Text = p.Descripcion,
                Value = p.IdEstadoCalificacion.ToString()
            }).OrderBy(c => c.Text).ToList();

            listadoEstadosCalificacion.Insert(0, ValorPorDefecto("(Seleccione el tipo de estado)"));

            return listadoEstadosCalificacion;

        }

        public List<SelectListItem> ObtenerTiposEstadoFisicoConstruccion()
        {
            List<EstadosCalificacion> list = EstadosDeCalificacion();

            List<SelectListItem> listadoEstadosCalificacion = list.Select(p => new SelectListItem()
            {
                Text = p.Descripcion,
                Value = p.IdEstadoCalificacion.ToString()
            }).OrderBy(c => c.Text).ToList();

            listadoEstadosCalificacion.Insert(0, ValorPorDefecto("(Seleccione el tipo de estado físico)"));

            return listadoEstadosCalificacion;

        }

        public List<SelectListItem> ObtenerListaPeriodos()
        {
            List<TiposPeriodo> list = ObtenerPeriodos();

            List<SelectListItem> listadoPeriodos = list.Select(p => new SelectListItem()
            {
                Text = p.Descripcion,
                Value = p.IdPeriodo.ToString()
            }).OrderBy(c => c.Text).ToList();

            listadoPeriodos.Insert(0, ValorPorDefecto("(Seleccione el periodo)"));

            return listadoPeriodos;

        }

        public async Task<List<SelectListItem>> ObtenerTiposMedidas()
        {
            List<TbTipoMedida> list = await propiedadRepository.ObtenerTodosLosTiposMedidas();

            List<SelectListItem> listadoMedidasPropiedades = list.Select(p => new SelectListItem()
            {
                Text = p.Descripcion,
                Value = p.IdTipoMedida.ToString()
            }).OrderBy(c => c.Text).ToList();

            listadoMedidasPropiedades.Insert(0, ValorPorDefecto("(Seleccione el tipo de medida)"));

            return listadoMedidasPropiedades;
        }

        public List<SelectListItem> ObtenerTiposPozoPropiedad()
        {
            List<TiposPozo> list = propiedadRepository.ObtenerTipoPozo();

            List<SelectListItem> listadoMedidasPropiedades = list.Select(p => new SelectListItem()
            {
                Text = p.Descripcion,
                Value = p.IdTipoPozo.ToString()
            }).OrderBy(c => c.Text).ToList();

            listadoMedidasPropiedades.Insert(0, ValorPorDefecto("(Seleccione el tipo de pozo)"));

            return listadoMedidasPropiedades;
        }

        public List<SelectListItem> ObtenerTiposEstadoPozoPropiedad()
        {
            List<EstadosPozo> list = propiedadRepository.ObtenerEstadosPozo();

            List<SelectListItem> listadoMedidasPropiedades = list.Select(p => new SelectListItem()
            {
                Text = p.Descripcion,
                Value = p.IdEstadoPozo.ToString()
            }).OrderBy(c => c.Text).ToList();

            listadoMedidasPropiedades.Insert(0, ValorPorDefecto("(Seleccione el estado del pozo)"));

            return listadoMedidasPropiedades;
        }

        public List<SelectListItem> ObtenerTiposTopografia()
        {
            List<Topografia> list = propiedadRepository.ObtenerTopografia();

            List<SelectListItem> listadoTopografiasPropiedad = list.Select(p => new SelectListItem()
            {
                Text = p.Descripcion,
                Value = p.IdTopografia.ToString()
            }).OrderBy(c => c.Text).ToList();

            listadoTopografiasPropiedad.Insert(0, ValorPorDefecto("(Seleccione el tipo de topografía)"));

            return listadoTopografiasPropiedad;
        }

        public List<SelectListItem> ObtenerTiposNivelCalle()
        {
            List<NivelCalle> list = propiedadRepository.ObtenerNivelesCalle();

            List<SelectListItem> listadoNivelesCallePropiedad = list.Select(p => new SelectListItem()
            {
                Text = p.Descripcion,
                Value = p.IdNivelCalle.ToString()
            }).OrderBy(c => c.Text).ToList();

            listadoNivelesCallePropiedad.Insert(0, ValorPorDefecto("(Seleccione el nivel de calle)"));

            return listadoNivelesCallePropiedad;
        }

        public async Task<List<SelectListItem>> ObtenerListaVisualizaciones()
        {
            List<TbPersVisual> list = await propiedadRepository.ObtenerTodasLasVistas();

            List<SelectListItem> listadoVistas = list.Select(p => new SelectListItem()
            {
                Text = p.Descripcion,
                Value = p.IdPerVisual.ToString()
            }).OrderBy(c => c.Text).ToList();

            listadoVistas.Insert(0, ValorPorDefecto("(Seleccione el tipo de vista)"));

            return listadoVistas;
        }

        public async Task<List<SelectListItem>> ObtenerDivisionesConstruccion()
        {
            List<TbDivisionesObra> list = await construccionRepository.ObtenerTodasLasDivisionesObra();

            List<SelectListItem> listaDivisiones = list.Select(p => new SelectListItem()
            {
                Text = p.Descripcion,
                Value = p.IdDivision.ToString()
            }).OrderBy(c => c.Text).ToList();

            listaDivisiones.Insert(0, ValorPorDefecto("(Seleccione el tipo de división)"));

            return listaDivisiones;
        }

        public async Task<List<SelectListItem>> ObtenerMaterialesObra()
        {
            List<TbMaterialesObra> list = await construccionRepository.ObtenerTodosLosMaterialesObra();

            List<SelectListItem> listadoMateriales = list.Select(p => new SelectListItem()
            {
                Text = p.Descripcion,
                Value = p.IdMaterial.ToString()
            }).OrderBy(c => c.Text).ToList();

            listadoMateriales.Insert(0, ValorPorDefecto("(Seleccione el tipo de material)"));

            return listadoMateriales;
        }

        public async Task<List<SelectListItem>> ObtenerListaTipoCableado()
        {
            List<TbTipocableado> list = await construccionRepository.ObtenerTodosLosTiposCableado();

            List<SelectListItem> listadoMateriales = list.Select(p => new SelectListItem()
            {
                Text = p.Descripcion,
                Value = p.IdTipoCableado.ToString()
            }).OrderBy(c => c.Text).ToList();

            listadoMateriales.Insert(0, ValorPorDefecto("(Seleccione el tipo de cableado)"));

            return listadoMateriales;
        }

        public async Task<List<SelectListItem>> ObtenerLista()
        {
            List<TbTipocableado> list = await construccionRepository.ObtenerTodosLosTiposCableado();

            List<SelectListItem> listadoMateriales = list.Select(p => new SelectListItem()
            {
                Text = p.Descripcion,
                Value = p.IdTipoCableado.ToString()
            }).OrderBy(c => c.Text).ToList();

            listadoMateriales.Insert(0, ValorPorDefecto("(Seleccione el tipo de cableado)"));

            return listadoMateriales;
        }

        public async Task<List<SelectListItem>> ObtenerListaCuotas()
        {
            List<TbTipoCuota> list = await propiedadRepository.ObtenerTodosLosTiposCuotas();

            List<SelectListItem> listadoCuotas = list.Select(p => new SelectListItem()
            {
                Text = p.Cuota,
                Value = p.IdCuota.ToString()
            }).OrderBy(c => c.Text).ToList();

            listadoCuotas.Insert(0, ValorPorDefecto("(Seleccione el tipo de cuota)"));

            return listadoCuotas;
        }

        public async Task<List<SelectListItem>> ObtenerListaServiciosMunicipales()
        {
            List<TbTiposerMunicipal> list = await propiedadRepository.ObtenerTodosLosServiciosMunicipales();

            List<SelectListItem> listadoServiciosMunicipales = list.Select(p => new SelectListItem()
            {
                Text = p.Descripcion,
                Value = p.IdTipoSer.ToString()
            }).OrderBy(c => c.Text).ToList();

            listadoServiciosMunicipales.Insert(0, ValorPorDefecto("(Seleccione el tipo de servicio municipal)"));

            return listadoServiciosMunicipales;
        }

        public async Task<List<SelectListItem>> ObtenerListaSituacionPropiedad()
        {
            List<TbTipoSituacion> list = await propiedadRepository.ObtenerTodosLosTiposSituacionPropiedad();

            List<SelectListItem> listadoSituacionesPropiedad = list.Select(p => new SelectListItem()
            {
                Text = p.Descripcion,
                Value = p.IdTipoSituacion.ToString()
            }).OrderBy(c => c.Text).ToList();

            listadoSituacionesPropiedad.Insert(0, ValorPorDefecto("(Seleccione el tipo de situación)"));

            return listadoSituacionesPropiedad;
        }

        public async Task<List<SelectListItem>> ObtenerListaServiciosPublicos()
        {
            List<TbServiciosPub> list = await propiedadRepository.ObtenerTodosLosServiciosPublicos();

            List<SelectListItem> listadoSituacionesPropiedad = list.Select(p => new SelectListItem()
            {
                Text = p.Descripcion,
                Value = p.IdServicioPublico.ToString()
            }).OrderBy(c => c.Text).ToList();

            listadoSituacionesPropiedad.Insert(0, ValorPorDefecto("(Seleccione el tipo de servicio)"));

            return listadoSituacionesPropiedad;
        }

        public async Task<List<SelectListItem>> ObtenerTipoDocumentosPropiedad()
        {
            List<TbDocumento> list = await propiedadRepository.ObtenerTodosLosTiposDocumentos();

            List<SelectListItem> listadoDocumentos = list.Select(p => new SelectListItem()
            {
                Text = p.Descripcion,
                Value = p.IdDocumento.ToString()
            }).OrderBy(c => c.Text).ToList();

            listadoDocumentos.Insert(0, ValorPorDefecto("(Seleccione el tipo de documento)"));

            return listadoDocumentos;
        }

        public async Task<List<SelectListItem>> ObtenerListaTiposIntermediarios()
        {
            List<TbTipoIntermediario> list = await propiedadRepository.ObtenerTodosLosTiposIntermediarios();

            List<SelectListItem> tiposIntermediarios = list.Select(p => new SelectListItem()
            {
                Text = p.Detalle,
                Value = p.IdTipoInter.ToString()
            }).OrderBy(c => c.Text).ToList();

            tiposIntermediarios.Insert(0, ValorPorDefecto("(Seleccione el tipo de intermediario)"));

            return tiposIntermediarios;
        }

        public async Task<List<SelectListItem>> ObtenerListaAccesibilidades()
        {
            List<TbAccesibilidad> list = await context.TbAccesibilidads.ToListAsync();

            List<SelectListItem> tipoAccesbilidades = list.Select(p => new SelectListItem()
            {
                Text = p.Descripcion,
                Value = p.IdAccesibilidad.ToString()
            }).OrderBy(c => c.Text).ToList();

            tipoAccesbilidades.Insert(0, ValorPorDefecto("(Seleccione el tipo de accesibilidad)"));

            return tipoAccesbilidades;
        }

        public List<SelectListItem> ObtenerTiposMoneda()
        {
            List<TipoMoneda> list = TiposMoneda();
            List<SelectListItem> tiposMoneda = list.Select(p => new SelectListItem()
            {
                Selected = p.Descripcion.Equals("Colones"),
                Text = p.Descripcion,
                Value = p.Id.ToString()
            }).OrderBy(c => c.Text).ToList();

            return tiposMoneda;
        }

        public List<SelectListItem> ObtenerRangosColones()
        {
            List<SelectListItem> tiposMoneda = RangosColones().OrderBy(c => c.Id).Select(p => new SelectListItem()
            {
                Text = p.Monto.ToString(),
                Value = p.Id.ToString()
            }).ToList();

            return tiposMoneda;
        }

        public List<SelectListItem> ObtenerRangosDolares()
        {
            List<SelectListItem> tiposMoneda = RangosDolares().OrderBy(c => c.Id).Select(p => new SelectListItem()
            {
                Text = p.Monto.ToString(),
                Value = p.Id.ToString()
            }).ToList();

            return tiposMoneda;
        }
    }
}

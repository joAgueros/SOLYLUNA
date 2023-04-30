using AccesoDatos.BlogCore.Models;
using Modelos.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccesoDatos.Data.Repository
{
    public interface IConstruccionRepository : IRepository<TbConstruccion>
    {
        public Task<List<TbCaracteristica>> ObtenerTodasLasCaracteristicas();
        public Task<List<TbEquipamiento>> ObtenerTodosLosEquipamientos();
        public Task<List<TbPersVisual>> ObtenerTodasLasVistas();
        public Task<List<TbTipocableado>> ObtenerTodosLosTiposCableado();
        public Task<List<TbMaterialesObra>> ObtenerTodosLosMaterialesObra();
        public Task<List<TbDivisionesObra>> ObtenerTodasLasDivisionesObra();
        public Task<List<TbTipoMedida>> ObtenerTodosLosTiposMedidas();
        public List<Construccion> ObtenerListaConstrucciones(int idPropiedad);
        public Task<Construccion> ObtenerConstruccion(int idConstruccion);
        public List<ConstruccionEquipamiento> ObtenerEquipamientosAdquiridos(int idConstruccion);
        public Task<Response> DisminuirEquipamiento(int idConstruccionEquipamiento);
        public Task<Response> AumentarEquipamiento(int idConstruccionEquipamiento);
        public Task<Response> EliminarEquipamiento(int idConstruccionEquipamiento);
        public Task<bool> AgregarEquipamiento(int idEquipamiento, int idConstruccion);
        public Task<bool> AgregarCaracteristicaConstruccion(int idCaracteristica, int idConstruccion);
        public List<ConstruccionCaracteristica> ObtenerCaracteristicasConstruccionAdquiridas(int idConstruccion);
        public Task<Response> DisminuirCaracteristicaConstruccion(int idConstruccionCaracteristica);
        public Task<Response> AumentarCaracteristicaConstruccion(int idConstruccionCaracteristica);
        public Task<Response> EliminarCaracteristicaConstruccion(int idConstruccionCaracteristica);
        public Task<Response> EditarDivision(ConstruccionDivision division);
        public Task<Response> AgregarDivision(Division division);
        public Task<Response> EliminarDivisionAgregada(Division division);
        public Task<Response> AgregarMaterialDivision(Material division);
        public Task<Response> EliminarMaterialDivision(Material division);
        public List<ConstruccionDivision> ObtenerDivisionesAdquiridas(int idConstruccion);
        public Task<Response> ObtenerDivisionAdquirida(int idConstruccion, int idConstruccionDivision);
        public Task<Response> ObtenerTipoCableadoAdquirido(Cableado cableado);
        public Task<Response> AgregarDatosBasicosConstruccion(EditarConstruccion construccion);
        public Task<Response> AgregarTiposDeCableadoConstruccion(Cableado cableado);
        public Task<Response> EliminarCableadoConstruccion(Cableado cableado);
        public Task<Response> EditarCableadoConstruccion(TiposCableado cableado);
        public List<ConstruccionCableado> ObtenerListadoTiposCableadoObtenidos(int idConstruccion);
        public Task<Response> CambiarEstadoConstruccion(Construccion construccion);
        public Task<Response> AgregarImagenConstruccion(string rutaImagen, int idConstruccion);
        public Task<Response> ObtenerImagenesConstruccion(int idConstruccion);
        public Task<Response> EliminarImagenGaleria(Imagen imagen);
        public Task<Response> ObtenerImagen(int idImagen);

    }
}

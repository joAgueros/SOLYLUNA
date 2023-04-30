using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Modelos.ViewModels.Propiedades
{
    public class ConstruccionViewModel
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public bool Status { get; set; }
        public string EstadoFisico { get; set; }
        public string Antiguedad { get; set; }
        public string FechaRegistra { get; set; }
        public decimal Medida { get; set; }
        public decimal TotalPeriodo { get; set; }
        public string Siglas { get; set; }
        public string TipoVisualizacion { get; set; }


        public IEnumerable<SelectListItem> TipoMedida { get; set; }
        public IEnumerable<SelectListItem> Periodo { get; set; }
        public IEnumerable<SelectListItem> EstadoFisicoConstruccion { get; set; }
        public IEnumerable<SelectListItem> Visualizacion { get; set; }
        public IEnumerable<SelectListItem> Cableado { get; set; }
        
        public int? TipoMedidadId { get; set; }
        public int? PeriodoId { get; set; }
        public int? EstadoFisicoId { get; set; }
        public int? VisualizacionId { get; set; }
        public int? CableadoId { get; set; }
    }
}

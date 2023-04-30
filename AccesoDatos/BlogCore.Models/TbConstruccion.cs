using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbConstruccion
    {
        public TbConstruccion()
        {
            TbAvaluos = new HashSet<TbAvaluo>();
            TbConstruccionCableados = new HashSet<TbConstruccionCableado>();
            TbConstruccionDiviciones = new HashSet<TbConstruccionDivicione>();
            TbConstruccionEquipamientos = new HashSet<TbConstruccionEquipamiento>();
            TbContruccionCaracteristicas = new HashSet<TbContruccionCaracteristica>();
            TbPropiedadConstruccions = new HashSet<TbPropiedadConstruccion>();
            TbRutaImgconsts = new HashSet<TbRutaImgconst>();
        }

        public int IdConstruccion { get; set; }
        public int? IdMedidaCon { get; set; }
        public string Antiguedad { get; set; }
        public string Estadofisico { get; set; }
        public int? IdPerVisual { get; set; }
        public DateTime FechaRegis { get; set; }
        public string Estado { get; set; }
        public string Descripcion { get; set; }
        public string CodigoIdentificador { get; set; }

        public virtual TbMedidaContruccion IdMedidaConNavigation { get; set; }
        public virtual TbPersVisual IdPerVisualNavigation { get; set; }
        public virtual ICollection<TbAvaluo> TbAvaluos { get; set; }
        public virtual ICollection<TbConstruccionCableado> TbConstruccionCableados { get; set; }
        public virtual ICollection<TbConstruccionDivicione> TbConstruccionDiviciones { get; set; }
        public virtual ICollection<TbConstruccionEquipamiento> TbConstruccionEquipamientos { get; set; }
        public virtual ICollection<TbContruccionCaracteristica> TbContruccionCaracteristicas { get; set; }
        public virtual ICollection<TbPropiedadConstruccion> TbPropiedadConstruccions { get; set; }
        public virtual ICollection<TbRutaImgconst> TbRutaImgconsts { get; set; }
    }
}

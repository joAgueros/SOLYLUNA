using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbTiposerMunicipal
    {
        public TbTiposerMunicipal()
        {
            TbServiciosMunicipales = new HashSet<TbServiciosMunicipale>();
        }

        public int IdTipoSer { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<TbServiciosMunicipale> TbServiciosMunicipales { get; set; }
    }
}

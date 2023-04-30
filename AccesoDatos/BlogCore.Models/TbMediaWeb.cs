using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbMediaWeb
    {
        public int IdMedWeb { get; set; }
        public int IdMedia { get; set; }

        public virtual TbMedium IdMediaNavigation { get; set; }
    }
}

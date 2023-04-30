using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class Event
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventStart { get; set; }
        public DateTime EventEnd { get; set; }
        public bool AllDay { get; set; }
    }
}

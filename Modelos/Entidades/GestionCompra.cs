using System;

namespace Modelos.Entidades
{
    public class GestionCompra
    {
        public int Id { get; set; }
        public int IdComprador { get; set; }
        public string Descripcion { get; set; }
        public string Activo { get; set; }
        public string FechaEntregaString { get; set; }
        public string FechaSolicitaString { get; set; }
    }
}

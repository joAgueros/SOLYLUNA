using AccesoDatos.Data.Helpers;
using System;

namespace AccesoDatos.Data.Repository
{
    public interface IContenedorTrabajo : IDisposable
    {
        IUserHelper Usuario { get; }
        ICombosHelper ComboBox { get; }
        IPropiedadRepository Propiedades { get; }
        IConstruccionRepository Construcciones { get; }
        IVendedoresRepository Vendedores { get; }
        ICompradoresRepository Compradores { get; }
        IIntermedarioRepository Intermediarios { get; }
        IAgendaRepository Events { get; }
        IBitacoraRepository Bitacora { get; }
        IContactoRepository Contacto { get; }
        IHomeRepository HomeCliente { get; }
        IAlquilerRepository Alquiler { get; }
        IHomeAdminRepository HomeAdmin { get; }

        void Save();
    }
}

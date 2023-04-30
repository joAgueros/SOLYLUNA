using AccesoDatos.BlogCore.Models;
using AccesoDatos.Data.Helpers;
using AccesoDatos.Data.Repository;
using Microsoft.AspNetCore.Identity;
using Modelos.Entidades;

namespace AccesoDatos.Data.EntidadesImplementadas
{
    public class ContenedorTrabajo : IContenedorTrabajo
    {
        private readonly SolyLunaDbContext context;
        private readonly UserManager<ApplicationUser> userManager;  /*cada cambio que se debe hacer a un usuario, sea cualquier accion, se hace a traves de este objeto*/
        private readonly SignInManager<ApplicationUser> signInManager; /*este permite realizar todo lo relacionado a inicios y cierres de sesion. Tiene inyeccion nativa igual que el UserManager*/
        private readonly RoleManager<IdentityRole> roleManager; /*esta trabaja directamente con la IdentityRole, y no con User directamente, NO OCUPA INYECTARSE TAMPOCO EN EL START UP*/

        public ContenedorTrabajo(SolyLunaDbContext db, UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 RoleManager<IdentityRole> roleManager)
        {
            context = db;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;

            Usuario = new UserHelper(this.userManager, this.signInManager, this.roleManager);
            Bitacora = new BitacoraRepository(context, Usuario);
            Construcciones = new ConstruccionRepository(context);
            Propiedades = new PropiedadRepository(context, Bitacora);
            HomeCliente = new HomeRepository(context, Propiedades, Construcciones);
            ComboBox = new CombosHelper(context, Propiedades, Construcciones);
            Intermediarios = new IntermediarioRepository(context);
            Vendedores = new VendedoresRepository(context, Bitacora);
            Contacto = new ContactoRepository(context);
            Compradores = new CompradoresRepository(context, Bitacora, Usuario);
            Events = new AgendaRepository(context);
            Alquiler = new AlquilerRepository(context, Propiedades);
            HomeAdmin = new HomeAdminRepository(context);
        }

        /*IR COLOCANDO ACA CADA CLASE MODELO CREADA*/

        public IUserHelper Usuario { get; private set; }
        public ICombosHelper ComboBox { get; private set; }
        public IPropiedadRepository Propiedades { get; private set; }
        public IConstruccionRepository Construcciones { get; private set; }
        public IVendedoresRepository Vendedores { get; private set; }
        public ICompradoresRepository Compradores { get; private set; }
        public IAgendaRepository Events { get; private set; }
        public IBitacoraRepository Bitacora { get; private set; }
        public IContactoRepository Contacto { get; private set; }
        public IHomeRepository HomeCliente { get; private set; }
        public IIntermedarioRepository Intermediarios { get; private set; }
        public IAlquilerRepository Alquiler { get; private set; }
        public IHomeAdminRepository HomeAdmin { get; private set; }


        /*********************************************/

        public void Dispose()
        {
            context.Dispose();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
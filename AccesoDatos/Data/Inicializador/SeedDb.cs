using AccesoDatos.BlogCore.Models;
using AccesoDatos.Data.Repository;
using Modelos.Entidades;
using System;
using System.Threading.Tasks;

namespace AccesoDatos.Data.Inicializador
{
    public class SeedDb
    {

        private readonly SolyLunaDbContext dbContext;
        private readonly IContenedorTrabajo contenedorTrabajo;

        public SeedDb(SolyLunaDbContext dbContext, IContenedorTrabajo contenedorTrabajo)
        {
            this.dbContext = dbContext;
            this.contenedorTrabajo = contenedorTrabajo;
        }

        public async Task SeedAsync()
        {
            try
            {
                await dbContext.Database.EnsureCreatedAsync();
                await CheckRolesAsync();
                await CheckUserAsync("Usuario", "Soporte", string.Empty, "soporte@naglocr.com", "naglocr123", string.Empty, "Admin");
                await CheckUserAsync("Usuario", "Administrador", string.Empty, "solyluna@admin.com", "123456", string.Empty, "SuperAdmin");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private async Task<ApplicationUser> CheckUserAsync(
            string nombre, string apellido1, string apellido2, string email, string password, string identificacion, string role)
        {
            ApplicationUser user = await contenedorTrabajo.Usuario.GetUserByEmailAsync(email);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    Nombre = nombre,
                    Apellidos = $"{apellido1} {apellido2}",
                    Identificacion = identificacion,
                    Email = email,
                    UserName = email,
                    LockoutEnd = DateTime.Now.ToLocalTime()
                };

                await contenedorTrabajo.Usuario.AddUserAsync(user, password);
                await contenedorTrabajo.Usuario.AddRoleToUserAsync(user, role);
                string token = await contenedorTrabajo.Usuario.GenerateEmailConfirmationTokenAsync(user);
                await contenedorTrabajo.Usuario.ConfirmEmailAsync(user, token);
            }

            return user;
        }
        private async Task CheckRolesAsync()
        {
            await contenedorTrabajo.Usuario.CheckRoleAsync("Admin");
            await contenedorTrabajo.Usuario.CheckRoleAsync("SuperAdmin");
        }

    }
}

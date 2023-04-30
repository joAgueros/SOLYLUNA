using AccesoDatos.Data.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Modelos.Entidades;
using Modelos.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccesoDatos.Data.EntidadesImplementadas
{
    public class UserHelper : IUserHelper  /*la clase UserHelper va a ser la unica que va a inyectar el UserManager*/
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserHelper(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        public async Task<bool> AddRoleToUserAsync(ApplicationUser user, string roleName)
        {
            try
            {
                await userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<IdentityResult> AddUserAsync(ApplicationUser user, string password)
        {
            try
            {
                return await userManager.CreateAsync(user, password);
            }
            catch (Exception)
            {
                return IdentityResult.Failed();
            }
        }

        public async Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string oldPassword, string newPassword)
        {
            return await userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public async Task<bool> CheckRoleAsync(string v)
        {
            bool roleExist = await roleManager.RoleExistsAsync(v);

            if (!roleExist)
            {
                await roleManager.CreateAsync(new IdentityRole
                {
                    Name = v
                });

                return true;
            }

            return roleExist;

        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public async Task<bool> IsUserInRoleAsync(ApplicationUser user, string roleName)
        {
            return await userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel loginViewModel)
        {
            /*en este caso el parametro false significa que no nay bloqueo en caso de equivocarse al digitar mal varias veces el password o usuario*/
            return await signInManager.PasswordSignInAsync(loginViewModel.NombreUsuario,
                loginViewModel.Contrasenia, loginViewModel.Recordatorio, false);
        }

        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> UpdateUserAsync(ApplicationUser user)
        {
            return await userManager.UpdateAsync(user);
        }

        public async Task<SignInResult> ValidatePasswordAsync(ApplicationUser user, string password)
        {
            /*Similar al meotdo LoginAsync anterior, en cuanto al parametro false*/
            /*en este caso el parametro false significa que no nay bloqueo en caso de equivocarse al digitar mal varias veces el password o usuario*/
            return await signInManager.CheckPasswordSignInAsync(user, password, false);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token)
        {
            return await userManager.ConfirmEmailAsync(user, token);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            return await userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await userManager.FindByIdAsync(userId);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user)
        {
            return await userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token, string password) /*se pasa el usuario, el token y el nuevo password*/
        {
            return await userManager.ResetPasswordAsync(user, token, password);
        }

        public async Task<List<Usuario>> GetAllUsersAsync()
        {
            List<ApplicationUser> listado = await userManager.Users
                .OrderBy(u => u.Nombre)
                .ToListAsync();

            List<Usuario> nueva = listado.Select(x => new Usuario
            {
                NombreCompleto = $"{x.Nombre} {x.Apellidos}",
                Correo = x.UserName,
                Confirmacion = x.EmailConfirmed ? "Si" : "No",
                Identificacion = x.Identificacion,
                Bloqueado = x.LockoutEnd > DateTime.Now.ToLocalTime() ? "Si" : "No"
            }).ToList();

            return nueva;
        }

        public async Task RemoveUserFromRoleAsync(ApplicationUser user, string roleName)
        {
            await userManager.RemoveFromRoleAsync(user, roleName); /*quitarle un rol determinado a un usuario*/
        }

        public async Task DeleteUserAsync(ApplicationUser user)
        {
            await userManager.DeleteAsync(user);
        }

        /*Metodos de bloqueo y desbloqueo*/

        public async Task<bool> BloquearUsuario(string idUsuario)
        {
            try
            {
                ApplicationUser usuarioDesdeBD = await GetUserByEmailAsync(idUsuario);
                usuarioDesdeBD.LockoutEnd = DateTime.Now.AddYears(100);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DesbloquearUsuario(string idUsuario)
        {
            try
            {
                ApplicationUser usuarioDesdeBD = await GetUserByEmailAsync(idUsuario);
                usuarioDesdeBD.LockoutEnd = DateTime.Now;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
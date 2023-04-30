using AccesoDatos.Data.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Modelos.Entidades;
using Modelos.ViewModels;
using Modelos.ViewModels.Usuario;
using SitioWeb.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitioWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly IContenedorTrabajo contenedorTrabajo;
        private readonly IMailHelper mailHelper;

        public AccountController(IContenedorTrabajo contenedorTrabajo, IMailHelper mailHelper)
        {
            this.contenedorTrabajo = contenedorTrabajo;
            this.mailHelper = mailHelper;
        }

        [HttpGet]
        public IActionResult NotAuthorized()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await contenedorTrabajo.Usuario.LoginAsync(model);
                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }

                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }

                ModelState.AddModelError(string.Empty, "Correo o contraseña incorrecta");
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await contenedorTrabajo.Usuario.LogoutAsync();
            return RedirectToAction("Index", "Home", new { area = "Cliente" });
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {

                string usuario = User.Identity.Name;

                ApplicationUser user = await contenedorTrabajo.Usuario.GetUserByEmailAsync(usuario);

                if (user != null)
                {
                    IdentityResult result = await contenedorTrabajo.Usuario.ChangePasswordAsync(user, model.ViejaContrasenia, model.NuevaContrasenia);
                    if (result.Succeeded)
                    {
                        ViewBag.Mensaje = "OK";
                    }
                    else
                    {
                        ViewBag.Mensaje = "Fallo";
                    }
                }
                else
                {
                    ViewBag.Mensaje = "No existe";
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Administradores()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerAdministradores()
        {
            List<Usuario> listadoUsuarios = await contenedorTrabajo.Usuario.GetAllUsersAsync();

            return Json(new { data = listadoUsuarios });
        }

        [HttpPost]
        public async Task<IActionResult> CambiarEstadoAdministrador(EditarUsuarioSistemaViewModel model)
        {
            if (model.Activa)
            {
                bool response = await contenedorTrabajo.Usuario.BloquearUsuario(model.Correo);
                if (response)
                {
                    contenedorTrabajo.Save();
                    return Json(new { success = true });
                }
            }
            else
            {
                bool response = await contenedorTrabajo.Usuario.DesbloquearUsuario(model.Correo);
                if (response)
                {
                    contenedorTrabajo.Save();
                    return Json(new { success = true });
                }
            }

            return Json(new { success = false });

        }

        [HttpPost]
        public async Task<IActionResult> RegistrarAdministrador(RegistrarUsuarioSistemaViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await contenedorTrabajo.Usuario.GetUserByEmailAsync(model.CorreoElectronico);

                if (user == null)
                {

                    user = new ApplicationUser
                    {
                        Nombre = model.Nombre,
                        Apellidos = $"{model.Apellido1} {model.Apellido2}",
                        Email = model.CorreoElectronico,
                        UserName = model.CorreoElectronico,
                        Identificacion = model.Identificacion,
                        LockoutEnd = DateTime.Now.ToLocalTime()
                    };

                    IdentityResult result = await contenedorTrabajo.Usuario.AddUserAsync(user, model.NuevaContrasenia);
                    if (result != IdentityResult.Success)
                    {
                        return Json(new { success = false, message = "Error" });
                    }

                    bool response = await contenedorTrabajo.Usuario.AddRoleToUserAsync(user, "Admin");
                    if (!response)
                    {
                        return Json(new { success = false, message = "Error" });
                    }

                    string myToken = await contenedorTrabajo.Usuario.GenerateEmailConfirmationTokenAsync(user);

                    string tokenLink = Url.Action("ConfirmarCorreo", "Home", new
                    {
                        area = "Cliente",
                        userid = user.Id,
                        token = myToken,
                    }, protocol: HttpContext.Request.Scheme);

                    mailHelper.SendMail(model.CorreoElectronico, string.Empty, "Confirmación de cuenta Sol y Luna", $"<h1>Confirmación de cuenta Sol y Luna</h1>" +
                        $"Para acceder al sitio, " +
                        $"por favor haga click en el siguiente enlace :</br> </br> <a href = \"{tokenLink}\"> Confirmar Cuenta </a>");

                    return Json(new { success = true, message = "OK" });
                }

                return Json(new { success = false, message = "Ya existe" });
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditarAdministrador(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new NotFoundViewResult("");
            }

            ApplicationUser administrador = await contenedorTrabajo.Usuario.GetUserByEmailAsync(id);

            if (administrador != null)
            {
                EditarUsuarioSistemaViewModel model = new()
                {

                    Nombre = administrador.Nombre.Trim(),
                    Apellidos = administrador.Apellidos.Trim(),
                    Identificacion = administrador.Identificacion,
                    Correo = administrador.UserName,
                    Activa = administrador.LockoutEnd > DateTime.Now.ToLocalTime()
                };

                return View(model);
            }

            return new NotFoundViewResult("");

        }

        [HttpPost]
        public async Task<IActionResult> EditarAdministrador(EditarUsuarioSistemaViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await contenedorTrabajo.Usuario.GetUserByEmailAsync(model.Correo);

                if (user != null)
                {
                    user.Nombre = model.Nombre;
                    user.Apellidos = model.Apellidos;
                    user.Identificacion = model.Identificacion;

                    IdentityResult resultado = await contenedorTrabajo.Usuario.UpdateUserAsync(user);

                    if (resultado.Succeeded)
                    {
                        return Json(new { success = true, message = "OK" });
                    }

                }

                return Json(new { success = false });
            }

            return View(model);

        }

        public IActionResult RecoverPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser user = await contenedorTrabajo.Usuario.GetUserByEmailAsync(model.Email);

                    if (user == null)
                    {
                        return Json(new { success = false, message = "No existe" });
                    }

                    string myToken = await contenedorTrabajo.Usuario.GeneratePasswordResetTokenAsync(user);
                    string link = Url.Action("ResetPassword", "Account",
                        new
                        {
                            area = "Admin",
                            token = myToken
                        }
                        , protocol: HttpContext.Request.Scheme);

                    mailHelper.SendMail(model.Email, string.Empty, "Recuperación de contraseña", $"<h1>Recuperación de contraseña para cuenta Sol y Luna</h1>" +
                           $"Para recuperar su contraseña, " +
                           $"por favor haga click en el siguiente enlace :</br> </br> <a href = \"{link}\"> Recuperar contraseña </a>");

                    return Json(new { success = true, message = "OK" }); ;

                }

                return View(model);
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Error" }); ;
            }
        }

        public IActionResult ResetPassword(string token)
        {
            return View(new ResetPasswordViewModel() { Token = token });
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            ApplicationUser user = await contenedorTrabajo.Usuario.GetUserByEmailAsync(model.UserName);

            if (user != null)
            {
                IdentityResult result = await contenedorTrabajo.Usuario.ResetPasswordAsync(user, model.Token, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Recuperacion", "Home", new { area = "Cliente" });
                }

                return View();
            }

            return Json(new { success = false, message = "No encontrado" });

        }

    }
}

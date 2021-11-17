using System;
using System.Linq;
using System.Security.Claims;
using tp_nt1.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tp_nt1.Database;
using tp_nt1.Models.Enums;
using tp_nt1.Extensions;

namespace tp_nt1.Controllers
{
    [AllowAnonymous]
    public class AccesosController : Controller
    {
        private readonly HistoriaClinicaDbContext _context;
        private const string _Return_Url = "ReturnUrl";

        public AccesosController(HistoriaClinicaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Ingresar(string returnUrl)
        {

            TempData[_Return_Url] = returnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult Ingresar(string username, string password, Rol rol)
        {
            string returnUrl = TempData[_Return_Url] as string;

            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            {
                Usuario usuario = null;

                if (rol == Rol.Empleado)
                {
                    usuario = _context.Empleados.FirstOrDefault(empleado => empleado.NombreUsuario == username);
                }
                else if (rol == Rol.Paciente)
                {
                    usuario = _context.Pacientes.FirstOrDefault(paciente => paciente.NombreUsuario == username);
                }
                else if (rol == Rol.Profesional)
                {
                    usuario = _context.Profesionales.FirstOrDefault(profesional => profesional.NombreUsuario == username);
                }

                if (usuario != null)
                {
                    var passwordEncriptada = password.Encriptar();

                    if (usuario.Password.SequenceEqual(passwordEncriptada))
                    {
                        ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                        identity.AddClaim(new Claim(ClaimTypes.Name, username));

                        identity.AddClaim(new Claim(ClaimTypes.Role, usuario.Rol.ToString()));

                        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()));

                        identity.AddClaim(new Claim(ClaimTypes.GivenName, usuario.Nombre));

                        ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal).Wait();

                        TempData["LoggedIn"] = true;


                        if (rol == Rol.Empleado)
                        {
                            return RedirectToAction(nameof(HomeController.Index), "Empleados");
                        }
                        else if (rol == Rol.Profesional)
                        {
                            return RedirectToAction(nameof(HomeController.Index), "Pacientes");
                        }
                        else if (rol == Rol.Paciente)
                        {
                            return RedirectToAction("MiHistoriaClinica", "HistoriasClinicas");
                        }
                        else
                        {
                            return RedirectToAction(nameof(HomeController.Index), "Home");
                        }
                    }
                }
            }

            if (rol != Rol.Empleado && rol != Rol.Paciente && rol != Rol.Profesional)
            {
                ViewBag.Error = "El Rol no fue seleccionado";
            }
            else
            {
                ViewBag.Error = "Usuario o contraseña incorrectos";
            }
            ViewBag.UserName = username;
            TempData[_Return_Url] = returnUrl;

            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Salir()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [Authorize]
        [HttpGet]
        public IActionResult NoAutorizado()
        {
            return View();
        }
    }

}


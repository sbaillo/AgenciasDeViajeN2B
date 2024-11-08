using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class UsuariosController : Controller
    {
        Sistema miSistema = Sistema.Instancia;

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string pass)
        {
            try
            {
                if (string.IsNullOrEmpty(email)) throw new Exception("Debe ingresar un email");
                if (string.IsNullOrEmpty(pass)) throw new Exception("Debe ingresar una contraseña");
                Usuario usuario = miSistema.Login(email, pass);
                if (usuario == null) throw new Exception("Email o contraseña incorrectas");

                //Declaracion de variables de session
                HttpContext.Session.SetString("email", email);
                HttpContext.Session.SetString("rol", usuario.Rol());

                return RedirectToAction("Index", "Home");

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult CambiarPass()
        {
            if(HttpContext.Session.GetString("email") == null)
            {
                return View("NoAutorizado");
            }

            return View();
        }

        [HttpPost]
        public IActionResult CambiarPass(string passNueva, string passRepetida)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return View("NoAutorizado");
            }

            try
            {
                if (string.IsNullOrEmpty(passNueva)) throw new Exception("Debe ingresar una contraseña nueva");
                if (string.IsNullOrEmpty(passRepetida)) throw new Exception("Debe repetira la contraseña nueva");
                if (passNueva != passRepetida) throw new Exception("Las contraseñas no coinciden");
                miSistema.CambiarPassDeUsuario(HttpContext.Session.GetString("email"), passNueva);
                ViewBag.Exito = "Password cambiada correctamente";

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View();
        }
    }
}

using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class DestinosController : Controller
    {
        Sistema miSistema = Sistema.Instancia;
        public IActionResult Listado()
        {
            if (HttpContext.Session.GetString("rol") == null)
            {
                return View("NoAutorizado");
            }

            if (TempData["Exito"] != null) ViewBag.Exito = TempData["Exito"];
            if (TempData["Error"] != null) ViewBag.Error = TempData["Error"];

            ViewBag.Listado = miSistema.Destinos;
            return View();
        }

        public IActionResult Detalle(string codigo)
        {
            if (HttpContext.Session.GetString("rol") == null)
            {
                return View("NoAutorizado");
            }

            Destino buscado = miSistema.ObtenerDestinoPorId(codigo);
            ViewBag.Destino = buscado;
            return View();
        }

        [HttpGet]
        public IActionResult CambiarPrecio()
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Admin")
            {
                return View("NoAutorizado");
            }

            return View();
        }

        [HttpPost]
        public IActionResult CambiarPrecio(string idDestino, double nuevoPrecio)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Admin")
            {
                return View("NoAutorizado");
            }

            try
            {
                if (string.IsNullOrEmpty(idDestino)) throw new Exception("El id del destino no es valido");
                if (nuevoPrecio < 0) throw new Exception("El precio no puede ser negativo");
                miSistema.CambiarPrecioDeDestino(idDestino, nuevoPrecio);
                ViewBag.Exito = $"Se cambió el precio de destino {idDestino} - Nuevo precio: ${nuevoPrecio}";
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View();
        }

        [HttpGet]
        public IActionResult CambiarPrecioAlt(string codigo)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Admin")
            {
                return View("NoAutorizado");
            }

            ViewBag.Codigo = codigo;
            return View();
        }

        [HttpPost]
        public IActionResult CambiarPrecioAlt(string codigo, double nuevoPrecio)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Admin")
            {
                return View("NoAutorizado");
            }

            try
            {
                if (string.IsNullOrEmpty(codigo)) throw new Exception("El id del destino no es valido");
                if (nuevoPrecio < 0) throw new Exception("El precio no puede ser negativo");
                miSistema.CambiarPrecioDeDestino(codigo, nuevoPrecio);
                TempData["Exito"] = $"Se cambió el precio de destino {codigo} - Nuevo precio: ${nuevoPrecio}";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Listado");
        }
    }
}

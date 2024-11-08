using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class PaquetesController : Controller
    {
        Sistema miSistema = Sistema.Instancia;

        [HttpGet]
        public IActionResult Alta()
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Admin")
            {
                return View("NoAutorizado");
            }

            return View(new Paquete());
        }

        [HttpPost]
        public IActionResult Alta(Paquete p, int idAgencia)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Admin")
            {
                return View("NoAutorizado");
            }

            try
            {
                Agencia agencia = miSistema.ObtenerAgenciaPorId(idAgencia);
                if (agencia == null) throw new Exception("La agencia no puede ser nula");
                p.Agencia = agencia;
                miSistema.AltaPaquete(p);
                ViewBag.Exito = $"Paquete de la agencia {agencia.Nombre} ingresado correctamante para el dia {p.Fecha.ToShortDateString()}";
                return View(new Paquete());
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(p);
            }
            
        }
    }
}

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
            return View(new Paquete());
        }

        [HttpPost]
        public IActionResult Alta(Paquete p, int idAgencia)
        {
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

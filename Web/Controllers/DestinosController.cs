using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class DestinosController : Controller
    {
        public IActionResult Listado()
        {
            ViewBag.Listado = Sistema.Instancia.Destinos;
            return View();
        }

        public IActionResult Detalle(string codigo)
        {
            Destino buscado = Sistema.Instancia.ObtenerDestinoPorId(codigo);
            ViewBag.Destino = buscado;
            return View();
        }

    }
}

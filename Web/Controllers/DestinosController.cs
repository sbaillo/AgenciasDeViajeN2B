using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class DestinosController : Controller
    {
        Sistema miSistema = Sistema.Instancia;
        public IActionResult Listado()
        {
            ViewBag.Listado = miSistema.Destinos;
            return View();
        }

        public IActionResult Detalle(string codigo)
        {
            Destino buscado = miSistema.ObtenerDestinoPorId(codigo);
            ViewBag.Destino = buscado;
            return View();
        }

        [HttpGet]
        public IActionResult CambiarPrecio()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CambiarPrecio(string idDestino, double nuevoPrecio)
        {
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
    }
}

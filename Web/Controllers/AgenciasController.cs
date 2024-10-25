using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Web.Controllers
{
    public class AgenciasController : Controller
    {
        private Sistema miSistema = Sistema.Instancia;

        public IActionResult Listado()
        {
            ViewBag.Listado = miSistema.Agencias;
            //ViewBag.Listado = Sistema.Instancia.Agencias;
            return View();
        }

        public IActionResult PorPais()
        {
            //ViewBag.Listado = miSistema.Agencias;
            return View();
        }
        
        public IActionResult ProcesarPorPais(string pais)
        {
            try
            {
                if (string.IsNullOrEmpty(pais)) throw new Exception("Debe ingresar el nombre de un pais");
                ViewBag.Listado = miSistema.AgenciasPorPais(pais);
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            
            return View("PorPais");
        }

        public IActionResult IrAOrt()
        {
            return Redirect("https://www.ort.edu.uy");
        }
    }
}

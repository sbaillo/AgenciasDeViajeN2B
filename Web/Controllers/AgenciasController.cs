using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Web.Controllers
{
    public class AgenciasController : Controller
    {
        private Sistema miSistema = Sistema.Instancia;

        [HttpGet]
        public IActionResult Listado()
        {
            ViewBag.Listado = miSistema.Agencias;
            if (TempData["Exito"] != null) ViewBag.Exito = TempData["Exito"];
            return View();
        }

        [HttpGet]
        public IActionResult PorPais()
        {
            //ViewBag.Listado = miSistema.Agencias;
            return View();
        }

        [HttpPost]
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

        [HttpGet]
        public IActionResult AltaAgenciaNacional()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AltaAgenciaNacional(string nombre, string pais, string rut, int anio)
        {
            try
            {
                if (string.IsNullOrEmpty(nombre)) throw new Exception("El nombre no puede ser vacio");
                if (string.IsNullOrEmpty(pais)) throw new Exception("El pais no puede ser vacio");
                if (string.IsNullOrEmpty(rut)) throw new Exception("El RUT no puede ser vacio");
                if (anio <= 0) throw new Exception("El año debe ser positivo");

                Agencia a = new Nacional(nombre, pais, rut, anio);
                miSistema.AltaAgencia(a);

                ViewBag.Exito = $"Agencia {nombre} dada de alta con exito";
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Nombre = nombre;
                ViewBag.Pais = pais;
                ViewBag.Rut = rut;
                ViewBag.Anio = anio;
            }

            return View();
        }

        [HttpGet]
        public IActionResult AltaAgenciaExtranjera()
        {
            return View(new Extranjera());
        }

        [HttpPost]
        public IActionResult AltaAgenciaExtranjera(Extranjera agencia)
        {
            try
            {
                //Podria validar los atributos de la agencia si quisiera
                if (string.IsNullOrEmpty(agencia.Nombre)) throw new Exception("El nombre no puede ser vacio");
                if (string.IsNullOrEmpty(agencia.Pais)) throw new Exception("El pais no puede ser vacio");
                if (agencia.Calificacion < 1) throw new Exception("La calificacion no puede ser menor a 1");

                miSistema.AltaAgencia(agencia);
                TempData["Exito"] = $"Agencia {agencia.Nombre} dada de alta correctamente";
                return RedirectToAction("Listado");
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(agencia);
            }
            
        }
    }
}

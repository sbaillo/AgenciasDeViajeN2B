using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Nacional : Agencia
    {
        private string _rut;
        private int _anio;

        public Nacional(string nombre, string pais, string rut, int anio):base(nombre, pais)
        {
            _rut = rut;
            _anio = anio;
        }

        public override void Validar()
        {
            base.Validar();
            if (string.IsNullOrEmpty(_rut)) throw new Exception("El RUT no puede ser vacio");
            if (_anio < 1900) throw new Exception("El año debe ser mayor a 1900");
        }
    }
}

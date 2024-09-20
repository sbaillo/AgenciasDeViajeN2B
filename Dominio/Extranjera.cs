using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    internal class Extranjera : Agencia
    {
        private int _calificacion;

        public Extranjera(string nombre, string pais, int calificacion):base(nombre,pais)
        {
            _calificacion = calificacion;
        }

        private void ValidarCalificacion()
        {
            if (_calificacion < 1 || _calificacion > 5) throw new Exception("La calificacion debe estar entre 1 y 5");
        }

        public override void Validar()
        {
            base.Validar();
            ValidarCalificacion();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Extranjera : Agencia
    {
        private int _calificacion;

        public Extranjera(string nombre, string pais, int calificacion):base(nombre,pais)
        {
            _calificacion = calificacion;
        }

        public Extranjera():base()
        {

        }

        public int Calificacion
        {
            get { return _calificacion; }
            set { _calificacion = value;}
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

        public override double DevolverPorcentajeDescuento()
        {
            double descuento = 5;
            if (_calificacion > 3) descuento = 8;
            return descuento;
        }

        public override double DevolverPrecioConDescuentoAplicado(double subTotal)
        {
            double total = subTotal *= 0.95;
            if (_calificacion > 3) total = subTotal *= 0.92;
            return total;
        }
    }
}

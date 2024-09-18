using Dominio.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    internal class Paquete : IValidable
    {
        private static int s_ultId = 1;
        private int _id;
        private DateTime _fecha = new DateTime(); //01/01/00001
        private Agencia _agencia;
        private static double s_costoBase = 400;
        private List<PaqueteDestino> _destinos = new List<PaqueteDestino>();

        public Paquete(DateTime fecha, Agencia agencia)
        {
            _id = s_ultId;
            s_ultId++;
            _fecha = fecha;
            _agencia = agencia;
        }

        public void Validar()
        {
            if (_agencia == null) throw new Exception("La agencia no puede ser nula");
            if (_fecha.Date == new DateTime().Date) throw new Exception("La fecha no es valida");
        }

        public double CalcularTotal()
        {
            double total = s_costoBase;
            foreach(PaqueteDestino pd in _destinos)
            {
                total += pd.CalcularSubtotal();
            }

            return total;
        }
    }
}

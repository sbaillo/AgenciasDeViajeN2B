using Dominio.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Paquete : IValidable
    {
        private static int s_ultId = 1;
        private int _id;
        private DateTime _fecha = new DateTime(); //01/01/00001
        private Agencia _agencia;
        private static double s_costoBase = 400;
        private List<PaqueteDestino> _destinos = new List<PaqueteDestino>();
        private double _precioCalculado = 0;

        public Paquete(DateTime fecha, Agencia agencia)
        {
            _id = s_ultId;
            s_ultId++;
            _fecha = fecha;
            _agencia = agencia;
        }

        public Paquete()
        {
            _id = s_ultId;
            s_ultId++;
        }

        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        public Agencia Agencia
        {
            get { return _agencia; }
            set { _agencia = value; }
        }

        public int Id
        {
            get { return _id; }
        }

        public void Validar()
        {
            if (_agencia == null) throw new Exception("La agencia no puede ser nula");
            if (_fecha.Date == new DateTime().Date) throw new Exception("La fecha no es valida");
        }

        public double CalcularPrecioFinal()
        {
            if(_precioCalculado == 0)
            {
                double total = s_costoBase;
                foreach (PaqueteDestino pd in _destinos)
                {
                    total += pd.CalcularSubtotal();
                }

                //Le pido a la agencia que me devuelva el porcentaje a aplicar
                double descuento = _agencia.DevolverPorcentajeDescuento();
                total -= total * descuento / 100;

                //Esto seria utilizando la otra variante
                //_precioCalculado = _agencia.DevolverPrecioConDescuentoAplicado(total);

                _precioCalculado = total;
            }

            return _precioCalculado;
        }

        public void AltaDestino(PaqueteDestino pd)
        {
            if (pd == null) throw new Exception("El paquete-destino no puede ser nulo");
            pd.Validar();
            if (_destinos.Contains(pd)) throw new Exception("Ya existe el destino en el paquete");
            _destinos.Add(pd);
        }

        public int CalcularTotalDias()
        {
            int total = 0;
            foreach(PaqueteDestino pd in _destinos)
            {
                total =+ pd.Dias;
            }
            return total;
        }

        public bool ContieneDestino(Destino d)
        {
            bool contiene = false;
            int i = 0;
            while(i < _destinos.Count && !contiene) 
            {
                if (_destinos[i].Destino.Equals(d)) contiene = true;
                i++;
            }

            return contiene;
        }
    }
}

using Dominio.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class PaqueteDestino : IValidable
    {
        private Destino _destino;
        private int _dias;

        public PaqueteDestino(Destino destino, int dias)
        {
            _destino = destino;
            _dias = dias;
        }

        public int Dias
        {
            get { return _dias; }
        }

        public Destino Destino
        {
            get { return _destino; }
        }

        public void Validar()
        {
            if (_destino == null) throw new Exception("El destino no puede ser nulo");
            ValidarDias();
        }

        private void ValidarDias()
        {
            if (_dias < 1) throw new Exception("No puede haber destinos con menos de 1 dia");
        }

        public double CalcularSubtotal()
        {
            return _dias * _destino.PrecioPorDia;
        }

        public override bool Equals(object? obj)
        {
            PaqueteDestino pd = obj as PaqueteDestino;
            return pd != null && pd._destino.Equals(_destino);
        }
    }
}

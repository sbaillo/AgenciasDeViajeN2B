using Dominio.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public abstract class Agencia : IValidable
    {
        protected int _id;
        protected static int s_ultId = 1;
        protected string _nombre;
        protected string _pais;

        public Agencia(string nombre, string pais)
        {
            _id = s_ultId;
            s_ultId++;
            _nombre = nombre;
            _pais = pais;
        }

        public int Id 
        {  
            get { return _id; } 
        }

        public string Pais
        { 
            get { return _pais; } 
        }

        public virtual void Validar()
        {
            if (string.IsNullOrEmpty(_pais)) throw new Exception("El pais no puede ser vacio");
            if (string.IsNullOrEmpty(_nombre)) throw new Exception("El nombre no puede ser vacio");
        }

        public override string ToString()
        {
            return $"{_nombre} - Pais: {_pais}";
        }

        public abstract double DevolverPorcentajeDescuento();

        public abstract double DevolverPrecioConDescuentoAplicado(double subTotal);
    }
}

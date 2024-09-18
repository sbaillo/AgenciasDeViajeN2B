using Dominio.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    internal class Destino : IValidable
    {
        private string _codigo;
        private string _nombre;
        private string _descripcion;
        private double _precioPorDia;
        private TipoDestino _tipo;

        public Destino(string codigo, string nombre, string descripcion, double precioPorDia, TipoDestino tipo)
        {
            _codigo = codigo;
            _nombre = nombre;
            _descripcion = descripcion;
            _precioPorDia = precioPorDia;
            _tipo = tipo;
        }

        public double PrecioPorDia
        {
            get { return _precioPorDia; }
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(_nombre)) throw new Exception("El nombre no puede ser vacio");
            if (string.IsNullOrEmpty(_descripcion)) throw new Exception("La descripcion no puede ser vacia");
            if (_precioPorDia <= 0) throw new Exception("El precio debe ser mayor a 0");
            ValidarCodigo();
        }

        public void ValidarCodigo()
        {
            if (string.IsNullOrEmpty(_codigo) || _codigo.Length != 8) throw new Exception("El codigo debe tener un largo de 8 caracteres");
        }
    }
}

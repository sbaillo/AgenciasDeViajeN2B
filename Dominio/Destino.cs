using Dominio.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Destino : IValidable
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

        public string Codigo
        {
            get { return _codigo; }
        }

        public string Nombre
        {
            get { return _nombre; }
        }

        public string Descripcion
        {
            get { return _descripcion; }
        }

        public TipoDestino Tipo
        {
            get { return _tipo; }
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

        public override bool Equals(object obj)
        {
            Destino destino = obj as Destino;
            return destino != null && this._codigo == destino._codigo;
        }

        public void CambiarPrecio(double nuevoPrecio)
        {
            if (nuevoPrecio <= 0) throw new Exception("El precio debe ser mayor a 0");
            _precioPorDia = nuevoPrecio;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Cliente : Usuario
    {
        public Destino DestinoFavorito { get; set; }

        public Cliente(string nombre, string email, string pass, Destino destinoFavorito):base(nombre, email, pass)
        {
            DestinoFavorito = destinoFavorito;
        }

        public override string Rol()
        {
            return "Cliente";
        }
    }
}

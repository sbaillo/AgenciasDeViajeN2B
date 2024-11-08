using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Admin : Usuario
    {
        public string Pin { get; set; }

        public Admin(string nombre, string email, string pass, string pin):base(nombre, email, pass)
        {
            Pin = pin;
        }

        public override void Validar()
        {
            base.Validar();
            if (string.IsNullOrEmpty(Pin)) throw new Exception("El pin no puede ser vacio");
        }

        public override string Rol()
        {
            return "Admin";
        }
    }
}

using Dominio.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public abstract class Usuario : IValidable
    {
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }

        public Usuario(string nombre, string email, string pass)
        {
            Nombre = nombre;
            Email = email;
            Pass = pass;
        }

        public virtual void Validar()
        {
            if (string.IsNullOrEmpty(Email)) throw new Exception("Email no puede ser vacio");
            if (string.IsNullOrEmpty(Pass) || Pass.Length < 4) throw new Exception("La contraseña debe tener al menos 4 caracteres");
            if (string.IsNullOrEmpty(Nombre)) throw new Exception("El nombre no puede ser vacio");
        }

        public abstract string Rol();

        public void CambiarPass(string passNueva)
        {
            if (string.IsNullOrEmpty(passNueva) || Pass.Length < 4) throw new Exception("La contraseña nueva debe tener al menor 4 caracteres");
            if (Pass == passNueva) throw new Exception("No puede repetir la misma");
            Pass = passNueva;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Sistema
    {
        //PRIMER PASO SINGLETON: CREAR UN ATRIBUTO ESTATICO DONDE GUARDAREMOS LA UNICA INSTANCIA DE SISTEMA QUE VA A EXISTIR
        private static Sistema s_instancia; 

        private List<Agencia> _agencias = new List<Agencia>();
        private List<Destino> _destinos = new List<Destino>();
        private List<Paquete> _paquetes = new List<Paquete>();


        //SEGUNDO PASO SINGLETON: HACER PRIVADO EL CONSTRUCTOR DE SISTEMA
        private Sistema() 
        {
            PrecargarAgencias();
            PrecargarDestinos();
            PrecargarPaquetes();
            PrecargarDestinosAPaquetes();
        }

        //TERCER PASO SINGLETON: HAGO LA PROPERTY ESTATICA DEL ATRIBUTO. SI EL ATRIBUTO ES NULO, ME GENERO UN NUEVO OBJETO DE TIPO SISTEMA
        //Y SIEMPRE RETORNO ESE OBJETO GUARDADO EN EL ATRIBUTO
        public static Sistema Instancia
        {
            get
            {
                if( s_instancia == null ) s_instancia = new Sistema();
                return s_instancia;
            }
        }

        public List<Agencia> Agencias
        {
            get { return _agencias; }
        }

        public List<Destino> Destinos
        {
            get { return _destinos; }
        }

        public void AltaAgencia(Agencia agencia)
        {
            if (agencia == null) throw new Exception("La agencia no puede ser nula");
            agencia.Validar();
            _agencias.Add(agencia);
        }

        private void PrecargarAgencias()
        {
            AltaAgencia(new Nacional("Rutatur", "Uruguay", "1518918989", 2000));
            AltaAgencia(new Nacional("Jetmar", "Uruguay", "1855188977", 1998));
            AltaAgencia(new Extranjera("Tudo viajem", "Brasil", 4));
            AltaAgencia(new Extranjera("Paseos Botafogo", "Brasil", 5));
            AltaAgencia(new Nacional("Urutur", "Uruguay", "5155118189", 1999));
        }

        public void AltaDestino(Destino destino)
        {
            if (destino == null) throw new Exception("El destino no puede ser nulo");
            destino.Validar();
            if (_destinos.Contains(destino)) throw new Exception("Ya existe ese destino en el sistema");
            _destinos.Add(destino);
        }

        private void PrecargarDestinos()
        {
            AltaDestino(new Destino("ABCD1234", "Destino 1", "Que buen destino", 15, TipoDestino.LOCALIDAD));
            AltaDestino(new Destino("ABCD1235", "Destino 2", "Que buen destino", 18, TipoDestino.PUNTOINTERES));
            AltaDestino(new Destino("ABCD1236", "Destino 3", "Que buen destino", 10, TipoDestino.ACTIVIDAD));
            AltaDestino(new Destino("ABCD1237", "Destino 4", "Que buen destino", 80, TipoDestino.LOCALIDAD));
            AltaDestino(new Destino("ABCD1238", "Destino 5", "Que buen destino", 50, TipoDestino.PUNTOINTERES));
            AltaDestino(new Destino("ABCD1239", "Destino 6", "Que buen destino", 40, TipoDestino.ACTIVIDAD));
        }

        public void AltaPaquete(Paquete paquete)
        {
            if (paquete == null) throw new Exception("El paquete no puede ser nulo");
            paquete.Validar();
            _paquetes.Add(paquete);
        }

        private void PrecargarPaquetes()
        {
            AltaPaquete(new Paquete(new DateTime(2024, 12, 20), ObtenerAgenciaPorId(1)));
            AltaPaquete(new Paquete(new DateTime(2025, 11, 10), ObtenerAgenciaPorId(1)));
            AltaPaquete(new Paquete(new DateTime(2023, 1, 2), ObtenerAgenciaPorId(2)));
            AltaPaquete(new Paquete(new DateTime(2024, 10, 27), ObtenerAgenciaPorId(3)));
        }

        public Agencia ObtenerAgenciaPorId(int id)
        {
            Agencia buscada = null;
            int i = 0;
            while(i < _agencias.Count && buscada == null)
            {
                if (_agencias[i].Id == id) buscada = _agencias[i];
                i++;
            }

            return buscada;
        }

        public Destino ObtenerDestinoPorId(string id)
        {
            Destino buscado = null;
            int i = 0;
            while(i <_destinos.Count && buscado == null)
            {
                if (_destinos[i].Codigo == id) buscado = _destinos[i];
                i++;
            }

            return buscado;
        }

        public Paquete ObtenerPaquetePorId(int id)
        {
            Paquete buscado = null;
            int i = 0;
            while(i < _paquetes.Count && buscado == null)
            {
                if (_paquetes[i].Id == id) buscado = _paquetes[i];
                i++;
            }

            return buscado;
        }

        public void AgregarDestinoAUnPaquete(int idPaquete, string idDestino, int dias) 
        {
            Paquete paqueteBuscado = ObtenerPaquetePorId(idPaquete);
            if (paqueteBuscado == null) throw new Exception("El id de paquete no corresponde a ningun paquete del sistema");
            Destino destinoBuscado = ObtenerDestinoPorId(idDestino);
            if (destinoBuscado == null) throw new Exception("El destino no se encontró");
            PaqueteDestino pd = new PaqueteDestino(destinoBuscado, dias);
            paqueteBuscado.AltaDestino(pd);
        }

        private void PrecargarDestinosAPaquetes() 
        {
            //Agrego destinos al paquete 1
            AgregarDestinoAUnPaquete(1, "ABCD1234", 1);
            AgregarDestinoAUnPaquete(1, "ABCD1235", 2);
            AgregarDestinoAUnPaquete(1, "ABCD1236", 5);

            //Agrego destinos al paquete 2
            AgregarDestinoAUnPaquete(2, "ABCD1237", 1);
            AgregarDestinoAUnPaquete(2, "ABCD1238", 2);

            //Agrego destinos al paquete 3
            AgregarDestinoAUnPaquete(3, "ABCD1234", 1);
            AgregarDestinoAUnPaquete(3, "ABCD1235", 2);
            AgregarDestinoAUnPaquete(3, "ABCD1239", 5);

            //Agrego destinos al paquete 4
            AgregarDestinoAUnPaquete(4, "ABCD1234", 7);
            AgregarDestinoAUnPaquete(4, "ABCD1235", 10);
        }

        public List<Agencia> AgenciasPorPais(string pais)
        {
            List<Agencia> buscadas = new List<Agencia>();
            foreach(Agencia a in _agencias)
            {
                if(a.Pais.ToUpper() == pais.ToUpper()) buscadas.Add(a);
            }

            return buscadas;
        }

        public List<Paquete> PaquetesConDuracionMayorA(int dia)
        {
            List<Paquete> buscados = new List<Paquete>();
            foreach(Paquete p in _paquetes)
            {
                if(p.CalcularTotalDias() >= dia) buscados.Add(p);
            }

            return buscados;
        }

        public List<Paquete> PaquetesMasCarosQueMonto(double monto)
        {
            List<Paquete> buscados = new List<Paquete>();
            foreach (Paquete p in _paquetes)
            {
                if (p.CalcularPrecioFinal() >= monto) buscados.Add(p);
            }

            return buscados;
        }

        public List<Paquete> PaquetesConDestino(string id)
        {
            Destino destinoBuscado = ObtenerDestinoPorId(id);
            List<Paquete> buscados = new List<Paquete>();
            foreach (Paquete p in _paquetes)
            {
                if (p.ContieneDestino(destinoBuscado)) buscados.Add(p);
            }

            return buscados;
        }

        public void CambiarPrecioDeDestino(string idDestino, double nuevoPrecio)
        {
            Destino d = ObtenerDestinoPorId(idDestino);
            if (d == null) throw new Exception("El destino no se encontró");
            d.CambiarPrecio(nuevoPrecio);
        }


    }
}

using Dominio;

namespace Consola
{
    internal class Program
    {
        private static Sistema miSistema;

        static void Main(string[] args)
        {
            miSistema = new Sistema();

            string opcion = "";

            while (opcion != "0")
            {
                MostrarMenu();
                opcion = PedirPalabras("Ingrese una opcion -> ");

                switch (opcion)
                {
                    case "1":
                        AltaDestino();
                        break;
                    case "2":
                        AltaPaquete();
                        break;
                    case "3":
                        AgregarDestinoAPaquete();
                        break;
                    case "4":
                        AgenciasPorPais();
                        break;
                    case "0":
                        Console.WriteLine("Saliendo ...");
                        break;
                    default:
                        Console.WriteLine("Opcion incorrecta");
                        break;
                }
            }
        }

        #region METODOS AUXILIARES

        static void MostrarMenu()
        {
            Console.Clear();
            MostrarMensajeColor(ConsoleColor.Cyan, "****************");
            MostrarMensajeColor(ConsoleColor.Cyan, "      MENU      ");
            MostrarMensajeColor(ConsoleColor.Cyan, "****************");
            Console.WriteLine();
            Console.WriteLine("1 - Alta destinos");
            Console.WriteLine("2 - Alta de paquetes");
            Console.WriteLine("3 - Agregar destinos al paquete");
            Console.WriteLine("4 - Agencias por pais");
            Console.WriteLine("0 - Salir");
        }

        /// <summary>
        /// Este metodo pide al usuario informacion y retorna un string con lo ingresado
        /// </summary>
        /// <param name="mensaje"></param>
        /// <returns></returns>
        static string PedirPalabras(string mensaje)
        {
            Console.Write(mensaje);
            string datos = Console.ReadLine();
            return datos;
        }

        static int PedirNumeros(string mensaje)
        {
            bool exito = false;
            int valorConvertido = 0;

            while (exito == false)
            {
                Console.Write(mensaje);
                exito = int.TryParse(Console.ReadLine(), out valorConvertido);

                if (exito == false)
                {
                    MostrarError("ERROR: Debe ingresar solo numeros");
                }
            }

            return valorConvertido;
        }

        static double PedirNumerosDouble(string mensaje)
        {
            bool exito = false;
            double valorConvertido = 0;

            while (exito == false)
            {
                Console.Write(mensaje);
                exito = double.TryParse(Console.ReadLine(), out valorConvertido);

                if (exito == false)
                {
                    MostrarError("ERROR: Debe ingresar solo numeros");
                }
            }

            return valorConvertido;
        }

        static bool PedirBooleano(string mensaje)
        {
            bool exito = false;
            bool dato = false;

            while (!exito)
            {
                Console.WriteLine($"{mensaje} [S/N]:");
                string datoString = Console.ReadLine();

                switch (datoString.ToUpper())
                {
                    case "S":
                        dato = true;
                        exito = true;
                        break;
                    case "N":
                        dato = false;
                        exito = true;
                        break;
                    default:
                        MostrarError("Opcion invalida");
                        break;
                }
            }

            return dato;
        }

        static DateTime PedirFecha(string mensaje)
        {
            bool exito = false;
            DateTime fecha = new DateTime();

            while (!exito)
            {
                Console.Write($"{mensaje} [dd/MM/yyyy]:");
                exito = DateTime.TryParse(Console.ReadLine(), out fecha);

                if (!exito)
                {
                    MostrarError("ERROR: La fecha no respeta el formato dd/MM/yyyy");
                }
            }

            return fecha;
        }

        static TipoDestino PedirTipoDestino()
        {
            bool exito = false;
            TipoDestino TipoDestino = new TipoDestino();

            while (!exito)
            {
                Console.WriteLine("Seleccione un tipo de destino:");
                foreach (int valor in Enum.GetValues(typeof(TipoDestino)))
                {
                    Console.WriteLine($"{valor} - {(TipoDestino)valor}");
                }

                string tipoString = Console.ReadLine();
                bool esNumero = int.TryParse(tipoString, out int tipoEntero);

                if (esNumero && Enum.IsDefined(typeof(TipoDestino), tipoEntero))
                {
                    TipoDestino = (TipoDestino)tipoEntero;
                    exito = true;
                }
                else
                {
                    MostrarError("Seleccione una opcion valida");
                }
            }

            return TipoDestino;
        }

        static void MostrarMensajeColor(ConsoleColor color1, string mensaje)
        {
            Console.ForegroundColor = color1;
            Console.WriteLine(mensaje);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        static void MostrarError(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(mensaje);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        static void MostrarExito(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(mensaje);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        static void PressToContinue()
        {
            Console.WriteLine();
            Console.WriteLine("Presione cualquier tecla para volver al menu...");
            Console.ReadKey();
        }

        #endregion

        private static void AltaDestino()
        {
            Console.Clear();
            MostrarMensajeColor(ConsoleColor.Yellow, "ALTA DE DESTINOS");
            Console.WriteLine();

            string id = PedirPalabras("Ingrese el Id del destino: ");
            string nombre = PedirPalabras("Ingrese nombre del destino: ");
            string desc = PedirPalabras("Ingrese descripcion: ");
            double precio = PedirNumerosDouble("Ingrese precio por dia: ");
            TipoDestino tipo = PedirTipoDestino();

            try
            {
                if (string.IsNullOrEmpty(id)) throw new Exception("El id no puede estar vacio");
                if (string.IsNullOrEmpty(nombre)) throw new Exception("El nombre no puede estar vacio");
                if (string.IsNullOrEmpty(desc)) throw new Exception("La descripcion no puede estar vacia");
                if (precio <= 0) throw new Exception("El precio debe ser mayor a 0");
                Destino d = new Destino(id, nombre, desc, precio, tipo);
                miSistema.AltaDestino(d);
                MostrarExito("Destino dado de alta");
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }

            PressToContinue();
        }

        static void AltaPaquete()
        {
            Console.Clear();
            MostrarMensajeColor(ConsoleColor.Yellow, "ALTA PAQUETE");
            Console.WriteLine();

            DateTime fecha = PedirFecha("Ingrese la fecha del paquete");
            int idAgencia = PedirNumeros("Ingrese id de agencia: ");

            try
            {
                Agencia agenciaBuscada = miSistema.ObtenerAgenciaPorId(idAgencia);
                if (agenciaBuscada == null) throw new Exception("No se encontró la agencia con el id dado");
                Paquete p = new Paquete(fecha, agenciaBuscada);
                miSistema.AltaPaquete(p);
                MostrarExito("Paquete agregado");
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }

            PressToContinue();
        }

        static void AgregarDestinoAPaquete()
        {
            Console.Clear();
            MostrarMensajeColor(ConsoleColor.Yellow, "AGREGAR DESTINO A PAQUETE");
            Console.WriteLine();

            int idPaquete = PedirNumeros("Ingrese id de paquete: ");
            string idDestino = PedirPalabras("Ingrese id de destino: ");
            int dias = PedirNumeros("Ingrese la cantidad de dias para este destino: ");

            try
            {
                //Hacer validaciones si corresponde
                miSistema.AgregarDestinoAUnPaquete(idPaquete, idDestino, dias);
                MostrarExito("Destino agregado al paquete");
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }

            PressToContinue();
        }

        static void AgenciasPorPais()
        {
            Console.Clear();
            MostrarMensajeColor(ConsoleColor.Yellow, "AGENCIAS POR PAIS");
            Console.WriteLine();

            string pais = PedirPalabras("Ingrese pais: ");

            try
            {
                if (string.IsNullOrEmpty(pais)) throw new Exception("El pais no puede ser vacio");
                List<Agencia> buscadas = miSistema.AgenciasPorPais(pais);
                if (buscadas.Count == 0) throw new Exception("No se encontraron agencias del pais dado");

                foreach (Agencia a in buscadas)
                {
                    Console.WriteLine(a);
                }
            }
            catch (Exception ex)
            {
                MostrarError (ex.Message);
            }

            PressToContinue();
        }
    }
}

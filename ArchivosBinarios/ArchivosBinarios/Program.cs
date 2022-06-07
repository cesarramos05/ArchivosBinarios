using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ArchivosBinarios
{
    //Clase
    class ArchivoBinarioEmpleados
    {
        //Declaracion de flujos
        BinaryWriter bw = null; //Flujo salida - escritura de datos
        BinaryReader br = null; //Flujo entrada - lectura de datos

        //Campos de la clase
        string Nombre, Direccion;
        long Telefono;
        int NumEmp, DiasTrabajados;
        float SalarioDiario;

        //Clase para crear el archivo
        public void CrearArchivo(string Archivo)
        {
            //Variable local metodo
            char resp;
            try
            {
                //Creacion del flujo para escribir datos al archivo
                bw = new BinaryWriter(new FileStream(Archivo, FileMode.Create, FileAccess.Write));

                //Captura de datos
                do
                {
                    Console.Clear();
                    Console.Write("Numero del empleado: ");
                    NumEmp = Int32.Parse(Console.ReadLine());
                    Console.Write("Nombre del empleado: ");
                    Nombre = Console.ReadLine();
                    Console.Write("Direccion del empleado: ");
                    Direccion = Console.ReadLine();
                    Console.Write("Telefono del empleado: ");
                    Telefono = Int64.Parse(Console.ReadLine());
                    Console.Write("Dias trabajados del empleado: ");
                    DiasTrabajados = Int32.Parse(Console.ReadLine());
                    Console.Write("Salario diario del empleado: ");
                    SalarioDiario = Single.Parse(Console.ReadLine());

                    //Escribe los datos al archivo
                    bw.Write(NumEmp);
                    bw.Write(Nombre);
                    bw.Write(Direccion);
                    bw.Write(Telefono);
                    bw.Write(DiasTrabajados);
                    bw.Write(SalarioDiario);

                    Console.Write("\n\nDeseas Almacenar otro registro (s/n)?");

                    resp = char.Parse(Console.ReadLine());
                } while ((resp == 's' )|| ( resp == 's'));

            }
            catch (IOException e)
            {
                Console.WriteLine("\nError : " +e.Message);
                Console.WriteLine("\nError : " + e.Message);
            }
            finally
            {
                if (bw != null) bw.Close(); //Cierra el flujo - escritura
                Console.Write("\nPresione <enter> para terminar la Escritura de datos y regresar al menu.");
                Console.ReadKey();
            }
        }

        public void MostrarArchivo(string Archivo)
        {
            try
            {
                //Verifica si existe el archivo
                if (File.Exists(Archivo))
                {
                    //Creacion flujo para leer datos del archivo
                    br = new BinaryReader(new FileStream(Archivo, FileMode.Open, FileAccess.Read));

                    //Despleigue de datos en pantalla
                    Console.Clear();
                    do
                    {
                        //Lectura de registros mientras no llegue a EndOfFile
                        NumEmp = br.ReadInt32();
                        Nombre = br.ReadString();
                        Direccion = br.ReadString();
                        Telefono = br.ReadInt64();
                        DiasTrabajados = br.ReadInt32();
                        SalarioDiario = br.ReadSingle();

                        //Muestra los datos 
                        Console.WriteLine("Numero del empleado: " + NumEmp);
                        Console.WriteLine("Nombre del empleado: " + Nombre);
                        Console.WriteLine("Direccion del empleado: " + Direccion);
                        Console.WriteLine("Telefono del empleado: " + Telefono);
                        Console.WriteLine("Dias Trabajados del empleado: " + DiasTrabajados);
                        Console.WriteLine("Salario diario del empleado: " + SalarioDiario);

                        Console.WriteLine("Sueldo total del empleado : {0:c} ", (DiasTrabajados * SalarioDiario));

                        Console.WriteLine("\n");
                    } while (true);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n\nEl Archivo " + Archivo + "No Exsite en el disco!!");
                    Console.Write("\nPresione <enter> para continuar...");
                    Console.ReadKey();
                }
            }
            catch (EndOfStreamException)
            {
                Console.WriteLine("\n\nFin del listado de empleados");
                Console.Write("\nPresione <enter> para continuar...");
                Console.ReadKey();
            }
            finally
            {
                if (br != null) br.Close(); //Cierra flujo
                Console.Write("\nPresione <enter> para terminar la lectura de datos y regresar al menu.");
                Console.ReadKey();
            }
        }
    }
    class Program
    {
        
        static void Main(string[] args)
        {
            //Declaracion variables auxiliares
            string Arch = null;
            int opcion;

            //Creacion del objeto
            ArchivoBinarioEmpleados al = new ArchivoBinarioEmpleados();

            //Menu de opciones
            do
            {
                Console.Clear();
                Console.WriteLine("\n*** ARCHIVO BINARIO EMPLEADOS ***");
                Console.WriteLine("1.- Creacion de un Archivo.");
                Console.WriteLine("2.- Lectura de un archivo.");
                Console.WriteLine("3.- Salida del programa.");
                Console.WriteLine("\nQue opcion deseas: ");
                opcion = Int16.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        //Bloque de escritura
                        try
                        {
                            //Captura nombre archivo
                            Console.Write("\nAlimenta el Nombre del Archivo a crear: ");
                            Arch = Console.ReadLine();

                            //Verifica si existe el archivo
                            char reps = 's';
                            if (File.Exists(Arch))
                            {
                                Console.Write("\nEl archivo existe!!, Deseas Sobreescribirlo (s/n)? ");
                                reps = char.Parse(Console.ReadLine());
                            }
                            if ((reps == 's') || (reps == 's'))
                            {
                                al.CrearArchivo(Arch);
                            }
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError :" + e.Message);
                            Console.WriteLine("\nRuta: " + e.StackTrace);
                        }
                        break;
                    case 2:
                        //Bloque lectura
                        try
                        {
                            //Captura nombre archivo
                            Console.Write("\nAlimenta el Nombre del Archivo que deseas leer: ");
                            Arch = Console.ReadLine();
                            al.MostrarArchivo(Arch);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError :" + e.Message);
                            Console.WriteLine("\nRuta: " + e.StackTrace);
                        }
                        break;
                    case 3:
                        Console.Write("\nPresione <enter> para salir del programa.");
                        Console.ReadKey();
                        break;

                    default:
                        Console.Write("\nEsa Opcion no EXISTE!! , pRESION <enter> para continuar...");
                        Console.ReadKey();
                        break;
                }
            } while (opcion != 3);
        }
    }
}

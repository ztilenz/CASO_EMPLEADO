using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace CASO_EMPLEADO
{
    internal class Program
    {
        static int max_emp = 50;
        static string[] nombres_empl = new string[max_emp];
        static string[] correo_emp = new string[max_emp];
        static int cont_empl = 0;
        static string rutaArchivo = "empleados_datos_txt";
        static public void Titulo()
        {
            Console.WriteLine("*****************************************");
            Console.WriteLine("**SISTEMA DE EMPLEADOS ARREGLOS Y CADENAS");
            Console.WriteLine("*****************************************");
        }
        static public void Registrar_empleado()
        {
            Console.WriteLine("*****REGISTRO DE EMPLEADO*****");
            if (cont_empl>max_emp)
            {
                Console.WriteLine("Capacidad maxima alcanzada");
                return;
            }
            Console.Write("Ingresar nombres y apellidos: ");
            string nombres = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(nombres))
            {
                Console.WriteLine("Error: El nombre no puede ser vacio...!");
                return;
            }
            string nombres_MAyus = nombres.ToUpper();
            string[] partes = nombres_MAyus.Split(' ');
            string primer_nombre;
            if (partes.Length > 0)
                primer_nombre = partes[0];
            else
                primer_nombre = "Usuario";
            String correo = $"{primer_nombre.ToLower()}@upn.edu.pe";
            nombres_empl[cont_empl] = nombres_MAyus;
            correo_emp[cont_empl] = correo;
            cont_empl++;
        }
        static public void buscar_emp()
        {
            Console.WriteLine("***********BUSCAR EMPLEADO***********");
            Console.WriteLine("*************************************");
            if (cont_empl==0)
            {
                Console.WriteLine("No hay empleados registrados");
                return;
            }
            Console.WriteLine("Ingrese el nombre del empleado a buscar: ");
            string buscar_nombre = Console.ReadLine()?.Trim().ToUpper();
            if (string.IsNullOrEmpty(buscar_nombre))
                return;
            bool encontrado = false;
            for (int i = 0; i < cont_empl; i++)
            {
                if (nombres_empl[i].Contains(buscar_nombre))
                {
                    Console.WriteLine("Nombre: " + nombres_empl[i] + "correo: " + correo_emp[i]);
                    encontrado = true;
                }
            }
            if (!encontrado)
                Console.WriteLine("Empleado no encontrado");
        }
        static public void ordenar_alfabeticamente()
        {
            if (cont_empl<= 1)
            {
                Console.WriteLine("No hay suficientes empleados para ordenar");
                return;
            }
            for (int i = 0; i < cont_empl -1; i++)
            {
                for (int j = 0; j < cont_empl-1-i;j++)
                {
                    if (string.Compare(nombres_empl[j], nombres_empl[j +1])>0)
                    {
                        string temp_nombre = nombres_empl[j];
                        nombres_empl[j] = nombres_empl[j + 1];
                        nombres_empl[j + 1] = temp_nombre;

                        string temp_correo = correo_emp[j];
                        correo_emp[j] = correo_emp[j + 1];
                        correo_emp[j + 1] = temp_correo;
                    }
                }
            }
        }
        static public void mostrar()
        {
            Console.WriteLine("*****LISTADO DE EMPLEADOS*****");
            Console.WriteLine("******************************");
            if (cont_empl==0)
            {
                Console.WriteLine("No hay empleados registrados");
                return;
            }
            Console.WriteLine("Nombres y Apellidos\t\t Correo corporativo");
            for (int i =0; i < cont_empl; i++)
            {
                Console.WriteLine(nombres_empl[i] + "\t\t\t" + correo_emp[i]);
            }
        }
        static public void Guardar_datos_archivo()
        {
            Console.WriteLine("*****GUARDAR DATOS EN ARCHIVO*****");
            Console.WriteLine("**********************************");
            if (cont_empl == 0)
            {
                Console.WriteLine("No hay empleados Registrados");
                return;
            }
            using (StreamWriter escribir = new StreamWriter(rutaArchivo))
            {
                for (int i = 0; i < cont_empl; i++)
                {
                    escribir.WriteLine(nombres_empl[i] + "," + correo_emp[i]);
                }
            }
            Console.WriteLine(nombres_empl + "empleados guardados");
        }
        static public void Leer_datos_archivo()
        {
            Console.WriteLine("***LEER DATOS DESDE ARCHIVO****");
            Console.WriteLine("*******************************");
            if(!File.Exists(rutaArchivo))
            {
                Console.WriteLine("Archivo no existe");
                return;
            }
            using (StreamReader lector = new StreamReader(rutaArchivo))
            {
                string linea;
                while((linea=lector.ReadLine()) !=null)
                {
                    string[] partes = linea.Split(',');
                    if (partes.Length == 2)
                    {
                        Console.WriteLine("Nombres:" + partes[0] + "correo:" + partes[1]);
                    }
                }
            }
        }
        static public void Cargar_datos_desde_archivo()
        {
            Console.WriteLine("***CARGAR DATOS DESDE ARCHIVO****");
            Console.WriteLine("*********************************");
            if (!File.Exists(rutaArchivo))
            {
                Console.WriteLine("Archivo no existe");
                return;
            }
            string[] lineas = File.ReadAllLines(rutaArchivo);
            cont_empl = 0;
            for (int i= 0; i < lineas.Length && i < max_emp; i++)
            {
                string[] partes = lineas[i].Split(',');
                if (partes.Length==2)
                {
                    nombres_empl[cont_empl] = partes[0];
                    correo_emp[cont_empl] = partes[1];
                    cont_empl++;
                }
            }
        }
        static void Main(string[] args)
        {
            int opc=0;
            while(opc != 8)
            {
                Console.Clear();
                Console.WriteLine("[1]Registrar empleado");
                Console.WriteLine("[2]Buscar Empleado");
                Console.WriteLine("[3]Ordenar alfabeticamente");
                Console.WriteLine("[4]Mostrar empleado");
                Console.WriteLine("[5]Guardar datos del archivo");
                Console.WriteLine("[6]Leer datos desde archivo");
                Console.WriteLine("[7]Cargar datos desde el archivo");
                Console.WriteLine("[8]Salir");
                Console.Write("Ingresar opcion: ");
                if (!int.TryParse(Console.ReadLine(), out opc))
                {
                    Console.WriteLine("Error: Ingrese una opcion numerica");
                    continue;
                }
                switch (opc)
                {
                    case 1:
                        Registrar_empleado();break;
                    case 2:
                        buscar_emp(); break;
                    case 3:
                        ordenar_alfabeticamente(); break;
                    case 4:
                        mostrar(); break;
                    case 5:
                        Guardar_datos_archivo(); break;
                    case 6:
                        Leer_datos_archivo(); break;
                    case 7:
                        Cargar_datos_desde_archivo(); break;
                    case 8:
                        Console.WriteLine("Gracias por usar el sistema"); break;
                    default:
                        Console.WriteLine("Opcion no valida");break;
                }
                Console.ReadKey();
            }
        }
    }
}

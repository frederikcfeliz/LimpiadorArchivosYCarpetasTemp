using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LimpiadorArchivosYCarpetasTemp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando proceso de limpieza de archivos y carpetas Temporales..........");

            //Aquí se definen las rutas de las carpetas temporales y archivos a eliminar
            List<string> rutasACrearYLimpiar = new List<string>()
            {
                Path.GetTempPath(),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Temp"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData", "Local", "Temp"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads")
            };

            //Iteracion sobre cada ruta y limpiar los archivos y carpetas temporales
            foreach (var ruta in rutasACrearYLimpiar)
            {

                LimpiarDirectorio(ruta);
            }
            Console.WriteLine("Proceso de limpieza finalizado.........");
            Console.ReadKey();
        }

        // Método para limpiar un directorio específico
        static void LimpiarDirectorio(string folderPath)
        {

            Console.WriteLine($"\n---- Procesando: {folderPath} ----");

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"ERROR: La ruta especificada no existe: {folderPath}");
                return;
            }

            DirectoryInfo di = new DirectoryInfo(folderPath);

            foreach (FileInfo file in di.GetFiles())
            {

                try
                {
                    file.Delete();
                    Console.WriteLine($"Archivo eliminado: {file.FullName}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"No se pudo eliminar el archivo: {file.FullName}. Error: {ex.Message}");
                }
            }

            //Eliminar subcarpetas
            foreach (DirectoryInfo dir in di.GetDirectories())
            {

                try
                {
                    dir.Delete(true);
                    Console.WriteLine($"Carpeta eliminada: {dir.FullName}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"No se pudo eliminar la carpeta: {dir.FullName}. Error: {ex.Message}");

                }
            }
            Console.WriteLine($"Limpieza compeltada para: {folderPath}");
        }
    }
}
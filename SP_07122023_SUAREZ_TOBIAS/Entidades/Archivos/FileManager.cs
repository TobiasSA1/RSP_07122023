using Entidades.Exceptions;
using System.Text.Json;

namespace Entidades.Files
{
    /// <summary>
    /// Clase estática que proporciona funcionalidades para la gestión de archivos.
    /// </summary>
    public static class FileManager
    {

        private static string path;

        /// <summary>
        /// Inicializa la clase FileManager.
        /// </summary>
        static FileManager()
        {
            string carpetaPersonalizada = "SP_07122023_SUAREZ_TOBIAS\\20231207_Suarez_Tobias";
            path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), carpetaPersonalizada);
            ValidaExistenciaDeDirectorio();
        }

        /// <summary>
        /// Valida la existencia del directorio y lo crea si no existe.
        /// </summary>
        private static void ValidaExistenciaDeDirectorio()
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception ex)
            {
                RegistrarExcepcion(ex);

                throw new FileManagerException("Error el crear el directorio", ex);
            }
        }

        /// <summary>
        /// Guarda datos en un archivo especificado.
        /// </summary>
        /// <param name="data">Datos a guardar.</param>
        /// <param name="nombreArchivo">Nombre del archivo.</param>
        /// <param name="append">Indica si se deben añadir datos al archivo existente.</param>
        public static void Guardar(string data, string nombreArchivo, bool append)
        {
            string filePath = Path.Combine(path, nombreArchivo);

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, append))
                {
                    writer.Write(data);
                }
            }
            catch (Exception ex)
            {
                RegistrarExcepcion(ex);
                throw new FileManagerException($"Error al guardar el archivo '{nombreArchivo}'", ex);
            }
        }

        /// <summary>
        /// Serializa un objeto y guarda el resultado en un archivo.
        /// </summary>
        /// <typeparam name="T">Tipo de objeto a serializar.</typeparam>
        /// <param name="elemento">Elemento a serializar.</param>
        /// <param name="nombreArchivo">Nombre del archivo.</param>
        /// <returns>True si la serialización y guardado fueron exitosos, de lo contrario, false.</returns>
        public static bool Serializar<T>(T elemento, string nombreArchivo)
        {
            try
            {
                string jsonData = JsonSerializer.Serialize(elemento);

                Guardar(jsonData, nombreArchivo, false);

                return true;
            }
            catch (Exception ex)
            {
                RegistrarExcepcion(ex);
                throw new FileManagerException($"Error al serializar el archivo '{nombreArchivo}'", ex);
            }
        }

        /// <summary>
        /// Registra una excepción en un archivo de log.
        /// </summary>
        /// <param name="ex">Excepción a registrar.</param>
        public static void RegistrarExcepcion(Exception ex)
        {            
            string logFilePath = Path.Combine(path, "logs.txt");

            string logMessage = $"{DateTime.Now} - {ex.GetType().FullName}: {ex.Message}{Environment.NewLine}{ex.StackTrace}{Environment.NewLine}";

            File.AppendAllText(logFilePath, logMessage);
        }

    }
}

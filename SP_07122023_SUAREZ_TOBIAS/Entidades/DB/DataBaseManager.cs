using System.Data.SqlClient;
using Entidades.Excepciones;
using Entidades.Exceptions;
using Entidades.Interfaces;

namespace Entidades.DataBase
{
    /// <summary>
    /// Clase que gestiona la interacción con la base de datos.
    /// </summary>
    public class DataBaseManager
    {

        private readonly SqlConnection connection;
        private readonly string stringConnection;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="DataBaseManager"/>.
        /// </summary>
        public DataBaseManager()
        {
            this.stringConnection = "Server=DESKTOP-488DPB0\\SQLEXPRESS;Database=20230622SP;Trusted_Connection=True";

            connection = new SqlConnection(stringConnection);
        }

        /// <summary>
        /// Obtiene la ruta de la imagen asociada a un tipo de comida desde la base de datos.
        /// </summary>
        /// <param name="tipoComida">El tipo de comida.</param>
        /// <returns>Ruta de la imagen asociada al tipo de comida.</returns>
        public string GetImagenComida(string tipo_comida)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("SELECT imagen FROM comidas WHERE tipo_comida = @tipo_comida", connection))
                {
                    command.Parameters.AddWithValue("@tipo_comida", tipo_comida);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.GetString(0);
                        }
                        else
                        {
                            throw new ComidaInvalidaException("Error Comida invalida");

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataBaseManagerException("Error al leer la imagen de la comida", ex);
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Guarda el ticket de una comida en la base de datos.
        /// </summary>
        /// <typeparam name="T">Tipo de comida que implementa la interfaz IComestible y tiene un constructor sin parámetros.</typeparam>
        /// <param name="nombreEmpleado">Nombre del empleado.</param>
        /// <param name="comida">Instancia de la comida.</param>
        /// <returns>True si la operación fue exitosa, de lo contrario, false.</returns>
        public bool GuardarTicket<T>(string nombreEmpleado, T comida) where T : IComestible, new()
        {
            try
            {
                if (comida == null)
                {
                    throw new ArgumentException("La comida no puede ser nula");
                }

                if (!(comida is IComestible))
                {
                    throw new ArgumentException("El tipo de comida no implementa la interfaz IComestible");
                }

                if (comida.GetType().GetConstructor(Type.EmptyTypes) == null)
                {
                    throw new ArgumentException("El tipo de comida no tiene un constructor público sin parámetros");
                }

                using (SqlCommand command = new SqlCommand("INSERT INTO tickets (empleado, ticket) VALUES (@empleado, @ticket)", connection))
                {
                    command.Parameters.AddWithValue("@empleado", nombreEmpleado);

                    command.Parameters.AddWithValue("@ticket",comida.ToString());

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                return true;

            }
            catch (Exception ex)
            {
                throw new DataBaseManagerException("Error al escribir el ticket de la comida", ex);
            }
            finally
            {
                connection.Close();
            }

        }


    }
}

using Entidades.Enumerados;
using Entidades.Exceptions;
using Entidades.Files;
using Entidades.Interfaces;
using Entidades.MetodosDeExtension;
using System.Text;
using Entidades.DataBase;

namespace Entidades.Modelos
{
    /// <summary>
    /// Clase que representa una hamburguesa.
    /// </summary>
    public class Hamburguesa : IComestible
    {

        private static int costoBase;
        private bool esDoble;
        private double costo;
        private bool estado = false;
        private string imagen;
        List<EIngrediente> ingredientes;
        Random random;

        /// <summary>
        /// Obtiene el estado de la hamburguesa (preparada o no).
        /// </summary>
        public bool Estado => estado;

        /// <summary>
        /// Obtiene la imagen asociada a la hamburguesa.
        /// </summary>
        public string Imagen => imagen;

        /// <summary>
        /// Obtiene el ticket que muestra los detalles de la hamburguesa y el total a pagar.
        /// </summary>
        public string Ticket => $"{this}\nTotal a pagar:{this.costo}";


        /// <summary>
        /// Inicializa el costo base de la hamburguesa.
        /// </summary>
        static Hamburguesa() => Hamburguesa.costoBase = 1500;

        /// <summary>
        /// Constructor por defecto de la hamburguesa.
        /// </summary>
        public Hamburguesa() : this(false) { }

        /// <summary>
        /// Constructor que permite especificar si la hamburguesa es doble.
        /// </summary>
        /// <param name="esDoble">Indica si la hamburguesa es doble o simple.</param>
        public Hamburguesa(bool esDoble)
        {
            this.esDoble = esDoble;
            this.random = new Random();
            IniciarPreparacion();
        }

        /// <summary>
        /// Agrega ingredientes aleatorios a la hamburguesa.
        /// </summary>
        private void AgregarIngredientes()
        {
            this.ingredientes = new List<EIngrediente>();
            this.ingredientes.AddRange(random.IngredientesAleatorios());
            this.costo = this.ingredientes.CalcularCostoIngredientes(costoBase);
        }

        /// <summary>
        /// Genera una representación en cadena de los datos de la hamburguesa.
        /// </summary>
        /// <returns>Cadena que representa los datos de la hamburguesa.</returns>
        private string MostrarDatos()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Hamburguesa {(this.esDoble ? "Doble" : "Simple")}");
            stringBuilder.AppendLine("Ingredientes: ");
            this.ingredientes.ForEach(i => stringBuilder.AppendLine(i.ToString()));
            return stringBuilder.ToString();

        }

        /// <summary>
        /// Sobrescribe el método ToString para obtener una representación de cadena de la hamburguesa.
        /// </summary>
        /// <returns>Cadena que representa la hamburguesa.</returns>
        public override string ToString() => this.MostrarDatos();

        /// <summary>
        /// Finaliza la preparación de la hamburguesa y asigna el costo total.
        /// </summary>
        /// <param name="cocinero">Nombre del cocinero que finaliza la preparación.</param>
        public void FinalizarPreparacion(string cocinero)
        {
            if (!this.estado)
            {
                this.costo = this.ingredientes.CalcularCostoIngredientes(costoBase);
                this.estado = true;
            }
        }

        /// <summary>
        /// Inicia la preparación de la hamburguesa, asignando ingredientes y calculando el costo.
        /// </summary>
        public void IniciarPreparacion()
        {
            if (this.estado == false) 
            {
                int numeroAleatorio = this.random.Next(1, 9);
                this.imagen = $"Hamburguesa_{numeroAleatorio}";
                this.AgregarIngredientes();
                this.costo = this.ingredientes.CalcularCostoIngredientes(costoBase);
            }

        }
    }
}
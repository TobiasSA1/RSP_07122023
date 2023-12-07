using Entidades.Enumerados;
using Entidades.Excepciones;

namespace Entidades.MetodosDeExtension
{
    /// <summary>
    /// Clase estática que contiene métodos de extensión relacionados con ingredientes.
    /// </summary>
    public static class IngredientesExtension
    {
        /// <summary>
        /// Calcula el costo total de una lista de ingredientes.
        /// </summary>
        /// <param name="ingredientes">Lista de ingredientes.</param>
        /// <param name="costoInicial">Costo inicial.</param>
        /// <returns>Costo total de los ingredientes.</returns>
        public static double CalcularCostoIngredientes(this List<EIngrediente> ingredientes, int costoInicial)
        {
                double costoTotal = costoInicial;

                double costoIngredientes;

                foreach (var ingrediente in ingredientes)
                {

                    switch (ingrediente)
                    {
                        case EIngrediente.ADHERESO:
                            costoIngredientes = 5.0; break;
    
                        case EIngrediente.QUESO:
                            costoIngredientes = 10.0; break;

                        case EIngrediente.JAMON:
                            costoIngredientes = 12.0; break;

                        case EIngrediente.HUEVO:
                            costoIngredientes = 13.0; break;

                        case EIngrediente.PANCETA:
                            costoIngredientes = 15.0; break;

                        default:
                            costoIngredientes = 0.0; break;
                    }

                costoTotal += costoIngredientes;

                }

                return costoTotal;
        }

        /// <summary>
        /// Genera una lista aleatoria de ingredientes.
        /// </summary>
        /// <param name="rand">Instancia de la clase Random.</param>
        /// <returns>Lista aleatoria de ingredientes.</returns>
        public static  List <EIngrediente> IngredientesAleatorios(this Random rand)
        {
            List<EIngrediente> ingredientes = new List<EIngrediente>()
            {
            EIngrediente.QUESO,
            EIngrediente.PANCETA,
            EIngrediente.ADHERESO,
            EIngrediente.HUEVO,
            EIngrediente.JAMON,
            };

            int randomCount = rand.Next(1, ingredientes.Count + 1);

            return ingredientes.Take(randomCount).ToList();
        }
    }
}

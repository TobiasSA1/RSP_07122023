using Entidades.Exceptions;
using Entidades.Files;
using Entidades.Modelos;

namespace MisTest
{
    [TestClass]
    public class TestCocina
    {
        [TestMethod]
        [ExpectedException(typeof(FileManagerException))]
        public void AlGuardarUnArchivo_ConNombreInvalido_TengoUnaExcepcion()
        {
            // Arrange
            string data = "Datos de prueba";
            string nombreArchivo = "archivo<>invalido.txt";
            bool append = false;

            // Act
            FileManager.Guardar(data, nombreArchivo, append);
        }

        [TestMethod]

        public void AlInstanciarUnCocinero_SeEspera_PedidosCero()
        {
            // Arrange
            Cocinero<Hamburguesa> cocinero = new Cocinero<Hamburguesa>("NombreDePrueba");

            // Act
            int cantidadPedidos = cocinero.CantPedidosFinalizados;

            // Assert
            Assert.AreEqual(0, cantidadPedidos);
        }
    }
}
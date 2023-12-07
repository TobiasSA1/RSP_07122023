namespace Entidades.Interfaces
{
    public interface IComestible
    {
        bool Estado {  get; }
        string Imagen { get; }
        string Ticket { get; }

        public void IniciarPreparacion() { }
        public void FinalizarPreparacion(string cocinero) { }


    }
}

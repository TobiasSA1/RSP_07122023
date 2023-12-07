using Entidades.DataBase;
using Entidades.Excepciones;
using Entidades.Exceptions;
using Entidades.Files;
using Entidades.Interfaces;
using System.ComponentModel.Design;

namespace Entidades.Modelos
{
    public delegate void DelegadoDemoraAtencion(double demora);
    public delegate void DelegadoPedidoEnCurso<T>(T pedido);

    public class Cocinero<T> where T : IComestible, new()
    {
        private int cantPedidosFinalizados;
        private string nombre;
        private double demoraPreparacionTotal;
        private CancellationTokenSource cancellation;
        private Task tarea;
        private T pedidoEnPreparacion;
        private Queue<T> pedidos;
        private Mozo<T> mozo;

        public Cocinero(string nombre)
        {
            this.nombre = nombre;
            this.mozo = new Mozo<T>();
            this.mozo.OnPedido += this.TomarNuevoPedido;
            this.pedidos = new Queue<T>();
        }

        public bool HabilitarCocina
        {
            get
            {
                return this.tarea is not null && (this.tarea.Status == TaskStatus.Running ||
                    this.tarea.Status == TaskStatus.WaitingToRun ||
                    this.tarea.Status == TaskStatus.WaitingForActivation);
            }
            set
            {
                if (value && (this.tarea is null ||
                              this.tarea.Status != TaskStatus.Running &&
                              this.tarea.Status != TaskStatus.WaitingToRun &&
                              this.tarea.Status != TaskStatus.WaitingForActivation))
                {
                    this.cancellation = new CancellationTokenSource();
                    this.tarea = Task.Run(() => this.EmpezarACocinar());
                    this.mozo.EmpezarATrabajar = true;
                }
                else
                {
                    this.cancellation?.Cancel();
                    this.mozo.EmpezarATrabajar = false;
                }
            }
        }

        public double TiempoMedioDePreparacion
        {
            get => this.cantPedidosFinalizados == 0 ? 0 : this.demoraPreparacionTotal / this.cantPedidosFinalizados;
        }

        public string Nombre { get => nombre; }

        public int CantPedidosFinalizados { get => cantPedidosFinalizados; }

        public Queue<T> Pedidos { get => pedidos; }

        private void EmpezarACocinar()
        {
            while (!cancellation.Token.IsCancellationRequested)
            {
                if (this.pedidos.Count > 0)
                {
                    this.pedidoEnPreparacion = this.pedidos.Dequeue();
                    this.OnPedido?.Invoke(this.pedidoEnPreparacion);
                    this.EsperarProximoIngreso();
                    this.cantPedidosFinalizados++;
                    DataBaseManager dataBaseManager = new DataBaseManager();
                    dataBaseManager.GuardarTicket(this.nombre, this.pedidoEnPreparacion);
                    this.pedidoEnPreparacion = default(T); 
                }
            }
        }

        private void EsperarProximoIngreso()
        {
            int tiempoEspera = 0;

            while (!cancellation.Token.IsCancellationRequested && this.pedidoEnPreparacion.Estado == false)
            {
                if (this.OnDemora != null)
                {
                    this.OnDemora.Invoke(tiempoEspera);
                }
                Thread.Sleep(1000);
                tiempoEspera++;
            }

            this.demoraPreparacionTotal += tiempoEspera;
        }

        public void TomarNuevoPedido(T pedido)
        {
            if (this.OnPedido != null)
            {
                this.pedidos.Enqueue(pedido);
            }
        }

        public event DelegadoDemoraAtencion OnDemora;
        public event DelegadoPedidoEnCurso<T> OnPedido;
    }
}

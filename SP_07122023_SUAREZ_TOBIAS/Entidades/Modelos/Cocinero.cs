using Entidades.DataBase;
using Entidades.Excepciones;
using Entidades.Exceptions;
using Entidades.Files;
using Entidades.Interfaces;
using System.ComponentModel.Design;

namespace Entidades.Modelos
{
    /// <summary>
    /// Delegado para notificar demora en la atención.
    /// </summary>
    /// <param name="demora">Tiempo de demora en la atención.</param>
    public delegate void DelegadoDemoraAtencion(double demora);

    /// <summary>
    /// Delegado para notificar nuevo ingreso de comestible.
    /// </summary>
    /// <param name="menu">Comestible ingresado.</param>
    public delegate void DelegadoNuevoIngreso(IComestible menu);

    /// <summary>
    /// Clase genérica que representa a un cocinero capaz de preparar comestibles.
    /// </summary>
    /// <typeparam name="T">Tipo de comestible que puede preparar el cocinero.</typeparam>
    public class Cocinero<T> where T : IComestible, new()
    {
        private int cantPedidosFinalizados;
        private string nombre;
        private double demoraPreparacionTotal;
        private CancellationTokenSource cancellation;
        private Task tarea;
        private T menu;

        /// <summary>
        /// Constructor de la clase Cocinero.
        /// </summary>
        /// <param name="nombre">Nombre del cocinero.</param>
        public Cocinero(string nombre)
        {
            this.nombre = nombre;
        }

        /// <summary>
        /// Obtiene o establece si la cocina está habilitada.
        /// </summary>
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
                    this.tarea = Task.Run(() => this.IniciarIngreso());
                }
                else
                {
                    this.cancellation?.Cancel();
                }
            }
        }

        /// <summary>
        /// Obtiene el tiempo medio de preparación de los pedidos finalizados.
        /// </summary>
        public double TiempoMedioDePreparacion
        {
            get => this.cantPedidosFinalizados == 0 ? 0 : this.demoraPreparacionTotal / this.cantPedidosFinalizados;
        }
        /// <summary>
        /// Obtiene el nombre del cocinero.
        /// </summary>
        public string Nombre { get => nombre; }

        /// <summary>
        /// Obtiene la cantidad de pedidos finalizados.
        /// </summary>
        public int CantPedidosFinalizados { get => cantPedidosFinalizados; }

        /// <summary>
        /// Inicia el proceso de ingreso y preparación de comestibles.
        /// </summary>
        private void IniciarIngreso()
        {
            while (!cancellation.Token.IsCancellationRequested)
            {
                this.NotificarNuevoIngreso();
                this.EsperarProximoIngreso();
                this.cantPedidosFinalizados++;
                DataBaseManager dataBaseManager = new DataBaseManager();
                dataBaseManager.GuardarTicket(this.nombre, this.menu);
            }
        }

        /// <summary>
        /// Notifica el nuevo ingreso de un comestible.
        /// </summary>
        private void NotificarNuevoIngreso()
        {
            DelegadoNuevoIngreso handler = this.OnIngreso;

            if (handler != null)
            {
                this.menu = new T();
                this.menu.IniciarPreparacion();
                handler.Invoke(this.menu);
            }
        }

        /// <summary>
        /// Espera el próximo ingreso de comestible y actualiza el tiempo de espera.
        /// </summary>
        private void EsperarProximoIngreso()
        {
            int tiempoEspera = 0;

            while (!cancellation.Token.IsCancellationRequested && this.menu.Estado == false)
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

        /// <summary>
        /// Evento que se dispara cuando hay demora en la atención.
        /// </summary>
        public event DelegadoDemoraAtencion OnDemora;

        /// <summary>
        /// Evento que se dispara cuando hay un nuevo ingreso de comestible.
        /// </summary>
        public event DelegadoNuevoIngreso OnIngreso;

    }
}

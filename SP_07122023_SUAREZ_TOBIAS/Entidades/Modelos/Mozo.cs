using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Modelos
{
    public delegate void DelegadoNuevoPedido<T>(T menu);
    public class Mozo<T> where T : IComestible, new()
    {
        private CancellationTokenSource cancellation;
        private T menu;
        private Task tarea;

        public bool EmpezarATrabajar
        {
            get
            {
                return this.tarea != null && (this.tarea.Status == TaskStatus.Running ||
                    this.tarea.Status == TaskStatus.WaitingToRun ||
                    this.tarea.Status == TaskStatus.WaitingForActivation);
            }
            set
            {
                if (value && (this.tarea == null ||
                              this.tarea.Status != TaskStatus.Running &&
                              this.tarea.Status != TaskStatus.WaitingToRun &&
                              this.tarea.Status != TaskStatus.WaitingForActivation))
                {
                    this.cancellation = new CancellationTokenSource();
                    this.tarea = Task.Run(() => this.TomarPedidos());
                }
                else
                {
                    this.cancellation?.Cancel();
                }
            }
        }

        private void TomarPedidos()
        {
            while (!cancellation.Token.IsCancellationRequested)
            {
                this.NotificarNuevoPedido();
                Thread.Sleep(5000); // Espera cada 5 segundos
            }
        }

        private void NotificarNuevoPedido()
        {
            DelegadoNuevoPedido<T> handler = this.OnPedido;

            if (handler != null)
            {
                this.menu = new T();
                this.menu.IniciarPreparacion();
                handler.Invoke(this.menu);
            }
        }

        public event DelegadoNuevoPedido<T> OnPedido;
    }
}

using Entidades.DataBase;
using Entidades.Excepciones;
using Entidades.Exceptions;
using Entidades.Files;
using Entidades.Interfaces;
using Entidades.Modelos;
using System.Net;
using System.Text.Json;
namespace FrmView
{
    public partial class FrmView : Form
    {
        private IComestible comida;
        private Cocinero<Hamburguesa> hamburguesero;

        public FrmView()
        {
            InitializeComponent();

            this.hamburguesero = new Cocinero<Hamburguesa>("Ramon");

            this.hamburguesero.OnDemora += this.MostrarConteo;
            this.hamburguesero.OnPedido += this.MostrarComida;
        }

        private void MostrarComida(Hamburguesa pedido)
        {
            try
            {
                DataBaseManager dataBaseManager = new DataBaseManager();

                this.comida = pedido;

                string rutaImagen = dataBaseManager.GetImagenComida(pedido.Imagen);

                if (!string.IsNullOrEmpty(rutaImagen))
                {
                    if (Uri.IsWellFormedUriString(rutaImagen, UriKind.Absolute))
                    {
                        using (WebClient client = new WebClient())
                        {
                            byte[] imageData = client.DownloadData(rutaImagen);
                            using (MemoryStream ms = new MemoryStream(imageData))
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    this.pcbComida.Image = Image.FromStream(ms);
                                    this.rchElaborando.Text = pedido.ToString();
                                });
                            }
                        }
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            this.pcbComida.Image = Image.FromFile(rutaImagen);
                            this.rchElaborando.Text = pedido.ToString();
                        });
                    }
                }
            }
            catch (DataBaseManagerException ex)
            {
                MessageBox.Show("Error al levantar IMG comida de la BD.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                FileManager.RegistrarExcepcion(ex);

                Exception execp = ex.InnerException;
                while (execp != null)
                {
                    MessageBox.Show($"{execp.Message}", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    FileManager.RegistrarExcepcion(execp);

                    execp = execp.InnerException;
                }
            }
        }

        private void MostrarConteo(double tiempo)
        {
            this.Invoke((MethodInvoker)delegate
            {
                this.lblTiempo.Text = $"{tiempo} segundos";
                this.lblTmp.Text = $"{this.hamburguesero.TiempoMedioDePreparacion.ToString("00.0")} segundos";
            });
        }

        //private void ActualizarAtendidos(IComestible comida)
        //{
        //    this.rchFinalizados.Text += "\n" + comida.Ticket;
        //}

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.hamburguesero.HabilitarCocina)
                {
                    this.hamburguesero.HabilitarCocina = true;
                    this.btnAbrir.Image = Properties.Resources.close_icon;
                }
                else
                {
                    this.hamburguesero.HabilitarCocina = false;
                    this.btnAbrir.Image = Properties.Resources.open_icon;
                }
            }
            catch (DataBaseManagerException ex)
            {
                MessageBox.Show($"{ex.Message}", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                FileManager.RegistrarExcepcion(ex);

                Exception innerException = ex.InnerException;

                while (innerException != null)
                {
                    MessageBox.Show($"{innerException.Message}", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    FileManager.RegistrarExcepcion(innerException);

                    innerException = innerException.InnerException;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir la cocina.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                FileManager.RegistrarExcepcion(ex);
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (this.comida != null)
            {
                this.comida.FinalizarPreparacion(this.hamburguesero.Nombre);
                //this.ActualizarAtendidos(this.comida);
                this.comida = null;
            }
            else
            {
                MessageBox.Show("El Cocinero no posee comidas.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void FrmView_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                bool exitoSerializacion = FileManager.Serializar(hamburguesero, "cocinero.json");

                if (exitoSerializacion)
                {
                    MessageBox.Show("Cocinero Serializado!", "Éxito!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error al serializar el Cocinero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FileManagerException ex)
            {
                MessageBox.Show($"{ex.Message}", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                FileManager.RegistrarExcepcion(ex);

                Exception innerException = ex.InnerException;
                while (innerException != null)
                {
                    MessageBox.Show($"{innerException.Message}", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    FileManager.RegistrarExcepcion(innerException);

                    innerException = innerException.InnerException;
                }
            }
        }
    }
}
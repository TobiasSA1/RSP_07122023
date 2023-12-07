namespace FrmView
{
    partial class FrmView
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmView));
            lblTitleComida = new Label();
            lblTiempo = new Label();
            lblTitleTiempo = new Label();
            btnAbrir = new Button();
            lblTmp = new Label();
            lblTitleTmp = new Label();
            btnSiguiente = new Button();
            rchElaborando = new RichTextBox();
            rchFinalizados = new RichTextBox();
            lblFinalizados = new Label();
            pcbComida = new PictureBox();
            lblProximaComida = new Label();
            ((System.ComponentModel.ISupportInitialize)pcbComida).BeginInit();
            SuspendLayout();
            // 
            // lblTitleComida
            // 
            lblTitleComida.AutoSize = true;
            lblTitleComida.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            lblTitleComida.Location = new Point(12, 9);
            lblTitleComida.Name = "lblTitleComida";
            lblTitleComida.Size = new Size(148, 32);
            lblTitleComida.TabIndex = 1;
            lblTitleComida.Text = "Preparando: ";
            // 
            // lblTiempo
            // 
            lblTiempo.AutoSize = true;
            lblTiempo.Font = new Font("Segoe UI", 18F, FontStyle.Italic, GraphicsUnit.Point);
            lblTiempo.ForeColor = Color.FromArgb(192, 64, 0);
            lblTiempo.Location = new Point(711, 174);
            lblTiempo.Name = "lblTiempo";
            lblTiempo.Size = new Size(87, 32);
            lblTiempo.TabIndex = 5;
            lblTiempo.Text = "tiempo";
            // 
            // lblTitleTiempo
            // 
            lblTitleTiempo.AutoSize = true;
            lblTitleTiempo.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            lblTitleTiempo.Location = new Point(610, 138);
            lblTitleTiempo.Name = "lblTitleTiempo";
            lblTitleTiempo.Size = new Size(305, 32);
            lblTitleTiempo.TabIndex = 4;
            lblTitleTiempo.Text = "Tiempo Preparacion Actual:";
            // 
            // btnAbrir
            // 
            btnAbrir.BackgroundImageLayout = ImageLayout.Stretch;
            btnAbrir.Cursor = Cursors.Hand;
            btnAbrir.FlatAppearance.BorderSize = 0;
            btnAbrir.FlatStyle = FlatStyle.Flat;
            btnAbrir.Image = Properties.Resources.open_icon;
            btnAbrir.Location = new Point(711, 21);
            btnAbrir.Name = "btnAbrir";
            btnAbrir.Size = new Size(64, 62);
            btnAbrir.TabIndex = 6;
            btnAbrir.UseVisualStyleBackColor = true;
            btnAbrir.Click += btnAbrir_Click;
            // 
            // lblTmp
            // 
            lblTmp.AutoSize = true;
            lblTmp.Font = new Font("Segoe UI", 18F, FontStyle.Italic, GraphicsUnit.Point);
            lblTmp.ForeColor = Color.FromArgb(192, 64, 0);
            lblTmp.Location = new Point(601, 304);
            lblTmp.Name = "lblTmp";
            lblTmp.Size = new Size(286, 32);
            lblTmp.TabIndex = 9;
            lblTmp.Text = "tiempo medio de atencion";
            // 
            // lblTitleTmp
            // 
            lblTitleTmp.AutoSize = true;
            lblTitleTmp.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            lblTitleTmp.Location = new Point(591, 258);
            lblTitleTmp.Name = "lblTitleTmp";
            lblTitleTmp.Size = new Size(341, 32);
            lblTitleTmp.TabIndex = 8;
            lblTitleTmp.Text = "Tiempo medio de Preparacion:";
            // 
            // btnSiguiente
            // 
            btnSiguiente.BackgroundImageLayout = ImageLayout.Zoom;
            btnSiguiente.Cursor = Cursors.Hand;
            btnSiguiente.FlatAppearance.BorderSize = 0;
            btnSiguiente.Image = Properties.Resources.comida_icon;
            btnSiguiente.Location = new Point(220, 156);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new Size(96, 96);
            btnSiguiente.TabIndex = 5;
            btnSiguiente.UseVisualStyleBackColor = true;
            btnSiguiente.Click += btnSiguiente_Click;
            // 
            // rchElaborando
            // 
            rchElaborando.Location = new Point(12, 51);
            rchElaborando.Name = "rchElaborando";
            rchElaborando.Size = new Size(548, 82);
            rchElaborando.TabIndex = 14;
            rchElaborando.Text = "";
            // 
            // rchFinalizados
            // 
            rchFinalizados.Location = new Point(12, 258);
            rchFinalizados.Name = "rchFinalizados";
            rchFinalizados.Size = new Size(548, 96);
            rchFinalizados.TabIndex = 15;
            rchFinalizados.Text = "";
            // 
            // lblFinalizados
            // 
            lblFinalizados.AutoSize = true;
            lblFinalizados.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            lblFinalizados.Location = new Point(12, 223);
            lblFinalizados.Name = "lblFinalizados";
            lblFinalizados.Size = new Size(136, 32);
            lblFinalizados.TabIndex = 16;
            lblFinalizados.Text = "Finalizados:";
            // 
            // pcbComida
            // 
            pcbComida.Location = new Point(12, 139);
            pcbComida.Name = "pcbComida";
            pcbComida.Size = new Size(102, 81);
            pcbComida.SizeMode = PictureBoxSizeMode.Zoom;
            pcbComida.TabIndex = 17;
            pcbComida.TabStop = false;
            // 
            // lblProximaComida
            // 
            lblProximaComida.AutoSize = true;
            lblProximaComida.Location = new Point(220, 138);
            lblProximaComida.Name = "lblProximaComida";
            lblProximaComida.Size = new Size(99, 15);
            lblProximaComida.TabIndex = 18;
            lblProximaComida.Text = "Proxima Comida:";
            // 
            // FrmView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.cocinero_icon;
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(943, 366);
            Controls.Add(lblProximaComida);
            Controls.Add(btnSiguiente);
            Controls.Add(pcbComida);
            Controls.Add(lblFinalizados);
            Controls.Add(rchFinalizados);
            Controls.Add(rchElaborando);
            Controls.Add(lblTmp);
            Controls.Add(lblTitleTmp);
            Controls.Add(btnAbrir);
            Controls.Add(lblTiempo);
            Controls.Add(lblTitleTiempo);
            Controls.Add(lblTitleComida);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "FrmView";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cocinero";
            FormClosing += FrmView_FormClosing;
            ((System.ComponentModel.ISupportInitialize)pcbComida).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblTitleComida;
        private Label lblTiempo;
        private Label lblTitleTiempo;
        private Button btnAbrir;
        private Label lblTmp;
        private Label lblTitleTmp;
        private Button btnSiguiente;
        private RichTextBox rchElaborando;
        private RichTextBox rchFinalizados;
        private Label lblFinalizados;
        private PictureBox pcbComida;
        private Label lblProximaComida;
    }
}
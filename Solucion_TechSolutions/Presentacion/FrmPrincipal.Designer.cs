namespace Presentacion
{
    partial class FrmPrincipal
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timerInactividad = new System.Windows.Forms.Timer(this.components);
            this.lblExpira = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuClientes = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuProductos = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVentas = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReportes = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuConsultaVentas = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUsuarios = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCerrarSesion = new System.Windows.Forms.ToolStripMenuItem();
            this.lblBienvenida = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // menuStrip1
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.mnuClientes, this.mnuProductos, this.mnuVentas, this.mnuReportes, this.mnuConsultaVentas, this.mnuUsuarios, this.mnuCerrarSesion });
            // mnuClientes
            this.mnuClientes.Name = "mnuClientes";
            this.mnuClientes.Text = "Clientes";
            this.mnuClientes.Click += new System.EventHandler(this.mnuClientes_Click);
            // mnuProductos
            this.mnuProductos.Name = "mnuProductos";
            this.mnuProductos.Text = "Productos";
            this.mnuProductos.Click += new System.EventHandler(this.mnuProductos_Click);
            // mnuVentas
            this.mnuVentas.Name = "mnuVentas";
            this.mnuVentas.Text = "Ventas";
            this.mnuVentas.Click += new System.EventHandler(this.mnuVentas_Click);
            // mnuReportes
            this.mnuReportes.Name = "mnuReportes";
            this.mnuReportes.Text = "Reportes";
            this.mnuReportes.Click += new System.EventHandler(this.mnuReportes_Click);
            // mnuConsultaVentas
            this.mnuConsultaVentas.Name = "mnuConsultaVentas";
            this.mnuConsultaVentas.Text = "Consulta de ventas";
            this.mnuConsultaVentas.Click += new System.EventHandler(this.mnuConsultaVentas_Click);
            // mnuUsuarios
            this.mnuUsuarios.Name = "mnuUsuarios";
            this.mnuUsuarios.Text = "Usuarios";
            this.mnuUsuarios.Click += new System.EventHandler(this.mnuUsuarios_Click);
            // mnuCerrarSesion
            this.mnuCerrarSesion.Name = "mnuCerrarSesion";
            this.mnuCerrarSesion.Text = "Cerrar sesión";
            this.mnuCerrarSesion.Click += new System.EventHandler(this.mnuCerrarSesion_Click);
            // lblBienvenida
            this.lblBienvenida.Name = "lblBienvenida";
            this.lblBienvenida.Text = "Bienvenido";
            this.lblBienvenida.Location = new System.Drawing.Point(20, 60);
            this.lblBienvenida.AutoSize = true;
            this.lblBienvenida.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            // timerInactividad (revisa cada segundo si la sesión venció por inactividad)
            this.timerInactividad.Interval = 1000;
            this.timerInactividad.Tick += new System.EventHandler(this.timerInactividad_Tick);
            // lblExpira
            this.lblExpira.Name = "lblExpira";
            this.lblExpira.Text = "";
            this.lblExpira.Location = new System.Drawing.Point(20, 100);
            this.lblExpira.AutoSize = true;
            // FrmPrincipal
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.lblBienvenida);
            this.Controls.Add(this.lblExpira);
            this.ClientSize = new System.Drawing.Size(560, 240);
            this.Name = "FrmPrincipal";
            this.Text = "Tech Solutions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.MainMenuStrip = this.menuStrip1;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Label lblBienvenida;
        private System.Windows.Forms.Label lblExpira;
        private System.Windows.Forms.Timer timerInactividad;
        private System.Windows.Forms.ToolStripMenuItem mnuClientes;
        private System.Windows.Forms.ToolStripMenuItem mnuProductos;
        private System.Windows.Forms.ToolStripMenuItem mnuVentas;
        private System.Windows.Forms.ToolStripMenuItem mnuReportes;
        private System.Windows.Forms.ToolStripMenuItem mnuConsultaVentas;
        private System.Windows.Forms.ToolStripMenuItem mnuUsuarios;
        private System.Windows.Forms.ToolStripMenuItem mnuCerrarSesion;
    }
}

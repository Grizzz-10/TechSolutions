namespace Presentacion
{
    partial class FrmCliente
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblDocumento = new System.Windows.Forms.Label();
            this.txtDocumento = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblCorreo = new System.Windows.Forms.Label();
            this.txtCorreo = new System.Windows.Forms.TextBox();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnReporte = new System.Windows.Forms.Button();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.dgvClientes = new System.Windows.Forms.DataGridView();
            this.SuspendLayout();
            // lblTitulo
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Location = new System.Drawing.Point(20, 12);
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Text = "Registrar cliente";
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.TabIndex = 0;
            // lblDocumento
            this.lblDocumento.Name = "lblDocumento";
            this.lblDocumento.Location = new System.Drawing.Point(20, 48);
            this.lblDocumento.AutoSize = true;
            this.lblDocumento.Text = "DNI / RUC:";
            this.lblDocumento.TabIndex = 1;
            // txtDocumento
            this.txtDocumento.Name = "txtDocumento";
            this.txtDocumento.Location = new System.Drawing.Point(120, 45);
            this.txtDocumento.Size = new System.Drawing.Size(150, 23);
            this.txtDocumento.MaxLength = 11;
            this.txtDocumento.TabIndex = 2;
            // lblNombre
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Location = new System.Drawing.Point(20, 81);
            this.lblNombre.AutoSize = true;
            this.lblNombre.Text = "Nombre:";
            this.lblNombre.TabIndex = 3;
            // txtNombre
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Location = new System.Drawing.Point(120, 78);
            this.txtNombre.Size = new System.Drawing.Size(300, 23);
            this.txtNombre.MaxLength = 100;
            this.txtNombre.TabIndex = 4;
            // lblCorreo
            this.lblCorreo.Name = "lblCorreo";
            this.lblCorreo.Location = new System.Drawing.Point(20, 114);
            this.lblCorreo.AutoSize = true;
            this.lblCorreo.Text = "Correo:";
            this.lblCorreo.TabIndex = 5;
            // txtCorreo
            this.txtCorreo.Name = "txtCorreo";
            this.txtCorreo.Location = new System.Drawing.Point(120, 111);
            this.txtCorreo.Size = new System.Drawing.Size(300, 23);
            this.txtCorreo.MaxLength = 100;
            this.txtCorreo.TabIndex = 6;
            // lblTelefono
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.Location = new System.Drawing.Point(20, 147);
            this.lblTelefono.AutoSize = true;
            this.lblTelefono.Text = "Teléfono:";
            this.lblTelefono.TabIndex = 7;
            // txtTelefono
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Location = new System.Drawing.Point(120, 144);
            this.txtTelefono.Size = new System.Drawing.Size(150, 23);
            this.txtTelefono.MaxLength = 15;
            this.txtTelefono.TabIndex = 8;
            // btnGuardar
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Location = new System.Drawing.Point(120, 180);
            this.btnGuardar.Size = new System.Drawing.Size(120, 30);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            this.btnGuardar.TabIndex = 9;
            // btnReporte
            this.btnReporte.Name = "btnReporte";
            this.btnReporte.Location = new System.Drawing.Point(255, 180);
            this.btnReporte.Size = new System.Drawing.Size(150, 30);
            this.btnReporte.Text = "Reporte de clientes";
            this.btnReporte.UseVisualStyleBackColor = true;
            this.btnReporte.Click += new System.EventHandler(this.btnReporte_Click);
            this.btnReporte.TabIndex = 10;
            // lblBuscar
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Location = new System.Drawing.Point(20, 232);
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Text = "Buscar:";
            this.lblBuscar.TabIndex = 11;
            // txtBuscar
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Location = new System.Drawing.Point(120, 229);
            this.txtBuscar.Size = new System.Drawing.Size(300, 23);
            this.txtBuscar.MaxLength = 100;
            this.txtBuscar.TabIndex = 12;
            // btnBuscar
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Location = new System.Drawing.Point(430, 227);
            this.btnBuscar.Size = new System.Drawing.Size(90, 28);
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            this.btnBuscar.TabIndex = 13;
            // dgvClientes
            this.dgvClientes.Name = "dgvClientes";
            this.dgvClientes.Location = new System.Drawing.Point(20, 265);
            this.dgvClientes.Size = new System.Drawing.Size(600, 180);
            this.dgvClientes.AllowUserToAddRows = false;
            this.dgvClientes.AllowUserToDeleteRows = false;
            this.dgvClientes.ReadOnly = true;
            this.dgvClientes.MultiSelect = false;
            this.dgvClientes.RowHeadersVisible = false;
            this.dgvClientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvClientes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvClientes.TabIndex = 14;
            // FrmCliente
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblDocumento);
            this.Controls.Add(this.txtDocumento);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.lblCorreo);
            this.Controls.Add(this.txtCorreo);
            this.Controls.Add(this.lblTelefono);
            this.Controls.Add(this.txtTelefono);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnReporte);
            this.Controls.Add(this.lblBuscar);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.dgvClientes);
            this.ClientSize = new System.Drawing.Size(640, 460);
            this.Name = "FrmCliente";
            this.Text = "Clientes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Load += new System.EventHandler(this.FrmCliente_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblDocumento;
        private System.Windows.Forms.TextBox txtDocumento;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblCorreo;
        private System.Windows.Forms.TextBox txtCorreo;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnReporte;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DataGridView dgvClientes;
    }
}

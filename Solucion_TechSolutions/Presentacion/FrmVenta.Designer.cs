namespace Presentacion
{
    partial class FrmVenta
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
            this.lblCliente = new System.Windows.Forms.Label();
            this.cboCliente = new System.Windows.Forms.ComboBox();
            this.lblProducto = new System.Windows.Forms.Label();
            this.cboProducto = new System.Windows.Forms.ComboBox();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.nudCantidad = new System.Windows.Forms.NumericUpDown();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // lblTitulo
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Location = new System.Drawing.Point(20, 12);
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Text = "Nueva venta";
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.TabIndex = 0;
            // lblCliente
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Location = new System.Drawing.Point(20, 53);
            this.lblCliente.AutoSize = true;
            this.lblCliente.Text = "Cliente:";
            this.lblCliente.TabIndex = 1;
            // cboCliente
            this.cboCliente.Name = "cboCliente";
            this.cboCliente.Location = new System.Drawing.Point(110, 50);
            this.cboCliente.Size = new System.Drawing.Size(340, 23);
            this.cboCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCliente.TabIndex = 2;
            // lblProducto
            this.lblProducto.Name = "lblProducto";
            this.lblProducto.Location = new System.Drawing.Point(20, 91);
            this.lblProducto.AutoSize = true;
            this.lblProducto.Text = "Producto:";
            this.lblProducto.TabIndex = 3;
            // cboProducto
            this.cboProducto.Name = "cboProducto";
            this.cboProducto.Location = new System.Drawing.Point(110, 88);
            this.cboProducto.Size = new System.Drawing.Size(340, 23);
            this.cboProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProducto.SelectedIndexChanged += new System.EventHandler(this.Calcular);
            this.cboProducto.TabIndex = 4;
            // lblCantidad
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Location = new System.Drawing.Point(20, 129);
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Text = "Cantidad:";
            this.lblCantidad.TabIndex = 5;
            // nudCantidad
            this.nudCantidad.Name = "nudCantidad";
            this.nudCantidad.Location = new System.Drawing.Point(110, 126);
            this.nudCantidad.Size = new System.Drawing.Size(100, 23);
            this.nudCantidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudCantidad.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            this.nudCantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudCantidad.ValueChanged += new System.EventHandler(this.Calcular);
            this.nudCantidad.TabIndex = 6;
            // lblTotal
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Location = new System.Drawing.Point(20, 170);
            this.lblTotal.AutoSize = true;
            this.lblTotal.Text = "Total estimado: S/ 0.00";
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotal.TabIndex = 7;
            // btnRegistrar
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Location = new System.Drawing.Point(110, 210);
            this.btnRegistrar.Size = new System.Drawing.Size(140, 32);
            this.btnRegistrar.Text = "Registrar venta";
            this.btnRegistrar.UseVisualStyleBackColor = true;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            this.btnRegistrar.TabIndex = 8;
            // FrmVenta
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblCliente);
            this.Controls.Add(this.cboCliente);
            this.Controls.Add(this.lblProducto);
            this.Controls.Add(this.cboProducto);
            this.Controls.Add(this.lblCantidad);
            this.Controls.Add(this.nudCantidad);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.btnRegistrar);
            this.ClientSize = new System.Drawing.Size(480, 280);
            this.Name = "FrmVenta";
            this.Text = "Registrar venta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Load += new System.EventHandler(this.FrmVenta_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.ComboBox cboCliente;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.ComboBox cboProducto;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.NumericUpDown nudCantidad;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnRegistrar;
    }
}

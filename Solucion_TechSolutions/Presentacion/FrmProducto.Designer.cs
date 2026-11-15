namespace Presentacion
{
    partial class FrmProducto
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
            this.lblCodigo = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.nudPrecio = new System.Windows.Forms.NumericUpDown();
            this.lblStock = new System.Windows.Forms.Label();
            this.nudStock = new System.Windows.Forms.NumericUpDown();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.lblLista = new System.Windows.Forms.Label();
            this.chkStockBajo = new System.Windows.Forms.CheckBox();
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.lblIngreso = new System.Windows.Forms.Label();
            this.nudIngreso = new System.Windows.Forms.NumericUpDown();
            this.btnIngresarStock = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // lblTitulo
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Location = new System.Drawing.Point(20, 12);
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Text = "Registrar producto";
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.TabIndex = 0;
            // lblCodigo
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Location = new System.Drawing.Point(20, 48);
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Text = "Código:";
            this.lblCodigo.TabIndex = 1;
            // txtCodigo
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Location = new System.Drawing.Point(120, 45);
            this.txtCodigo.Size = new System.Drawing.Size(150, 23);
            this.txtCodigo.MaxLength = 20;
            this.txtCodigo.TabIndex = 2;
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
            // lblPrecio
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.Location = new System.Drawing.Point(20, 114);
            this.lblPrecio.AutoSize = true;
            this.lblPrecio.Text = "Precio (S/):";
            this.lblPrecio.TabIndex = 5;
            // nudPrecio
            this.nudPrecio.Name = "nudPrecio";
            this.nudPrecio.Location = new System.Drawing.Point(120, 111);
            this.nudPrecio.Size = new System.Drawing.Size(120, 23);
            this.nudPrecio.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudPrecio.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            this.nudPrecio.DecimalPlaces = 2;
            this.nudPrecio.TabIndex = 6;
            // lblStock
            this.lblStock.Name = "lblStock";
            this.lblStock.Location = new System.Drawing.Point(20, 147);
            this.lblStock.AutoSize = true;
            this.lblStock.Text = "Stock inicial:";
            this.lblStock.TabIndex = 7;
            // nudStock
            this.nudStock.Name = "nudStock";
            this.nudStock.Location = new System.Drawing.Point(120, 144);
            this.nudStock.Size = new System.Drawing.Size(120, 23);
            this.nudStock.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudStock.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            this.nudStock.TabIndex = 8;
            // btnGuardar
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Location = new System.Drawing.Point(120, 180);
            this.btnGuardar.Size = new System.Drawing.Size(120, 30);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            this.btnGuardar.TabIndex = 9;
            // lblLista
            this.lblLista.Name = "lblLista";
            this.lblLista.Location = new System.Drawing.Point(20, 228);
            this.lblLista.AutoSize = true;
            this.lblLista.Text = "Inventario";
            this.lblLista.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblLista.TabIndex = 10;
            // chkStockBajo
            this.chkStockBajo.Name = "chkStockBajo";
            this.chkStockBajo.Location = new System.Drawing.Point(200, 227);
            this.chkStockBajo.AutoSize = true;
            this.chkStockBajo.Text = "Solo stock bajo (5 o menos)";
            this.chkStockBajo.CheckedChanged += new System.EventHandler(this.chkStockBajo_CheckedChanged);
            this.chkStockBajo.TabIndex = 11;
            // dgvProductos
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.Location = new System.Drawing.Point(20, 255);
            this.dgvProductos.Size = new System.Drawing.Size(600, 180);
            this.dgvProductos.AllowUserToAddRows = false;
            this.dgvProductos.AllowUserToDeleteRows = false;
            this.dgvProductos.ReadOnly = true;
            this.dgvProductos.MultiSelect = false;
            this.dgvProductos.RowHeadersVisible = false;
            this.dgvProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProductos.TabIndex = 12;
            // lblIngreso
            this.lblIngreso.Name = "lblIngreso";
            this.lblIngreso.Location = new System.Drawing.Point(20, 453);
            this.lblIngreso.AutoSize = true;
            this.lblIngreso.Text = "Ingresar mercadería al producto seleccionado:";
            this.lblIngreso.TabIndex = 13;
            // nudIngreso
            this.nudIngreso.Name = "nudIngreso";
            this.nudIngreso.Location = new System.Drawing.Point(330, 450);
            this.nudIngreso.Size = new System.Drawing.Size(90, 23);
            this.nudIngreso.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudIngreso.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            this.nudIngreso.Value = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudIngreso.TabIndex = 14;
            // btnIngresarStock
            this.btnIngresarStock.Name = "btnIngresarStock";
            this.btnIngresarStock.Location = new System.Drawing.Point(430, 447);
            this.btnIngresarStock.Size = new System.Drawing.Size(120, 28);
            this.btnIngresarStock.Text = "Ingresar stock";
            this.btnIngresarStock.UseVisualStyleBackColor = true;
            this.btnIngresarStock.Click += new System.EventHandler(this.btnIngresarStock_Click);
            this.btnIngresarStock.TabIndex = 15;
            // FrmProducto
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblCodigo);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.lblPrecio);
            this.Controls.Add(this.nudPrecio);
            this.Controls.Add(this.lblStock);
            this.Controls.Add(this.nudStock);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.lblLista);
            this.Controls.Add(this.chkStockBajo);
            this.Controls.Add(this.dgvProductos);
            this.Controls.Add(this.lblIngreso);
            this.Controls.Add(this.nudIngreso);
            this.Controls.Add(this.btnIngresarStock);
            this.ClientSize = new System.Drawing.Size(640, 500);
            this.Name = "FrmProducto";
            this.Text = "Productos e inventario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Load += new System.EventHandler(this.FrmProducto_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.NumericUpDown nudPrecio;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.NumericUpDown nudStock;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label lblLista;
        private System.Windows.Forms.CheckBox chkStockBajo;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.Label lblIngreso;
        private System.Windows.Forms.NumericUpDown nudIngreso;
        private System.Windows.Forms.Button btnIngresarStock;
    }
}

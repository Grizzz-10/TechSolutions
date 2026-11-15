namespace Presentacion
{
    partial class FrmReporte
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblDesde = new System.Windows.Forms.Label();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.lblHasta = new System.Windows.Forms.Label();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.lblVista = new System.Windows.Forms.Label();
            this.cboVista = new System.Windows.Forms.ComboBox();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.dgvVentas = new System.Windows.Forms.DataGridView();
            this.lblResumen = new System.Windows.Forms.Label();
            this.btnAnular = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // lblDesde
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.Location = new System.Drawing.Point(20, 18);
            this.lblDesde.AutoSize = true;
            this.lblDesde.Text = "Desde:";
            this.lblDesde.TabIndex = 0;
            // dtpDesde
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Location = new System.Drawing.Point(75, 15);
            this.dtpDesde.Size = new System.Drawing.Size(110, 23);
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.TabIndex = 1;
            // lblHasta
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Location = new System.Drawing.Point(200, 18);
            this.lblHasta.AutoSize = true;
            this.lblHasta.Text = "Hasta:";
            this.lblHasta.TabIndex = 2;
            // dtpHasta
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Location = new System.Drawing.Point(255, 15);
            this.dtpHasta.Size = new System.Drawing.Size(110, 23);
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.TabIndex = 3;
            // lblVista
            this.lblVista.Name = "lblVista";
            this.lblVista.Location = new System.Drawing.Point(385, 18);
            this.lblVista.AutoSize = true;
            this.lblVista.Text = "Vista:";
            this.lblVista.TabIndex = 4;
            // cboVista
            this.cboVista.Name = "cboVista";
            this.cboVista.Location = new System.Drawing.Point(430, 15);
            this.cboVista.Size = new System.Drawing.Size(150, 23);
            this.cboVista.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVista.SelectedIndexChanged += new System.EventHandler(this.cboVista_SelectedIndexChanged);
            this.cboVista.TabIndex = 5;
            // btnGenerar
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Location = new System.Drawing.Point(600, 12);
            this.btnGenerar.Size = new System.Drawing.Size(120, 30);
            this.btnGenerar.Text = "Generar";
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            this.btnGenerar.TabIndex = 6;
            // dgvVentas
            this.dgvVentas.Name = "dgvVentas";
            this.dgvVentas.Location = new System.Drawing.Point(20, 60);
            this.dgvVentas.Size = new System.Drawing.Size(740, 340);
            this.dgvVentas.AllowUserToAddRows = false;
            this.dgvVentas.AllowUserToDeleteRows = false;
            this.dgvVentas.ReadOnly = true;
            this.dgvVentas.MultiSelect = false;
            this.dgvVentas.RowHeadersVisible = false;
            this.dgvVentas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVentas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVentas.TabIndex = 7;
            // lblResumen
            this.lblResumen.Name = "lblResumen";
            this.lblResumen.Location = new System.Drawing.Point(20, 418);
            this.lblResumen.AutoSize = true;
            this.lblResumen.Text = "Ventas: 0  |  Total: S/ 0.00";
            this.lblResumen.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblResumen.TabIndex = 8;
            // btnAnular
            this.btnAnular.Name = "btnAnular";
            this.btnAnular.Location = new System.Drawing.Point(560, 410);
            this.btnAnular.Size = new System.Drawing.Size(200, 32);
            this.btnAnular.Text = "Anular venta seleccionada";
            this.btnAnular.UseVisualStyleBackColor = true;
            this.btnAnular.Click += new System.EventHandler(this.btnAnular_Click);
            this.btnAnular.TabIndex = 9;
            // FrmReporte
            this.Controls.Add(this.lblDesde);
            this.Controls.Add(this.dtpDesde);
            this.Controls.Add(this.lblHasta);
            this.Controls.Add(this.dtpHasta);
            this.Controls.Add(this.lblVista);
            this.Controls.Add(this.cboVista);
            this.Controls.Add(this.btnGenerar);
            this.Controls.Add(this.dgvVentas);
            this.Controls.Add(this.lblResumen);
            this.Controls.Add(this.btnAnular);
            this.ClientSize = new System.Drawing.Size(780, 460);
            this.Name = "FrmReporte";
            this.Text = "Reporte de ventas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Load += new System.EventHandler(this.FrmReporte_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.Label lblVista;
        private System.Windows.Forms.ComboBox cboVista;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.DataGridView dgvVentas;
        private System.Windows.Forms.Label lblResumen;
        private System.Windows.Forms.Button btnAnular;
    }
}

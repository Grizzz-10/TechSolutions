namespace Presentacion
{
    partial class FrmReporteClientes
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.btnVer = new System.Windows.Forms.Button();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // lblNombre
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(20, 18);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Text = "Nombre:";
            // txtNombre
            this.txtNombre.Location = new System.Drawing.Point(90, 15);
            this.txtNombre.MaxLength = 100;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(220, 23);
            this.txtNombre.TabIndex = 0;
            // btnVer
            this.btnVer.Location = new System.Drawing.Point(325, 13);
            this.btnVer.Name = "btnVer";
            this.btnVer.Size = new System.Drawing.Size(110, 27);
            this.btnVer.TabIndex = 1;
            this.btnVer.Text = "Ver reporte";
            this.btnVer.UseVisualStyleBackColor = true;
            this.btnVer.Click += new System.EventHandler(this.btnVer_Click);
            // reportViewer1
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.reportViewer1.Location = new System.Drawing.Point(0, 52);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(860, 548);
            this.reportViewer1.TabIndex = 2;
            // FrmReporteClientes
            this.AcceptButton = this.btnVer;
            this.ClientSize = new System.Drawing.Size(860, 600);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.btnVer);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FrmReporteClientes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reporte de clientes";
            this.Load += new System.EventHandler(this.FrmReporteClientes_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Button btnVer;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
    }
}

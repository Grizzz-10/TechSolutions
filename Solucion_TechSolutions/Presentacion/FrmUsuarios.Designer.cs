namespace Presentacion
{
    partial class FrmUsuarios
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
            this.lblLogin = new System.Windows.Forms.Label();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.lblClave = new System.Windows.Forms.Label();
            this.txtClave = new System.Windows.Forms.TextBox();
            this.lblConfirmar = new System.Windows.Forms.Label();
            this.txtConfirmar = new System.Windows.Forms.TextBox();
            this.lblRol = new System.Windows.Forms.Label();
            this.cboRol = new System.Windows.Forms.ComboBox();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.lblLista = new System.Windows.Forms.Label();
            this.dgvUsuarios = new System.Windows.Forms.DataGridView();
            this.btnDesbloquear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // lblTitulo
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Location = new System.Drawing.Point(20, 12);
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Text = "Registrar usuario";
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.TabIndex = 0;
            // lblLogin
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Location = new System.Drawing.Point(20, 48);
            this.lblLogin.AutoSize = true;
            this.lblLogin.Text = "Usuario:";
            this.lblLogin.TabIndex = 1;
            // txtLogin
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Location = new System.Drawing.Point(140, 45);
            this.txtLogin.Size = new System.Drawing.Size(200, 23);
            this.txtLogin.MaxLength = 30;
            this.txtLogin.TabIndex = 2;
            // lblClave
            this.lblClave.Name = "lblClave";
            this.lblClave.Location = new System.Drawing.Point(20, 81);
            this.lblClave.AutoSize = true;
            this.lblClave.Text = "Contraseña:";
            this.lblClave.TabIndex = 3;
            // txtClave
            this.txtClave.Name = "txtClave";
            this.txtClave.Location = new System.Drawing.Point(140, 78);
            this.txtClave.Size = new System.Drawing.Size(200, 23);
            this.txtClave.MaxLength = 64;
            this.txtClave.UseSystemPasswordChar = true;
            this.txtClave.TabIndex = 4;
            // lblConfirmar
            this.lblConfirmar.Name = "lblConfirmar";
            this.lblConfirmar.Location = new System.Drawing.Point(20, 114);
            this.lblConfirmar.AutoSize = true;
            this.lblConfirmar.Text = "Confirmar:";
            this.lblConfirmar.TabIndex = 5;
            // txtConfirmar
            this.txtConfirmar.Name = "txtConfirmar";
            this.txtConfirmar.Location = new System.Drawing.Point(140, 111);
            this.txtConfirmar.Size = new System.Drawing.Size(200, 23);
            this.txtConfirmar.MaxLength = 64;
            this.txtConfirmar.UseSystemPasswordChar = true;
            this.txtConfirmar.TabIndex = 6;
            // lblRol
            this.lblRol.Name = "lblRol";
            this.lblRol.Location = new System.Drawing.Point(20, 147);
            this.lblRol.AutoSize = true;
            this.lblRol.Text = "Rol:";
            this.lblRol.TabIndex = 7;
            // cboRol
            this.cboRol.Name = "cboRol";
            this.cboRol.Location = new System.Drawing.Point(140, 144);
            this.cboRol.Size = new System.Drawing.Size(200, 23);
            this.cboRol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRol.TabIndex = 8;
            // btnRegistrar
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Location = new System.Drawing.Point(140, 180);
            this.btnRegistrar.Size = new System.Drawing.Size(120, 30);
            this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.UseVisualStyleBackColor = true;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            this.btnRegistrar.TabIndex = 9;
            // lblLista
            this.lblLista.Name = "lblLista";
            this.lblLista.Location = new System.Drawing.Point(20, 225);
            this.lblLista.AutoSize = true;
            this.lblLista.Text = "Usuarios registrados";
            this.lblLista.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblLista.TabIndex = 10;
            // dgvUsuarios
            this.dgvUsuarios.Name = "dgvUsuarios";
            this.dgvUsuarios.Location = new System.Drawing.Point(20, 250);
            this.dgvUsuarios.Size = new System.Drawing.Size(520, 125);
            this.dgvUsuarios.AllowUserToAddRows = false;
            this.dgvUsuarios.AllowUserToDeleteRows = false;
            this.dgvUsuarios.ReadOnly = true;
            this.dgvUsuarios.MultiSelect = false;
            this.dgvUsuarios.RowHeadersVisible = false;
            this.dgvUsuarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsuarios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsuarios.TabIndex = 11;
            // btnDesbloquear
            this.btnDesbloquear.Name = "btnDesbloquear";
            this.btnDesbloquear.Location = new System.Drawing.Point(20, 385);
            this.btnDesbloquear.Size = new System.Drawing.Size(170, 30);
            this.btnDesbloquear.Text = "Desbloquear seleccionado";
            this.btnDesbloquear.UseVisualStyleBackColor = true;
            this.btnDesbloquear.Click += new System.EventHandler(this.btnDesbloquear_Click);
            this.btnDesbloquear.TabIndex = 12;
            // FrmUsuarios
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblLogin);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.lblClave);
            this.Controls.Add(this.txtClave);
            this.Controls.Add(this.lblConfirmar);
            this.Controls.Add(this.txtConfirmar);
            this.Controls.Add(this.lblRol);
            this.Controls.Add(this.cboRol);
            this.Controls.Add(this.btnRegistrar);
            this.Controls.Add(this.lblLista);
            this.Controls.Add(this.dgvUsuarios);
            this.Controls.Add(this.btnDesbloquear);
            this.ClientSize = new System.Drawing.Size(560, 430);
            this.Name = "FrmUsuarios";
            this.Text = "Usuarios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Load += new System.EventHandler(this.FrmUsuarios_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Label lblClave;
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.Label lblConfirmar;
        private System.Windows.Forms.TextBox txtConfirmar;
        private System.Windows.Forms.Label lblRol;
        private System.Windows.Forms.ComboBox cboRol;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.Label lblLista;
        private System.Windows.Forms.DataGridView dgvUsuarios;
        private System.Windows.Forms.Button btnDesbloquear;
    }
}

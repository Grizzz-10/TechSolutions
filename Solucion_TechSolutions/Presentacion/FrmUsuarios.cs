// CAPA PRESENTACIÓN – usuarios: registrar (se guarda sal + hash, nunca la contraseña) y desbloquear
using System;
using System.Windows.Forms;
using Entidades;
using Negocio;

namespace Presentacion
{
    public partial class FrmUsuarios : Form
    {
        private readonly UsuarioBLL bll = new UsuarioBLL();
        private readonly bool primerAdministrador;

        // primerAdministrador = true: aún no hay usuarios; se crea el Administrador y se cierra.
        public FrmUsuarios(bool primerAdministrador)
        {
            InitializeComponent();
            this.primerAdministrador = primerAdministrador;
        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            cboRol.Items.AddRange(Roles.Todos);

            if (primerAdministrador)
            {
                Text = "Configuración inicial: primer administrador";
                lblTitulo.Text = "Cree el administrador del sistema";
                cboRol.SelectedItem = Roles.Administrador;
                cboRol.Enabled = false;                     // el primer usuario siempre es Administrador
                lblLista.Visible = false;
                dgvUsuarios.Visible = false;
                btnDesbloquear.Visible = false;
                ClientSize = new System.Drawing.Size(ClientSize.Width, 230);
                return;
            }

            cboRol.SelectedIndex = 1;                       // Vendedor por defecto
            CargarLista();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtClave.Text != txtConfirmar.Text)
                    throw new NegocioException("Las contraseñas no coinciden.");

                if (primerAdministrador)
                    bll.RegistrarPrimerAdministrador(txtLogin.Text.Trim(), txtClave.Text);
                else
                    bll.Registrar(txtLogin.Text.Trim(), txtClave.Text, cboRol.Text);

                txtLogin.Clear(); txtClave.Clear(); txtConfirmar.Clear();
                Mensajes.Info("Usuario registrado. Se guardaron la sal y el hash, no la contraseña.");

                if (primerAdministrador) Close();
                else CargarLista();
            }
            catch (Exception ex)
            {
                Mensajes.Mostrar(ex);
            }
        }

        private void btnDesbloquear_Click(object sender, EventArgs e)
        {
            try
            {
                var u = dgvUsuarios.CurrentRow == null ? null : dgvUsuarios.CurrentRow.DataBoundItem as Usuario;
                if (u == null) throw new NegocioException("Seleccione un usuario de la lista.");
                if (!u.Bloqueado && u.IntentosFallidos == 0) throw new NegocioException("Ese usuario no está bloqueado.");

                bll.Desbloquear(u.Id);
                CargarLista();
                Mensajes.Info("Usuario desbloqueado: " + u.Login);
            }
            catch (Exception ex)
            {
                Mensajes.Mostrar(ex);
            }
        }

        private void CargarLista()
        {
            try
            {
                dgvUsuarios.DataSource = bll.Listar();
            }
            catch (Exception ex)
            {
                Mensajes.Mostrar(ex);
            }
        }
    }
}

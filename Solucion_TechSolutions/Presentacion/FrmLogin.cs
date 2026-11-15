// CAPA PRESENTACIÓN – inicio de sesión
using System;
using System.Windows.Forms;
using Negocio;

namespace Presentacion
{
    public partial class FrmLogin : Form
    {
        private readonly UsuarioBLL bll = new UsuarioBLL();

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                bll.IniciarSesion(txtLogin.Text.Trim(), txtClave.Text);
                txtClave.Clear();
                DialogResult = DialogResult.OK;           // cierra el login y deja pasar al menú
            }
            catch (UnauthorizedAccessException ex)         // clave incorrecta o cuenta bloqueada
            {
                Mensajes.Mostrar(ex);
                txtClave.Clear();
                txtClave.Focus();
            }
            catch (Exception ex)
            {
                Mensajes.Mostrar(ex);
            }
        }
    }
}

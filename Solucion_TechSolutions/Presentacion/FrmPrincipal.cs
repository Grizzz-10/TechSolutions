// CAPA PRESENTACIÓN – menú con niveles de acceso por rol + cierre de sesión por inactividad
using System;
using System.Linq;
using System.Windows.Forms;
using Entidades;
using Negocio;

namespace Presentacion
{
    public partial class FrmPrincipal : Form
    {
        private readonly FiltroInactividad filtro = new FiltroInactividad();

        public FrmPrincipal()
        {
            InitializeComponent();

            Usuario u = Sesion.Instancia.Usuario;
            if (u == null) throw new InvalidOperationException("No hay una sesión activa.");

            lblBienvenida.Text = "Bienvenido, " + u.Login + "  (" + u.Rol + ")";

            // Cada rol ve solo lo que necesita (la tabla completa está en Negocio/Permisos.cs).
            // OJO: ocultar el menú es solo comodidad; la seguridad real la aplica la capa de Negocio.
            mnuClientes.Visible = Permisos.Puede(u.Rol, Modulo.Clientes);
            mnuProductos.Visible = Permisos.Puede(u.Rol, Modulo.Productos);
            mnuVentas.Visible = Permisos.Puede(u.Rol, Modulo.Ventas);
            mnuReportes.Visible = Permisos.Puede(u.Rol, Modulo.Reportes);
            mnuConsultaVentas.Visible = Permisos.Puede(u.Rol, Modulo.Reportes);
            mnuUsuarios.Visible = Permisos.Puede(u.Rol, Modulo.Usuarios);

            // Inactividad: el filtro reinicia el contador con cada movimiento/tecla; el Timer lo revisa cada segundo.
            Application.AddMessageFilter(filtro);
            FormClosed += FrmPrincipal_FormClosed;
            Sesion.Instancia.RegistrarActividad();
            ActualizarContador();
            timerInactividad.Start();
        }

        private void timerInactividad_Tick(object sender, EventArgs e)
        {
            Sesion sesion = Sesion.Instancia;

            // Vencida por tiempo (o ya cerrada por la capa de Negocio): se cierra todo y se vuelve al login.
            if (!sesion.EstaActiva || sesion.Expirada)
            {
                timerInactividad.Stop();
                sesion.CerrarPorInactividad();

                foreach (Form f in Application.OpenForms.Cast<Form>().Where(x => x != this).ToList())
                    f.Close();                                  // cierra las ventanas hijas abiertas

                BeginInvoke(new Action(Close));                 // y luego el menú (Program.cs muestra el login y el aviso)
                return;
            }
            ActualizarContador();
        }

        private void ActualizarContador()
        {
            lblExpira.Text = "La sesión se cerrará por inactividad en " + Sesion.Instancia.TiempoRestante.ToString(@"mm\:ss");
        }

        private void mnuClientes_Click(object sender, EventArgs e) { Abrir(new FrmCliente()); }
        private void mnuProductos_Click(object sender, EventArgs e) { Abrir(new FrmProducto()); }
        private void mnuVentas_Click(object sender, EventArgs e) { Abrir(new FrmVenta()); }
        // Reportes: el reporte real (visor + ReporteClientes.rdlc, con procedimiento almacenado).
        private void mnuReportes_Click(object sender, EventArgs e) { Abrir(new FrmReporteClientes()); }
        // Consulta de ventas: la grilla con vistas LINQ y la anulación de ventas (transacción).
        private void mnuConsultaVentas_Click(object sender, EventArgs e) { Abrir(new FrmReporte()); }
        private void mnuUsuarios_Click(object sender, EventArgs e) { Abrir(new FrmUsuarios(false)); }

        private void mnuCerrarSesion_Click(object sender, EventArgs e)
        {
            Close();   // vuelve al login (ver Program.cs)
        }

        private void FrmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            timerInactividad.Stop();
            Application.RemoveMessageFilter(filtro);
        }

        private static void Abrir(Form f)
        {
            using (f) f.ShowDialog();
        }
    }
}

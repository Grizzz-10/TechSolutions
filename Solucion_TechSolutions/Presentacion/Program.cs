using System;
using System.Configuration;
using System.Windows.Forms;
using Negocio;

namespace Presentacion
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 0) Tiempo de inactividad permitido (App.config > MinutosInactividad; por defecto 5 minutos).
            int minutos;
            if (int.TryParse(ConfigurationManager.AppSettings["MinutosInactividad"], out minutos) && minutos >= 1 && minutos <= 120)
                Sesion.TiempoMaximoInactividad = TimeSpan.FromMinutes(minutos);

            // 1) Si todavía no hay usuarios, primero se crea el Administrador (no viene en el script SQL).
            var usuarios = new UsuarioBLL();
            try
            {
                if (!usuarios.HayUsuarios())
                {
                    using (var inicial = new FrmUsuarios(true))
                        inicial.ShowDialog();
                    if (!usuarios.HayUsuarios()) return;      // cerró sin registrar
                }
            }
            catch (Exception ex)
            {
                Mensajes.Mostrar(ex);
                return;
            }

            // 2) Ciclo de sesiones: login -> menú principal -> (cerrar sesión) -> login...
            while (true)
            {
                using (var login = new FrmLogin())
                    if (login.ShowDialog() != DialogResult.OK) break;     // cerró el login: termina la aplicación

                using (var principal = new FrmPrincipal())
                    principal.ShowDialog();

                if (Sesion.Instancia.CerradaPorInactividad)
                    Mensajes.Info("Su sesión se cerró por inactividad. Inicie sesión de nuevo.");

                usuarios.CerrarSesion();                                  // al salir del menú, se limpia la sesión
            }
        }
    }
}

// CAPA PRESENTACIÓN – un solo lugar para decidir QUÉ se le muestra al usuario cuando algo falla.
using System;
using System.Diagnostics;
using System.Windows.Forms;
using Entidades;

namespace Presentacion
{
    internal static class Mensajes
    {
        public static void Mostrar(Exception ex)
        {
            if (ex is NegocioException)                       // validación o regla de negocio: se muestra tal cual
            {
                MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (ex is UnauthorizedAccessException)       // sin permiso o login fallido
            {
                MessageBox.Show(ex.Message, "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else                                              // error inesperado: el detalle técnico NO se muestra
            {
                Trace.TraceError(ex.ToString());              // queda en el registro de diagnóstico, no en pantalla
                MessageBox.Show(
                    "Ocurrió un error inesperado. Verifique la conexión con la base de datos e intente de nuevo.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void Info(string texto)
        {
            MessageBox.Show(texto, "Tech Solutions", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

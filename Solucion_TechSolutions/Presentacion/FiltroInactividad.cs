// CAPA PRESENTACIÓN – detecta actividad del usuario (mouse y teclado) en TODA la aplicación,
// incluidas las ventanas hijas, y avisa a la sesión para reiniciar el contador de inactividad.
using System.Drawing;
using System.Windows.Forms;
using Negocio;

namespace Presentacion
{
    internal sealed class FiltroInactividad : IMessageFilter
    {
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_MOUSEMOVE = 0x0200;
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_RBUTTONDOWN = 0x0204;
        private const int WM_MOUSEWHEEL = 0x020A;

        private Point ultimaPosicion;

        public bool PreFilterMessage(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_MOUSEMOVE:
                    {
                        // Windows envía WM_MOUSEMOVE aun sin mover el mouse: solo cuenta si la posición cambió.
                        Point actual = Cursor.Position;
                        if (actual != ultimaPosicion)
                        {
                            ultimaPosicion = actual;
                            Sesion.Instancia.RegistrarActividad();
                        }
                        break;
                    }
                case WM_KEYDOWN:
                case WM_LBUTTONDOWN:
                case WM_RBUTTONDOWN:
                case WM_MOUSEWHEEL:
                    Sesion.Instancia.RegistrarActividad();
                    break;
            }
            return false;   // nunca "consume" el mensaje: la aplicación funciona igual
        }
    }
}

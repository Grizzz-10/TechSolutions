// Proyecto: Entidades
using System.ComponentModel;

namespace Entidades
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Documento { get; set; }   // DNI (8 dígitos) o RUC (11 dígitos)
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }

        [Browsable(false)] public string Descripcion { get { return Nombre + " (" + Documento + ")"; } }
    }
}

// Proyecto: Entidades
using System.ComponentModel;

namespace Entidades
{
    public class Producto
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }

        [Browsable(false)] public string Descripcion { get { return Codigo + " - " + Nombre + " (stock: " + Stock + ")"; } }
    }
}

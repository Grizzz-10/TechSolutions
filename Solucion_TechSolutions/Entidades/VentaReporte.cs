// Proyecto: Entidades
using System;
using System.ComponentModel;

namespace Entidades
{
    // Fila del reporte de ventas.
    public class VentaReporte
    {
        public int IdVenta { get; set; }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Total { get; set; }
        public string Vendedor { get; set; }

        [Browsable(false)] public bool Anulada { get; set; }
        public string Estado { get { return Anulada ? "ANULADA" : "Vigente"; } }
    }
}

// Proyecto: Entidades
namespace Entidades
{
    // Fila de un resumen de ventas agrupado (por producto, vendedor o cliente). Se arma con LINQ en Negocio.
    public class ResumenVenta
    {
        public string Concepto { get; set; }
        public int NumVentas { get; set; }
        public int Unidades { get; set; }
        public decimal Total { get; set; }
    }
}

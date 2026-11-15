// Proyecto: Entidades
namespace Entidades
{
    // Datos que se necesitan para REGISTRAR una venta.
    // El precio NO viaja desde la pantalla: lo toma la base de datos en el momento de la venta.
    public class Venta
    {
        public int IdCliente { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public int IdUsuario { get; set; }   // lo asigna la capa de Negocio desde la sesión, no la pantalla
    }
}

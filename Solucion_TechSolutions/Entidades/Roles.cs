// Proyecto: Entidades (Biblioteca de clases)
namespace Entidades
{
    // Nombres de roles en un solo lugar (evita escribir "Administrador" a mano en cada capa).
    public static class Roles
    {
        public const string Administrador = "Administrador";
        public const string Vendedor = "Vendedor";
        public const string Almacenero = "Almacenero";

        public static readonly string[] Todos = { Administrador, Vendedor, Almacenero };
    }
}

// CAPA NEGOCIO – reglas de productos e inventario
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Datos;
using Entidades;

namespace Negocio
{
    public class ProductoBLL
    {
        private static readonly Regex patronCodigo = new Regex(@"^[A-Za-z0-9\-]{2,20}$", RegexOptions.Compiled);
        public const int UmbralStockBajo = 5;
        private readonly ProductoDAL dal = new ProductoDAL();

        public void Registrar(Producto p)
        {
            Permisos.Exigir(Modulo.Productos);

            p.Codigo = (p.Codigo ?? "").Trim();
            p.Nombre = (p.Nombre ?? "").Trim();

            if (!patronCodigo.IsMatch(p.Codigo))
                throw new NegocioException("El código debe tener de 2 a 20 caracteres (letras, números o guion).");
            if (p.Nombre.Length == 0 || p.Nombre.Length > 100)
                throw new NegocioException("El nombre es obligatorio (máximo 100 caracteres).");
            if (p.Precio <= 0 || p.Precio > 1000000m)
                throw new NegocioException("El precio debe ser mayor que 0.");
            if (p.Stock < 0 || p.Stock > 1000000)
                throw new NegocioException("El stock inicial no es válido.");

            dal.Insertar(p);
        }

        // Ventas también necesita ver los productos (y su stock) para vender.
        public List<Producto> Listar()
        {
            Permisos.Exigir(Modulo.Productos, Modulo.Ventas);
            return dal.Listar();
        }

        // LINQ: productos con stock bajo, del más urgente (menos stock) al menos urgente.
        public List<Producto> StockBajo(List<Producto> productos, int umbral)
        {
            Permisos.Exigir(Modulo.Productos);
            return productos
                .Where(p => p.Stock <= umbral)
                .OrderBy(p => p.Stock)
                .ThenBy(p => p.Nombre)
                .ToList();
        }

        public void IngresarStock(int idProducto, int cantidad)
        {
            Permisos.Exigir(Modulo.Productos);
            if (cantidad < 1 || cantidad > 100000)
                throw new NegocioException("La cantidad debe estar entre 1 y 100 000.");
            dal.AgregarStock(idProducto, cantidad);
        }
    }
}

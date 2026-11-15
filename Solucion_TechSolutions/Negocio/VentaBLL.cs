// CAPA NEGOCIO – reglas de ventas y reportes
using System;
using System.Collections.Generic;
using System.Linq;
using Datos;
using Entidades;

namespace Negocio
{
    public class VentaBLL
    {
        public static readonly string[] Agrupaciones = { "Producto", "Vendedor", "Cliente" };

        private readonly VentaDAL dal = new VentaDAL();

        public void Registrar(int idCliente, int idProducto, int cantidad)
        {
            Permisos.Exigir(Modulo.Ventas);

            if (idCliente <= 0) throw new NegocioException("Seleccione un cliente.");
            if (idProducto <= 0) throw new NegocioException("Seleccione un producto.");
            if (cantidad < 1 || cantidad > 10000) throw new NegocioException("La cantidad debe estar entre 1 y 10 000.");

            dal.Registrar(new Venta
            {
                IdCliente = idCliente,
                IdProducto = idProducto,
                Cantidad = cantidad,
                IdUsuario = Sesion.Instancia.Usuario.Id    // el vendedor sale de la sesión, no de la pantalla
            });
        }

        // Anula una venta y devuelve el stock (transacción COMMIT/ROLLBACK en la capa de Datos).
        public void Anular(int idVenta)
        {
            Permisos.Exigir(Modulo.Reportes);          // solo Administrador
            if (idVenta <= 0) throw new NegocioException("Seleccione una venta.");
            dal.Anular(idVenta, Sesion.Instancia.Usuario.Id);
        }

        // LINQ: resumen de ventas VIGENTES agrupadas por producto, vendedor o cliente (de mayor a menor total).
        public List<ResumenVenta> Resumir(List<VentaReporte> ventas, string agrupacion)
        {
            Permisos.Exigir(Modulo.Reportes);

            Func<VentaReporte, string> clave;
            switch (agrupacion)
            {
                case "Producto": clave = v => v.Producto; break;
                case "Vendedor": clave = v => v.Vendedor; break;
                case "Cliente": clave = v => v.Cliente; break;
                default: throw new NegocioException("Agrupación no válida.");
            }

            return ventas
                .Where(v => !v.Anulada)
                .GroupBy(clave)
                .Select(g => new ResumenVenta
                {
                    Concepto = g.Key,
                    NumVentas = g.Count(),
                    Unidades = g.Sum(v => v.Cantidad),
                    Total = g.Sum(v => v.Total)
                })
                .OrderByDescending(r => r.Total)
                .ToList();
        }

        // LINQ: total de las ventas vigentes (las anuladas no suman).
        public decimal TotalVigente(List<VentaReporte> ventas)
        {
            return ventas.Where(v => !v.Anulada).Sum(v => v.Total);
        }

        public List<VentaReporte> Reporte(DateTime desde, DateTime hasta)
        {
            Permisos.Exigir(Modulo.Reportes);
            if (desde.Date > hasta.Date) throw new NegocioException("La fecha inicial no puede ser mayor que la final.");
            return dal.Reporte(desde.Date, hasta.Date.AddDays(1));
        }
    }
}

// CAPA DATOS – ventas. El registro de una venta es una TRANSACCIÓN:
// o se guardan la venta Y el descuento de stock, o no se guarda nada (así no hay inconsistencias).
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Entidades;

namespace Datos
{
    public class VentaDAL
    {
        public void Registrar(Venta v)
        {
            using (SqlConnection cn = Conexion.Instancia.Crear())
            {
                cn.Open();
                using (SqlTransaction tx = cn.BeginTransaction())
                {
                    try
                    {
                        // 1) Descuenta el stock SOLO si alcanza (la condición y el descuento son una sola operación,
                        //    por eso dos ventas simultáneas no pueden dejar el stock en negativo).
                        //    OUTPUT devuelve el precio vigente: el precio lo decide la BD, no la pantalla.
                        decimal precio;
                        using (SqlCommand cmd = new SqlCommand(
                            "UPDATE Producto SET Stock = Stock - @cant " +
                            "OUTPUT INSERTED.Precio " +
                            "WHERE IdProducto = @idp AND Stock >= @cant", cn, tx))
                        {
                            cmd.Parameters.Add("@cant", SqlDbType.Int).Value = v.Cantidad;
                            cmd.Parameters.Add("@idp", SqlDbType.Int).Value = v.IdProducto;
                            object r = cmd.ExecuteScalar();
                            if (r == null)
                                throw new NegocioException("Stock insuficiente o el producto no existe.");
                            precio = (decimal)r;
                        }

                        // 2) Registra la venta con ese precio.
                        using (SqlCommand cmd = new SqlCommand(
                            "INSERT INTO Venta (IdCliente, IdProducto, Cantidad, PrecioUnitario, IdUsuario) " +
                            "VALUES (@idc, @idp, @cant, @pre, @idu)", cn, tx))
                        {
                            cmd.Parameters.Add("@idc", SqlDbType.Int).Value = v.IdCliente;
                            cmd.Parameters.Add("@idp", SqlDbType.Int).Value = v.IdProducto;
                            cmd.Parameters.Add("@cant", SqlDbType.Int).Value = v.Cantidad;
                            SqlParameter pre = cmd.Parameters.Add("@pre", SqlDbType.Decimal);
                            pre.Precision = 10; pre.Scale = 2; pre.Value = precio;
                            cmd.Parameters.Add("@idu", SqlDbType.Int).Value = v.IdUsuario;
                            cmd.ExecuteNonQuery();
                        }

                        tx.Commit();
                    }
                    catch (SqlException ex)
                    {
                        tx.Rollback();
                        if (SqlErrores.EsClaveForanea(ex))
                            throw new NegocioException("El cliente seleccionado no existe.");
                        throw;
                    }
                    catch
                    {
                        tx.Rollback();   // cualquier otro error: se deshace todo
                        throw;
                    }
                }
            }
        }

        // ANULAR una venta = TRANSACCIÓN con dos pasos que deben cumplirse JUNTOS:
        //   (1) marcar la venta como anulada   (2) devolver las unidades al stock.
        // Si falla cualquiera -> ROLLBACK (no queda ni la anulación ni el stock a medias). Si todo sale bien -> COMMIT.
        public void Anular(int idVenta, int idUsuario)
        {
            using (SqlConnection cn = Conexion.Instancia.Crear())
            {
                cn.Open();
                using (SqlTransaction tx = cn.BeginTransaction())
                {
                    try
                    {
                        int idProducto, cantidad;

                        // 1) Solo se anula si NO estaba anulada (la condición y el cambio son una sola operación:
                        //    dos anulaciones simultáneas no pueden devolver el stock dos veces).
                        using (SqlCommand cmd = new SqlCommand(
                            "UPDATE Venta SET Anulada = 1, FechaAnulacion = SYSDATETIME(), IdUsuarioAnula = @idu " +
                            "OUTPUT INSERTED.IdProducto, INSERTED.Cantidad " +
                            "WHERE IdVenta = @id AND Anulada = 0", cn, tx))
                        {
                            cmd.Parameters.Add("@idu", SqlDbType.Int).Value = idUsuario;
                            cmd.Parameters.Add("@id", SqlDbType.Int).Value = idVenta;
                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                if (!dr.Read())
                                    throw new NegocioException("La venta no existe o ya fue anulada.");
                                idProducto = dr.GetInt32(0);
                                cantidad = dr.GetInt32(1);
                            }
                        }

                        // 2) Devuelve el stock.
                        using (SqlCommand cmd = new SqlCommand(
                            "UPDATE Producto SET Stock = Stock + @cant WHERE IdProducto = @idp", cn, tx))
                        {
                            cmd.Parameters.Add("@cant", SqlDbType.Int).Value = cantidad;
                            cmd.Parameters.Add("@idp", SqlDbType.Int).Value = idProducto;
                            cmd.ExecuteNonQuery();
                        }

                        tx.Commit();       // COMMIT: ambos cambios quedan guardados
                    }
                    catch
                    {
                        tx.Rollback();     // ROLLBACK: se deshace todo
                        throw;
                    }
                }
            }
        }

        // 'hastaExclusivo' = primer instante que ya NO entra (el día siguiente al último día pedido).
        public List<VentaReporte> Reporte(DateTime desde, DateTime hastaExclusivo)
        {
            var lista = new List<VentaReporte>();
            using (SqlConnection cn = Conexion.Instancia.Crear())
            using (SqlCommand cmd = new SqlCommand(
                "SELECT TOP (5000) v.IdVenta, v.Fecha, c.Nombre, p.Nombre, v.Cantidad, v.PrecioUnitario, v.Total, u.Login, v.Anulada " +
                "FROM Venta v " +
                "JOIN Cliente c ON c.IdCliente = v.IdCliente " +
                "JOIN Producto p ON p.IdProducto = v.IdProducto " +
                "JOIN Usuario u ON u.IdUsuario = v.IdUsuario " +
                "WHERE v.Fecha >= @desde AND v.Fecha < @hasta " +
                "ORDER BY v.Fecha DESC", cn))
            {
                cmd.Parameters.Add("@desde", SqlDbType.DateTime2).Value = desde;
                cmd.Parameters.Add("@hasta", SqlDbType.DateTime2).Value = hastaExclusivo;
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                        lista.Add(new VentaReporte
                        {
                            IdVenta = dr.GetInt32(0),
                            Fecha = dr.GetDateTime(1),
                            Cliente = dr.GetString(2),
                            Producto = dr.GetString(3),
                            Cantidad = dr.GetInt32(4),
                            PrecioUnitario = dr.GetDecimal(5),
                            Total = dr.GetDecimal(6),
                            Vendedor = dr.GetString(7),
                            Anulada = dr.GetBoolean(8)
                        });
                }
            }
            return lista;
        }
    }
}

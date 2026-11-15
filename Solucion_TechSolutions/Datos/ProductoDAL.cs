// CAPA DATOS – tabla Producto (consultas parametrizadas)
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Entidades;

namespace Datos
{
    public class ProductoDAL
    {
        public void Insertar(Producto p)
        {
            using (SqlConnection cn = Conexion.Instancia.Crear())
            using (SqlCommand cmd = new SqlCommand(
                "INSERT INTO Producto (Codigo, Nombre, Precio, Stock) VALUES (@cod, @nom, @pre, @stk)", cn))
            {
                cmd.Parameters.Add("@cod", SqlDbType.VarChar, 20).Value = p.Codigo;
                cmd.Parameters.Add("@nom", SqlDbType.VarChar, 100).Value = p.Nombre;
                SqlParameter pre = cmd.Parameters.Add("@pre", SqlDbType.Decimal);
                pre.Precision = 10; pre.Scale = 2; pre.Value = p.Precio;            // igual que DECIMAL(10,2)
                cmd.Parameters.Add("@stk", SqlDbType.Int).Value = p.Stock;
                try
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex) when (SqlErrores.EsDuplicado(ex))
                {
                    throw new NegocioException("Ya existe un producto con ese código.");
                }
            }
        }

        public List<Producto> Listar()
        {
            var lista = new List<Producto>();
            using (SqlConnection cn = Conexion.Instancia.Crear())
            using (SqlCommand cmd = new SqlCommand(
                "SELECT TOP (500) IdProducto, Codigo, Nombre, Precio, Stock FROM Producto ORDER BY Nombre", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                        lista.Add(new Producto
                        {
                            Id = dr.GetInt32(0),
                            Codigo = dr.GetString(1),
                            Nombre = dr.GetString(2),
                            Precio = dr.GetDecimal(3),
                            Stock = dr.GetInt32(4)
                        });
                }
            }
            return lista;
        }

        // Ingreso de mercadería: suma al stock directamente en la BD (no "leer, sumar y guardar").
        public void AgregarStock(int idProducto, int cantidad)
        {
            using (SqlConnection cn = Conexion.Instancia.Crear())
            using (SqlCommand cmd = new SqlCommand(
                "UPDATE Producto SET Stock = Stock + @cant WHERE IdProducto = @id", cn))
            {
                cmd.Parameters.Add("@cant", SqlDbType.Int).Value = cantidad;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = idProducto;
                cn.Open();
                if (cmd.ExecuteNonQuery() == 0)
                    throw new NegocioException("El producto no existe.");
            }
        }
    }
}

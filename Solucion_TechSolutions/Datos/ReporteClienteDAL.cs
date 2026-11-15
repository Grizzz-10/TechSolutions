// CAPA DATOS: aquí .NET se conecta con el PROCEDIMIENTO ALMACENADO dbo.usp_ReporteClientes.
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Entidades;

namespace Datos
{
    public class ReporteClienteDAL
    {
        public List<Cliente> ListarPorNombre(string nombre)
        {
            List<Cliente> lista = new List<Cliente>();

            using (SqlConnection cn = Conexion.Instancia.Crear())                              // 1) conexión (App.config)
            using (SqlCommand cmd = new SqlCommand("dbo.usp_ReporteClientes", cn))              // 2) NOMBRE del procedimiento
            {
                cmd.CommandType = CommandType.StoredProcedure;                                  // 3) «es un procedimiento»
                cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = nombre;           // 4) mismo nombre y tipo que en SQL

                cn.Open();                                                                      // 5) abrir la conexión
                using (SqlDataReader dr = cmd.ExecuteReader())                                  // 6) ejecutar el procedimiento
                {
                    while (dr.Read())                                                           // 7) una vuelta por cada fila
                    {
                        lista.Add(new Cliente
                        {
                            Documento = dr["Documento"].ToString(),                             //    columna -> propiedad
                            Nombre = dr["Nombre"].ToString(),
                            Correo = dr["Correo"].ToString(),
                            Telefono = dr["Telefono"].ToString()
                        });
                    }
                }
            }                                                                                   // 8) using cierra la conexión
            return lista;
        }
    }
}

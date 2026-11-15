// CAPA DATOS – tabla Cliente (consultas parametrizadas)
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Entidades;

namespace Datos
{
    public class ClienteDAL
    {
        private const string Columnas = "IdCliente, Documento, Nombre, Correo, Telefono";

        public void Insertar(Cliente c)
        {
            using (SqlConnection cn = Conexion.Instancia.Crear())
            using (SqlCommand cmd = new SqlCommand(
                "INSERT INTO Cliente (Documento, Nombre, Correo, Telefono) VALUES (@doc, @nom, @cor, @tel)", cn))
            {
                cmd.Parameters.Add("@doc", SqlDbType.VarChar, 11).Value = c.Documento;
                cmd.Parameters.Add("@nom", SqlDbType.VarChar, 100).Value = c.Nombre;
                cmd.Parameters.Add("@cor", SqlDbType.VarChar, 100).Value = (object)c.Correo ?? DBNull.Value;
                cmd.Parameters.Add("@tel", SqlDbType.VarChar, 15).Value = (object)c.Telefono ?? DBNull.Value;
                try
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex) when (SqlErrores.EsDuplicado(ex))
                {
                    throw new NegocioException("Ya existe un cliente con ese documento.");
                }
            }
        }

        public List<Cliente> Listar()
        {
            using (SqlConnection cn = Conexion.Instancia.Crear())
            using (SqlCommand cmd = new SqlCommand(
                "SELECT TOP (500) " + Columnas + " FROM Cliente ORDER BY Nombre", cn))
            {
                return Leer(cn, cmd);
            }
        }

        // Búsqueda con LIKE: el texto va en un PARÁMETRO (aunque escriban  ' OR 1=1 --  se busca como texto).
        // Además se "escapan" los comodines (% _ [) para que el usuario no los use como patrón.
        public List<Cliente> Buscar(string texto)
        {
            using (SqlConnection cn = Conexion.Instancia.Crear())
            using (SqlCommand cmd = new SqlCommand(
                "SELECT TOP (500) " + Columnas + " FROM Cliente " +
                "WHERE Nombre LIKE @t ESCAPE '\\' OR Documento LIKE @t ESCAPE '\\' ORDER BY Nombre", cn))
            {
                string seguro = texto.Replace("\\", "\\\\").Replace("%", "\\%").Replace("_", "\\_").Replace("[", "\\[");
                cmd.Parameters.Add("@t", SqlDbType.VarChar, 120).Value = "%" + seguro + "%";
                return Leer(cn, cmd);
            }
        }

        private static List<Cliente> Leer(SqlConnection cn, SqlCommand cmd)
        {
            var lista = new List<Cliente>();
            cn.Open();
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                    lista.Add(new Cliente
                    {
                        Id = dr.GetInt32(0),
                        Documento = dr.GetString(1),
                        Nombre = dr.GetString(2),
                        Correo = dr.IsDBNull(3) ? null : dr.GetString(3),
                        Telefono = dr.IsDBNull(4) ? null : dr.GetString(4)
                    });
            }
            return lista;
        }
    }
}

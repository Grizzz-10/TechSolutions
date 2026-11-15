// CAPA DATOS – tabla Usuario.
// Todas las consultas son PARAMETRIZADAS: lo que escribe el usuario viaja como DATO,
// nunca como parte del texto SQL. Así se evita la inyección SQL.
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Entidades;

namespace Datos
{
    public class UsuarioDAL
    {
        // Solo el inicio de sesión necesita el hash y la sal.
        public Usuario BuscarPorLogin(string login)
        {
            using (SqlConnection cn = Conexion.Instancia.Crear())
            using (SqlCommand cmd = new SqlCommand(
                "SELECT IdUsuario, Login, ClaveHash, Sal, Rol, IntentosFallidos, Bloqueado " +
                "FROM Usuario WHERE Login = @login", cn))
            {
                cmd.Parameters.Add("@login", SqlDbType.VarChar, 30).Value = login;   // mismo tipo y tamaño que la columna
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (!dr.Read()) return null;
                    return new Usuario
                    {
                        Id = dr.GetInt32(0),
                        Login = dr.GetString(1),
                        ClaveHash = dr.GetString(2),
                        Sal = dr.GetString(3),
                        Rol = dr.GetString(4),
                        IntentosFallidos = dr.GetInt32(5),
                        Bloqueado = dr.GetBoolean(6)
                    };
                }
            }
        }

        // Para listar NO se consulta ClaveHash ni Sal: lo que no se trae, no se puede filtrar.
        public List<Usuario> Listar()
        {
            var lista = new List<Usuario>();
            using (SqlConnection cn = Conexion.Instancia.Crear())
            using (SqlCommand cmd = new SqlCommand(
                "SELECT IdUsuario, Login, Rol, IntentosFallidos, Bloqueado FROM Usuario ORDER BY Login", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                        lista.Add(new Usuario
                        {
                            Id = dr.GetInt32(0),
                            Login = dr.GetString(1),
                            Rol = dr.GetString(2),
                            IntentosFallidos = dr.GetInt32(3),
                            Bloqueado = dr.GetBoolean(4)
                        });
                }
            }
            return lista;
        }

        public int Contar()
        {
            using (SqlConnection cn = Conexion.Instancia.Crear())
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Usuario", cn))
            {
                cn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public void Insertar(Usuario u)
        {
            using (SqlConnection cn = Conexion.Instancia.Crear())
            using (SqlCommand cmd = new SqlCommand(
                "INSERT INTO Usuario (Login, ClaveHash, Sal, Rol) VALUES (@login, @hash, @sal, @rol)", cn))
            {
                AgregarParametros(cmd, u);
                try
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex) when (SqlErrores.EsDuplicado(ex))
                {
                    throw new NegocioException("Ese usuario ya existe.");
                }
            }
        }

        // Crea el primer administrador SOLO si la tabla está vacía, en UNA sola sentencia atómica.
        // (Contar y luego insertar en dos pasos permitiría crear dos "primeros" administradores a la vez.)
        public bool InsertarPrimerAdministrador(Usuario u)
        {
            using (SqlConnection cn = Conexion.Instancia.Crear())
            using (SqlCommand cmd = new SqlCommand(
                "INSERT INTO Usuario (Login, ClaveHash, Sal, Rol) " +
                "SELECT @login, @hash, @sal, @rol " +
                "WHERE NOT EXISTS (SELECT 1 FROM Usuario WITH (UPDLOCK, HOLDLOCK))", cn))
            {
                AgregarParametros(cmd, u);
                cn.Open();
                return cmd.ExecuteNonQuery() == 1;
            }
        }

        // Suma un intento fallido y bloquea la cuenta al llegar al máximo.
        // Todo en una sola sentencia (atómica). Devuelve el estado resultante, o null si no se actualizó
        // (usuario inexistente o ya bloqueado).
        public Usuario RegistrarFallo(string login, int maxIntentos)
        {
            using (SqlConnection cn = Conexion.Instancia.Crear())
            using (SqlCommand cmd = new SqlCommand(
                "UPDATE Usuario " +
                "SET IntentosFallidos = IntentosFallidos + 1, " +
                "    Bloqueado = CASE WHEN IntentosFallidos + 1 >= @max THEN 1 ELSE 0 END " +
                "OUTPUT INSERTED.IntentosFallidos, INSERTED.Bloqueado " +
                "WHERE Login = @login AND Bloqueado = 0", cn))
            {
                cmd.Parameters.Add("@login", SqlDbType.VarChar, 30).Value = login;
                cmd.Parameters.Add("@max", SqlDbType.Int).Value = maxIntentos;
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (!dr.Read()) return null;
                    return new Usuario { Login = login, IntentosFallidos = dr.GetInt32(0), Bloqueado = dr.GetBoolean(1) };
                }
            }
        }

        // Sirve para: inicio de sesión correcto (limpia el contador) y desbloqueo por un administrador.
        public void Reiniciar(int idUsuario)
        {
            using (SqlConnection cn = Conexion.Instancia.Crear())
            using (SqlCommand cmd = new SqlCommand(
                "UPDATE Usuario SET IntentosFallidos = 0, Bloqueado = 0 WHERE IdUsuario = @id", cn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = idUsuario;
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private static void AgregarParametros(SqlCommand cmd, Usuario u)
        {
            cmd.Parameters.Add("@login", SqlDbType.VarChar, 30).Value = u.Login;
            cmd.Parameters.Add("@hash", SqlDbType.VarChar, 100).Value = u.ClaveHash;
            cmd.Parameters.Add("@sal", SqlDbType.VarChar, 50).Value = u.Sal;
            cmd.Parameters.Add("@rol", SqlDbType.VarChar, 20).Value = u.Rol;
        }
    }
}

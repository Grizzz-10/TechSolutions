// Proyecto: Datos (Biblioteca de clases)
// Referencias: System.Configuration
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Datos
{
    // Patrón Singleton: una sola instancia que guarda la cadena de conexión.
    // Lazy<T> garantiza que se crea una sola vez, incluso con varios hilos.
    public sealed class Conexion
    {
        private static readonly Lazy<Conexion> instancia =
            new Lazy<Conexion>(() => new Conexion(), true);

        private readonly string cadena;

        private Conexion()                                  // constructor privado
        {
            ConnectionStringSettings cs = ConfigurationManager.ConnectionStrings["TechSolutions"];
            if (cs == null)
                throw new ConfigurationErrorsException("Falta la cadena de conexión 'TechSolutions' en App.config.");
            cadena = cs.ConnectionString;
        }

        public static Conexion Instancia
        {
            get { return instancia.Value; }
        }

        // Cada operación crea y cierra SU PROPIA conexión (no se comparte una conexión abierta).
        public SqlConnection Crear()
        {
            return new SqlConnection(cadena);
        }
    }
}

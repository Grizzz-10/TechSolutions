// Proyecto: Datos
using System.Data.SqlClient;

namespace Datos
{
    // Traduce números de error de SQL Server a situaciones conocidas.
    internal static class SqlErrores
    {
        public static bool EsDuplicado(SqlException ex)          // UNIQUE / PRIMARY KEY repetido
        {
            return ex.Number == 2627 || ex.Number == 2601;
        }

        public static bool EsClaveForanea(SqlException ex)       // FOREIGN KEY: el registro relacionado no existe
        {
            return ex.Number == 547;
        }
    }
}

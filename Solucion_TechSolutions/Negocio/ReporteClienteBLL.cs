// CAPA NEGOCIO: reglas antes de pedir los datos del reporte. No sabe nada de SQL ni de pantallas.
using System.Collections.Generic;
using Datos;
using Entidades;

namespace Negocio
{
    public class ReporteClienteBLL
    {
        private readonly ReporteClienteDAL dal = new ReporteClienteDAL();

        public List<Cliente> Reporte(string nombre)
        {
            Permisos.Exigir(Modulo.Clientes);                                  // regla: solo roles con acceso a Clientes

            nombre = (nombre ?? "").Trim();                                    // regla: sin espacios de más
            if (nombre.Length > 100)                                           // regla: el procedimiento acepta hasta 100
                throw new NegocioException("El nombre no puede tener más de 100 caracteres.");
            return dal.ListarPorNombre(nombre);
        }
    }
}

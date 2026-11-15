// CAPA NEGOCIO – reglas de clientes
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Datos;
using Entidades;

namespace Negocio
{
    public class ClienteBLL
    {
        private static readonly Regex patronDocumento = new Regex(@"^(\d{8}|\d{11})$", RegexOptions.Compiled);
        private static readonly Regex patronCorreo = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
        private static readonly Regex patronTelefono = new Regex(@"^\d{6,15}$", RegexOptions.Compiled);

        private readonly ClienteDAL dal = new ClienteDAL();

        public void Registrar(Cliente c)
        {
            Permisos.Exigir(Modulo.Clientes);

            c.Documento = (c.Documento ?? "").Trim();
            c.Nombre = (c.Nombre ?? "").Trim();
            c.Correo = Vacio(c.Correo);
            c.Telefono = Vacio(c.Telefono);

            if (!patronDocumento.IsMatch(c.Documento))
                throw new NegocioException("El documento debe tener 8 dígitos (DNI) u 11 dígitos (RUC).");
            if (c.Nombre.Length == 0 || c.Nombre.Length > 100)
                throw new NegocioException("El nombre es obligatorio (máximo 100 caracteres).");
            if (c.Correo != null && (c.Correo.Length > 100 || !patronCorreo.IsMatch(c.Correo)))
                throw new NegocioException("El correo no es válido.");
            if (c.Telefono != null && !patronTelefono.IsMatch(c.Telefono))
                throw new NegocioException("El teléfono debe tener entre 6 y 15 dígitos.");

            dal.Insertar(c);
        }

        // Ventas también necesita la lista de clientes para elegir a quién vender.
        public List<Cliente> Listar()
        {
            Permisos.Exigir(Modulo.Clientes, Modulo.Ventas);
            return dal.Listar();
        }

        public List<Cliente> Buscar(string texto)
        {
            Permisos.Exigir(Modulo.Clientes);
            texto = (texto ?? "").Trim();
            if (texto.Length == 0) return dal.Listar();
            if (texto.Length > 100) throw new NegocioException("El texto de búsqueda es demasiado largo.");
            return dal.Buscar(texto);
        }

        private static string Vacio(string s)
        {
            s = (s ?? "").Trim();
            return s.Length == 0 ? null : s;
        }
    }
}

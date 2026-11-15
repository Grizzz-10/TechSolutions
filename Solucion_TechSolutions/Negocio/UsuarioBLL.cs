// CAPA NEGOCIO – reglas de inicio de sesión, bloqueo y registro de usuarios
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Datos;
using Entidades;

namespace Negocio
{
    public class UsuarioBLL
    {
        public const int MaxIntentos = 3;

        private const string MsgIncorrecto = "Usuario o contraseña incorrectos.";   // genérico: no dice cuál falló
        private const string MsgBloqueado = "Cuenta bloqueada por intentos fallidos. Contacte al administrador.";
        private static readonly Regex patronLogin = new Regex("^[A-Za-z0-9_.]{4,30}$", RegexOptions.Compiled);

        private readonly UsuarioDAL dal = new UsuarioDAL();

        public void IniciarSesion(string login, string clave)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrEmpty(clave))
                throw new UnauthorizedAccessException(MsgIncorrecto);

            Usuario u = dal.BuscarPorLogin(login);                       // 1) trae SU sal y SU hash

            if (u == null)
            {
                Seguridad.GastarTiempo(clave);                           // mismo tiempo que un usuario real
                throw new UnauthorizedAccessException(MsgIncorrecto);
            }

            if (u.Bloqueado)                                             // bloqueada: ni siquiera se prueba la clave
                throw new UnauthorizedAccessException(MsgBloqueado);

            if (!Seguridad.Verificar(clave, u.Sal, u.ClaveHash))         // 2) hash de lo escrito + sal, y se compara
            {
                Usuario estado = dal.RegistrarFallo(u.Login, MaxIntentos);   // suma 1; al tercer fallo bloquea
                if (estado != null && estado.Bloqueado)
                    throw new UnauthorizedAccessException(MsgBloqueado);
                throw new UnauthorizedAccessException(MsgIncorrecto);
            }

            if (u.IntentosFallidos > 0) dal.Reiniciar(u.Id);            // clave correcta: el contador vuelve a 0
            Sesion.Instancia.Iniciar(u);
        }

        public void CerrarSesion()
        {
            Sesion.Instancia.Cerrar();
        }

        // ¿Ya existe algún usuario? Si no, la aplicación pide crear al primer administrador.
        // Es la única operación de esta clase que se permite sin sesión.
        public bool HayUsuarios()
        {
            return dal.Contar() > 0;
        }

        // El primer administrador NO está en el script SQL: lo crea la aplicación la primera vez que se ejecuta.
        public void RegistrarPrimerAdministrador(string login, string clave)
        {
            Usuario u = Preparar(login, clave, Roles.Administrador);
            if (!dal.InsertarPrimerAdministrador(u))
                throw new NegocioException("Ya existe un administrador. Inicie sesión.");
        }

        // Solo un Administrador con sesión abierta puede crear usuarios.
        public void Registrar(string login, string clave, string rol)
        {
            Permisos.Exigir(Modulo.Usuarios);
            dal.Insertar(Preparar(login, clave, rol));
        }

        public List<Usuario> Listar()
        {
            Permisos.Exigir(Modulo.Usuarios);
            return dal.Listar();
        }

        public void Desbloquear(int idUsuario)
        {
            Permisos.Exigir(Modulo.Usuarios);
            dal.Reiniciar(idUsuario);
        }

        // Valida y arma el usuario con su sal y su hash. La contraseña NO se guarda ni se devuelve.
        private static Usuario Preparar(string login, string clave, string rol)
        {
            if (string.IsNullOrWhiteSpace(login) || !patronLogin.IsMatch(login))
                throw new NegocioException("El usuario debe tener de 4 a 30 caracteres: letras, números, punto o guion bajo.");
            Seguridad.ValidarClave(clave, login);
            if (Array.IndexOf(Roles.Todos, rol) < 0)
                throw new NegocioException("Rol no válido.");

            string sal = Seguridad.NuevaSal();           // 1) sal aleatoria propia de este usuario
            string hash = Seguridad.Hash(clave, sal);    // 2) hash de la clave con esa sal
            return new Usuario { Login = login, Sal = sal, ClaveHash = hash, Rol = rol };   // 3) se guardará esto, NO la clave
        }
    }
}

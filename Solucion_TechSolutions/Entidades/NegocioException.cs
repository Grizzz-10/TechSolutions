// Proyecto: Entidades
using System;

namespace Entidades
{
    // Error "esperado" (validación o regla de negocio). Su mensaje SÍ se puede mostrar al usuario.
    // Cualquier otra excepción se considera inesperada y NO se muestra tal cual.
    public class NegocioException : Exception
    {
        public NegocioException(string mensaje) : base(mensaje) { }
    }
}

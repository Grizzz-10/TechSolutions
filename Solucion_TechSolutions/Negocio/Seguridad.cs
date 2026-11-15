// CAPA NEGOCIO – contraseñas con sal y hash (PBKDF2 + SHA-256, 600 000 iteraciones).
// Requiere .NET Framework 4.7.2 o superior.
using System;
using System.Security.Cryptography;

namespace Negocio
{
    public static class Seguridad
    {
        private const int Iteraciones = 600000;
        private const int BytesHash = 32;

        // Sal falsa, usada solo para gastar el mismo tiempo cuando el usuario NO existe (ver UsuarioBLL).
        private static readonly string salFicticia = NuevaSal();

        public static string NuevaSal()
        {
            byte[] sal = new byte[16];
            using (var rng = RandomNumberGenerator.Create())   // generador criptográfico, no Random
                rng.GetBytes(sal);
            return Convert.ToBase64String(sal);
        }

        public static string Hash(string clave, string salBase64)
        {
            return Convert.ToBase64String(CalcularHash(clave, salBase64));
        }

        // Al iniciar sesión se calcula el hash de lo escrito, con LA SAL de ese usuario, y se compara.
        public static bool Verificar(string clave, string salBase64, string hashGuardado)
        {
            byte[] calculado = CalcularHash(clave, salBase64);
            byte[] guardado;
            try { guardado = Convert.FromBase64String(hashGuardado); }
            catch (FormatException) { return false; }
            return IgualesEnTiempoConstante(calculado, guardado);
        }

        // Gasta el mismo tiempo que una verificación real (evita saber si el usuario existe midiendo la demora).
        public static void GastarTiempo(string clave)
        {
            CalcularHash(clave, salFicticia);
        }

        // Reglas de contraseña: 8 a 64 caracteres, con mayúscula, minúscula y número.
        public static void ValidarClave(string clave, string login)
        {
            if (string.IsNullOrEmpty(clave) || clave.Length < 8 || clave.Length > 64)
                throw new Entidades.NegocioException("La contraseña debe tener entre 8 y 64 caracteres.");

            bool mayus = false, minus = false, numero = false;
            foreach (char c in clave)
            {
                if (char.IsUpper(c)) mayus = true;
                else if (char.IsLower(c)) minus = true;
                else if (char.IsDigit(c)) numero = true;
            }
            if (!(mayus && minus && numero))
                throw new Entidades.NegocioException("La contraseña debe tener mayúscula, minúscula y número.");
            if (string.Equals(clave, login, StringComparison.OrdinalIgnoreCase))
                throw new Entidades.NegocioException("La contraseña no puede ser igual al usuario.");
        }

        private static byte[] CalcularHash(string clave, string salBase64)
        {
            byte[] sal = Convert.FromBase64String(salBase64);
            using (var pbkdf2 = new Rfc2898DeriveBytes(clave ?? string.Empty, sal, Iteraciones, HashAlgorithmName.SHA256))
                return pbkdf2.GetBytes(BytesHash);
        }

        // Compara TODOS los bytes siempre; un "==" de texto se detiene en la primera diferencia y filtra información por tiempo.
        private static bool IgualesEnTiempoConstante(byte[] a, byte[] b)
        {
            int diferencia = a.Length ^ b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
                diferencia |= a[i] ^ b[i];
            return diferencia == 0;
        }
    }
}

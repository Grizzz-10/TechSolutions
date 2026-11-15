// CAPA NEGOCIO – usuario que tiene la sesión abierta.
// Patrón Singleton: en toda la aplicación hay UNA sola sesión y todas las capas consultan la misma.
// También controla la INACTIVIDAD: si pasa el tiempo máximo sin actividad, la sesión se considera vencida.
using System;
using System.Diagnostics;
using Entidades;

namespace Negocio
{
    public sealed class Sesion
    {
        private static readonly Lazy<Sesion> instancia = new Lazy<Sesion>(() => new Sesion(), true);
        private static readonly Stopwatch reloj = Stopwatch.StartNew();   // reloj monótono (no depende de la hora de Windows)

        private long ultimaActividadMs;

        private Sesion() { }                                   // constructor privado

        public static Sesion Instancia
        {
            get { return instancia.Value; }
        }

        // Tiempo máximo sin actividad. La pantalla lo toma de App.config (MinutosInactividad); por defecto 5 minutos.
        public static TimeSpan TiempoMaximoInactividad { get; set; } = TimeSpan.FromMinutes(5);

        // Usuario actual (null si no hay sesión). Nunca contiene hash ni sal.
        public Usuario Usuario { get; private set; }

        public bool EstaActiva
        {
            get { return Usuario != null; }
        }

        // true si la última sesión se cerró por inactividad (para avisarlo en el login).
        public bool CerradaPorInactividad { get; private set; }

        // Tiempo que falta para que la sesión venza por inactividad.
        public TimeSpan TiempoRestante
        {
            get
            {
                if (!EstaActiva) return TimeSpan.Zero;
                TimeSpan transcurrido = TimeSpan.FromMilliseconds(reloj.ElapsedMilliseconds - ultimaActividadMs);
                TimeSpan restante = TiempoMaximoInactividad - transcurrido;
                return restante < TimeSpan.Zero ? TimeSpan.Zero : restante;
            }
        }

        public bool Expirada
        {
            get { return EstaActiva && TiempoRestante == TimeSpan.Zero; }
        }

        // Se llama cuando el usuario hace algo (mover el mouse, teclear, ejecutar una operación).
        // Una sesión ya vencida NO se revive con actividad.
        public void RegistrarActividad()
        {
            if (EstaActiva && !Expirada)
                ultimaActividadMs = reloj.ElapsedMilliseconds;
        }

        internal void Iniciar(Usuario u)
        {
            Usuario = new Usuario { Id = u.Id, Login = u.Login, Rol = u.Rol };   // copia SIN ClaveHash ni Sal
            CerradaPorInactividad = false;
            ultimaActividadMs = reloj.ElapsedMilliseconds;
        }

        public void Cerrar()
        {
            Usuario = null;
        }

        public void CerrarPorInactividad()
        {
            Usuario = null;
            CerradaPorInactividad = true;
        }
    }
}

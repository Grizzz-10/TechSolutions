// Proyecto: Entidades
using System.ComponentModel;

namespace Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Login { get; set; }

        [Browsable(false)] public string ClaveHash { get; set; }   // nunca se muestra en pantalla
        [Browsable(false)] public string Sal { get; set; }         // nunca se muestra en pantalla

        public string Rol { get; set; }
        public int IntentosFallidos { get; set; }
        public bool Bloqueado { get; set; }
    }
}

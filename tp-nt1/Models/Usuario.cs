using System;
namespace tp_nt1.Models
{
    public abstract class Usuario
    {   
        public Guid Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public DateTime FechaAlta { get; set; }
        public string Password { get; set; }
        public string DNI { get; set; }
        public string Telefono { get; set; }

    }
}

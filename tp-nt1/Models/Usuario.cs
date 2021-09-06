using System;
namespace tp_nt1.Models
{
    public class Usuario
    {
        public Usuario()
        {
        }

        public Usuario(string nombre, string email, DateTime fecha, string password)
        {
            Id = Guid.NewGuid();
            Nombre = nombre;
            Email = email;
            FechaAlta = fecha;
            Password = password;
        }

        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public DateTime FechaAlta { get; set; }
        public string Password { get; set; }
    }
}

using System;
namespace tp_nt1.Models
{
    public abstract class Usuario
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Dni { get; set; }

        public string Email { get; set; }

        public string Telefono { get; set; }

        public string Direccion { get; set; }

        public DateTime FechaAlta { get; set; }

        public byte[] Password { get; set; }
    }
}

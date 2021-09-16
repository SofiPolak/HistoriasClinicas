using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace tp_nt1.Models
{
    public abstract class Usuario
    {

        [Key]
        public Guid Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public DateTime FechaAlta { get; set; }
        public string Password { get; set; }
        public string DNI { get; set; }
        public string Telefono { get; set; }
        public Direccion Direccion { get; set; } // es la relacion con direccion 

    }
}

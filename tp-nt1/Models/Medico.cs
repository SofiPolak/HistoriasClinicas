using System;
namespace tp_nt1.Models
{
    public class Medico
    {
        public Medico()
        {
        }

        public Medico(string nombre, string apellido, int dni, int telefono, Direccion dir, DateTime fecha, string email, int matricula, Especialidad esp)
        {
            Id = Guid.NewGuid();
            Nombre = nombre;
            Apellido = apellido;
            Dni = dni;
            Telefono = telefono;
            Direccion = dir;
            FechaAlta = fecha;
            Email = email;
            Matricula = matricula;
            Especialidad = esp;
        }

        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Dni { get; set; }
        public int Telefono { get; set; }
        public Direccion Direccion { get; set; }
        public DateTime FechaAlta { get; set; }
        public string Email { get; set; }
        public int Matricula { get; set; }
        public Especialidad Especialidad { get; set; }
    }

   
}

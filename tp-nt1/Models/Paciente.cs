using System;
namespace tp_nt1.Models
{
    public class Paciente
    {
        public Paciente()
        {
        }

        public Paciente(string nombre, string apellido, int dni, int telefono,
            Direccion dir, DateTime fecha, string email, ObraSocial obra,
            HistoriaClinica hist)
        {
            Id = Guid.NewGuid();
            Nombre = nombre;
            Apellido = apellido;
            Dni = dni;
            Telefono = telefono;
            Direccion = dir;
            FechaAlta = fecha;
            Email = email;
            ObraSocial = obra;
            HistoriaClinica = hist;
        }
        
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Dni { get; set; }
        public int Telefono { get; set; }
        public Direccion Direccion { get; set; }
        public DateTime FechaAlta { get; set; }
        public string Email { get; set; }
        public ObraSocial ObraSocial { get; set; }
        public HistoriaClinica HistoriaClinica { get; set; }
    }

}

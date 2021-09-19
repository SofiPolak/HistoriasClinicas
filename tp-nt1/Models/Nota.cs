using System;
namespace tp_nt1.Models
{
    public class Nota
    {
        public Guid Id { get; set; }

        public string Mensaje { get; set; }

        public Evolucion Evolucion { get; set; }

        public Empleado Empleado { get; set; }

        public DateTime FechaYHora { get; set; }
    }
}

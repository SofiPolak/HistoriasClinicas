using System;
namespace tp_nt1.Models
{
    public class Nota
    {
        public Nota()
        {
        }

        public Nota(Evolucion evo, Empleado emp, string mensaje, DateTime fechayhora)
        {
            Id = Guid.NewGuid();
            Evolucion = evo;
            Empleado = emp;
            Mensaje = mensaje;
            FechaYHora = fechayhora;
        }

        public Guid Id { get; set; }
        public Evolucion Evolucion { get; set; }
        public Empleado Empleado { get; set; }
        public string Mensaje { get; set; }
        public DateTime FechaYHora { get; set; }
       

    }
}

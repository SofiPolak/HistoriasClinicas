using System;
using System.Collections.Generic;

namespace tp_nt1.Models
{
    public class Episodio
    {
        public Guid Id { get; set; }
        public string Motivo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaYHoraInicio { get; set; }
        public DateTime FechaYHoraAlta { get; set; }
        public DateTime FechaYHoraCierre { get; set; }
        public bool EstadoAbierto { get; set; }
        public List<Evolucion> RegistroEvoluciones { get; set; }
        public Epicrisis Epicrisis { get; set; }
        public Empleado EmpleadoRegistra { get; set; }

    }
}

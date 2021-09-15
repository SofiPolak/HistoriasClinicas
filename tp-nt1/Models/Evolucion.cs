using System;
using System.Collections.Generic;

namespace tp_nt1.Models
{
    public class Evolucion
    {
        public Guid Id { get; set; }
        public Medico Medico { get; set; }
        public DateTime FechaYHoraInicio { get; set; }
        public DateTime FechaYHoraAlta { get; set; }
        public DateTime FechaYHoraCierre { get; set; }
        public string DescripcionAtencion { get; set; }
        public bool EstadoAbierto { get; set; }
        public List<Nota> Notas { get; set; }

    }
}

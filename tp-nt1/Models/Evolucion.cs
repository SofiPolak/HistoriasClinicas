using System;
using System.Collections.Generic;

namespace tp_nt1.Models
{
    public class Evolucion
    {
        public Evolucion()
        {
        }

        public Evolucion(Medico medico, DateTime inicio, DateTime alta, DateTime cierre, string desc, bool estado)
        {
            Id = Guid.NewGuid();
            Medico = medico;
            FechaYHoraInicio = inicio;
            FechaYHoraAlta = alta;
            FechaYHoraCierre = cierre;
            DescripcionAtencion = desc;
            EstadoAbierto = estado;
            Notas = new List<Nota>();
            
        }

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

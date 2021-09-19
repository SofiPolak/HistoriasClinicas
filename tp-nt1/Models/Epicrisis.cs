using System;
namespace tp_nt1.Models
{
    public class Epicrisis
    {
        public Guid Id { get; set; }

        public Episodio Episodio { get; set; }

        public Medico Medico { get; set; }

        public DateTime FechaYHora { get; set; }

        public Diagnostico Diagnostico { get; set; }        
    }
}

using System;
namespace tp_nt1.Models
{
    public class Epicrisis
    {
        public Epicrisis()
        {
        }

        public Epicrisis(Episodio epi, Medico med, DateTime fechayhora,
            Diagnostico diag)
        {
            Id = Guid.NewGuid();
            Episodio = epi;
            Medico = med;
            FechaYHora = fechayhora;
            Diagnostico = diag;
        }

        public Guid Id { get; set; }
        public Episodio Episodio { get; set; }
        public Medico Medico { get; set; }
        public DateTime FechaYHora { get; set; }
        public Diagnostico Diagnostico { get; set; }
        
    }
}

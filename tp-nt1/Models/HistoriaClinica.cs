using System;
using System.Collections.Generic;

namespace tp_nt1.Models
{
    public class HistoriaClinica
    {
        public HistoriaClinica()
        {
        }

        public HistoriaClinica(Paciente pac)
        {
            Paciente = pac;
            Episodios = new List<Episodio>();
        }

        public Paciente Paciente { get; set; }
        public List<Episodio> Episodios { get; set; }
        
    }
}

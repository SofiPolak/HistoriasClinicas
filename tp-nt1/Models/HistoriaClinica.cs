using System;
using System.Collections.Generic;

namespace tp_nt1.Models
{
    public class HistoriaClinica
    {
        public Guid Id { get; set; }
        public Paciente Paciente { get; set; }
        public List<Episodio> Episodios { get; set; }        
    }
}

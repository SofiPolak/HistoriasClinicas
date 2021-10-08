using System;
using tp_nt1.Models.Enums;

namespace tp_nt1.Models
{
    public class Paciente : Usuario
    { 
        public ObraSocial ObraSocial { get; set; }

        public HistoriaClinica HistoriaClinica { get; set; }
    }
}

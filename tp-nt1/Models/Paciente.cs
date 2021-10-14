using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace tp_nt1.Models
{
    public class Paciente : Usuario
    { 
        public HistoriaClinica HistoriaClinica { get; set; }


        [ForeignKey(nameof(ObraSocial))]
        public Guid ObraSocialId { get; set; }
        public ObraSocial ObraSocial { get; set; }
    }
}

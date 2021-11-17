using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using tp_nt1.Models.Enums;

namespace tp_nt1.Models
{
    public class Paciente : Usuario
    { 
        public HistoriaClinica HistoriaClinica { get; set; }

        [ForeignKey(nameof(ObraSocial))]
        public Guid ObraSocialId { get; set; }

        [Display(Name = "Obra Social")]
        public ObraSocial ObraSocial { get; set; }

        public override Rol Rol => Rol.Paciente;
    }
}

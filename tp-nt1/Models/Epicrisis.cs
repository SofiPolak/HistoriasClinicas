using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tp_nt1.Models
{
    public class Epicrisis
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Fecha y hora")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}")]
        public DateTime FechaYHora { get; set; }

        public Diagnostico Diagnostico { get; set; }

        [ForeignKey(nameof(Episodio))]
        public Guid EpisodioId { get; set; }
        public Episodio Episodio { get; set; }

        [ForeignKey(nameof(Profesional))]
        public Guid? ProfesionalId { get; set; }
        public Profesional Profesional{ get; set; }
    }
}

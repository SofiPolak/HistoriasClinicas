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

        public Profesional Profesional { get; set; }
            
        public DateTime FechaYHora { get; set; }

        public Diagnostico Diagnostico { get; set; }

        [ForeignKey(nameof(Episodio))]
        public Guid EpisodioId { get; set; }
        public Usuario Episodio { get; set; }
    }
}

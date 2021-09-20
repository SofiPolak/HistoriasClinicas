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

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Profesional Profesional { get; set; }
            
        public DateTime FechaYHora { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Diagnostico Diagnostico { get; set; }

        [ForeignKey(nameof(Episodio))]
        public Guid EpisodioId { get; set; }
        public Usuario Episodio { get; set; }

    }
}

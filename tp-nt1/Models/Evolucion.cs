using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tp_nt1.Models
{
    public class Evolucion
    {
        [Key]
        public Guid Id { get; set; }

        public Profesional Profesional { get; set; }
        public DateTime FechaYHoraInicio { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}")]
        public DateTime FechaYHoraAlta { get; set; }

        public DateTime FechaYHoraCierre { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(250, ErrorMessage = "{0} admite un máximo de {1} caracteres")]

        public string DescripcionAtencion { get; set; }

        public bool EstadoAbierto { get; set; }

        [ForeignKey(nameof(Nota))]
        public Guid NotaoId { get; set; }
        public Nota Nota{ get; set; }

        [ForeignKey(nameof(Episodio))]
        public Guid EpisodioId { get; set; }
        public Episodio Episodio { get; set; }

    }
}

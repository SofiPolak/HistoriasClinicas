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

        [Display(Name = "Fecha de inicio")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}")]
        public DateTime FechaYHoraInicio { get; set; }

        [Display(Name = "Fecha de alta")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}")]
        public DateTime? FechaYHoraAlta { get; set; }

        [Display(Name = "Fecha de cierre")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}")]
        public DateTime? FechaYHoraCierre { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(250, ErrorMessage = "{0} admite un máximo de {1} caracteres")]
        [Display(Name = "Descripcion")]
        public string DescripcionAtencion { get; set; }

        [Display(Name = "Estado abierto")]
        public bool EstadoAbierto { get; set; }

        public List<Nota> Notas { get; set; }

        [ForeignKey(nameof(Episodio))]
        public Guid EpisodioId { get; set; }
        public Episodio Episodio { get; set; }

        [ForeignKey(nameof(Profesional))]
        public Guid ProfesionalId { get; set; }
        public Profesional Profesional { get; set; }

    }
}

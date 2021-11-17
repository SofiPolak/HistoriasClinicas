using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tp_nt1.Models
{
    public class Nota
    {
        [Key]
        public Guid Id { get; set; }
    
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(250, ErrorMessage = "{0} admite un máximo de {1} caracteres")]
        public string Mensaje { get; set; }

        [Display(Name = "Fecha y hora")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}")]
        public DateTime FechaYHora { get; set; }

        [ForeignKey(nameof(Empleado))]
        public Guid? EmpleadoId { get; set; }
        public Empleado Empleado { get; set; }

        [ForeignKey(nameof(Profesional))]
        public Guid? ProfesionalId { get; set; }
        public Profesional Profesional { get; set; }

        [ForeignKey(nameof(Evolucion))]
        public Guid EvolucionId { get; set; }
        public Evolucion Evolucion { get; set; }
    }
}

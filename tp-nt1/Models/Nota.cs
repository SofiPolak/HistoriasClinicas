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
        public Evolucion Evolucion { get; set; } // Nota no tiene una evolucion, pero esto indica la relacion, lo dejamos asi??????

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Empleado Empleado { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(250, ErrorMessage = "{0} admite un máximo de {1} caracteres")]
        public string Mensaje { get; set; }
        public DateTime FechaYHora { get; set; }
       
    }
}

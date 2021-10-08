using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using tp_nt1.Models.Enums;

namespace tp_nt1.Models
{
    public class Profesional : Usuario
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(8, ErrorMessage = "{0} admite un máximo de {1} caracteres")]
        [RegularExpression(@"[a-zA-Z0-9]*", ErrorMessage = "El campo {0} sólo admite caracteres alfanuméricos")]
        public string Matricula { get; set; }

        public Especialidad Especialidad { get; set; }

        public List<Epicrisis> Epicrisis{ get; set; }

        public List<Evolucion> Evoluciones { get; set; }

        public List<Nota> Notas { get; set; }        
    }
}

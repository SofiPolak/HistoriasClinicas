using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace tp_nt1.Models
{
    public class Profesional : Usuario
    {

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(8, ErrorMessage = "{0} admite un máximo de {1} caracteres")]
        [RegularExpression(@"[a-zA-Z0-9]*", ErrorMessage = "El campo {0} sólo admite caracteres alfanuméricos")]
        public int Matricula { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Especialidad Especialidad { get; set; }

        public List<Epicrisis> ListaEpicrisis{ get; set; }

        public List<Evolucion> Evoluciones { get; set; }

        public List<Nota> Notas { get; set; }

        [ForeignKey(nameof(Epicrisis))]
        public Guid EpicrisisId { get; set; }
        public Epicrisis Epicrisis { get; set; }

        [ForeignKey(nameof(Nota))]
        public Guid NotaId { get; set; }
        public Nota Nota { get; set; }

        [ForeignKey(nameof(Evolucion))]
        public Guid EvolucionId { get; set; }
        public Evolucion Evolucion { get; set; }
    }

   
}

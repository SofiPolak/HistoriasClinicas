using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tp_nt1.Models
{
    public class Diagnostico
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(250, ErrorMessage = "{0} admite un máximo de {1} caracteres")]
        public string Descripcion { get; set; }
 
        [MaxLength(250, ErrorMessage = "{0} admite un máximo de {1} caracteres")]
        public string Recomendacion { get; set; }

        [ForeignKey(nameof(Epicrisis))]
        public Guid EpicrisisId { get; set; }
        public Epicrisis Epicrisis { get; set; }
    }
}

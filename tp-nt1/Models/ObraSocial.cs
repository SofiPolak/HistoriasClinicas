using System;
using System.ComponentModel.DataAnnotations;

namespace tp_nt1.Models
{
    public class ObraSocial
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "{0} admite un máximo de {1} caracteres")]
        public string Nombre { get; set; }

        public bool Activo { get; set; }
    }
}

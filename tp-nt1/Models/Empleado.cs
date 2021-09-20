﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace tp_nt1.Models
{
    public class Empleado : Usuario
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(8, ErrorMessage = "{0} admite un máximo de {1} caracteres")]
        [RegularExpression(@"[a-zA-Z0-9]*", ErrorMessage = "El campo {0} sólo admite caracteres alfanuméricos")]
        public string Legajo { get; set; }
        public List<Nota> Notas { get; set; }

        public List<Episodio> Episodios { get; set; }

        [ForeignKey(nameof(Nota))]
        public Guid NotaId { get; set; }
        public Nota Nota { get; set; }

    }
}

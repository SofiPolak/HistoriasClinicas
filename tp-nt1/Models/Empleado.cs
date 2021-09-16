using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace tp_nt1.Models
{
    public class Empleado : Usuario
    {
        public string Legajo { get; set; }
        public List<Nota> Notas { get; set; }

        public List<Episodio> Episodios { get; set; }

        [ForeignKey(nameof(Nota))]
        public Guid NotaId { get; set; }
        public Nota Nota { get; set; }

    }
}

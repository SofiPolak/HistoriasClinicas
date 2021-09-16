using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tp_nt1.Models
{
    public class Diagnostico
    {
        [Key]
        public Guid Id { get; set; }
        public string Descripcion { get; set; }
        public string Recomendacion { get; set; }

        [ForeignKey(nameof(Epicrisis))]
        public Guid EpicrisisId { get; set; }
        public Epicrisis Epicrisis { get; set; }

    }
}

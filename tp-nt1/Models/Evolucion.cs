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
        public Medico Medico { get; set; }
        public DateTime FechaYHoraInicio { get; set; }
        public DateTime FechaYHoraAlta { get; set; }
        public DateTime FechaYHoraCierre { get; set; }
        public string DescripcionAtencion { get; set; }
        public bool EstadoAbierto { get; set; }
        public List<Nota> Notas { get; set; }


        [ForeignKey(nameof(Nota))]
        public Guid NotaoId { get; set; }
        public Nota Nota{ get; set; }

        [ForeignKey(nameof(Episodio))]
        public Guid EpisodioId { get; set; }
        public Episodio Episodio { get; set; }

    }
}

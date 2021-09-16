using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tp_nt1.Models
{
    public class HistoriaClinica
    {
        [Key]
        Guid Id { get; set; }
        public List<Episodio> Episodios { get; set; }

        [ForeignKey(nameof(Paciente))]
        public Guid PacienteId { get; set; }
        public Paciente Paciente { get; set; }

    }
}

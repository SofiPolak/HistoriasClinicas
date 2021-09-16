using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace tp_nt1.Models
{
    public class Medico : Usuario
    {

        public int Matricula { get; set; }
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

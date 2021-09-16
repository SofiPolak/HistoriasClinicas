using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tp_nt1.Models
{
    public class Episodio
    {
        [Key]
        public Guid Id { get; set; }
        public string Motivo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaYHoraInicio { get; set; }
        public DateTime FechaYHoraAlta { get; set; }
        public DateTime FechaYHoraCierre { get; set; }
        public bool EstadoAbierto { get; set; }
        public List<Evolucion> RegistroEvoluciones { get; set; }
        public Epicrisis Epicrisis { get; set; }
       

        [ForeignKey(nameof(Empleado))]
        public Guid EmpleadoId { get; set; }
        public Empleado EmpleadoRegistra { get; set; }

        [ForeignKey(nameof(HistoriaClinica))]
        public Guid HistoriaId { get; set; }
        public HistoriaClinica HistoriaClinica { get; set; }

    }
}

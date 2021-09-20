﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tp_nt1.Models
{
    public class Episodio
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(100, ErrorMessage = "{0} admite un máximo de {1} caracteres")]
        public string Motivo { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(250, ErrorMessage = "{0} admite un máximo de {1} caracteres")]
        public string Descripcion { get; set; }

        public DateTime FechaYHoraInicio { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}")]
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

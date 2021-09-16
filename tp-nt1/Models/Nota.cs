using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tp_nt1.Models
{
    public class Nota
    {

        [Key]
        public Guid Id { get; set; }
        public Evolucion Evolucion { get; set; }
        public Empleado Empleado { get; set; }
        public string Mensaje { get; set; }
        public DateTime FechaYHora { get; set; }
       

    }
}

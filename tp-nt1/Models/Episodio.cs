using System;
using System.Collections.Generic;

namespace tp_nt1.Models
{
    public class Episodio
    {
        public Episodio()
        {
        }

        public Episodio(string motivo, string desc, DateTime inicio, DateTime alta, DateTime cierre, bool estado, Epicrisis epi, Empleado emp)
        {
            Id = Guid.NewGuid();
            Motivo = motivo;
            Descripcion = desc;
            FechaYHoraInicio = inicio;
            FechaYHoraAlta = alta;
            FechaYHoraCierre = cierre;
            EstadoAbierto = estado;
            RegistroEvoluciones = new List<Evolucion>();
            Epicrisis = epi;
            EmpleadoRegistra = emp;
        }

        public Guid Id { get; set; }
        public string Motivo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaYHoraInicio { get; set; }
        public DateTime FechaYHoraAlta { get; set; }
        public DateTime FechaYHoraCierre { get; set; }
        public bool EstadoAbierto { get; set; }
        public List<Evolucion> RegistroEvoluciones { get; set; }
        public Epicrisis Epicrisis { get; set; }
        public Empleado EmpleadoRegistra { get; set; }

    }
}

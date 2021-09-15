using System;
namespace tp_nt1.Models
{
    public class Medico: Usuario
    { 
        public int Matricula { get; set; }
        public Especialidad Especialidad { get; set; }
    }

   
}

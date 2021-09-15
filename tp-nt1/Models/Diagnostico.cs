using System;
namespace tp_nt1.Models
{
    public class Diagnostico
    {
        public Guid Id { get; set; }
        public Epicrisis Epicrisis { get; set; }
        public string Descripcion { get; set; }
        public string Recomendacion { get; set; }
        
    }
}

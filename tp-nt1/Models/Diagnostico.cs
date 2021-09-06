using System;
namespace tp_nt1.Models
{
    public class Diagnostico
    {
        public Diagnostico()
        {
        }

        public Diagnostico(Epicrisis epi, string desc, string reco)
        {
            Id = Guid.NewGuid();
            Epicrisis = epi;
            Descripcion = desc;
            Recomendacion = reco;
        }

        public Guid Id { get; set; }
        public Epicrisis Epicrisis { get; set; }
        public string Descripcion { get; set; }
        public string Recomendacion { get; set; }
        
    }
}

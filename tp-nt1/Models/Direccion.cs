using System;
namespace tp_nt1.Models
{
    public class Direccion
    {
        public string Calle { get; set; }
        public int Numero { get; set; }
        public string Localidad { get; set; }
        public string CodigoPostal { get; set; }
        public string Provincia { get; set; }

        public Direccion()
        {

        }

        public Direccion (string calle, int numero, string localidad, string cp, string prov)
        {
            Calle = calle;
            Numero = numero;
            Localidad = localidad;
            CodigoPostal = cp;
            Provincia = prov;
        }
    }

}
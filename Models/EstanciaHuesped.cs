
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mokki.Models
{
    public class EstanciaHuesped
    {
        public int Id { get; set; }
        public Estancia Estancia { get; set; }
        public int EstanciaId { get; set; }
        public Huesped Huesped { get; set; }
        public int HuespedId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}

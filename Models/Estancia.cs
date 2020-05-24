using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mokki.Models
{
    public class Estancia
    {
        public int Id { get; set; }

        public int Duracion { get; set; }
        public string Foto { get; set; }

        public Anfitrion Anfitrion { get; set; }
        public int AnfitrionId { get; set; }
        public List<EstanciaHuesped> ListaEstanciaHuesped { get; set; }

    }
}

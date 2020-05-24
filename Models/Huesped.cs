using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mokki.Models
{
    public class Huesped
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Ciudad { get; set; }
        public List<EstanciaHuesped> ListaEstanciaHuesped { get; set; }

        public AppUser User { get; set; }
        public string UserId { get; set; }

    }
}
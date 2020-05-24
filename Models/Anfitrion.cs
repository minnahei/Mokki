using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mokki.Models
{
    public class Anfitrion
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Pueblo { get; set; }


        public Estancia Estancia { get; set; }
        public AppUser User { get; set; }
        public string UserId { get; set; }
    }
}

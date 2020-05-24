using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mokki.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        [MaxLength(20)]
        public string Nombre { get; set; }
        [MaxLength(50)]
        public string Apellidos { get; set; }
        public string Provincia { get; set; }
        [Phone]
        public string Telefono { get; set; }
        public string Foto { get; set; }

    }
}

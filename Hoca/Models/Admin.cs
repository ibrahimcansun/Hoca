using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hoca.Models
{
    public class Admin
    {
        [Key]
        public int AdminID { get; set; }
        [Required]
        public string Email { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        [Required]
        public string Sifre { get; set; }
    }
}

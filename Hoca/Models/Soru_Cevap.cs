using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hoca.Models
{
    public class Soru_Cevap
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int SoruId { get; set; }

        [Required]
        public int CevapId { get; set; }
    }
}

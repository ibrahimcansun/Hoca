using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hoca.Models
{
    public class Cevaplar
    {
        [Key]
        public int CevapID { get; set; }
        
        [Required]
        public string Cevap { get; set; }
        
        [Required]
        public OgrenciUser Ogretmen { get; set; }//cevabi veren ogretmen
    }
}

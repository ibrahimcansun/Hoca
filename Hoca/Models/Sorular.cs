using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hoca.Models
{
    public class Sorular
    {
        [Key]
        public int SoruID { get; set; }

        [Required]//sorunun basligi olmasi sart
        public string SoruBaslik { get; set; }//(x+y)^2 acilimi nedir

        [Required]//sorunun icerigi olmasi sart
        public string SoruIcerik { get; set; }//matematik cozuyorum ama x+y2 bilmiyorum yardim
        
        public SoruKategori SoruKategori { get; set; }//matematik
        
        public System.DateTime Tarih { get; set; } = System.DateTime.Now;//şimdiki zaman
        
        [Required]
        public string OgrenciId1 { get; set; }//soruyu soran ogrenci

        public string OgrenciAd { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hoca.Models
{
    public class Ogretmen
    {
        [Key]
        public int OgretmenID { get; set; }
        [Required]
        public string Ad { get; set; }
        public string Soyad { get; set; }
        [Required]//mail adresi giris icin lazim
        public string Mail { get; set; }
        [Required]//sifre giris icin lazim
        public string Sifre { get; set; }
        public string Bolum { get; set; }//Ogretmenin tarih,kimya,matematik bolumu
        public int CevaplananSoruSayisi { get; set; }//bugune kadar cevap verdigi soru sayisi
        public List<Sorular> Sorular { get; set; }//cevaplanan sorular
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoca.Models
{
    public class OgrenciUser : IdentityUser
    {
        public Siniflar? Sinif { get; set; }//kacinci sinif oldugu
        //public IList<Sorular> SorduguSorular { get; set; }//ogrencinin sordugu sorular
        public int sorulanSoruSayisi { get; set; } = 0;//ogrencinin bugune kadar sordugu soru sayisi
        public string FotoPath { get; set; }//ogrencinin fotografinin klasor yolu
    }
}

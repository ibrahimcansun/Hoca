using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hoca.Models
{
    public class Ogrenci
    {
        [Key]
        public int Id { get; set; }//ogrencinin numarasi key
        
        [NotMapped]
        public string EncryptedId { get; set; }

        [Required(ErrorMessage = "Adınızı girmeniz gerekmektedir")]
        [Display(Name ="Adı")]
        [MaxLength(30,ErrorMessage = "Adınız 30 karakterden uzun olamaz")]
        public string Ad { get; set; }
        //Soyad zorunlu degil
        [Display(Name = "Soyadı")]
        [MaxLength(30, ErrorMessage = "Soyadınız 30 karakterden uzun olamaz")]
        public string Soyad { get; set; }
        [Required(ErrorMessage = "Mail adresinizi girmeniz gerekmektedir")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",ErrorMessage ="Mailiniz yanlış")]
        [Display(Name = "Mail adresi")]
        [MaxLength(345, ErrorMessage = "Mail adresiniz 345 karakterden uzun olamaz")]
        public string Email { get; set; }//giris yaparken lazim
        [Required(ErrorMessage = "Şifrenizi girmeniz gerekmektedir")]
        [Display(Name = "Şifre")]
        [MinLength(6, ErrorMessage = "Şifreniz en az 6 karakter uzunluğunda olabilir")]
        public string Sifre { get; set; }//giris yaparken lazim
        [Required(ErrorMessage = "Lütfen bir sınıf seçin")]
        [Display(Name = "Sınıf")]
        public Siniflar? Sinif { get; set; }//kacinci sinif oldugu
        public IList<Sorular> SorduguSorular { get; set; }//ogrencinin sordugu sorular
        //public int sorulanSoruSayisi { get; set; } = 0;//ogrencinin bugune kadar sordugu soru sayisi
        public string FotoPath { get; set; }//ogrencinin fotografinin klasor yolu

    }
}

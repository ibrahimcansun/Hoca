using Hoca.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hoca.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Adınızı girmeniz gerekmektedir.")]
        [MaxLength(30, ErrorMessage = "Adınız en fazla 30 karakter uzunluğunda olabilir.")]
        [Display(Name = "Adınız")]
        public string Name { get; set; }

        [Remote(action: "IsEmailInUse", controller: "Hesap")]
        [Required(ErrorMessage = "Mail adresinizi girmeniz gerekmektedir.")]
        [EmailAddress(ErrorMessage = "E-posta adresiniz geçerli bir e-posta adresi değil.")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Şifrenizi girmeniz gerekmektedir")]
        [DataType(DataType.Password, ErrorMessage = "Şifreniz gereken karakterleri içermiyor")]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [DataType(DataType.Password, ErrorMessage = "Şifrenizi tekrarlarken yanlış girdiniz")]
        [Display(Name = "Şifrenizi tekrar giriniz")]
        [Compare("Password",
            ErrorMessage = "Şifreler uyuşmuyor, lütfen tekrar giriniz.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Sınıfınızı belirtmeniz gerekmektedir.")]
        public Siniflar? Class { get; set; }
    }
}

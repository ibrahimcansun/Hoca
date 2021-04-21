using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hoca.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Sifre { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Şifreyi tekrar girin")]
        [Compare("Sifre", ErrorMessage = "Şifreler uyuşmuyor")]
        public string SifreUyum { get; set; }

        public string Token { get; set; }
    }
}

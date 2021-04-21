using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hoca.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Adınızı girmeniz gerekmektedir.")]
        [Display(Name = "Adınız")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Mail adresinizi girmeniz gerekmektedir.")]
        [EmailAddress]
        [Display(Name = "Mail adresi")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifrenizi girmeniz gerekmektedir.")]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Beni hatırla")]
        public bool BeniHatirla { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }
    }
}

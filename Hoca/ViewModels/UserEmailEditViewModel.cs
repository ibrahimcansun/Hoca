using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hoca.ViewModels
{
    public class UserEmailEditViewModel
    {
        public string Id { get; set; }

        [Remote(action: "IsEmailInUse", controller: "Hesap")]
        [Required(ErrorMessage = "Mail adresinizi girmelisiniz.")]
        [EmailAddress(ErrorMessage = "Geçerli bir mail adresi girmelisiniz.")]
        public string Email { get; set; }
    }
}

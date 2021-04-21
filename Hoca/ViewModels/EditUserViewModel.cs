using Hoca.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hoca.ViewModels
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            Claims = new List<string>();
            Roles = new List<string>();
        }
        
        public string Id { get; set; }

        [Required(ErrorMessage = "Adınızı girmeniz gerekmektedir.")]
        public string UserName { get; set; }

        //[Remote(action: "IsEmailInUse", controller: "Hesap")]
        [Required(ErrorMessage = "Mail adresinizi girmelisiniz.")]
        [EmailAddress(ErrorMessage = "Geçerli bir mail adresi girmelisiniz.")]
        public string Email { get; set; }

        public Siniflar? Class { get; set; }

        public List<string> Claims { get; set; }
        public IList<string> Roles { get; set; }
    }
}

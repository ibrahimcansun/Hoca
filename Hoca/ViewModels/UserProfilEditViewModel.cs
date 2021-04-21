using Hoca.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hoca.ViewModels
{
    public class UserProfilEditViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Adınızı girmeniz gerekmektedir.")]
        [Display(Name = "Adım")]
        public string UserName { get; set; }

        [Display(Name = "Sınıfım")]
        public Siniflar? Class { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hoca.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required(ErrorMessage = "Rol adını girmeniz gerekmektedir.")]
        [Display(Name = "Rol Adı")]
        public string RoleName { get; set; }
    }
}

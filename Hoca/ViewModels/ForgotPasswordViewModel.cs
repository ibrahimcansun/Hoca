using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hoca.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Mail alanı boş bırakılamaz")]
        [EmailAddress(ErrorMessage ="Mail adresi girmelisiniz")]
        public string Email { get; set; }
    }
}

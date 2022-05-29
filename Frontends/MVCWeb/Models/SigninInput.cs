using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWeb.Models
{
    public class SigninInput
    {
        [Required]
        [Display(Name = "kullanici adiniz")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "sifreniz")]
        public string Password { get; set; }
        [Display(Name = "beni hatirla")]
        public bool IsRemember { get; set; }
    }
}

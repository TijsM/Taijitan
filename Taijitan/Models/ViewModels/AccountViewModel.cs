using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.ViewModels
{
    public class AccountViewModel
    {
        [Required(ErrorMessage ="Gelieve het email in te vullen")]
        public string Email { get; set; }

        [Display(Name = "Wachtwoord")]
        [Required(ErrorMessage = "Gelieve het wachtwoord in te vullen")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^.*(?=.{8,})(?=.*[\d])(?=.*[\W]).*$", ErrorMessage = "Het wachtwoord moet een hoofdletter en speciaal teken bevatten")]
        [MinLength(8, ErrorMessage = "Het wachtwoord moet minstens 8 karakters bevatten")]
        public string Password { get; set; }

        [Display(Name = "Bevestig wachtwoord")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "De wachtwoorden zijn niet gelijk")]
        public string PasswordConfirm { get; set; }
    }
}

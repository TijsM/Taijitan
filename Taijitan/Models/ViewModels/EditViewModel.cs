using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Models.UserViewModel
{
    public class EditViewModel
    {
        [Required(ErrorMessage = "Naam is verplicht in te vullen")]
        [Display(Name ="Naam")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Voornaam is verplicht in te vullen")]
        [Display(Name = "Voornaam")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Geboortedatum is verplicht in te vullen")]
        [Display(Name = "Geboortedatum")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Straatnaam is verplicht in te vullen")]
        [Display(Name = "Straatnaam")]
        public string Street { get; set; }

        [DataType(DataType.PostalCode)]
        [Display(Name = "Postcode")]
        [Required(ErrorMessage = "Postcode is verplicht in te vullen")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Stadnaam is verplicht in te vullen")]
        [Display(Name = "Stadnaam")]
        public string CityName { get; set; }

        [Required(ErrorMessage = "Land is verplicht in te vullen")]
        [Display(Name="Land")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Huisnummer is verplicht in te vullen")]
        [Display(Name = "Huisnummer")]
        public string HouseNumber { get; set; }

        [Required(ErrorMessage = "Telefoonnummer is verplicht in te vullen")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telefoonnummer")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "E-mailadres is verplicht in te vullen")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mailadres")]
        public string Email { get; set; }


        public EditViewModel(User user)
        {
            Name = user.Name;
            FirstName = user.FirstName;
            DateOfBirth = user.DateOfBirth;
            Street = user.Street;
            PostalCode = user.City?.Postalcode ?? "Not Found";
            CityName = user.City?.Name ?? "Not Found";
            Country = user.Country;
            HouseNumber = user.HouseNumber;
            PhoneNumber = user.PhoneNumber;
            Email = user.Email;
        }

        public EditViewModel()
        {

        }
    }
}

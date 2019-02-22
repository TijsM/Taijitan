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
        [Required(ErrorMessage = "{0} is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Streetname")]
        public string Street { get; set; }

        [DataType(DataType.PostalCode)]
        [Required(ErrorMessage = "{0} is required")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "City")]
        public string CityName { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Country { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "House number")]
        public string HouseNumber { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail adres")]
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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Models.MemberViewModels
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
        public string PhonenNumber { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        public EditViewModel(Member member)
        {
            Name = member.Name;
            FirstName = member.FirstName;
            DateOfBirth = member.DateOfBirth;
            Street = member.Street;
            PostalCode = member.City?.Postalcode ?? "Not Found";
            CityName = member.City?.Name ?? "Not Found";
            Country = member.Country;
            HouseNumber = member.HouseNumber;
            Email = member.Email;
        }
    }
}

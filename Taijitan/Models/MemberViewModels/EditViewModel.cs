using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Models.MemberViewModels
{
    public class EditViewModel
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string CityName { get; set; }
        public string Country { get; set; }
        public string HouseNumber { get; set; }
        public string PhoneNumber { get; set; }
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

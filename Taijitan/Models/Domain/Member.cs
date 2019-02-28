using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public class Member : User
    {
        public Formula Formula { get; set; }
        public Rank Rank { get; set; }

        public Member(string name, string firstName, DateTime dateOfBirth,string street,City city,
            string country,string houseNumber,string phoneNumber, string email,Formula formula)
        {
            Name = name;
            FirstName = firstName;
            DateOfBirth = dateOfBirth;
            PhoneNumber = phoneNumber;
            Email = email;
            Street = street;
            City = city;
            Country = country;
            HouseNumber = houseNumber;
            Rank = Rank.Starter;
            Formula = formula;
        }
        private Member() { }
    }
}

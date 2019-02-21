using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public class Member
    {
        public int MemberId { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Street { get; set; }
        public City City { get; set; }
        public string Country { get; set; }
        public string HouseNumber { get; set; }
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public Member(string name, string firstName, DateTime dateOfBirth,string street,City city,string country,string houseNumber,string phoneNumber, string email)
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
        }
        private Member() { }

        public void Change(string name, string firstName, DateTime dateOfBirth, string street, City city, string country, string houseNumber, string phoneNumber, string email)
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
        }

    }
}

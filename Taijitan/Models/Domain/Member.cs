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
        public string Firstname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string HouseNumber { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }

        public Member(string naam, string voornaam, DateTime geboortedatum,string street,string city,string country,string houseNumber,int telefoonnummer, string email)
        {
            Name = naam;
            Firstname = voornaam;
            DateOfBirth = geboortedatum;
            PhoneNumber = telefoonnummer;
            Email = email;
            Street = street;
            City = city;
            Country = country;
            HouseNumber = houseNumber;
        }

        public void Change(string naam, string voornaam, DateTime geboortedatum, string street, string city, string country, string houseNumber, int telefoonnummer, string email)
        {
            Name = naam;
            Firstname = voornaam;
            DateOfBirth = geboortedatum;
            PhoneNumber = telefoonnummer;
            Email = email;
            Street = street;
            City = city;
            Country = country;
            HouseNumber = houseNumber;
        }

    }
}

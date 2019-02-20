using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public class Member
    {
        public string Name { get; set; }
        public string Firstname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }

        public Member(string naam, string voornaam, DateTime geboortedatum, string adres, int telefoonnummer, string email)
        {
            Name = naam;
            Firstname = voornaam;
            DateOfBirth = geboortedatum;
            Address = adres;
            PhoneNumber = telefoonnummer;
            Email = email;
        }

        public void WijzigGegevens(string naam, string voornaam, DateTime geboortedatum, string adres, int telefoonnummer, string email)
        {
            Name = naam;
            Firstname = voornaam;
            DateOfBirth = geboortedatum;
            Address = adres;
            PhoneNumber = telefoonnummer;
            Email = email;
        }

    }
}

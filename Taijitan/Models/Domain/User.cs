using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Street { get; set; }
        public City City { get; set; }
        public Country Country { get; set; }
        public string HouseNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }



        public DateTime DateRegistred { get; set; }
        public Gender Gender { get; set; }
        public Country Nationality { get; set; }
        public string PersonalNationalNumber { get; set; }//rijksregisternummer
        public string BirthPlace { get; set; }
        public string LandlineNumber { get; set; }//vaste telefoonlijn --> niet verplicht
        public string MailParent { get; set; }//--> niet verplicht

        public virtual void Change(string name, string firstName, DateTime dateOfBirth, string street, City city, Country country, string houseNumber, string phoneNumber, string email, DateTime dateRegistred, Gender gender, Country nationality, string personalNationalNumber, string birthPlace, string landlineNumber = "Niet gekend", string mailParent = "niet gekend" ) {
            Name = name;
            FirstName = firstName;
            DateOfBirth = DateOfBirth;
            Street = street;
            City = city;
            Country = country;
            HouseNumber = houseNumber;
            PhoneNumber = phoneNumber;
            Email = email;
            DateRegistred = dateRegistred;
            Gender = gender;
            Nationality = nationality;
            PersonalNationalNumber = personalNationalNumber;
            BirthPlace = birthPlace;
            LandlineNumber = landlineNumber;
            MailParent = mailParent;
        }
    }
}

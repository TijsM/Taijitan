using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public class Teacher : User
    {
        public Teacher(string name, string firstName, DateTime dateOfBirth, string street, City city, Country country, string houseNumber, string phoneNumber, string email, DateTime dateRegistred, Gender gender, Country nationality, string personalNationalNumber, string birthPlace, string landlineNumber = "", string mailParent = "")
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
            DateRegistred = DateRegistred;
            Gender = gender;
            Nationality = nationality;
            PersonalNationalNumber = personalNationalNumber;
            BirthPlace = birthPlace;
            LandlineNumber = landlineNumber;
            MailParent = mailParent;
        }
        private Teacher() {
        }
    }
}

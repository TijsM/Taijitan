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
        public IList<SessionMember> SessionMembers { get; set; }

        public Member(string name, string firstName, DateTime dateOfBirth,string street,City city,
            Country country,string houseNumber,string phoneNumber, string email,Formula formula, DateTime dateRegistred, Gender gender, Country nationality, string personalNationalNumber, string birthPlace, string landlineNumber = "Niet gekend", string mailParent = "niet gekend")
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
            Gender = gender;
            Nationality = nationality;
            PersonalNationalNumber = personalNationalNumber;
            BirthPlace = birthPlace;
            LandlineNumber = landlineNumber;
            MailParent = mailParent;

            SessionMembers = new List<SessionMember>();
        }
        private Member() { }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public class Member : User
    {
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

        public override void Change(string name, string firstName, DateTime dateOfBirth, string street, City city, string country, string houseNumber, string phoneNumber, string email)
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

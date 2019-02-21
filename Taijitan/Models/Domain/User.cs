﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public abstract class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Street { get; set; }
        public City City { get; set; }
        public string Country { get; set; }
        public string HouseNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public abstract void Change(string name, string firstName, DateTime dateOfBirth, string street, City city, string country, string houseNumber, string phoneNumber, string email);
    }
}

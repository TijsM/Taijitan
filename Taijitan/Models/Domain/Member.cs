using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public class Member
    {

        #region Fields
        private string _name;
        private string _firstName;
        private DateTime _dateOfBirth;
        private string _phoneNumber;
        private string _email;
        #endregion


        public int MemberId { get; set; }
        public string Name {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("City must have a name");
                _name = value;
            }
        }
        public string FirstName {
            get => _firstName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("City must have a name");
                _firstName = value;
            }
              
        }
        public DateTime DateOfBirth {
            get => _dateOfBirth;
            set
            {
                if (_dateOfBirth > DateTime.Today)
                    throw new ArgumentException("City must have a name");
                _dateOfBirth = value;
            }
        }
        public string Street { get; set; }
        public City City { get; set; }
        public string Country { get; set; }
        public string HouseNumber { get; set; }
        public string PhoneNumber {
            get => _phoneNumber;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("City must have a name");
                _phoneNumber = value;
            }
        }
  
        public string Email {
            get => _email;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("City must have a name");
                _email = value;
            }
        }

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

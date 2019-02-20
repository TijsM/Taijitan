using System;
using System.Collections.Generic;
using System.Text;
using Taijitan.Models.Domain;
using Xunit;

namespace TaijitanTest.Models
{
    public class MemberTest
    {
        private const string _name = "De Bakker";
        private const string _firstName = "Thomas";
        private DateTime _dateOfBirth = new DateTime(1999 / 06 / 14);
        private const string _street = "Kerkstraat";
        private City _city = new City("9810", "Nazareth");
        private const string _country = "Belgium";
        private const string _housenumber = "8";
        private const string _phoneNumber = "0499857447";
        private const string _email = "thomas.debakker@gmail.com";


        #region Constructor
        [Fact]
        public void NewMember_ValidData_CreatesMember()
        {
            var member = new Member(_name, _firstName, _dateOfBirth, _street, _city, _country, _housenumber, _phoneNumber, _email);
            Assert.Equal(_name, member.Name);
            Assert.Equal(_firstName, member.FirstName);
            Assert.Equal(_dateOfBirth, member.DateOfBirth);
            Assert.Equal(_street, member.Street);
            Assert.Equal(_city.Name, member.City.Name);
            Assert.Equal(_country, member.Country);
            Assert.Equal(_housenumber, member.HouseNumber);
            Assert.Equal(_phoneNumber, member.PhoneNumber);
            Assert.Equal(_email, member.Email);
        }
        #endregion


    }
}

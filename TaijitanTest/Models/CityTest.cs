using System;
using System.Collections.Generic;
using System.Text;
using Taijitan.Models.Domain;
using Xunit;

namespace TaijitanTest.Models
{
    public class CityTest
    {
        #region Constructor
        [Fact]
        public void NewCity_ValidData_CreatesCity()
        {
            City city = new City("8000", "Brugge");
            Assert.Equal("8000", city.Postalcode);
            Assert.Equal("Brugge", city.Name);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("abcd")]
        [InlineData("a123")]
        [InlineData("123a")]
        [InlineData("78904")]
        [InlineData("123")]
        [InlineData("a1234")]
        public void NewCity_InvalidPostalCode_ThrowsArgumentException(string postalCode)
        {
            Assert.Throws<ArgumentException>(() => new City(postalCode, "Brugge"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void NewCity_InvalidName_ThrowsArgumentException(string name)
        {
            Assert.Throws<ArgumentException>(() => new City("8000", name));
        }

        #endregion
    }
}

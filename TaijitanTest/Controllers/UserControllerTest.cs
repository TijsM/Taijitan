using Microsoft.AspNetCore.Mvc;
using Moq;
using Taijitan.Controllers;
using Taijitan.Models.Domain;
using Taijitan.Models.UserViewModel;
using TaijitanTest.Data;
using Xunit;

namespace TaijitanTest.Controllers
{
    public class UserControllerTest
    {
        
        #region Fields
        private readonly DummyApplicationDbContext _dummyContext;
        private readonly UserController _userController;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<ICityRepository> _mockCityRepository;

        private readonly User _tomJansens;
        private readonly string _tomJansensFirstName;

    
        #endregion

        #region Constructors
        public UserControllerTest()
        {
            _dummyContext = new DummyApplicationDbContext();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockCityRepository = new Mock<ICityRepository>();

            _tomJansens = _dummyContext.UserTomJansens;
            _tomJansensFirstName = _tomJansens.FirstName;
        }
        #endregion

        #region TestEditHttpGet
        [Fact]
        public void EddiHttpGet_ValidProductId_PassesUsersDetails()
        {
           var result = _userController.
        }
        #endregion







    }
}

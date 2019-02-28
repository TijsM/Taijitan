using Microsoft.AspNetCore.Identity;
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
        private UserController _userController;
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<ICityRepository> _mockCityRepository;

        private UserManager<IdentityUser> _userManager;

        private User _tomJansens;
        private int _tomJansensId;


        #endregion

        #region Constructors
        public UserControllerTest()
        {
            /*
            _dummyContext = new DummyApplicationDbContext();

            _userManager = new Mock<UserManager<IdentityUser>>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockCityRepository = new Mock<ICityRepository>();

            _userController = new UserController
                (_mockUserRepository.Object, _mockCityRepository.Object, _userManager.Object);

            _tomJansens = _dummyContext.UserTomJansens;
            _tomJansensId = _tomJansens.UserId;*/
        }
        #endregion

        #region TestEditHttpGet
        [Fact]
        public void EddiHttpGet_ValidProductId_PassesUsersDetails()
        {
            //Arrange
            _tomJansens = _dummyContext.UserTomJansens;
            _tomJansensId = _tomJansens.UserId;

            _mockUserRepository = new Mock<IUserRepository>();
            _mockCityRepository = new Mock<ICityRepository>();

            //_userManager = new Mock<UserManager<IdentityUser>>();

            _userController = new UserController
               (_mockUserRepository.Object, _mockCityRepository.Object, _userManager);

            //Act
            var result = _userController.Edit(_tomJansensId) as ViewResult;
            result.ViewData["userId"] = _tomJansensId;
            var userViewModel = result?.Model as EditViewModel;

            //Assert
            Assert.Equal("Tom", userViewModel?.FirstName);
        }
        #endregion







    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
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
        private Member _tomJansens;
        private string _tomJansenCityPostalCode;
        private int _tomJansensId;
        private string _NonExistingPostalCode;
        private int _NonExistingUserId;
        private string _tomJansensEmail;
        private Formula _tomJansensFormula;
        private string _partOfName;

        #endregion

        #region Constructors
        public UserControllerTest()
        {        
            _dummyContext = new DummyApplicationDbContext();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockCityRepository = new Mock<ICityRepository>();
            _tomJansens = _dummyContext.UserTomJansens;
            _tomJansensId = _tomJansens.UserId;
            _tomJansenCityPostalCode = _tomJansens.City.Postalcode;
            _NonExistingPostalCode = "9999";
            _NonExistingUserId = 9999;
            _partOfName = "de";
            _tomJansensEmail = _tomJansens.Email;
            _tomJansensFormula = _tomJansens.Formula;

            _mockCityRepository.Setup(c => c.GetAll()).Returns(_dummyContext.Cities);
            _mockCityRepository.Setup(c => c.GetByPostalCode(_tomJansenCityPostalCode)).Returns(_dummyContext.TomJansensCity);
            _mockCityRepository.Setup(c => c.GetByPostalCode(_NonExistingPostalCode)).Returns(null as City);

            _mockUserRepository.Setup(c => c.GetAll()).Returns(_dummyContext.Users);
            _mockUserRepository.Setup(c => c.GetById(_tomJansensId)).Returns(_dummyContext.UserTomJansens);
            _mockUserRepository.Setup(c => c.GetByEmail(_tomJansensEmail)).Returns(_dummyContext.UserTomJansens);
            _mockUserRepository.Setup(c => c.GetByFormula(_tomJansensFormula)).Returns(_dummyContext.UsersFormula1);
            _mockUserRepository.Setup(c => c.GetByPartofName(_partOfName)).Returns(_dummyContext.UsersByPartOfName);

            _userController = new UserController(_mockUserRepository.Object, _mockCityRepository.Object)
            {
                TempData = new Mock<ITempDataDictionary>().Object //to be able to test the TempData
            };

            
        }
        #endregion

        #region TestEditHttpGet
        [Fact]
        public void EditHttpGet_ValidUserId_PassesUsersDetailsInAnEditViewModelToView()
        {
            var result = _userController.Edit(_tomJansensId) as ViewResult;
            var userViewmodel = result?.Model as EditViewModel;
            Assert.Equal("Tom", userViewmodel?.FirstName);
            Assert.Equal("Jansens", userViewmodel?.Name);
            Assert.Equal("tom.jansens@gmail.com", userViewmodel?.Email);
            Assert.Equal("Gent", userViewmodel?.CityName);
            //...
        }
        [Fact]
        public void EditHttpGet_UserNotFound_ReturnsNotFound()
        {
            var result = _userController.Edit(_NonExistingUserId);
            Assert.IsType<NotFoundResult>(result);
        }
        #endregion

        #region TestEditHttpPost
        [Fact]
        public void EditHttpPost_ValidEdit_UpdatesAndPersistsTheUser()
        {
            var userViewmodel = new EditViewModel(_tomJansens)
            {
                Name = "Deschacht",
                Country = "Denmark",
            };
            _userController.Edit(_tomJansensId,_tomJansens,userViewmodel);
            Assert.Equal("Deschacht", _tomJansens.Name);
            Assert.Equal("Denmark", _tomJansens.Country);
            _mockUserRepository.Verify(m => m.SaveChanges(), Times.Once);
        }
        [Fact]
        public void EditHttpPost_ValidEdit_RedirectsToIndexOfHomeController()
        {
            var userViewmodel = new EditViewModel(_tomJansens);
            var result = _userController.Edit(_tomJansensId,_tomJansens, userViewmodel) as RedirectToActionResult;
            Assert.Equal("Index", result?.ActionName);
            Assert.Equal("Home", result?.ControllerName);
        }
        #endregion
    }
}

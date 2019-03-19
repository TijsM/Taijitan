using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Taijitan.Controllers;
using Taijitan.Models.Domain;
using TaijitanTest.Data;
using Xunit;
using System.Linq;
using Taijitan.Models.ViewModels;

namespace TaijitanTest.Controllers
{
    public class SessionControllerTest
    {

        #region Fields
        private readonly DummyApplicationDbContext _dummyContext;
        private SessionController _sessionController;
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<ISessionRepository> _mockSessionRepository;
        private Mock<IFormulaRepository> _mockFormulaRepository;
        private Mock<ITrainingDayRepository> _mockTrainingDayRepository;
        private Mock<INonMemberRepository> _nonMemberRepository;
        private Member _tomJansens;
        private Session _session1;
        private string _tomJansenCityPostalCode;
        private int _tomJansensId;
        private int _session1Id;
        private string _NonExistingPostalCode;
        private int _NonExistingUserId;
        private string _tomJansensEmail;
        private Formula _tomJansensFormula;
        private string _partOfName;
        private Admin _alain;
        #endregion


        #region Constructor
        public SessionControllerTest()
        {
            _dummyContext = new DummyApplicationDbContext();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockSessionRepository = new Mock<ISessionRepository>();
            _mockFormulaRepository = new Mock<IFormulaRepository>();
            _mockTrainingDayRepository = new Mock<ITrainingDayRepository>();
            _nonMemberRepository = new Mock<INonMemberRepository>();
            _tomJansens = _dummyContext.UserTomJansens;
            _session1 = _dummyContext.Session1;
            _tomJansensId = _tomJansens.UserId;
            _session1Id = _session1.SessionId;
            _tomJansenCityPostalCode = _tomJansens.City.Postalcode;
            _NonExistingPostalCode = "9999";
            _NonExistingUserId = 9999;
            _partOfName = "de";
            _tomJansensEmail = _tomJansens.Email;
            _tomJansensFormula = _tomJansens.Formula;
            _alain = _dummyContext.Alain;

            //Setups
            _mockUserRepository.Setup(c => c.GetAll()).Returns(_dummyContext.Users);
            _mockUserRepository.Setup(c => c.GetById(_tomJansensId)).Returns(_dummyContext.UserTomJansens);
            _mockUserRepository.Setup(c => c.GetByEmail(_tomJansensEmail)).Returns(_dummyContext.UserTomJansens);
            _mockUserRepository.Setup(c => c.GetByFormula(_tomJansensFormula)).Returns(_dummyContext.UsersFormula1);
            _mockUserRepository.Setup(c => c.GetByPartofName(_partOfName)).Returns(_dummyContext.UsersByPartOfName);

            _mockSessionRepository.Setup(c => c.GetAll()).Returns(_dummyContext.Sessions);
            _mockSessionRepository.Setup(c => c.GetById(_session1Id)).Returns(_dummyContext.Session1);

            _mockFormulaRepository.Setup(c => c.GetAll()).Returns(_dummyContext._formulas);
            _mockFormulaRepository.Setup(c => c.GetByTrainingDay);
            //_mockTrainingDayRepository;
            //_nonMemberRepository;

        _sessionController = new SessionController(_mockUserRepository.Object, _mockSessionRepository.Object, _mockFormulaRepository.Object, _mockTrainingDayRepository.Object,_nonMemberRepository.Object)
            {
                TempData = new Mock<ITempDataDictionary>().Object
            };
        }
        #endregion

        #region TestCreateHttpGet
        [Fact]
        public void CreateHttpGet_ValidSession_PassesFormulas()
        {
            var result = _sessionController.Create() as ViewResult;
            ViewDataDictionary viewData = result.ViewData;

            SelectList list = new SelectList((IEnumerable)viewData["Formulas"]);
            Assert.False(list == null);
            Assert.IsType<SelectList>(result.ViewData["Formulas"]);
        }
        #endregion

        #region TestCreateHttpPost
        //public void CreateHttpPost_ValidSession_CreatesANewSession()
        //{
        //    var sessionViewModel = new SessionViewModel(_dummyContext.Session1);

        //}
        #endregion







    }
}

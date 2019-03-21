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
using System.Linq;
using Taijitan.Models.ViewModels;
using Xunit;

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
        private Teacher _teacher;
        private TrainingDay _dinsdag;
        private DayOfWeek _dayOfWeekDinsdag;
        private int _trainingsDayId;
        private string _nonMemberFirstName;
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
            _teacher = _dummyContext.Teacher1;
            _dinsdag = _dummyContext.Dinsdag;
            _dayOfWeekDinsdag = DayOfWeek.Tuesday;
            _trainingsDayId = _dinsdag.TrainingDayId;
            _nonMemberFirstName = "Bernard";

            //Setups
            _mockUserRepository.Setup(c => c.GetAll()).Returns(_dummyContext.Users);
            _mockUserRepository.Setup(c => c.GetById(_tomJansensId)).Returns(_dummyContext.UserTomJansens);
            _mockUserRepository.Setup(c => c.GetByEmail(_tomJansensEmail)).Returns(_dummyContext.UserTomJansens);
            _mockUserRepository.Setup(c => c.GetByFormula(_tomJansensFormula)).Returns(_dummyContext.UsersFormula1);
            _mockUserRepository.Setup(c => c.GetByPartofName(_partOfName)).Returns(_dummyContext.UsersByPartOfName);

            _mockSessionRepository.Setup(c => c.GetAll()).Returns(_dummyContext.Sessions);
            _mockSessionRepository.Setup(c => c.GetById(_session1Id)).Returns(_dummyContext.Session1);

            _mockFormulaRepository.Setup(c => c.GetAll()).Returns(_dummyContext.Formulas);
            _mockFormulaRepository.Setup(c => c.GetByTrainingDay(_dinsdag)).Returns(_dummyContext.DinsdagFormule);

            _mockTrainingDayRepository.Setup(c => c.GetAll()).Returns(_dummyContext.TrainingsDays);
            _mockTrainingDayRepository.Setup(c => c.getById(_trainingsDayId)).Returns(_dummyContext.Dinsdag);
            _mockTrainingDayRepository.Setup(c => c.GetByDayOfWeek(_dayOfWeekDinsdag)).Returns(_dummyContext.Dinsdag);

            _nonMemberRepository.Setup(c => c.GetAll()).Returns(_dummyContext.NonMembers);
            _nonMemberRepository.Setup(c => c.GetByFirstName(_nonMemberFirstName)).Returns(_dummyContext.NonMemberBernard);

        _sessionController = new SessionController(_mockUserRepository.Object, _mockSessionRepository.Object, _mockFormulaRepository.Object, _mockTrainingDayRepository.Object,_nonMemberRepository.Object)
            {
                TempData = new Mock<ITempDataDictionary>().Object
            };
        }
        #endregion

        #region TestCreateHttpGet
        [Fact]
        public void CreateHttpGet_PassesAllFormulesToView()
        {
            var result = _sessionController.Create() as ViewResult;
            List<Formula> formulas = (result?.Model as IEnumerable<Formula>)?.ToList();
            Assert.Equal(_dummyContext.Formulas.Count(), formulas.Count);
        }
        #endregion

        #region TestCreateHttpPost
        [Fact]
        public void CreateHttpPost_ValidCreate_CreatesAndPersistsSession()
        {
            var sessionViewModel = new SessionViewModel(_session1);
            _sessionController.Create(sessionViewModel, _teacher);
            Assert.Equal(_session1Id, _session1.SessionId);
            _mockSessionRepository.Verify(s => s.SaveChanges(), Times.Once);
        }

        [Fact]
        public void CreateHttpPost_InvalidCreate_ReturnsViewResult()
        {
            var sessionViewModel = new SessionViewModel();
            _sessionController.ModelState.AddModelError("", "Any error");
            var result = _sessionController.Create(sessionViewModel) as ViewResult;
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void CreateHttptPost_InvalidCreate_ReturnsCorrectView()
        {
            var sessionViewModel = new SessionViewModel();
            _sessionController.ModelState.AddModelError("", "Any error");
            var result = _sessionController.Create(sessionViewModel) as ViewResult;
            Assert.Equal("Register", result.ViewName);
        }
        #endregion

        #region TestRegister
        //[Fact]
        //public void RegisterTest_ReturnsCorrectView()
        //{
        //    var sessionViewModel = new SessionViewModel();
        //    _sessionController.ModelState.AddModelError("", "Any error");
        //    var result = _sessionController.Register(_session1Id) as ViewResult;
        //    Assert.Equal("Register", result.ViewName);
        //}
        #endregion

        #region TestAddToPrestentHttpPost

        #endregion

        #region TestAddUnconfirmedHttpPost

        #endregion

        #region TestAddOtherMember

        #endregion

        #region TestAddNonMemberHttpPost

        #endregion

        #region TestRemoveNonMemberHttpPost

        #endregion

        #region TestGetSessions

        #endregion

        #region TestSummaryPresences

        #endregion
    }
}

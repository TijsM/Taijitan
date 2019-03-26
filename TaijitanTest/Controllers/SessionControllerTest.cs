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
            _mockUserRepository.Setup(c => c.GetByPartofName(_partOfName)).Returns(_dummyContext.UsersByPartOfName);
            _mockUserRepository.Setup(c => c.GetByFormula(_dummyContext.DinDon)).Returns(_dummyContext.Members);
            _mockUserRepository.Setup(c => c.GetByFormula(_dummyContext.DinZat)).Returns(_dummyContext.Members);
            _mockUserRepository.Setup(c => c.GetAllMembers()).Returns(_dummyContext.Members);

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
        public void CreateHttpGet_PassesAllFormulasToView()
        {
            var result = _sessionController.Create() as ViewResult;
            var formulas = result?.ViewData["formulas"] as SelectList;
            Assert.Equal(_dummyContext.Formulas.Count(), formulas.Count());
        }
        [Fact]
        public void CreateHttpGet_ReturnsSessionViewModel()
        {
            var result = _sessionController.Create() as ViewResult;
            Assert.IsType<SessionViewModel>(result?.Model);
        }
        #endregion

        #region TestCreateHttpPost
        [Fact]
        public void CreateHttpPost_ValidCreate_CreatesAndPersistsSession()
        {
            var sessionViewModel = new SessionViewModel(_session1);
            _sessionController.Create(new Session(),sessionViewModel, _teacher);
            Assert.Equal(_session1Id, _session1.SessionId);
            _mockSessionRepository.Verify(s => s.SaveChanges(), Times.Once);
        }
        [Fact]
        public void CreateHttptPost_InvalidModelState_RedirectToCreateActionMethod()
        {
            _sessionController.ModelState.AddModelError("", "Any error");
            var result = _sessionController.Create(new Session(),new SessionViewModel()) as RedirectToActionResult;
            Assert.Equal("Create", result?.ActionName);
        }
        #endregion

        #region TestRegister
        [Fact]
        public void RegisterTest_PassesSessionviewModelToView()
        {
            var sessionViewModel = new SessionViewModel();
            _sessionController.ModelState.AddModelError("", "Any error");
            var result = _sessionController.Register(_dummyContext.Session1, _dummyContext.Teacher1) as ViewResult;
            Assert.IsType<SessionViewModel>(result?.Model);
        }
        #endregion

        #region TestAddToPresentHttpPost
        [Fact]
        public void AddToPresent_NonExistingUser_ReturnsNotFound()
        {
            Assert.IsType<NotFoundResult>(_sessionController.AddToPresent(1,1, null));
        }
        [Fact]
        public void AddToPresent_NonExistingSessionId_ReturnsNotFound()
        {
            Assert.IsType<NotFoundResult>(_sessionController.AddToPresent(99, 1,_dummyContext.Teacher1));
        }
        [Fact]
        public void AddToPresent_NonExistingMember_ReturnsNotFound()
        {
            Assert.IsType<NotFoundResult>(_sessionController.AddToPresent(_session1.SessionId,99, _dummyContext.Teacher1));
        }
        [Fact]
        public void AddToPresent_ValidData_RedirectsToActionRegister()
        {
            var result = _sessionController.AddToPresent(_session1.SessionId,_tomJansensId, _dummyContext.Teacher1) as RedirectToActionResult;
            Assert.Equal("Register", result?.ActionName);
        }
        #endregion

        #region TestAddUnconfirmedHttpPost
        [Fact]
        public void AddToUnconfirmed_NonExistingUser_ReturnsNotFound()
        {
            Assert.IsType<NotFoundResult>(_sessionController.AddToUnconfirmed(1, 1, null));
        }
        [Fact]
        public void AddToUnconfirmed_NonExistingSessionId_ReturnsNotFound()
        {
            Assert.IsType<NotFoundResult>(_sessionController.AddToUnconfirmed(99, 1, _dummyContext.Teacher1));
        }
        [Fact]
        public void AddToPresent_ValidData_PassesSessionIdToView()
        {
            var result = _sessionController.AddToPresent(_session1.SessionId, _tomJansensId, _dummyContext.Teacher1) as RedirectToActionResult;
            Assert.Equal(_session1.SessionId, result?.RouteValues["id"]);
        }
        [Fact]
        public void AddToUnconfirmed_ValidData_RedirectsToActionRegister()
        {
            var result = _sessionController.AddToUnconfirmed(_session1.SessionId, _tomJansensId, _dummyContext.Teacher1) as RedirectToActionResult;
            Assert.Equal("Register", result?.ActionName);
        }
        [Fact]
        public void AddToUnconfirmed_ValidData_PassesSessionIdToView()
        {
            var result = _sessionController.AddToUnconfirmed(_session1.SessionId, _tomJansensId, _dummyContext.Teacher1) as RedirectToActionResult;
            Assert.Equal(_session1.SessionId, result?.RouteValues["id"]);
        }
        #endregion

        #region TestAddOtherMember
        [Fact]
        public void AddToOtherMember_NonExistingUser_ReturnsNotFound()
        {
            Assert.IsType<NotFoundResult>(_sessionController.AddOtherMember(99,null));
        }
        [Fact]
        public void AddToOtherMember_NonExistingSession_ReturnsNotFound()
        {
            Assert.IsType<NotFoundResult>(_sessionController.AddOtherMember(99,_tomJansens));
        }
        [Fact]
        public void AddToOtherMember_ValidData_PassesOtherMembersToView()
        {
            var result = _sessionController.AddOtherMember(_session1Id, _teacher,"de") as ViewResult;
            var othermembers = result?.ViewData["otherMembers"] as List<Member>;
            Assert.Empty(othermembers);
        }
        #endregion
        
        #region TestAddNonMemberHttpPost
        [Fact]
        public void AddNonMember_ValidData_PassesSessionIdToView()
        {
            var result = _sessionController.AddNonMember("Jarne","Deschacht","Jarne.deschacht@student.hogent.be",_session1Id) as RedirectToActionResult;
            Assert.Equal(_session1.SessionId, result?.RouteValues["id"]);
        }
        [Fact]
        public void AddNonMember_ValidData_RedirectToRegister()
        {
            var result = _sessionController.AddNonMember("Jarne", "Deschacht", "Jarne.deschacht@student.hogent.be", _session1Id) as RedirectToActionResult;
            Assert.Equal("Register", result?.ActionName);
        }
        [Fact]
        public void AddNonMember_NonExistingSession_ReturnsNotFound()
        {
            Assert.IsType<NotFoundResult>(_sessionController.AddNonMember("Jarne", "Deschacht", "Jarne.deschacht@student.hogent.be", 99));
        }
        #endregion

        #region TestRemoveNonMemberHttpPost
        [Fact]
        public void RemoveNonMember_NonExistingSession_ReturnsNotFound()
        {
            Assert.IsType<NotFoundResult>(_sessionController.RemoveNonMember("Jarne",99));
        }
        [Fact]
        public void RemoveNonMember_NonExistingNonMember_ReturnsNotFound()
        {
            Assert.IsType<NotFoundResult>(_sessionController.RemoveNonMember("Jarne",_session1Id));
        }
        [Fact]
        public void RemoveNonMember_ValidData_RedirectsToRegister()
        {
            _sessionController.AddNonMember("Jarne", "Deschacht", "Jarne.deschacht@student.hogent.be", _session1Id);
            var result = _sessionController.RemoveNonMember("Jarne",_session1Id) as RedirectToActionResult;
            Assert.Equal("Register", result?.ActionName);
        }
        [Fact]
        public void RemoveNonMember_ValidData_PassesSessionIdToView()
        {
            _sessionController.AddNonMember("Jarne", "Deschacht", "Jarne.deschacht@student.hogent.be", _session1Id);
            var result = _sessionController.RemoveNonMember("Jarne", _session1Id) as RedirectToActionResult;
            Assert.Equal(_session1.SessionId, result?.RouteValues["id"]);
        }
        #endregion

        #region TestGetSessions
        [Fact]
        public void GetSessions_PassesSessionsToView()
        {
            Assert.Equal(_dummyContext.Sessions, (_sessionController.GetSessions() as ViewResult)?.ViewData["sessions"]);
        }
        #endregion

        #region TestSummaryPresences
        [Fact]
        public void SummaryPresences_NonExistingSession_ReturnsNotFound()
        {
            Assert.IsType<NotFoundResult>(_sessionController.SummaryPresences(99));
        }
        [Fact]
        public void SummaryPresences_PassesSessionsToView()
        {
            _sessionController.AddNonMember("Jarne", "Deschacht", "Jarne.deschacht@student.hogent.be", _session1Id);
            var nonmembers = (_sessionController.SummaryPresences(_session1Id) as ViewResult)?.ViewData["NonMembers"] as List<NonMember>;
            Assert.Single(nonmembers);
        }
        #endregion
    }
}

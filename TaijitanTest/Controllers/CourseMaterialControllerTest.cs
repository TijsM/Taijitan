using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taijitan.Controllers;
using Taijitan.Models.Domain;
using Taijitan.Models.ViewModels;
using TaijitanTest.Data;
using Xunit;

namespace TaijitanTest.Controllers
{
    public class CourseMaterialControllerTest
    {
        #region properties
        private readonly DummyApplicationDbContext _dummyApplicationContext;
        private readonly CourseMaterialController _courseMaterialController;
        private readonly Mock<ISessionRepository> _mockSessionRepository;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<ICourseMaterialRepository> _mockCourseMaterialRepository;
        private readonly Mock<ICommentRepository> _mockCommentRepository;
        private Member _tomJansens;
        private int _tomJansensId;

        private int _idOfStartedSession = 0;
        private int _idOfCourseMaterial = 1;
        private int _idComment = 1;
        private int _nonExistingSessionId = 999;
        #endregion

        #region Constructor
        public CourseMaterialControllerTest()
        {
            _dummyApplicationContext = new DummyApplicationDbContext();
            _mockSessionRepository = new Mock<ISessionRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockCourseMaterialRepository = new Mock<ICourseMaterialRepository>();
            _mockCommentRepository = new Mock<ICommentRepository>();
            _tomJansensId = _dummyApplicationContext.UserTomJansens.UserId;
            _tomJansens = _dummyApplicationContext.UserTomJansens;

            //setups
            _mockSessionRepository.Setup(c => c.GetById(_idOfStartedSession)).Returns(_dummyApplicationContext.Session1);

            _mockCourseMaterialRepository.Setup(c => c.GetByRank(Rank.Kyu6)).Returns(_dummyApplicationContext.CourseMaterials);
            _mockCourseMaterialRepository.Setup(c => c.GetById(_idOfCourseMaterial)).Returns(_dummyApplicationContext.CourseMaterials.First());

            _mockUserRepository.Setup(c => c.GetById(_tomJansensId)).Returns(_tomJansens);

            _mockCommentRepository.Setup(c => c.GetById(_idComment)).Returns(_dummyApplicationContext.CommentWithId1);

            // the controller
            _courseMaterialController = new CourseMaterialController(_mockSessionRepository.Object, _mockUserRepository.Object, _mockCourseMaterialRepository.Object, _mockCommentRepository.Object)
            {
                TempData = new Mock<ITempDataDictionary>().Object,
            };
        }
        #endregion

        #region TestConfirmHttpGet
        [Fact]
        public void Confirm_nonExcistingSession_returnsNewSession()
        {
            var result = _courseMaterialController.Confirm(_dummyApplicationContext.Session1) as ViewResult;
            Assert.Equal("", result?.ViewData["partialView"]);
        }
        [Fact]
        public void Confirm_existingSession_returnsTheSession()
        {
            var result = _courseMaterialController.Confirm(_dummyApplicationContext.Session1) as ViewResult;
            var courseMaterialViewModel = result?.Model as CourseMaterialViewModel;
            Assert.Equal(_dummyApplicationContext.Session1, courseMaterialViewModel.Session);
        }
        [Fact]
        public void Confirm_existingSession_returnsCourseMaterialMiewModel()
        {
            var result = _courseMaterialController.Confirm(new Session()) as ViewResult;
            Assert.IsType<CourseMaterialViewModel>(result?.Model);
        }
        [Fact]
        public void Confirm_NonExistingSession_returnsNotFound()
        {
            Session nonExistingSession = new Session()
            {
                SessionId = _nonExistingSessionId
            };
            Assert.IsType<NotFoundResult>(_courseMaterialController.Confirm(nonExistingSession));
        }
        #endregion

        #region TestSelectMember
        [Fact]
        public void SelectMember_ValidData_ReturnsCourseMaterialViewModel()
        {
            var result = _courseMaterialController.SelectMember(_idOfStartedSession,_tomJansensId) as ViewResult;
            Assert.IsType<CourseMaterialViewModel>(result?.Model);
        }
        [Fact]
        public void SelectMember_NoValidSessionId_ReturnsCourseMaterialViewModel()
        {
            Assert.IsType<NotFoundResult>(_courseMaterialController.SelectMember(_nonExistingSessionId, _tomJansensId));
        }
        [Fact]
        public void SelectMember_NoValidUserId_ReturnsCourseMaterialViewModel()
        {
            Assert.IsType<NotFoundResult>(_courseMaterialController.SelectMember(_nonExistingSessionId,999));
        }
        [Fact]
        public void SelectRank_ValidData_PassesCorrectSelectedMember()
        {
            var result = _courseMaterialController.SelectRank(_idOfStartedSession, Rank.Kyu6, _tomJansensId) as ViewResult;
            Assert.Equal(_tomJansens, (result?.Model as CourseMaterialViewModel).SelectedMember);
        }
        #endregion

        #region TestSelectRank
        [Fact]
        public void SelectRank_ValidData_ReturnsCourseMaterialViewModel()
        {
            var result = _courseMaterialController.SelectRank(_idOfStartedSession,Rank.Kyu6, _tomJansensId) as ViewResult;
            Assert.IsType<CourseMaterialViewModel>(result?.Model);
        }
        [Fact]
        public void SelectRank_NoValidSessionId_ReturnsCourseMaterialViewModel()
        {
            Assert.IsType<NotFoundResult>(_courseMaterialController.SelectRank(_nonExistingSessionId,Rank.Kyu6, _tomJansensId));
        }
        [Fact]
        public void SelectRank_NoValidUserId_ReturnsCourseMaterialViewModel()
        {
            Assert.IsType<NotFoundResult>(_courseMaterialController.SelectRank(_nonExistingSessionId,Rank.Kyu6, 999));
        }
        [Fact]
        public void SelectRank_ValidData_PassesCorrectCourseMaterialsToView()
        {
            var result = _courseMaterialController.SelectRank(_idOfStartedSession, Rank.Kyu6, _tomJansensId) as ViewResult;
            Assert.Equal(_dummyApplicationContext.CourseMaterials, (result?.Model as CourseMaterialViewModel).CourseMaterials);
        }
        #endregion

        #region TestSelectCourse
        [Fact]
        public void SelectCourse_ValidData_ReturnsCourseMaterialViewModel()
        {
            var result = _courseMaterialController.SelectCourse(_idOfStartedSession, Rank.Kyu6, _tomJansensId,_idOfCourseMaterial, new CourseMaterialViewModel()) as ViewResult;
            Assert.IsType<CourseMaterialViewModel>(result?.Model);
        }
        [Fact]
        public void SelectCourse_NoValidSessionId_ReturnsCourseMaterialViewModel()
        {
            Assert.IsType<NotFoundResult>(_courseMaterialController.SelectCourse(_nonExistingSessionId, Rank.Kyu6, _tomJansensId,_idOfCourseMaterial, new CourseMaterialViewModel()));
        }
        [Fact]
        public void SelectCourse_NoValidUserId_ReturnsCourseMaterialViewModel()
        {
            Assert.IsType<NotFoundResult>(_courseMaterialController.SelectCourse(_nonExistingSessionId, Rank.Kyu6, 999,_idOfCourseMaterial, new CourseMaterialViewModel()));
        }
        [Fact]
        public void SelectCourse_ValidData_PassesCorrectCourseMaterialsToView()
        {
            var result = _courseMaterialController.SelectCourse(_idOfStartedSession, Rank.Kyu6, _tomJansensId,_idOfCourseMaterial, new CourseMaterialViewModel()) as ViewResult;
            Assert.Equal(_dummyApplicationContext.CourseMaterials.First(), (result?.Model as CourseMaterialViewModel).SelectedCourseMaterial);
        }
        #endregion

        #region TestAddComment
        [Fact]
        public void AddComment_InValidCourseViewModel_ReturnsView()
        {
            Assert.IsType<ViewResult>(_courseMaterialController.AddComment("comment",null,_dummyApplicationContext.Comments.ToList()));
        }
        [Fact]
        public void AddComment_InValidData_ReturnsNotFound()
        {
            Assert.IsType<NotFoundResult>(_courseMaterialController.AddComment("comment",new CourseMaterialViewModel() {
                SelectedCourseMaterial = _dummyApplicationContext.CourseMaterials.Skip(1).First(),SelectedMember = _tomJansens},
                _dummyApplicationContext.Comments.ToList()));
        }
        [Fact]
        public void AddComment_ValidData_RedirectsToActionSelectCourseAndPassesCorrectDataToView()
        {
            var result = _courseMaterialController.AddComment("comment", new CourseMaterialViewModel()
            {
                SelectedCourseMaterial
                = _dummyApplicationContext.CourseMaterials.First(),
                SelectedMember = _tomJansens,
                Session = _dummyApplicationContext.Session1,
                SelectedRank = Rank.Dan1
            }, _dummyApplicationContext.Comments.ToList()) as RedirectToActionResult;
            Assert.Equal("SelectCourse", result?.ActionName);
            Assert.Equal(_dummyApplicationContext.Session1.SessionId, result?.RouteValues["sessionId"]);
            Assert.Equal(Rank.Dan1, result?.RouteValues["rank"]);
            Assert.Equal(_tomJansensId, result?.RouteValues["selectedUserId"]);
            Assert.Equal(_dummyApplicationContext.CourseMaterials.First().MaterialId, result?.RouteValues["matId"]);

        }
        [Fact]
        public void AddComment_InvalidComments_RedirectsToActionSelectCourseAndPassesCorrectDataToView()
        {
            var result = _courseMaterialController.AddComment("comment", new CourseMaterialViewModel()
            {
                SelectedCourseMaterial
                = _dummyApplicationContext.CourseMaterials.First(),
                SelectedMember = _tomJansens,
                Session = _dummyApplicationContext.Session1,
                SelectedRank = Rank.Dan1
            },null) as RedirectToActionResult;
            Assert.Equal("SelectCourse", result?.ActionName);
            Assert.Equal(_dummyApplicationContext.Session1.SessionId, result?.RouteValues["sessionId"]);
            Assert.Equal(Rank.Dan1, result?.RouteValues["rank"]);
            Assert.Equal(_tomJansensId, result?.RouteValues["selectedUserId"]);
            Assert.Equal(_dummyApplicationContext.CourseMaterials.First().MaterialId, result?.RouteValues["matId"]);
        }
        #endregion

        #region TestViewComments
        [Fact]
        public void ViewCommentsHttpGet()
        {
            var result = _courseMaterialController.ViewComments() as ViewResult;
            Assert.IsType<ViewResult>(result);
            Assert.True((bool)result?.ViewData["IsEmpty"]);
        }
        #endregion

        #region TestSelectComment
        [Fact]
        public void SelectComment_ValidComment_ReturnsCorrectView()
        {
            var result = _courseMaterialController.SelectComment(_idComment) as ViewResult;
            Assert.IsType<ViewResult>(result);
            Assert.False((bool)result?.ViewData["IsEmpty"]);
            Assert.Equal(_dummyApplicationContext.CommentWithId1,result?.ViewData["Comment"]);
        }
        [Fact]
        public void SelectComment_InValidComment_ReturnsCorrectView()
        {
            var result = _courseMaterialController.SelectComment(999) as ViewResult;
            Assert.IsType<ViewResult>(result);
            Assert.True((bool)result?.ViewData["IsEmpty"]);
        }
        #endregion

        #region TestRemoveComment
        [Fact]
        public void RemoveComment_ValidComment_RemovesComment()
        {
            var result = _courseMaterialController.RemoveComment(_idComment) as ViewResult;
            Assert.IsType<ViewResult>(result);
            Assert.True((bool)result?.ViewData["IsEmpty"]);
        }
        [Fact]
        public void RemoveComment_InValidComment_ReturnsNotFound()
        {
            Assert.IsType<NotFoundResult>(_courseMaterialController.RemoveComment(999));
        }
        #endregion


    }

}

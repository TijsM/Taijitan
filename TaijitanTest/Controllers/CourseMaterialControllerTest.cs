using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Collections.Generic;
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

        private int _idOfStartedSession = 1;
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

            //setups
            _mockSessionRepository.Setup(c => c.GetById(_idOfStartedSession)).Returns(_dummyApplicationContext.Session1);

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
            var result = _courseMaterialController.Confirm(_idOfStartedSession) as ViewResult;
            Assert.Equal("", result?.ViewData["partialView"]);
        }
        [Fact]
        public void Confirm_existingSession_returnsTheSession()
        {
            var result = _courseMaterialController.Confirm(_idOfStartedSession) as ViewResult;
            var courseMaterialViewModel = result?.Model as CourseMaterialViewModel;
            Assert.Equal(_dummyApplicationContext.Session1, courseMaterialViewModel.Session);
        }
        [Fact]
        public void Confirm_existingSession_returnsCourseMaterialMiewModel()
        {
            var result = _courseMaterialController.Confirm(_idOfStartedSession) as ViewResult;
            Assert.IsType<CourseMaterialViewModel>(result?.Model);
        }
        [Fact]
        public void Confirm_NonExistingSession_returnsNotFound()
        {
            Assert.IsType<NotFoundResult>(_courseMaterialController.Confirm(_nonExistingSessionId));
        }
        #endregion
    }

}

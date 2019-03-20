using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Taijitan.Controllers;
using Taijitan.Models.Domain;
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

        private int idOfSession = 1;
        private int idOfStartedSession = 2;
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
            _mockSessionRepository.Setup(c => c.GetById(idOfSession)).Returns(_dummyApplicationContext.Session1);
            //_mockSessionRepository.Setup(c => c.GetById(idOfStartedSession)).Returns(_dummyApplicationContext.Session2);

            // the controller
            _courseMaterialController = new CourseMaterialController(_mockSessionRepository.Object, _mockUserRepository.Object, _mockCourseMaterialRepository.Object, _mockCommentRepository.Object)
            {
                TempData = new Mock<ITempDataDictionary>().Object
            };
        }
        #endregion

        #region TestConfirmHttpGet
        [Fact]
        public void Confirm_nonExcistingSession_returnsNewSession()
        {
            var currentSession = _mockSessionRepository.Object.GetById(idOfSession);
            var result = _courseMaterialController.Confirm(idOfSession) as ViewResult;
            ViewDataDictionary viewData = result.ViewData;

            Assert.Equal("", viewData["partialView"]);
        }

        [Fact]
        public void Confirm_existingSessiong_returnsTheSession()
        {
            var currentSession = _mockSessionRepository.Object.GetById(idOfStartedSession);
            var result = _courseMaterialController.Confirm(idOfStartedSession) as ViewResult;
            ViewDataDictionary viewData = result.ViewData;

           
        }
        #endregion
    }

}

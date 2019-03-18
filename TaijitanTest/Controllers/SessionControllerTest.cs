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
        private readonly SessionController _sessionController;
        private readonly DummyApplicationDbContext _dummyContext;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<ISessionRepository> _mockSessionRepository;
        private readonly Mock<IFormulaRepository> _mockFormulaRepository;
        private readonly Mock<ITrainingDayRepository> _mockTrainingDayRepository;
        private readonly Mock<INonMemberRepository> _nonMemberRepository;
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

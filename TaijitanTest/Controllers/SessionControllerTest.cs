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
    public class SessionControllerTest
    {

        #region Fields
        private readonly SessionController _sessionController;
        private readonly DummyApplicationDbContext _dummyContext;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<ISessionRepository> _mockSessionRepository;
        private readonly Mock<IFormulaRepository> _mockFormulaRepository;
        private readonly Mock<ITrainingDayRepository> _mockTrainingDayRepository;

        #endregion


        #region Constructor
        public SessionControllerTest()
        {
            _dummyContext = new DummyApplicationDbContext();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockSessionRepository = new Mock<ISessionRepository>();
            _mockFormulaRepository = new Mock<IFormulaRepository>();

            _sessionController = new SessionController(_mockUserRepository.Object, _mockSessionRepository.Object, _mockFormulaRepository.Object, _mockTrainingDayRepository.Object);
        }
        #endregion

        #region TestCreateHttpGent
        [Fact]
        public void CreateHttpGet_ValidSession_PassesFormulas()
        {
            //var result = _sessionController.Create();

        }
        #endregion







    }
}

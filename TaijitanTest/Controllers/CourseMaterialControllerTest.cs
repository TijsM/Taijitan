using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Taijitan.Models.Domain;

namespace TaijitanTest.Controllers
{
    class CourseMaterialControllerTest
    {
        
        private readonly Mock<ISessionRepository> _mockSessionRepository;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<ICourseMaterialRepository> _mockCourseMaterialRepository;
        private readonly Mock<ICommentRepository> _MockCommentRepository;

    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taijitan.Controllers;
using Taijitan.Models.Domain;
using TaijitanTest.Data;
using Xunit;

namespace TaijitanTest.Controllers
{
    public class HomeControllerTest
    {
        #region Fields
        private readonly DummyApplicationDbContext _dummyContext;
        private HomeController _homeController;
        private Mock<ICommentRepository> _mockCommentRepository;
        private const int _commentId = 1;
        #endregion

        #region contructor
        public HomeControllerTest()
        {
            _dummyContext = new DummyApplicationDbContext();
            _mockCommentRepository = new Mock<ICommentRepository>();

            _mockCommentRepository.Setup(m => m.GetById(_commentId)).Returns(_dummyContext.CommentWithId1);
            _mockCommentRepository.Setup(m => m.GetById(2)).Returns(_dummyContext.Comments.Skip(1).First);
            _mockCommentRepository.Setup(m => m.GetById(3)).Returns(_dummyContext.Comments.Skip(2).First);
            _mockCommentRepository.Setup(m => m.GetById(4)).Returns(_dummyContext.Comments.Skip(3).First);
            _mockCommentRepository.Setup(m => m.GetById(5)).Returns(_dummyContext.Comments.Skip(4).First);
            _mockCommentRepository.Setup(m => m.GetById(6)).Returns(_dummyContext.Comments.Skip(5).First);

            _homeController = new HomeController(_mockCommentRepository.Object)
            {
                TempData = new Mock<ITempDataDictionary>().Object
            };
        }
        #endregion

        #region TestIndexMethod
        [Fact]
        public void Index_ExistingUserExistingSession_PassesSessionToView()
        {
            var result = _homeController.Index(_dummyContext.Comments.ToList(), _dummyContext.UserTomJansens, _dummyContext.Session1) as ViewResult;
            Assert.Equal(_dummyContext.Session1,result?.ViewData["session"]);
        }
        [Fact]
        public void Index_UserIsAdmin_ReturnsView()
        {
            Assert.IsType<ViewResult>(_homeController.Index(_dummyContext.Comments.ToList(), _dummyContext.Alain, _dummyContext.Session1));
        }
        #endregion

        #region HttpPost_MarkRead
        [Fact]
        public void MarkReadHttpPost_NonValidList_RedirectsToIndex()
        {
            var result = _homeController.MarkRead(null) as RedirectToActionResult;
            Assert.Equal("Index", result?.ActionName);
        }
        [Fact]
        public void MarkReadHttpPost_ValidList_RedirectsToIndex()
        {
            var result = _homeController.MarkRead(_dummyContext.Comments.ToList()) as RedirectToActionResult;
            Assert.Equal("Index", result?.ActionName);
        }
        #endregion
    }
}

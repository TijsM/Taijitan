using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taijitan.Models.Domain;
using TaijitanTest.Data;
using Xunit;

namespace TaijitanTest.Models
{
    public class CommentTest
    {
        private readonly DummyApplicationDbContext _dummyApplicationContext;
        public CommentTest()
        {
            _dummyApplicationContext = new DummyApplicationDbContext();
        }
        [Fact]
        public void CreateComment_ValidData_CreatesComment()
        {
            Comment c = new Comment("content", _dummyApplicationContext.CourseMaterial, _dummyApplicationContext.Members.First()) { CommentId = 1};
            Assert.Equal("content", c.Content);
            Assert.Equal(1, c.CommentId);
            Assert.Equal(_dummyApplicationContext.Members.First(), c.Member);
            Assert.Equal(_dummyApplicationContext.CourseMaterial, c.Course);
            Assert.False(c.IsRead);
            Assert.Equal(DateTime.Now, c.DateCreated, new TimeSpan(0, 0, 1));
        }

    }
}

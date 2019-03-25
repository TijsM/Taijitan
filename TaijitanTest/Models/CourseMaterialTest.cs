using System;
using System.Collections.Generic;
using System.Text;
using Taijitan.Models.Domain;
using TaijitanTest.Data;
using Xunit;

namespace TaijitanTest.Models
{
    public class CourseMaterialTest
    {
        private readonly DummyApplicationDbContext _dummyApplicationContext;
        public CourseMaterialTest()
        {
            _dummyApplicationContext = new DummyApplicationDbContext();
        }

        [Fact]
        public void CreateCourseMaterial_ValidData_CreatesCourseMaterial()
        {
            CourseMaterial cm = new CourseMaterial(Rank.Dan1, "testURL", "testtest", _dummyApplicationContext.Images, "test") { MaterialId = 1};
            Assert.Empty(cm.Comments);
            Assert.Equal(1, cm.MaterialId);
            Assert.Equal("testURL", cm.YoutubeURL);
            Assert.Equal("testtest", cm.FullDescription);
            Assert.Equal(_dummyApplicationContext.Images, cm.Images);
            Assert.Equal("test", cm.Title);
            Assert.Equal(Rank.Dan1, cm.Rank);

        }
        [Fact]
        public void CreateCourseMaterial_DefaultCTOR_CreatesCourseMaterial()
        {
            CourseMaterial cm = new CourseMaterial();
            Assert.Empty(cm.Comments);
            Assert.Empty(cm.Images);

        }

    }
}

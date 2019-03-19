using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public int CourseId { get; set; }
        public int MemberId { get; set; }
        public DateTime DateCreated { get; set; }

        public Comment()
        {
            DateCreated = DateTime.Now;
        }

        public Comment(string content, CourseMaterial course, Member member)
        {
            Content = content;
            CourseId = course.MaterialId;
            MemberId = member.UserId;
            DateCreated = DateTime.Now;
        }
    }
}

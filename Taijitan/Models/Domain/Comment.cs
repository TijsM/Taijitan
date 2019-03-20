using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Comment
    {
        public int CommentId { get; set; }
        [JsonProperty]
        public string Content { get; set; }
        [JsonProperty]
        public CourseMaterial Course { get; set; }
        [JsonProperty]
        public Member Member { get; set; }
        public DateTime DateCreated { get; set; }
        [JsonProperty]
        public bool IsRead { get; set; }

        public Comment()
        {
            DateCreated = DateTime.Now;
            IsRead = false;
        }

        public Comment(string content, CourseMaterial course, Member member)
        {
            Content = content;
            Course = course;
            Member = member;
            DateCreated = DateTime.Now;
            IsRead = false;
        }
    }
}

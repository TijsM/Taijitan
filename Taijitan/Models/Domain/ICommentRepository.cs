using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public interface ICommentRepository
    {
        IEnumerable<Comment> GetAll();
        IEnumerable<Comment> GetByMember(Member member);
        IEnumerable<Comment> GetByCourseMaterial(CourseMaterial course);
        Comment GetById(int id);
        void Add(Comment comment);
        void Delete(Comment comment);
        void SaveChanges();
    }
}

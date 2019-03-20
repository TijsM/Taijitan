using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Data.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Comment> _comments;
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
            _comments = context.Comments;
        }
        public void Add(Comment comment)
        {
            _comments.Add(comment);
        }

        public void Delete(Comment comment)
        {
            _comments.Remove(comment);
        }

        public IEnumerable<Comment> GetAll()
        {
            return _comments.Include(c => c.Member).Include(c => c.Course).ToList();
        }

        public IEnumerable<Comment> GetByCourseMaterial(CourseMaterial course)
        {
            return _comments.Where(c => c.Course.Equals(course)).ToList();
        }

        public Comment GetById(int id)
        {
            return _comments.Where(c => c.CommentId == id).FirstOrDefault();
        }

        public IEnumerable<Comment> GetByMember(Member member)
        {
            return _comments.Where(c => c.Member.Equals(member)).ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

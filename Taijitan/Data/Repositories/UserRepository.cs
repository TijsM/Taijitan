using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<User> _users;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
            _users = context.Users_Domain;
        }


        public void Add(User user)
        {
            _users.Add(user);
        }

        public void Delete(User user)
        {
            _users.Remove(user);
        }

        public IEnumerable<User> GetAll()
        {
            return _users
                .Include(m => m.City)
                .AsNoTracking()
                .ToList();
        }

        public User GetByEmail(string email)
        {
            return _users
                .Include(m => m.City)
                .SingleOrDefault(m => m.Email == email);
        }

        public User GetById(int id)
        {
            return _users
                .Include(m =>m.City)
                .SingleOrDefault(m => m.UserId == id);
        }

        public IEnumerable<User> GetByPartofName(string searchTerm)
        {
            return _users
                .Where(u => u.Name.Contains(searchTerm))
                .Include(m => m.City)
                .AsNoTracking()
                .ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

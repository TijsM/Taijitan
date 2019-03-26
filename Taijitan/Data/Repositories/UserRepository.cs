using Microsoft.AspNetCore.Identity;
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
        private readonly IEnumerable<Member> _members;
        private readonly UserManager<IdentityUser> _userManager;

        public UserRepository(ApplicationDbContext context,UserManager<IdentityUser> userManager)
        {
            _context = context;
            _users = context.Users_Domain;
            _members = _users.OfType<Member>().Include(u => u.Formula).Where(item => item.GetType() == typeof(Member)).ToList();
            _userManager = userManager;
        }

        public IEnumerable<Member> GetAllMembers()
        {
            return _members;
        }

        public void Add(User user)
        {
            _users.Add(user);
        }

        public void Delete(User user)
        {
            _users.Remove(user);
            RemoveUser(user).Wait();
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

        public IEnumerable<Member> GetByFormula(Formula formula)
        {
            return _members.Where(m => m.Formula.FormulaId == formula.FormulaId).ToList();
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
        private async Task RemoveUser(User user)
        {
            var usr = await _userManager.FindByEmailAsync(user.Email);
            await _userManager.DeleteAsync(usr);
        }
    }
}

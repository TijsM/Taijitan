using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        IEnumerable<User> GetByPartofName(string searchTerm);
        IEnumerable<Member> GetByFormula(Formula formula);
        User GetById(int id);
        User GetByEmail(string email);
        void Add(User user);
        void Delete(User user);
        void SaveChanges();
        IEnumerable<Member> GetAllMembers();
    }
}

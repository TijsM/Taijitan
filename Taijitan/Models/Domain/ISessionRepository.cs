using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public interface ISessionRepository
    {
        IEnumerable<Session> GetAll();
        Session GetById(int id);
        void Add(Session session);
        void Delete(Session session);
        void SaveChanges();
        IEnumerable<Session> GetByUser(int id);
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Data.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly ApplicationDbContext _context;
        private  readonly DbSet<Session> _sessions;

        public SessionRepository(ApplicationDbContext context)
        {
            _context = context;
            _sessions = context.Sessions;
        }

        public void Add(Session session)
        {
            _sessions.Add(session);
        }

        public void Delete(Session session)
        {
            _sessions.Remove(session);
        }

        public IEnumerable<Session> GetAll()
        {
            return _sessions.Include(s => s.Members).Include(s => s.MembersPresent).AsNoTracking().ToList();
        }

        public Session GetById(int id)
        {
            return _sessions.Include(s => s.Members).Include(s => s.MembersPresent).Include(s => s.TrainingDay).SingleOrDefault(s => s.SessionId == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

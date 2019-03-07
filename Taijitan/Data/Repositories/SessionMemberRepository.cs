using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Data.Repositories
{
    public class SessionMemberRepository : ISessionMemberRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<SessionMember> _sessionMembers;

        public SessionMemberRepository(ApplicationDbContext context)
        {
            _context = context;
            _sessionMembers = context.SessionMembers;
        }

        public void Add(SessionMember sessionMember)
        {
            _sessionMembers.Add(sessionMember);
        }

        public void Delete(SessionMember sessionMember)
        {
            _sessionMembers.Remove(sessionMember);
        }

        public IEnumerable<SessionMember> GetAll()
        {
            return _sessionMembers.Include(sm => sm.Session).AsNoTracking().ToList();
        }
        public SessionMember GetById(int sessionId, int memberId)
        {
            return _sessionMembers.SingleOrDefault(s => s.MemberId == memberId && s.SessionId == sessionId);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public interface ISessionMemberRepository
    {
        IEnumerable<SessionMember> GetAll();
        SessionMember GetById(int sessionId,int memberId);

        void Add(SessionMember sessionMember);
        void Delete(SessionMember sessionMember);
        void SaveChanges();
    }
}

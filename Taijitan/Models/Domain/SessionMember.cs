using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public class SessionMember
    {
        public int SessionId { get; set; }
        public Session Session { get; set; }
        public int MemberId { get; set; } //= userId
        public Member Member { get; set; }

        public SessionMember(int sessionId, Session session, int memberId, Member member)
        {
            SessionId = sessionId;
            Session = session;
            MemberId = memberId;
            Member = member;
        }

        public SessionMember()
        {

        }
    }
}

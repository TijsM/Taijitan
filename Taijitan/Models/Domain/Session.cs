using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public class Session
    {
        public  int SessionId { get; set; }
        public IEnumerable<Member> Members { get; set; }
        public IEnumerable<Member> MembersPresent { get; set; }
        public DateTime Date { get; set; }
        public Formula Formula { get; set; }
        public Teacher Teacher { get; set; }

        public Session(Formula formula,Teacher teacher,IEnumerable<Member> members)
        {
            MembersPresent = new List<Member>();
            Members = members;
            Date = DateTime.Now;
            Formula = formula;
            Teacher = teacher;
        }
        public Session()
        {
            MembersPresent = new List<Member>();
        }
        public void AddToMembersPresent(Member mb)
        {
            List<Member> _hulpPresent = MembersPresent.ToList();
            _hulpPresent.Add(mb);
            MembersPresent = _hulpPresent.AsReadOnly();

            List<Member> _hulp = Members.ToList();
            _hulp.Remove(mb);
            Members = _hulp;
        }

        public void AddToMembers(Member mb)
        {
            List<Member> _hulp = Members.ToList();
            _hulp.Add(mb);
            Members = _hulp.AsReadOnly();

            List<Member> _hulpPresent = MembersPresent.ToList();
            _hulpPresent.Remove(mb);
            MembersPresent = _hulpPresent.AsReadOnly();
        }
    }
}

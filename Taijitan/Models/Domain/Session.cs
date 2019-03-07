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
        public IEnumerable<Formula> Formulas { get; set; }
        public Teacher Teacher { get; set; }
        public TrainingDay TrainingDay { get; set; }
        public IList<SessionMember> SessionMembers{ get; set; }

        public Session(List<Formula> formulas,Teacher teacher,IEnumerable<Member> members)
        {
            MembersPresent = new List<Member>();
            Members = members;
            TrainingDay = formulas != null ? formulas.First().TrainingDays.SingleOrDefault(d => d.DayOfWeek.Equals(DateTime.Now.DayOfWeek)) : null;
            Date = DateTime.Now;
            Formulas = formulas.ToList();
            Teacher = teacher;
            SessionMembers = new List<SessionMember>();
        }
        public Session()
        {
            MembersPresent = new List<Member>();
            Formulas = new List<Formula>();
            SessionMembers = new List<SessionMember>();
        }
        public void AddToMembersPresent(Member mb)
        {
            List<Member> _hulpPresent = MembersPresent.ToList();
            _hulpPresent.Add(mb);
            MembersPresent = _hulpPresent;

            List<Member> _hulp = Members.ToList();
            _hulp.Remove(mb);
            Members = _hulp;
        }

        public void AddToMembers(Member mb)
        {
            List<Member> _hulp = Members.ToList();
            _hulp.Add(mb);
            Members = _hulp;

            List<Member> _hulpPresent = MembersPresent.ToList();
            _hulpPresent.Remove(mb);
            MembersPresent = _hulpPresent;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public class Session
    {
        public int SessionId { get; set; }
        public IEnumerable<Member> Members { get; set; }
        public IEnumerable<Member> MembersPresent { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<Formula> Formulas => SessionFormulas.Select(sf => sf.Formula).ToList();
        public Teacher Teacher { get; set; }
        public TrainingDay TrainingDay { get; set; }
        public ICollection<SessionMember> SessionMembers{ get; set; }
        public IEnumerable<NonMember> NonMembers { get; set; }
        public ICollection<SessionFormula> SessionFormulas { get; set; }

        public Session(List<Formula> formulas,Teacher teacher,IEnumerable<Member> members)
        {
            MembersPresent = new List<Member>();
            SessionFormulas = new List<SessionFormula>();
            Members = members;
            TrainingDay = formulas != null ? formulas.First().TrainingDays.SingleOrDefault(d => d.DayOfWeek.Equals(DateTime.Now.DayOfWeek)) : null;
            Date = DateTime.Now;
            Teacher = teacher;
            SessionMembers = new List<SessionMember>();
            NonMembers = new List<NonMember>();
            foreach (Formula f in formulas)
            {
                SessionFormulas.Add(new SessionFormula(SessionId, this, f.FormulaId, f));
            }
        }
        public Session()
        {
            MembersPresent = new List<Member>();
            SessionFormulas = new List<SessionFormula>();
            SessionMembers = new List<SessionMember>();
            NonMembers = new List<NonMember>();
            NonMembers = new List<NonMember>();
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
        public void AddToSessionMembers(List<Member> members)
        {
            List<SessionMember> hulp = SessionMembers.ToList();
            foreach (Member meme in members)
            {
                hulp.Add(new SessionMember(SessionId, this, meme.UserId, meme));
            }
            SessionMembers = hulp;

            
        }

        public void AddNonMember(string firstName, string lastName, string email)
        {
            var nonMember = new NonMember(firstName, lastName, email);
            List<NonMember> hList = NonMembers.ToList();
            hList.Add(nonMember);
            NonMembers = hList;
        }
    }
}

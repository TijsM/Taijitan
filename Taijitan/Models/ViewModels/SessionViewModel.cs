using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Models.ViewModels
{
    public class SessionViewModel
    {
        public IEnumerable<Member> Members { get; set; }
        public IEnumerable<Member> MembersPresent { get; set; }
        public DateTime Date { get; set; }
        public Formula Formula { get; set; }
        public Teacher Teacher { get; set; }

        public void Change(Session s)
        {
            Members = s.Members;
            MembersPresent = Enumerable.Empty<Member>();
            Date = s.Date;
            Formula = s.Formula;
            Teacher = s.Teacher;
        }
        public SessionViewModel()
        {

        }

        public void AddToMembersPresent (Member mb)
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

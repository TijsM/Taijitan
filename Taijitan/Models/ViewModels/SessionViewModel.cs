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
        public Formula SessionFormula { get; set; }
        public Teacher SessionTeacher { get; set; }
        public int SessionId { get; set; }

        public void Change(Session s)
        {
            Members = s.Members;
            MembersPresent = s.MembersPresent;
            Date = s.Date;
            SessionFormula = s.Formula;
            SessionTeacher = s.Teacher;
            SessionId = s.SessionId;
        }
        public SessionViewModel(Session s)
        {
            Members = s.Members;
            MembersPresent = s.MembersPresent;
            Date = s.Date;
            SessionFormula = s.Formula;
            SessionTeacher = s.Teacher;
            SessionId = s.SessionId;
        }
        public SessionViewModel()
        {
            SessionId = 69;
        }       
        
        public void UpdateMembers(IEnumerable<Member> members, IEnumerable<Member> memberspresent)
        {
            Members = members;
            MembersPresent = memberspresent;
        }

    }
}

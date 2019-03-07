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
        public int TrainingDayId { get; set; }
        public TrainingDay TrainingDay { get; set; }
        public Teacher SessionTeacher { get; set; }
        public int SessionId { get; set; }
        public IEnumerable<NonMember> NonMembers { get; set; }

        public void Change(Session s)
        {
            Members = s.Members;
            MembersPresent = s.MembersPresent;
            Date = s.Date;
            TrainingDayId = s.TrainingDay.TrainingDayId;
            SessionTeacher = s.Teacher;
            SessionId = s.SessionId;
            TrainingDay = s.TrainingDay != null ? s.TrainingDay : null;
            NonMembers = s.NonMembers;
        }
        public SessionViewModel(Session s)
        {
            Members = s.Members;
            MembersPresent = s.MembersPresent;
            Date = s.Date;
            TrainingDayId = s.TrainingDay.TrainingDayId;
            SessionTeacher = s.Teacher;
            SessionId = s.SessionId;
            TrainingDay = s.TrainingDay != null ? s.TrainingDay : null;
            NonMembers = s.NonMembers;
        }
        public SessionViewModel()
        {
            Members = new List<Member>();
            MembersPresent = new List<Member>();
            NonMembers = new List<NonMember>();
        }       
        
        public void UpdateMembers(IEnumerable<Member> members, IEnumerable<Member> memberspresent)
        {
            Members = members;
            MembersPresent = memberspresent;
        }

    }
}

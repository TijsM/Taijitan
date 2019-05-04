using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public class Activity
    {
        public int ActivityId { get; set; }
        public String Name { get; set; }
        public TypeActivity Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Boolean IsFull { get; set; }
        public ICollection<Member> Members { get; set; }
        public ICollection<ActivityMember> ActivityMembers { get; set; }


        public Activity(string name, TypeActivity type, DateTime start, DateTime end, List<Member> members)
        {
            Name = name;
            Type = type;
            StartDate = start;
            EndDate = end;
            IsFull = false;
            Members = members;

            ActivityMembers = new List<ActivityMember>();
            foreach (var m in Members)
            {
                ActivityMember am = new ActivityMember(ActivityId, this, m.UserId, m);
                ActivityMembers.Add(am);
            }
        }

        public Activity()
        {

        }
    }


   
}

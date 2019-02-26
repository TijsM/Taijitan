using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Models.ViewModels
{
    public class SessionViewModel
    {
        public IEnumerable<Member> Members { get; set; }
        public DateTime Date { get; set; }
        public Formula Formula { get; set; }
        public Teacher Teacher { get; set; }

        public void Change(Session s)
        {
            Members = s.Members;
            Date = s.Date;
            Formula = s.Formula;
            Teacher = s.Teacher;
        }
        public SessionViewModel()
        {

        }
    }
}

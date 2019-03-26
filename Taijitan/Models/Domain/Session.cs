using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Session
    {
        #region Properties
        [JsonProperty]
        public int SessionId { get; set; }
        public IEnumerable<Member> Members { get; set; }
        public IEnumerable<Member> MembersPresent { get; set; }
        [JsonProperty]
        public DateTime Date { get; set; }
        public IEnumerable<Formula> Formulas => SessionFormulas.Select(sf => sf.Formula).ToList();
        public Teacher Teacher { get; set; }
        public TrainingDay TrainingDay { get; set; }
        [JsonProperty]
        public bool SessionStarted { get; set; }
        public ICollection<SessionMember> SessionMembers { get; set; }
        public IEnumerable<NonMember> NonMembers { get; set; }
        public ICollection<SessionFormula> SessionFormulas { get; set; } 
        #endregion

        #region Constructors
        public Session(IEnumerable<Formula> formulas, Teacher teacher, IEnumerable<Member> members)
        {
            MembersPresent = new List<Member>();
            SessionFormulas = new List<SessionFormula>();
            Members = members;

            ICollection<TrainingDay> temp = new HashSet<TrainingDay>();
            foreach (Formula formula in formulas)
            {
                List<TrainingDay> trainingDays = formula.FormulaTrainingDays.Select(td => td.TrainingDay).ToList();
                if (trainingDays != null && formula != null)
                {
                    foreach (TrainingDay day in trainingDays)
                    {
                        if (!temp.Contains(day) && day != null)
                        {
                            temp.Add(day);
                        }
                    }
                }
            }
            TrainingDay prefDay = temp.SingleOrDefault(t => t.DayOfWeek.Equals(DateTime.Today.DayOfWeek));
            TrainingDay = prefDay != null ? prefDay : temp.First(t => t != null);

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
            Date = DateTime.Now;

            MembersPresent = new List<Member>();
            SessionFormulas = new List<SessionFormula>();
            SessionMembers = new List<SessionMember>();
            NonMembers = new List<NonMember>();
            NonMembers = new List<NonMember>();

            
        } 
        #endregion

        #region Methods
        public void AddToMembersPresent(Member mb)
        {
            if (mb != null)
            {
                List<Member> _hulpPresent = MembersPresent.ToList();
                _hulpPresent.Add(mb);
                MembersPresent = _hulpPresent;

                List<Member> _hulp = Members.ToList();
                _hulp.Remove(mb);
                Members = _hulp;
            }
        }

        public void PutFormulas(IEnumerable<Formula> formulas)
        {
            ICollection<TrainingDay> temp = new HashSet<TrainingDay>();
            foreach (Formula formula in formulas)
            {
                List<TrainingDay> trainingDays = formula.FormulaTrainingDays.Select(td => td.TrainingDay).ToList();
                if (trainingDays != null && formula != null)
                {
                    foreach (TrainingDay day in trainingDays)
                    {
                        if (!temp.Contains(day) && day != null)
                        {
                            temp.Add(day);
                        }
                    }
                }
            }
            foreach (Formula f in formulas)
            {
                SessionFormulas.Add(new SessionFormula(SessionId, this, f.FormulaId, f));
            }
            TrainingDay prefDay = temp.SingleOrDefault(t => t.DayOfWeek.Equals(DateTime.Today.DayOfWeek));
            TrainingDay = prefDay != null ? prefDay : temp.First(t => t != null);
        }

        public void AddToMembers(Member mb)
        {
            if (mb != null)
            {
                List<Member> _hulp = Members.ToList();
                _hulp.Add(mb);
                Members = _hulp;

                List<Member> _hulpPresent = MembersPresent.ToList();
                _hulpPresent.Remove(mb);
                MembersPresent = _hulpPresent; 
            }
        }
        public void AddToSessionMembers(List<Member> members)
        {
            if (members != null)
            {
                List<SessionMember> hulp = SessionMembers.ToList();
                foreach (Member meme in members)
                {
                    hulp.Add(new SessionMember(SessionId, this, meme.UserId, meme));
                }
                SessionMembers = hulp; 
            }
        }

        public void AddNonMember(NonMember nonMember)
        {
            if (nonMember != null)
            {
                List<NonMember> hList = NonMembers.ToList();
                hList.Add(nonMember);
                NonMembers = hList; 
            }
        }

        public void RemoveNonMember(NonMember nonMember)
        {
            if (nonMember != null)
            {
                List<NonMember> hList = NonMembers.ToList();
                hList.Remove(nonMember);
                NonMembers = hList; 
            }
        }
        public void Start()
        {
            if (!SessionStarted)
            {
                AddToSessionMembers(MembersPresent.ToList());
                SessionStarted = true;
            }
        }
        #endregion

    }
}

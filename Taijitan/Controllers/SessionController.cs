using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Taijitan.Filters;
using Taijitan.Helpers;
using Taijitan.Models.Domain;
using Taijitan.Models.ViewModels;

namespace Taijitan.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(UserFilter))]
    public class SessionController : Controller
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITrainingDayRepository _trainingDayRepository;
        private readonly IFormulaRepository _formulaRepository;
        //private readonly UserManager<IdentityUser> _userManager;

        public SessionController(IUserRepository userRepository, ISessionRepository sessionRepository, IFormulaRepository formulaRepository, ITrainingDayRepository trainingDayRepository)
        {
            _userRepository = userRepository;
            _sessionRepository = sessionRepository;
            _trainingDayRepository = trainingDayRepository;
            _formulaRepository = formulaRepository;
            //_userManager = userManager;
        }
        [Authorize(Policy = "Teacher")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Formulas"] = new SelectList(_trainingDayRepository.GetAll().Select(t => new { t.TrainingDayId, t.Name }).ToList(), "TrainingDayId", "Name");
            return View(new SessionViewModel());
        }
        [Authorize(Policy = "Teacher")]
        [HttpPost]
        public IActionResult Create(SessionViewModel svm, User user = null)
        {
            Teacher t;

            //string userEmail = _userManager.GetUserName(HttpContext.User);
            string userEmail = user.Email;
            t = (Teacher)_userRepository.GetByEmail(userEmail);
            IEnumerable<Formula> formulasOfDay = _formulaRepository.GetByTrainingDay(_trainingDayRepository.getById(svm.TrainingDayId));
            IList<Member> members = new List<Member>();
            foreach (var formula in formulasOfDay)
            {
                IEnumerable<Member> m = _userRepository.GetByFormula(formula);
                foreach (var member in m)
                    members.Add(member);
            }
            IEnumerable<Member> membersSession = new List<Member>();
            membersSession = members;
            Session s = new Session(formulasOfDay.ToList(), t, membersSession);
            s.TrainingDay = _trainingDayRepository.getById(svm.TrainingDayId);

            _sessionRepository.Add(s);
            _sessionRepository.SaveChanges();
            svm.Change(s);
            return View("Register", svm);
        }

        [Authorize(Policy = "Teacher")]
        public IActionResult Register(int id, User user = null)
        {
            Session session = _sessionRepository.GetById(id);
            //string userEmail = _userManager.GetUserName(HttpContext.User);
            string userEmail = user.Email;
            Teacher t = (Teacher)_userRepository.GetByEmail(userEmail);
            SessionViewModel svm = new SessionViewModel(session);
            svm.SessionTeacher = t;
            svm.TrainingDay = session.TrainingDay;
            return View(svm);
        }

        [Authorize(Policy = "Teacher")]
        [HttpPost]
        public IActionResult AddToPresent(int sessionId, int id, User user = null)
        {
            Teacher t;
            string userEmail = user.Email;
            //string userEmail = _userManager.GetUserName(HttpContext.User);
            t = (Teacher)_userRepository.GetByEmail(userEmail);
            Session CurrentSession = _sessionRepository.GetById(sessionId);
            Member m = (Member)_userRepository.GetById(id);
            CurrentSession.AddToMembersPresent(m);
            SessionViewModel svm = new SessionViewModel(CurrentSession);
            svm.SessionTeacher = t;
            svm.TrainingDay = CurrentSession.TrainingDay;
            _sessionRepository.SaveChanges();
            return RedirectToAction("Register", new { id = sessionId });
        }

        [Authorize(Policy = "Teacher")]
        [HttpPost]
        public IActionResult AddToUnconfirmed(int sessionId, int id, User user = null)
        {
            Teacher t;
            string userEmail = user.Email;
            //string userEmail = _userManager.GetUserName(HttpContext.User);
            t = (Teacher)_userRepository.GetByEmail(userEmail);
            Session CurrentSession = _sessionRepository.GetById(sessionId);
            Member m = (Member)_userRepository.GetById(id);
            CurrentSession.AddToMembers(m);
            SessionViewModel svm = new SessionViewModel(CurrentSession);
            svm.SessionTeacher = t;
            svm.TrainingDay = CurrentSession.TrainingDay;
            _sessionRepository.SaveChanges();
            return RedirectToAction("Register", new { id = sessionId });
        }




        [Authorize(Policy = "Teacher")]
        public IActionResult AddOtherMember(int id, string searchTerm = "", User user = null)
        {
            var session = _sessionRepository.GetById(id);
            IEnumerable<Member> allMembers = _userRepository.GetAllMembers();
            var sessionMembers = session.Members;
            IEnumerable<Member> otherMembers = allMembers;

            foreach (Member m in session.MembersPresent)
            {
                List<Member> hList = sessionMembers.ToList<Member>();
                hList.Add(m);
                sessionMembers = hList;
            }
            foreach (Member m in sessionMembers)
            {
                otherMembers = otherMembers.Where(om => om.UserId != m.UserId);
            }

            if (searchTerm != null && !searchTerm.Equals(""))
            {
                otherMembers = otherMembers.Where(u => u.Name.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }

            Teacher t;
            string userEmail = user.Email;
            //string userEmail = _userManager.GetUserName(HttpContext.User);
            t = (Teacher)_userRepository.GetByEmail(userEmail);
            SessionViewModel svm = new SessionViewModel(session);
            svm.SessionTeacher = t;
            svm.TrainingDay = session.TrainingDay;
            ViewData["otherMembers"] = otherMembers.ToList<Member>();
            return View(svm);
        }

        [Authorize(Policy = "Teacher")]
        [HttpPost]
        public IActionResult AddNonMember(string firstName, string lastName, string email, int id)
        {
            Session s = _sessionRepository.GetById(id);
            s.AddNonMember(firstName, lastName, email);
            _sessionRepository.SaveChanges();
            return RedirectToAction("Register", new { id });
        }

     

        [Authorize(Policy = "Admin")]
        public IActionResult GetSessions()
        {
            ViewData["Sessions"] = _sessionRepository.GetAll();
            return View("SelectSession");
        }

        [Authorize(Policy = "Admin")]
        public IActionResult SummaryPresences(int id)
        {
            Session s = _sessionRepository.GetById(id);
            ViewData["NonMembers"] = s.NonMembers;
            return View(s.MembersPresent);
        }

    }
}
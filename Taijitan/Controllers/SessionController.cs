using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Taijitan.Helpers;
using Taijitan.Models.Domain;
using Taijitan.Models.ViewModels;

namespace Taijitan.Controllers
{
    [Authorize(Policy = "Teacher")]
    public class SessionController : Controller
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITrainingDayRepository _trainingDayRepository;
        private readonly IFormulaRepository _formulaRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public SessionController(IUserRepository userRepository,ISessionRepository sessionRepository,IFormulaRepository formulaRepository,ITrainingDayRepository trainingDayRepository,UserManager<IdentityUser> userManager)
        {
            _userRepository = userRepository;
            _sessionRepository = sessionRepository;
            _trainingDayRepository = trainingDayRepository;
            _formulaRepository = formulaRepository;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Create()
        {
            
            ViewData["Formulas"] = new SelectList(_trainingDayRepository.GetAll().Select(t => new { t.TrainingDayId, t.Name }).ToList(), "TrainingDayId", "Name");
            return View(new SessionViewModel());
        }
        [HttpPost]
        public IActionResult Create(SessionViewModel svm)
        {
            Teacher t;
            string userEmail = _userManager.GetUserName(HttpContext.User);
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
            Session s = new Session(formulasOfDay.ToList(), t,membersSession);
            s.TrainingDay = _trainingDayRepository.getById(svm.TrainingDayId);
            
            _sessionRepository.Add(s);
            _sessionRepository.SaveChanges();
            svm.Change(s);
            TempData["session"] = s;
            return View("Register",svm);
        }

      
        //[HttpPost]
        public IActionResult AddToPresent(int sessionId, int id)
        {
            Teacher t;
            string userEmail = _userManager.GetUserName(HttpContext.User);
            t = (Teacher)_userRepository.GetByEmail(userEmail);
            Session CurrentSession = _sessionRepository.GetById(sessionId);
            //Session CurrentSession = _sessionRepository.GetById(1);
            Member m = (Member)_userRepository.GetById(id);
            CurrentSession.AddToMembersPresent(m);
            SessionViewModel svm = new SessionViewModel(CurrentSession);
            svm.SessionTeacher = t;
            svm.TrainingDay = CurrentSession.TrainingDay;
            _sessionRepository.SaveChanges();
            return View("Register", svm);
        }

        //[HttpPost]
        public IActionResult AddToUnconfirmed(int sessionId, int id)
        {
            Teacher t;
            string userEmail = _userManager.GetUserName(HttpContext.User);
            t = (Teacher)_userRepository.GetByEmail(userEmail);
            Session CurrentSession = _sessionRepository.GetById(sessionId);
            Member m = (Member)_userRepository.GetById(id);
            CurrentSession.AddToMembers(m);
            SessionViewModel svm = new SessionViewModel(CurrentSession);
            svm.SessionTeacher = t;
            svm.TrainingDay = CurrentSession.TrainingDay;
            _sessionRepository.SaveChanges();
            return View("Register", svm);
        }



    }
}
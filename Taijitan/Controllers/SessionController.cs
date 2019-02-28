using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private readonly UserManager<IdentityUser> _userManager;

        public SessionController(IUserRepository userRepository,ISessionRepository sessionRepository,UserManager<IdentityUser> userManager)
        {
            _userRepository = userRepository;
            _sessionRepository = sessionRepository;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Formulas"] = EnumHelpers.ToSelectList<Formula>();
            return View(new SessionViewModel());
        }
        [HttpPost]
        public IActionResult Create(SessionViewModel svm)
        {
            Teacher t;
            string userEmail = _userManager.GetUserName(HttpContext.User);
            t = (Teacher)_userRepository.GetByEmail(userEmail);
            IEnumerable<Member> members = _userRepository.GetByFormula(svm.SessionFormula);
            Session s = new Session(svm.SessionFormula, t, members);
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
            Member m = (Member)_userRepository.GetById(id);
            CurrentSession.AddToMembersPresent(m);
            SessionViewModel svm = new SessionViewModel(CurrentSession);
            svm.SessionTeacher = t;
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
            _sessionRepository.SaveChanges();
            return View("Register", svm);
        }



    }
}
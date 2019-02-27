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
        [Authorize(Policy ="Teacher")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Formulas"] = EnumHelpers.ToSelectList<Formula>();
            return View(new SessionViewModel());
        }
        [HttpPost]
        [Authorize(Policy = "Teacher")]
        public IActionResult Create(SessionViewModel svm)
        {
            Teacher t;
            string userEmail = _userManager.GetUserName(HttpContext.User);
            t = (Teacher)_userRepository.GetByEmail(userEmail);
            IEnumerable<Member> members = _userRepository.GetByFormula(svm.Formula);
            Session s = new Session(svm.Formula, t, members);
            _sessionRepository.Add(s);
            _sessionRepository.SaveChanges();
            svm.Change(s);
            return View("Register",svm);
        }

      
        [HttpPost]
        public IActionResult AddToPresent(int id, SessionViewModel svm)
        {
            Member m = (Member)_userRepository.GetById(id);
            svm.AddToMembersPresent(m);
            return View(svm);
            
        }

        [HttpPost]
        public IActionResult AddToUnconfirmed(SessionViewModel svm, int id)
        {
            Member m = (Member)_userRepository.GetById(id);
            svm.AddToMembers(m);
            return View(svm);
        }



    }
}
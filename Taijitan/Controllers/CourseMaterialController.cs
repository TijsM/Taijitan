using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Taijitan.Models.Domain;

namespace Taijitan.Controllers
{
    public class CourseMaterialController : Controller
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IUserRepository _userRepository;


        public CourseMaterialController(ISessionRepository sessionRepository, IUserRepository userRepository)
        {
            _sessionRepository = sessionRepository;
            _userRepository = userRepository;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Confirm(int sessionId)
        {
            Session currentSession = _sessionRepository.GetById(sessionId);
            //IEnumerable<Member> membersPresent = currentSession.MembersPresent;
            //foreach (var member in membersPresent)
            //{

            //}
            ViewData["RankValue"] = GiveAllRanksAsList();
            currentSession.AddToSessionMembers(currentSession.MembersPresent.ToList());
            _sessionRepository.SaveChanges();
            return View("Training", currentSession);
        }



        [HttpPost]
        public IActionResult SelectMember(int sessionId, int id)
        {
            var user = (Member)_userRepository.GetById(id);

            ViewData["RankValue"] = GiveAllRanksAsList();
            ViewData["SelectedMember"] = _userRepository.GetById(id);
            var currentSession = _sessionRepository.GetById(sessionId);
            return View("Training", currentSession);
        }

        private ICollection<Rank> GiveAllRanksAsList()
        {
            ICollection<Rank> ranks = Enum.GetValues(typeof(Rank)).Cast<Rank>().ToList();
            return ranks;
        }
    }
}
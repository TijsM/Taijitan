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
        private readonly ICourseMaterialRepository _courseMaterialRepository;


        public CourseMaterialController(ISessionRepository sessionRepository, IUserRepository userRepository, ICourseMaterialRepository courseMaterialRepository)
        {
            _sessionRepository = sessionRepository;
            _userRepository = userRepository;
            _courseMaterialRepository = courseMaterialRepository;
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
            ViewData["Material"] = _courseMaterialRepository.GetByRank(Rank.Kyu6);
            ViewData["Ranks"] = GiveAllRanksAsList();
            ViewData["SelectedRank"] = Rank.Kyu6;
            currentSession.AddToSessionMembers(currentSession.MembersPresent.ToList());
            _sessionRepository.SaveChanges();
            return View("Training", currentSession);
        }



        [HttpPost]
        public IActionResult SelectMember(int sessionId, int id)
        {
            var user = (Member)_userRepository.GetById(id);
            ViewData["Material"] = _courseMaterialRepository.GetByRank(Rank.Kyu6);
            ViewData["Ranks"] = GiveAllRanksAsList();
            ViewData["SelectedMember"] = _userRepository.GetById(id);
            ViewData["SelectedRank"] = Rank.Kyu6;
            var currentSession = _sessionRepository.GetById(sessionId);
            return View("Training", currentSession);
        }

        private ICollection<Rank> GiveAllRanksAsList()
        {
            ICollection<Rank> ranks = Enum.GetValues(typeof(Rank)).Cast<Rank>().ToList();
            return ranks;
        }

        public IActionResult SelectRank(int sessionId, Rank rank, int selectedUserId)
        {
            ViewData["Material"] = _courseMaterialRepository.GetByRank(rank);
            ViewData["SelectedMember"] = _userRepository.GetById(selectedUserId);
            ViewData["Ranks"] = GiveAllRanksAsList();
            ViewData["SelectedRank"] = rank;
            var currentSession = _sessionRepository.GetById(sessionId);
            return View("Training", currentSession);
        }
    }
}
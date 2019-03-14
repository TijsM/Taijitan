using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Taijitan.Models.Domain;
using Taijitan.Models.ViewModels;

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
        public IActionResult Confirm(int sessionId)
        {
            Session currentSession = _sessionRepository.GetById(sessionId);
            if (!currentSession.SessionStarted)
            {
                currentSession.AddToSessionMembers(currentSession.MembersPresent.ToList());
                currentSession.SessionStarted = true;
                _sessionRepository.SaveChanges();
            }
            ViewData["partialView"] = "";
            CourseMaterialViewModel vm = new CourseMaterialViewModel()
            {
                Session = currentSession,
                CourseMaterials = _courseMaterialRepository.GetByRank(Rank.Kyu6),
                AllRanks = GiveAllRanksAsList(),
                SelectedRank = Rank.Kyu6
            };
            return View("Training", vm);
        }
        [HttpPost]
        public IActionResult SelectMember(int sessionId, int id)
        {
            ViewData["partialView"] = "lessons";
            CourseMaterialViewModel vm = new CourseMaterialViewModel()
            {
                Session = _sessionRepository.GetById(sessionId),
                CourseMaterials = _courseMaterialRepository.GetByRank(Rank.Kyu6),
                AllRanks = GiveAllRanksAsList(),
                SelectedMember = (Member)_userRepository.GetById(id),
                SelectedRank = Rank.Kyu6
            };
            return View("Training", vm);
        }
        private ICollection<Rank> GiveAllRanksAsList()
        {
            ICollection<Rank> ranks = Enum.GetValues(typeof(Rank)).Cast<Rank>().ToList();
            return ranks;
        }
        public IActionResult SelectRank(int sessionId, Rank rank, int selectedUserId)
        {
            ViewData["partialView"] = "lessons";
            CourseMaterialViewModel vm = new CourseMaterialViewModel()
            {
                Session = _sessionRepository.GetById(sessionId),
                CourseMaterials = _courseMaterialRepository.GetByRank(rank),
                AllRanks = GiveAllRanksAsList(),
                SelectedMember = (Member)_userRepository.GetById(selectedUserId),
                SelectedRank = rank,
            };
            return View("Training", vm);
        }
        public IActionResult SelectCourse(int sessionId, Rank rank, int selectedUserId, int matId)
        {
            ViewData["partialView"] = "course";
            CourseMaterialViewModel vm = new CourseMaterialViewModel()
            {
                Session = _sessionRepository.GetById(sessionId),
                CourseMaterials = _courseMaterialRepository.GetByRank(rank),
                SelectedCourseMaterial = _courseMaterialRepository.GetById(matId),
                AllRanks = GiveAllRanksAsList(),
                SelectedMember = (Member)_userRepository.GetById(selectedUserId),
                SelectedRank = rank,
            };
            return View("Training", vm);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Taijitan.Models.Domain;
using Taijitan.Models.ViewModels;

namespace Taijitan.Controllers
{
    public class CourseMaterialController : Controller
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICourseMaterialRepository _courseMaterialRepository;
        private readonly ICommentRepository _commentRepository;

        public CourseMaterialController(ISessionRepository sessionRepository, IUserRepository userRepository, ICourseMaterialRepository courseMaterialRepository, ICommentRepository commentRepository)
        {
            _sessionRepository = sessionRepository;
            _userRepository = userRepository;
            _courseMaterialRepository = courseMaterialRepository;
            _commentRepository = commentRepository;
        }
        public IActionResult Confirm(int id)
        {
            Session currentSession = _sessionRepository.GetById(id);
            if (!currentSession.SessionStarted)
            {
                currentSession.AddToSessionMembers(currentSession.MembersPresent.ToList());
                currentSession.SessionStarted = true;
                _sessionRepository.SaveChanges();
            }
            HttpContext.Session.SetString("Session", JsonConvert.SerializeObject(currentSession));
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
            HttpContext.Session.SetString("CourseMaterialViewModel", JsonConvert.SerializeObject(vm));
            return View("Training", vm);
        }

        [HttpPost]
        public IActionResult AddComment(string comment)
        {
            if (HttpContext.Session.GetString("CourseMaterialViewModel") != null)
            {
                CourseMaterialViewModel model = JsonConvert.DeserializeObject<CourseMaterialViewModel>(HttpContext.Session.GetString("CourseMaterialViewModel"));
                Comment c = new Comment(comment, model.SelectedCourseMaterial, model.SelectedMember);
                _commentRepository.Add(c);
                _commentRepository.SaveChanges();
                TempData["message"] = "Het commentaar is succesvol verstuurd!";
                return RedirectToAction(nameof(SelectCourse), new { sessionId = model.Session.SessionId, rank = model.SelectedRank, selectedUserId = model.SelectedMember.UserId, matId = model.SelectedCourseMaterial.MaterialId });
            }
            return View("Training");
        }
    }
}
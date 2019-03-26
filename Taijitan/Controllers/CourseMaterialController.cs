using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Newtonsoft.Json;
using Taijitan.Models.Domain;
using Taijitan.Models.ViewModels;
using MailKit.Net.Smtp;
using Taijitan.Filters;
using Microsoft.AspNetCore.Authorization;

namespace Taijitan.Controllers
{
    [ServiceFilter(typeof(SessionFilter))]
    [ServiceFilter(typeof(CourseMaterialFilter))]
    [ServiceFilter(typeof(HomeFilter))]
    [Authorize]
    public class CourseMaterialController : Controller
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICourseMaterialRepository _courseMaterialRepository;
        private readonly ICommentRepository _commentRepository;

        public CourseMaterialController(ISessionRepository sessionRepository, IUserRepository userRepository,
            ICourseMaterialRepository courseMaterialRepository, ICommentRepository commentRepository)
        {
            _sessionRepository = sessionRepository;
            _userRepository = userRepository;
            _courseMaterialRepository = courseMaterialRepository;
            _commentRepository = commentRepository;
        }
        [Authorize(Policy = "Teacher")]
        public IActionResult Confirm(Session session)
        {
            Session tempSession = _sessionRepository.GetById(session.SessionId);
            if (tempSession == null)
                return NotFound();

            tempSession.Start();
            session.Start();
            _sessionRepository.SaveChanges();

            ViewData["partialView"] = "";
            CourseMaterialViewModel vm = new CourseMaterialViewModel()
            {
                Session = tempSession,
                CourseMaterials = _courseMaterialRepository.GetByRank(Rank.Kyu6),
                AllRanks = GiveAllRanksAsList(),
                SelectedRank = Rank.Kyu6
            };
            return View("Training", vm);
        }
        [HttpPost]
        [Authorize(Policy = "Teacher")]
        public IActionResult SelectMember(int sessionId, int id)
        {
            ViewData["partialView"] = "lessons";
            var session = _sessionRepository.GetById(sessionId);
            var courseMaterial = _courseMaterialRepository.GetByRank(Rank.Kyu6);
            var selectedMember = (Member)_userRepository.GetById(id);

            if (session == null || selectedMember == null)
                return NotFound();

            CourseMaterialViewModel vm = new CourseMaterialViewModel()
            {
                Session = session,
                CourseMaterials = courseMaterial,
                AllRanks = GiveAllRanksAsList(),
                SelectedMember = selectedMember,
                SelectedRank = Rank.Kyu6
            };
            return View("Training", vm);
        }
        [Authorize(Policy = "Teacher")]
        private ICollection<Rank> GiveAllRanksAsList()
        {
            return Enum.GetValues(typeof(Rank)).Cast<Rank>().ToList();
        }
        [Authorize(Policy = "Teacher")]
        public IActionResult SelectRank(int sessionId, Rank rank, int selectedUserId)
        {
            ViewData["partialView"] = "lessons";
            var session = _sessionRepository.GetById(sessionId);
            var courseMaterial = _courseMaterialRepository.GetByRank(rank);
            var selectedMember = (Member)_userRepository.GetById(selectedUserId);

            if (session == null || selectedMember == null)
                return NotFound();

            CourseMaterialViewModel vm = new CourseMaterialViewModel()
            {
                Session = session,
                CourseMaterials = courseMaterial,
                AllRanks = GiveAllRanksAsList(),
                SelectedMember = selectedMember,
                SelectedRank = rank,
            };
            return View("Training", vm);
        }
        [Authorize(Policy = "Teacher")]
        public IActionResult SelectCourse(int sessionId, Rank rank, int selectedUserId, int matId, CourseMaterialViewModel cmvm)
        {
            var session = _sessionRepository.GetById(sessionId);
            var courseMaterial = _courseMaterialRepository.GetByRank(rank);
            var selectedMember = (Member)_userRepository.GetById(selectedUserId);
            var selectedCourseMaterial = _courseMaterialRepository.GetById(matId);

            if (session == null || selectedMember == null)
                return NotFound();

            ViewData["partialView"] = "course";
            cmvm.Update(session, courseMaterial, selectedCourseMaterial, GiveAllRanksAsList(), selectedMember, rank);
            return View("Training", cmvm);
        }

        [HttpPost]
        [Authorize(Policy = "Teacher")]
        public IActionResult AddComment(string comment, CourseMaterialViewModel cmvm, ICollection<Comment> notifications)
        {
            if (cmvm != null)
            {
                //viewModel uit session halen
                CourseMaterial course = _courseMaterialRepository.GetById(cmvm.SelectedCourseMaterial.MaterialId);
                Member member = (Member)_userRepository.GetById(cmvm.SelectedMember.UserId);

                if (course == null || member == null)
                    return NotFound();

                Comment c = new Comment(comment, course, member);
                _commentRepository.Add(c);
                _commentRepository.SaveChanges();
                SendMail(c);
                TempData["message"] = "Het commentaar is succesvol verstuurd!";
                
                //Notificaties
                if (notifications != null)
                {
                    notifications.OrderBy(n => n.DateCreated);
                    while(notifications.Where(n => n.IsRead).Count() > 0 && notifications.Count() > 5)
                    {
                        notifications.Remove(notifications.Last());
                    }
                    notifications.Add(c);
                } else
                {
                    notifications = new List<Comment>();
                    notifications.Add(c);
                }
                return RedirectToAction(nameof(SelectCourse), new { sessionId = cmvm.Session.SessionId, rank = cmvm.SelectedRank,
                    selectedUserId = cmvm.SelectedMember.UserId, matId = cmvm.SelectedCourseMaterial.MaterialId });
            }
            return View("Training");
        }
        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult ViewComments()
        {
            ViewData["IsEmpty"] = true;
            return ShowComments();
        }
        public IActionResult SelectComment(int id)
        {
            Comment comment = _commentRepository.GetById(id);

            if (comment == null)
            {
                ViewData["IsEmpty"] = true;
            }
            else
            {
                ViewData["IsEmpty"] = false;
                ViewData["Comment"] = comment;

            }
            return ShowComments();
        }
        [Authorize(Policy = "Admin")]
        public IActionResult RemoveComment(int id)
        {
            Comment comment = _commentRepository.GetById(id);
            if (comment == null)
                return NotFound();

            _commentRepository.Delete(comment);
            _commentRepository.SaveChanges();
            ViewData["IsEmpty"] = true;
            return ShowComments();
        }
        [Authorize(Policy = "Admin")]
        private IActionResult ShowComments()
        {
            var comments = _commentRepository.GetAll();
            return View("ViewComments", comments);
        }
        [Authorize(Policy = "Admin")]
        private void SendMail(Comment comment)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("project.groep08@gmail.com"));
            message.To.Add(new MailboxAddress("receivetaijitan@maildrop.cc"));
            message.Subject = "nieuwe commentaar van " + comment.Member.FirstName;
            message.Body = new TextPart("html")
            {
                Text =
                "Gebruiker die commentaar leverde: " + comment.Member.FirstName +" " + comment.Member.Name
                + "<br />"
                + "Datum van de commentaar: " + comment.DateCreated.ToShortDateString()
                + "<br />"
                + "Lesmateriaal van de commentaar: " + comment.Course.Title
                + "<br />"
                + "Commentaar: "
                + "<br />"
                + comment.Content
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587);
                client.Authenticate("groep08.project@gmail.com", "_123Groep8_123_");

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
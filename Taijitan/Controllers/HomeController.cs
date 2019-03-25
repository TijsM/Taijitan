using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Taijitan.Filters;
using Taijitan.Models;
using Taijitan.Models.Domain;

namespace Taijitan.Controllers
{
    [ServiceFilter(typeof(HomeFilter))]
    [ServiceFilter(typeof(UserFilter))]
    [ServiceFilter(typeof(SessionFilter))]
    public class HomeController : Controller {
        private readonly ICommentRepository _commentRepository;

        public HomeController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public IActionResult Index(ICollection<Comment> notifications, User User, Session session)
        {
            TempData["Role"] = "";
            TempData["IsHome"] = "true";
            TempData["FullName"] = "";
            if (User != null)
            {
                var user = User;
                TempData["Role"] = user.GetRole();
                TempData["UserId"] = user.UserId;
                TempData["FullName"] = user.FirstName + " " + user.Name;
                if (session != null && session.Members != null)
                {
                    ViewData["Session"] = session;
                }
                //notifications
                if (user.IsRole("Admin"))
                {
                    while (notifications.Count > 5 && notifications.Where(c => c.IsRead).Count() > 0)
                    {
                        Comment commentToDelete = notifications.Where(c => c.IsRead).Last();
                        notifications.Remove(commentToDelete);
                    }
                    TempData["Notifications"] = notifications;
                    TempData["AmountUnread"] = notifications.Where(c => !c.IsRead).Count();
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult MarkRead(ICollection<Comment> notifications)
        {
            if (notifications != null)
            {
                foreach (Comment c in notifications)
                {
                    c.IsRead = true;
                    Comment com = _commentRepository.GetById(c.CommentId);
                    com.IsRead = true;
                }
                _commentRepository.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

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
using Taijitan.Models;
using Taijitan.Models.Domain;

namespace Taijitan.Controllers
{

    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly ICommentRepository _commentRepostitory;

        public HomeController(UserManager<IdentityUser> userManager, IUserRepository userRepository, ICommentRepository commentRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _commentRepostitory = commentRepository;
        }

        public IActionResult Index()
        {
            TempData["Role"] = "";
            TempData["IsHome"] = "true";
            if (_userManager.GetUserName(HttpContext.User) != null)
            {
                var user = _userRepository.GetByEmail(_userManager.GetUserName(HttpContext.User));
                TempData["Role"] = user.GetType();
                TempData["Role"] = TempData["role"].ToString().Split(".")[3];
                TempData["UserId"] = user.UserId;
                if (HttpContext.Session.GetString("Session") != null)
                {
                    ViewData["Session"] = JsonConvert.DeserializeObject<Session>(HttpContext.Session.GetString("Session"));
                }
                if (TempData["Role"].Equals("Admin"))
                {
                    if (HttpContext.Session.GetString("Notifications") != null)
                    {
                        ICollection<Comment> comments = JsonConvert.DeserializeObject<ICollection<Comment>>(HttpContext.Session.GetString("Notifications"));
                        while (comments.Count > 5 && comments.Where(c => c.IsRead).Count() > 0)
                        {
                            Comment commentToDelete = comments.Where(c => c.IsRead).First();
                            comments.Remove(commentToDelete);
                        }
                        TempData["Notifications"] = comments;
                        TempData["AmountUnread"] = comments.Where(c => !c.IsRead).Count();
                        HttpContext.Session.SetString("Notifications", JsonConvert.SerializeObject(comments));
                    }
                    else
                    {
                        ICollection<Comment> comments = new List<Comment>();
                        comments = _commentRepostitory.GetAll().Where(c => !c.IsRead).ToList();
                        int aantal = 5 - comments.Count();
                        var extraComments = _commentRepostitory.GetAll().Where(c => c.IsRead).Take(aantal);
                        if(extraComments != null)
                        {
                            foreach (Comment c in extraComments)
                            {
                                comments.Add(c);
                            }
                        }
                        TempData["AmountUnread"] = comments.Where(c => !c.IsRead).Count();
                        TempData["Notifications"] = comments;
                        HttpContext.Session.SetString("Notifications", JsonConvert.SerializeObject(comments));
                    }
                }

            }
            return View();
        }

        [HttpPost]
        public IActionResult MarkRead()
        {
            if (HttpContext.Session.GetString("Notifications") != null)
            {
                ICollection<Comment> comments = JsonConvert.DeserializeObject<ICollection<Comment>>(HttpContext.Session.GetString("Notifications"));
                foreach (Comment c in comments)
                {
                    c.IsRead = true;
                    Comment com = _commentRepostitory.GetById(c.CommentId);
                    com.IsRead = true;
                }
                _commentRepostitory.SaveChanges();
                HttpContext.Session.SetString("Notifications", JsonConvert.SerializeObject(comments));
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

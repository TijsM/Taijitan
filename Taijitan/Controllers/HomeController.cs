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

        public HomeController(UserManager<IdentityUser> userManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            TempData["Role"] = "";
            if(_userManager.GetUserName(HttpContext.User) != null)
            {
                var user = _userRepository.GetByEmail(_userManager.GetUserName(HttpContext.User));
                TempData["Role"] = user.GetType();
                TempData["Role"] = TempData["role"].ToString().Split(".")[3];
                TempData["UserId"] = user.UserId;
                if(HttpContext.Session.GetString("Session") != null)
                {
                    ViewData["Session"] = JsonConvert.DeserializeObject<Session>(HttpContext.Session.GetString("Session"));
                }
            }
            return View();
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

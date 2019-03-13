using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Taijitan.Controllers
{
    public class ErrorsController : Controller
    {
        public IActionResult Index()
        {
            TempData["ErrorCode"] = "500";
            return View("Error");
        }

        public IActionResult Error(string Id)
        {
            TempData["ErrorCode"] = Id;
            return View("Error");
        }
    }
}
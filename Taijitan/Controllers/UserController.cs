using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Taijitan.Filters;
using Taijitan.Models.Domain;
using Taijitan.Models.UserViewModel;


namespace Taijitan.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(UserFilter))]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ICityRepository _cityRepository;


        public UserController(IUserRepository userRepository, ICityRepository cityRepository)
        {
            _userRepository = userRepository;
            _cityRepository = cityRepository;

        }


        public IActionResult Index(User user = null)
        {
            //User u = null;
            //if (id == null)
            //{

            //    u = user;

            //}
            //else
            //{
            //    u = _userRepository.GetById((int)id);
            //}

            if (user == null)
                return NotFound();
            TempData["Role"] = user.GetType();
            TempData["Role"] = TempData["role"].ToString().Split(".")[3];
            TempData["userId"] = user.UserId;
            TempData["EditViewModel"] = new EditViewModel(user);
            return View("Index");
        }


        public IActionResult Summary(string searchTerm = "")
        {
            IEnumerable<User> users;
            if (searchTerm == null || searchTerm.Equals(""))
            {
                users = _userRepository.GetAll();
            }
            else
            {
                users = _userRepository.GetByPartofName(searchTerm);
            }
            return View(users);
        }


        public IActionResult Edit(User user)
        {
            //User u = null;
            //if (id == null)
            //{
            //    string userEmail = _userManager.GetUserName(HttpContext.User);
            //    u = _userRepository.GetByEmail(userEmail);

            //}
            //else
            //{
            //    u = _userRepository.GetById((int)id);
            //}

            if (user == null)
                return NotFound();
            TempData["Role"] = user.GetType();
            TempData["Role"] = TempData["Role"].ToString().Split(".")[3];
            TempData["userId"] = user.UserId;
            //TempData.Put<User>("user", u);
            var model = new EditViewModel(user);
            return View("Edit", model);
        }
        [HttpPost]
        public IActionResult Edit(int id, EditViewModel evm)
        {
            User u = null;
            if (ModelState.IsValid)
            {
                try
                {
                    u = _userRepository.GetById(id);
                    u.Change(evm.Name, evm.FirstName, evm.DateOfBirth, evm.Street, _cityRepository.GetByPostalCode(evm.PostalCode), evm.Country, evm.HouseNumber, evm.PhoneNumber, evm.Email);
                    _userRepository.SaveChanges();
                    TempData["message"] = "Je persoonlijke gegevens werden aangepast";
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            ViewData["userId"] = id;
            return View(evm);
        }

        [Authorize(policy: "Admin")]
        public IActionResult Delete(int id)
        {
            User us = _userRepository.GetById(id);
            _userRepository.Delete(us);
            _userRepository.SaveChanges();
            return RedirectToAction("Summary");
        }
    }
}
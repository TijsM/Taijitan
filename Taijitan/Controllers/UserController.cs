using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Taijitan.Filters;
using Taijitan.Helpers;
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
            if (user == null)
                return NotFound();
            TempData["Role"] = user.GetType();
            TempData["Role"] = TempData["role"].ToString().Split(".")[3];
            TempData["userId"] = user.UserId;
            TempData["EditViewModel"] = new EditViewModel(user);
            ViewData["Countries"] = EnumHelpers.ToSelectList<Country>();
            ViewData["Genders"] = EnumHelpers.ToSelectList<Gender>();
            return View("Index");
        }

        [Authorize(Policy = "Admin")]
        public IActionResult Summary()
        {
            IEnumerable<User> users = _userRepository.GetAll();
            return View(users);
        }


        public IActionResult Edit(int id, int isFromSummary = 0)
        {
            User user = _userRepository.GetById(id);

            if (user == null)
                return NotFound();
            TempData["Role"] = user.GetType().ToString().Split(".")[3];
            TempData["userId"] = user.UserId;
            ViewData["isFromSummary"] = isFromSummary;
            ViewData["Countries"] = EnumHelpers.ToSelectList<Country>();
            ViewData["Genders"] = EnumHelpers.ToSelectList<Gender>();

            var model = new EditViewModel(user);
            return View("Edit", model);
        }
        [HttpPost]
        public IActionResult Edit(int id, User user, EditViewModel evm, int isFromSummary = 0)
        {
            User u = null;
            if (ModelState.IsValid)
            {
                try
                {
                    u = _userRepository.GetById(id);
                    u.Change(evm.Name, evm.FirstName, evm.DateOfBirth, evm.Street, _cityRepository.GetByPostalCode(evm.PostalCode), evm.Country, evm.HouseNumber, evm.PhoneNumber, evm.Email, evm.DateRegistred, evm.Gender, evm.Nationality, evm.PersonalNationalNumber, evm.BirthPlace, evm.LandLineNumber, evm.MailParent);
                    _userRepository.SaveChanges();
                    string rol = user.GetType().ToString().Split(".")[3];
                    TempData["message"] = $"De persoonlijke gegevens van {u.FirstName} {u.Name} werden aangepast";

                    if (rol.Equals("Admin") && isFromSummary == 1)
                    {
                        return RedirectToAction(nameof(Summary));
                    }
                    return RedirectToAction("Index", "Home");
                }
                catch
                {
                    TempData["error"] = $"er ging iets mis bij het wijzigen van {u.FirstName} {u.Name}";
                    return RedirectToAction(nameof(Summary));
                }
            }
            ViewData["userId"] = id;
            return View(evm);
        }

        [Authorize(Policy = "Admin")]
        public IActionResult Delete(int id, string confirmed = "true")
        {
            if (confirmed.Equals("true"))
            {
                User us = _userRepository.GetById(id);
                _userRepository.Delete(us);
                _userRepository.SaveChanges();
            }
            return RedirectToAction("Summary");
        }
    }
}
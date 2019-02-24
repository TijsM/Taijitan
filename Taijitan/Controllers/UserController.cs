﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Taijitan.Models.Domain;
using Taijitan.Models.UserViewModel;


namespace Taijitan.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ICityRepository _cityRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(IUserRepository userRepository,ICityRepository cityRepository, UserManager<IdentityUser> userManager)
        {
            _userRepository = userRepository;
            _cityRepository = cityRepository;
            _userManager = userManager;
        }

        [Authorize(policy: "Admin")]
        public IActionResult Index(string searchTerm = "")
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

        //public IActionResult Index(string searchTerm)
        //{
        //    IEnumerable<User> users;
        //    users = _userRepository.GetByPartofName(searchTerm);
        //    return View(users);
        //}

        public IActionResult Edit()
        {
            string userEmail = _userManager.GetUserName(HttpContext.User);
            var u = _userRepository.GetByEmail(userEmail);
            if (u == null)
                return NotFound();
            TempData["Role"] = u.GetType();
            TempData["Role"] = TempData["Role"].ToString().Split(".")[3];
            TempData["userId"] = u.UserId;
            //TempData.Put<User>("user", u);
            TempData["EditViewModel"] = new EditViewModel(u);
            return View();
        }
        [HttpPost]
        public IActionResult Edit(int id,EditViewModel evm)
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
                catch(Exception e)
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
            return RedirectToAction(nameof(Index));
        }
    }
}
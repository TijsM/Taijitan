using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Taijitan.Data;
using Taijitan.Models.Domain;
using Taijitan.Models.MemberViewModels;

namespace Taijitan.Controllers
{
    public class MemberController : Controller
    {
        private readonly IUserRepository _memberRepository;
        private readonly ICityRepository _cityRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public MemberController(IUserRepository memberRepository,ICityRepository cityRepository, UserManager<IdentityUser> userManager)
        {
            _memberRepository = memberRepository;
            _cityRepository = cityRepository;
            _userManager = userManager;
        }
        [Authorize]
        public IActionResult Edit(int id)
        {
            var userName = _userManager.GetUserName(HttpContext.User);
            ViewData["user"] = userName;

            Member m = _memberRepository.GetById(id);
            if (m == null)
                return NotFound();

            return View(new EditViewModel(m));
        }
        [HttpPost]
        public IActionResult Edit(int id,EditViewModel evm)
        {
            Member m = null;
            if (ModelState.IsValid)
            {
                
                try
                {
                    m = _memberRepository.GetById(id);
                    m.Change(evm.Name, evm.FirstName, evm.DateOfBirth, evm.Street, _cityRepository.GetByPostalCode(evm.PostalCode), evm.Country, evm.HouseNumber, evm.PhoneNumber, evm.Email);
                    _memberRepository.SaveChanges();
                }
                catch
                {

                }
            }
            return View();
        }
    }
}
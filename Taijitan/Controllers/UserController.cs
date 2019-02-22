using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public UserController(IUserRepository userRepository,ICityRepository cityRepository)
        {
            _userRepository = userRepository;
            _cityRepository = cityRepository;
        }
        public IActionResult Edit(int id)
        {
            User u = _userRepository.GetById(id);
            if (u == null)
                return NotFound();

            return View(new EditViewModel(u));
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
                }
                catch
                {

                }
            }
            return View(); ;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Taijitan.Models.Domain;
using Taijitan.Models.MemberViewModels;

namespace Taijitan.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ICityRepository _cityRepository;

        public MemberController(IMemberRepository memberRepository,ICityRepository cityRepository)
        {
            _memberRepository = memberRepository;
            _cityRepository = cityRepository;
        }
        public IActionResult Edit(int id)
        {
            Member m = _memberRepository.GetById(id);
            if (m == null)
                return NotFound();

            return View(new EditViewModel(m));
        }

      

        [HttpPost]
        public IActionResult Edit(int id,EditViewModel evm)
        {

            
            if (ModelState.IsValid)
            {
                Member m = null;
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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Taijitan.Filters;
using Taijitan.Helpers;
using Taijitan.Models.Domain;
using Taijitan.Models.UserViewModel;
using Taijitan.Models.ViewModels;

namespace Taijitan.Controllers
{
    [ServiceFilter(typeof(UserFilter))]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ICityRepository _cityRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private const string adminCode = "Admin123";
        private bool isAuthenticatedToCreate = false;

        public UserController(IUserRepository userRepository, ICityRepository cityRepository, UserManager<IdentityUser> userManager)
        {
            _userRepository = userRepository;
            _cityRepository = cityRepository;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Index(User user = null)
        {
            if (user == null)
                return NotFound();

            ViewData["Role"] = user.GetRole();
            ViewData["userId"] = user.UserId;
            ViewData["EditViewModel"] = new EditViewModel(user);
            ViewData["Countries"] = EnumHelpers.ToSelectList<Country>();
            ViewData["Genders"] = EnumHelpers.ToSelectList<Gender>();
            return View("Index");
        }

        [Authorize(Policy = "Admin")]
        [HttpGet]
        public IActionResult Summary(User user)
        {
            User admin = _userRepository.GetById(user.UserId);
            ViewData["AdminEmail"] = admin.Email;
            return View(_userRepository.GetAll());
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id, int isFromSummary = 0)
        {
            User user = _userRepository.GetById(id);

            if (user == null)
                return NotFound();

            ViewData["Role"] = user.GetRole();
            ViewData["userId"] = user.UserId;
            ViewData["isFromSummary"] = isFromSummary;
            ViewData["Countries"] = EnumHelpers.ToSelectList<Country>();
            ViewData["Genders"] = EnumHelpers.ToSelectList<Gender>();
            return View("Edit", new EditViewModel(user));
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, User user, EditViewModel evm, int isFromSummary = 0)
        {
            User u = null;
            if (ModelState.IsValid)
            {
                u = _userRepository.GetById(id);
                u.Change(evm.Name, evm.FirstName, evm.Street, _cityRepository.GetByPostalCode(evm.PostalCode),
                    evm.Country, evm.HouseNumber, evm.PhoneNumber, evm.Gender, evm.Nationality, evm.BirthPlace, evm.Rank,
                    evm.LandLineNumber, evm.MailParent);
                _userRepository.SaveChanges();
                TempData["message"] = $"De persoonlijke gegevens van {u.FirstName} {u.Name} werden aangepast";

                if (user.IsRole("Admin") && isFromSummary == 1)
                {
                    return RedirectToAction(nameof(Summary));
                }
                return RedirectToAction("Index", "Home");
            }
            ViewData["userId"] = id;
            return View(evm);
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public IActionResult Delete(int id, string confirmed = "true")
        {
            User user = _userRepository.GetById(id);

            if (user == null)
                return NotFound();

            if (confirmed.Equals("true"))
            {
                _userRepository.Delete(user);
                _userRepository.SaveChanges();
            }
            return RedirectToAction("Summary");
        }

        [HttpGet]
        public IActionResult CreateAccount()
        {
            return View("CheckAdminCode");
        }

        [HttpPost]
        public IActionResult CreateAccount(string code)
        {
            if (code.Equals(adminCode))
            {
                isAuthenticatedToCreate = true;
                return View("CreateAccount");
            }
            else
            {
                TempData["CodeError"] = "De code is incorrect";
                return RedirectToAction(nameof(CreateAccount));
            }

        }

        [HttpGet]
        public IActionResult CreateAccountPassword()
        {
            if(isAuthenticatedToCreate == false)
            {
                return RedirectToAction(nameof(CreateAccount));
            }
            return View("CreateAccount", new AccountViewModel());
        }

        [HttpPost]
        public IActionResult CreateAccountPassword(AccountViewModel accountViewModel)
        {
            if(ModelState.IsValid)
            {
                if (_userRepository.GetByEmail(accountViewModel.Email) != null)
                {
                    if (_userManager.FindByEmailAsync(accountViewModel.Email).Result == null)
                    {
                        CreateUser(accountViewModel.Email, accountViewModel.Email, accountViewModel.Password, "Member").Wait();
                        TempData["message"] = "Het account is succesvol aangemaakt";
                        isAuthenticatedToCreate = false;
                        return View("~/Areas/Identity/Pages/Account/Login.cshtml");
                    }
                    else
                    {
                        TempData["EmailError"] = "Er bestaat al een account op dit email adres";
                        return View("CreateAccount", accountViewModel);
                    }
                }
                else
                {
                    TempData["EmailError"] = "Er is geen gebruiker met dit email adres";
                    return View("CreateAccount", accountViewModel);
                }
            }
            else
            {
                return View("CreateAccount", accountViewModel);
            }
        }
        private async Task CreateUser(string userName, string email, string password, string role)
        {
            var user = new IdentityUser { UserName = userName, Email = email };
            await _userManager.CreateAsync(user, password);
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, role));
        }
    }
}
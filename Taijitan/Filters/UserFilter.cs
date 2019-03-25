using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Filters
{
    public class UserFilter : ActionFilterAttribute
    {
        private readonly IUserRepository _userRepository;

        public UserFilter(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {

            context.ActionArguments["User"] = context.HttpContext.User.Identity.IsAuthenticated ?
                _userRepository.GetByEmail(context.HttpContext.User.Identity.Name) : null;
            base.OnActionExecuting(context);
        }
        
    }
}

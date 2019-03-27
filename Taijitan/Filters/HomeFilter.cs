using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Filters
{
    public class HomeFilter : ActionFilterAttribute
    {
        private ICollection<Comment> _notifications;
        private Session _session;
        private readonly ICommentRepository _commentRepository;

        public HomeFilter(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _notifications = ReadNotificationsFromSession(context.HttpContext);
            context.ActionArguments["notifications"] = _notifications;
            System.Security.Claims.ClaimsPrincipal user = context.HttpContext.User;
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            WriteNotificationsToSession(_notifications, context.HttpContext);
            base.OnActionExecuted(context);
        }

        private ICollection<Comment> ReadNotificationsFromSession(HttpContext context)
        {
            if(context.Session.GetString("Notifications") == null)
            {
                var notifications = _commentRepository.GetAll().Where(c => !c.IsRead).ToList();
                int aantal = 5 - notifications.Count();
                aantal = aantal < 0 ? 0 : aantal;
                var extraComments = _commentRepository.GetAll().Where(c => c.IsRead).Take(aantal);
                if (extraComments != null)
                {
                    foreach (Comment c in extraComments)
                    {
                        notifications.Add(c);
                    }
                }
                _notifications = notifications.OrderByDescending(c => c.DateCreated).ToList(); ;
            } else
            {
                _notifications = JsonConvert.DeserializeObject<ICollection<Comment>>(context.Session.GetString("Notifications")).OrderByDescending(c => c.DateCreated).ToList();
            }
            return _notifications;
        }
        private void WriteNotificationsToSession(ICollection<Comment> notifications, HttpContext context)
        {
            context.Session.SetString("Notifications", JsonConvert.SerializeObject(notifications));
        }
    }
}

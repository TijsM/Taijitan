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
    public class SessionFilter : ActionFilterAttribute
    {
        private Session _session;

        public SessionFilter()
        {
            
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _session = ReadSessionFromSession(context.HttpContext);
            context.ActionArguments["session"] = _session;
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            WriteSessionToSession(_session, context.HttpContext);
            base.OnActionExecuted(context);
        }

        private Session ReadSessionFromSession(HttpContext context)
        {
            Session cart = context.Session.GetString("session") == null ?
                new Session() : JsonConvert.DeserializeObject<Session>(context.Session.GetString("session"));
            return cart;
        }
        private void WriteSessionToSession(Session session, HttpContext context)
        {
            context.Session.SetString("session", JsonConvert.SerializeObject(session));
        }
    }
}

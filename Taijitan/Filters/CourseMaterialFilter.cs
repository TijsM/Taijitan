using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;
using Taijitan.Models.ViewModels;

namespace Taijitan.Filters
{
    public class CourseMaterialFilter : ActionFilterAttribute
    {
        private CourseMaterialViewModel _cmvm;

        public CourseMaterialFilter()
        {

        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _cmvm = ReadViewModelFromSession(context.HttpContext);
            context.ActionArguments["courseMaterialViewModel"] = _cmvm;
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            WriteViewModelToSession(_cmvm, context.HttpContext);
            base.OnActionExecuted(context);
        }

        private CourseMaterialViewModel ReadViewModelFromSession(HttpContext context)
        {
            return context.Session.GetString("CourseMaterialViewModel") == null ?
                new CourseMaterialViewModel() : JsonConvert.DeserializeObject<CourseMaterialViewModel>(context.Session.GetString("CourseMaterialViewModel"));
        }
        private void WriteViewModelToSession(CourseMaterialViewModel cmvm, HttpContext context)
        {
            context.Session.SetString("CourseMaterialViewModel", JsonConvert.SerializeObject(cmvm));
        }
    }
}

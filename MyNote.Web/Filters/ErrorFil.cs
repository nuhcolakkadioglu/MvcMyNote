using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace MyNote.Web.Filters
{
    public class ErrorFil : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.Controller.TempData["error"] = filterContext.Exception;
            filterContext.ExceptionHandled = true;
            filterContext.Result = new RedirectResult("/Home/ErrorFil");
        }
    }
}
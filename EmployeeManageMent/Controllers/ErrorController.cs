using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManageMent.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the page could not found";
                    ViewBag.Path = statusCodeResult.OriginalPath;
                    ViewBag.Qs = statusCodeResult.OriginalQueryString;
                    break;
            }
            return View("NotFound");
        }
        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var ExceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.ExcepTionPath = ExceptionHandlerPathFeature.Path;
            ViewBag.ExceptionMessage = ExceptionHandlerPathFeature.Error.Message;
            ViewBag.StackTrace = ExceptionHandlerPathFeature.Error.StackTrace;
            return View("Error");
        }
    }
}
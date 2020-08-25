using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }

        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resource you requested could not found (Status Code = 404)";
                    ViewBag.Path = statusCodeResult.OriginalPath;
                    ViewBag.ToString = statusCodeResult.ToString();
                    ViewBag.QueryStr = statusCodeResult.OriginalQueryString;
                    break;


            }

            return View("NotFound");
        }



        [AllowAnonymous]
        [Route("Error")]
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.ExceptionPath = exceptionHandlerPathFeature.Path;
            ViewBag.ExceptionMessage = exceptionHandlerPathFeature.Error.Message;
            ViewBag.ExceptionStackTrace = exceptionHandlerPathFeature.Error.StackTrace;
            ViewBag.ExceptionInnerException = exceptionHandlerPathFeature.Error.InnerException;
            ViewBag.ExceptionSource = exceptionHandlerPathFeature.Error.Source;

            logger.LogWarning($"This is an exception on path = {exceptionHandlerPathFeature.Path} &" +
                              $" Error = {exceptionHandlerPathFeature.Error}");

            return View("Error");
        }
    }
}

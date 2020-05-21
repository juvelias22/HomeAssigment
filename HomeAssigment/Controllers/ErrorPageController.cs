using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeAssigment.Controllers
{
    public class ErrorPageController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }
        public ActionResult Error(int statusCode, Exception exception)
        {
            Response.StatusCode = statusCode;
            var error = new Models.Error
            {
                StatusCode = statusCode.ToString(),
                StatusDescription = HttpWorkerRequest.GetStatusDescription(statusCode),
                Message = exception.Message,
                DateTime = DateTime.Now
            };
            return View(error);
        }

    }
}
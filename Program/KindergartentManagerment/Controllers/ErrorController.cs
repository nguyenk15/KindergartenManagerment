using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Web.Routing;
using System.Web;
using System.Web.Mvc;

namespace KindergartentManagerment.Controllers
{
    //
    // GET: /Error/
    public class ErrorController : Controller
    {
        public ActionResult NotFound()
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return View();
        }
        public ActionResult ServerError()
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return View();
            // Todo: Pass the exception into the view model, which you can make.
            //       That's an exercise, dear reader, for -you-.
            //       In case u want to pass it to the view, if you're admin, etc.
            // if (User.IsAdmin) // <-- I just made that up :) U get the idea...
            // {
            //     var exception = Server.GetLastError();
            //     // etc..
            // }
        }
        // Shhh .. secret test method .. ooOOooOooOOOooohhhhhhhh
        public ActionResult ThrowError()
        {
            throw new NotImplementedException("Pew ^ Pew");
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace RecipeBook.Web.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Problem()
        {
            int code = HttpContext.Response.StatusCode;
            var response = new HttpResponseMessage((HttpStatusCode)code);

            return View(response);
        }
    }
}

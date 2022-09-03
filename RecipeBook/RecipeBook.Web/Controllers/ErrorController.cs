using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;

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

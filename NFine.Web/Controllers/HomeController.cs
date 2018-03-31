using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NFine.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace NFine.Web.Controllers
{
    public class HomeController : BaseController
    {

       // [Authorize]
        public override ActionResult Index()
        {
            if (!string.IsNullOrWhiteSpace(HttpContext.Request.Query["ReturnUrl"].ToString()))
            {
                return Redirect(HttpContext.Request.Query["ReturnUrl"].ToString());
            }
            return View();
        }

        public IActionResult Default()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

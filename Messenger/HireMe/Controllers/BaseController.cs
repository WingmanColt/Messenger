using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HireMe.Models;
using Microsoft.AspNetCore.Mvc;

namespace HireMe.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            // return NotFound();
            return Redirect("./Identity/Messenger");
        }

        public IActionResult Errors(string code)
        {
            // return NotFound();
            return this.Content(code);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MvcMovie.Controllers
{
    public class HelloWorldController : Controller
    {   
        // GET: /HelloWorld

        public IActionResult Index()
        {
            return View();
        }

        // GET: /HelloWorld/Welcome

        public string Welcome2()
        {
           return "This is the default action for /Welcome/";


        }

        // This takes in parameters.  If you leave them out, numTimes will default to 1
        public string Welcome3(string name, int ID = 1)
        {
            // HtmlEncoder... projects against malicious attacks.  This will print this out on the screen
            return HtmlEncoder.Default.Encode($"Hello {name}, ID: {ID}");
        }

        // Add the URL parameters to the ViewData dictionary object.  This data will then be passed to the View template
        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["numTimes"] = numTimes;

            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCore_Encoders.Models;
using System.Text.Encodings.Web;

namespace AspNetCore_Encoders.Controllers
{
    public class HomeController : Controller
    {

        HtmlEncoder _htmlEncoder;
        JavaScriptEncoder _javaScriptEncoder;
        UrlEncoder _urlEncoder;

        public HomeController(HtmlEncoder htmlEncoder,
                              JavaScriptEncoder javascriptEncoder,
                              UrlEncoder urlEncoder)
        {
            // Use HTML, JavaScript and URL encoders via dependency injection (DI)

            _htmlEncoder = htmlEncoder;
            _javaScriptEncoder = javascriptEncoder;
            _urlEncoder = urlEncoder;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            // Simple code example for HTML encoder

            var example = "\"Quoted Value with spaces and &\"";
            var encodedValue = _htmlEncoder.Encode(example);

            ViewData["Title"] = encodedValue;

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

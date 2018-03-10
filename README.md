# ASP.NET Core code samples for preventing XSS

This repository contains C# samples that show how to use Encoders (HTML, Javascript & HTML) and how to implement Content Security Policies

## Explore the code samples

Take a minute to explore the repo. It contains short code snippets written in C# 

* **Snippets**: short reusable code blocks demonstrating how to protect against XSS attacks.

## About XSS

Cross-site scripting is one of the major and widespread vulnerability in Web Applications today. Also known as XSS, it is a vulnerability that allows an attacker to inject malicious code into the contents of a web page. The malicious code is usually injected in a form of HTML, CSS or javascript.

In one type of XSS attack, a vulnerable web page can then be viewed by the victim where the injected code executes in the browser. Once the code executes, the attacker can hijack a user’s session, redirect users to another web site, steal cookies, deface websites, etc. XSS can be prevented by properly escaping output (a.k.a. output encoding) and implement Content Security Policy [CSP] (https://www.owasp.org/index.php/Content_Security_Policy) 

Content Security Policy defines approved sources of content (whitelisting) that the browser may load. Output encoding on the other hand converts untrusted input into a safe form and into the proper context

See the [XSS (Cross Site Scripting) Prevention Cheat Sheet](https://www.owasp.org/index.php/XSS_(Cross_Site_Scripting)_Prevention_Cheat_Sheet#Bonus_Rule_.232:_Implement_Content_Security_Policy) for more details.

### Sample code #1

Simple code sample to implement CSP in your ASP.NET Core web application.

```cs
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            // CSP implementation starts here

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add(
                    "Content-Security-Policy",
                    "script-src 'self'; " +
                    "style-src 'self'; " +
                    "img-src 'self'");

                await next();
            });

            //

            app.UseStaticFiles();

            app.UseMvc();
        }
```


### Sample code #2
Simple code sample to output encode in your ASP.NET Core web application.

```cs
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


```

## Request other samples

Not finding a sample that demonstrates something you want? Please feel free to open an issue.
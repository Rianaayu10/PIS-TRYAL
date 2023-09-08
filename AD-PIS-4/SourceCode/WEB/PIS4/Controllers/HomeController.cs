using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PIS4.Models;
using System.Net.Http.Json;
using Newtonsoft.Json;
using NuGet.Common;
using Microsoft.Extensions.Primitives;
using Azure.Core;

namespace ColorAdmin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            if (Request.Cookies["jwtCookie"] is not null)
            {
                return View();
            }

            else
            {
                return RedirectToAction("UserLogin", "Login");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        
        }
    }
}

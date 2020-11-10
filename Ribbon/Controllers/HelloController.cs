using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Ribbon.Models;

namespace Ribbon.Controllers
{
    public class HelloController : Controller
    {
        private readonly IConfiguration _configuration;

        public HelloController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Developer()
        {
            var vm = new DeveloperDetailViewModel()
            {
                FirstName = _configuration["Developer:FirstName"],
                LastName = _configuration["Developer:LastName"]
            };

            return View(vm);
        }
    }
}

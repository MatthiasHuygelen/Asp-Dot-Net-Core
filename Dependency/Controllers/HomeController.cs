using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dependency.Models;
using Dependency.Services;

namespace Dependency.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScopedService scopedService;
        private readonly ISingletonService singletonService;
        private readonly ITransientService transientService;
        private readonly IScopedService scopedService2;
        private readonly ISingletonService singletonService2;
        private readonly ITransientService transientService2;
        private readonly IGuidService guidService;

        public HomeController(ILogger<HomeController> logger
            , IScopedService scopedService
            , ISingletonService singletonService
            , ITransientService transientService
            , IScopedService scopedService2
            , ISingletonService singletonService2
            , ITransientService transientService2
            , IGuidService guidService
            )
        {
            _logger = logger;
            this.scopedService = scopedService;
            this.singletonService = singletonService;
            this.transientService = transientService;
            this.scopedService2 = scopedService2;
            this.singletonService2 = singletonService2;
            this.transientService2 = transientService2;
            this.guidService = guidService;
        }

        public IActionResult Index()
        {
            var scoped = scopedService.ToonGuid();
            var scoped2 = scopedService2.ToonGuid();
            var guidser = guidService.ToonGuid();
            var trans = transientService.ToonGuid();
            var trans2 = transientService2.ToonGuid();
            var single = singletonService.ToonGuid();
            var single2 = singletonService2.ToonGuid();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

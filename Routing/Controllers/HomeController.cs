using Microsoft.AspNetCore.Mvc;

namespace Routing.Controllers
{
    [Route("")]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [Route("")]
        [Route("[action]")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("[action]")]
        public IActionResult Contact()
        {
            return View();
        }

        [Route("/important")]
        [Route("/privacy")]
        public IActionResult Privacy()
        {
            return View();
        }
    }
}

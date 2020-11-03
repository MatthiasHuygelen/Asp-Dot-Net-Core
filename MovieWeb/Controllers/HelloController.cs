using Microsoft.AspNetCore.Mvc;

namespace MovieWeb.Controllers
{
    public class HelloController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public string Welcome(string name, string achternaam)
        {
            return $"Hello {name} {achternaam}";
        }

        public IActionResult Today()
        {
            return View();
        }
    }
}

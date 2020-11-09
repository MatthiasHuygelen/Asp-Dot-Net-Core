using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contactenlijst.Database;
using Microsoft.AspNetCore.Mvc;

namespace Contactenlijst.Controllers
{
    public class ContactenController : Controller
    {
        private readonly IContactenDatabase _contactenDatabase;

        public ContactenController(IContactenDatabase contactenDatabase)
        {
            _contactenDatabase = contactenDatabase;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

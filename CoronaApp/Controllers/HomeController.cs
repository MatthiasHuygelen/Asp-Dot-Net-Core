using CoronaApp.Data;
using CoronaApp.Domain;
using CoronaApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Utility;

namespace CoronaApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<Company> _userManager;

        public HomeController(ApplicationDbContext dbContext , UserManager<Company> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var vm = new VisitCreateViewModel
            {
                Companies = _dbContext.Users.Select(x => new SelectListItem(x.CompanyName, x.Id)),
                Date = DateTime.Now
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] VisitCreateViewModel vm)
        {
            if (!TryValidateModel(vm))
            {
                return View(vm);
            }

            var company = _dbContext.Users.FirstOrDefault(x => x.Id == vm.CompanyId.ToString());

            _dbContext.Visits.Add(new Visit { Date = vm.Date, Company = company, Name = vm.Name });
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(ThankYou));
        }

        public IActionResult ThankYou()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Overview(DateTime? date)
        {
            (DateTime start, DateTime end) = date != null ?
                MonthUtil.GenerateStartEnd(date.Value.Year, date.Value.Month) :
                MonthUtil.GenerateStartEnd(DateTime.Now.Year, DateTime.Now.Month);

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var roles = await _userManager.GetRolesAsync(user);

            var visits = _dbContext.Visits.Include(x => x.Company)
                                          .Where(x => x.Date >= start)
                                          .Where(x => x.Date <= end)
                                          .Where(x => x.Company.Id == user.Id)
                                          .Select(x => new VisitListViewModel {Name = x.Name , Date = x.Date });

            return View(new OverviewViewModel { Date = start, Visits= visits });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

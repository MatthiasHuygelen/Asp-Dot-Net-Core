using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using UitgaveBeheer.Database;
using UitgaveBeheer.Domain;
using UitgaveBeheer.Models;

namespace UitgaveBeheer.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly string PhotoFolder = "PhotoFolder";
        private readonly IExpenseDatabase _expenseDatabase;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IConfiguration _configuration;

        public ExpensesController(IExpenseDatabase expenseDatabase, IWebHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            _expenseDatabase = expenseDatabase;
            _hostEnvironment = hostEnvironment;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            var vm = _expenseDatabase.GetExpenses()
                                     .Select(x =>
                                     new ExpenseListViewModel
                                     {
                                         Id = x.Id,
                                         Description = x.Description,
                                         Date = x.Date,
                                         Value = x.Value
                                     });

            return View(vm);
        }

        public IActionResult Detail([FromRoute] int id)
        {
            var expense = _expenseDatabase.GetExpense(id);

            return View(new ExpenseDetailViewModel
            {
                Description = expense.Description,
                Date = expense.Date,
                Value = expense.Value,
                Categorie = expense.Categorie,
                PhotoUrl = expense.PhotoUrl
            });
        }

        public IActionResult Create()
        {
            return View(new ExpenseCreateViewModel() { Date = DateTime.Now });
        }

        [HttpPost]
        public IActionResult Create([FromForm] ExpenseCreateViewModel vm)
        {
            if (!TryValidateModel(vm))
            {
                return View(vm);
            }

            var newExpense = new Expense
            {
                Description = vm.Description,
                Date = vm.Date,
                Value = vm.Value,
                Categorie = vm.Categorie
            };

            if (vm.Photo != null)
            {
                string uniqueFileName = UploadPhoto(vm.Photo);

                newExpense.PhotoUrl = $"/{_configuration[PhotoFolder]}/" + uniqueFileName;
            }

            _expenseDatabase.Insert(newExpense);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit([FromRoute] int id)
        {
            var expense = _expenseDatabase.GetExpense(id);

            return View(new ExpenseEditViewModel
            {
                Categorie = expense.Categorie,
                Value = expense.Value,
                Date = expense.Date,
                Description = expense.Description,
                PhotoUrl = expense.PhotoUrl
            });
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, ExpenseEditViewModel vm)
        {
            if (!TryValidateModel(vm))
            {
                return View(vm);
            }

            var contact = new Expense
            {
                Description = vm.Description,
                Date = vm.Date,
                Value = vm.Value,
                Categorie = vm.Categorie,
            };

            var dbExpense = _expenseDatabase.GetExpense(id);

            if (vm.Photo == null)
            {
                contact.PhotoUrl = dbExpense.PhotoUrl;
            }
            else
            {
                if (!string.IsNullOrEmpty(dbExpense.PhotoUrl))
                {
                    DeletePhoto(dbExpense.PhotoUrl);
                }

                string uniqueFileName = UploadPhoto(vm.Photo);

                contact.PhotoUrl = $"/{PhotoFolder}/" + uniqueFileName;
            }
            _expenseDatabase.Update(id, contact);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete([FromRoute] int id)
        {
            var expense = _expenseDatabase.GetExpense(id);

            return View(new ExpenseDeleteViewModel
            {
                Id = expense.Id,
                Description = $"{expense.Description} - {expense.Value}€ - {expense.Date}"
            });
        }

        [HttpPost]
        public IActionResult ConfirmDelete([FromRoute] int id)
        {
            var expense = _expenseDatabase.GetExpense(id);

            if (!string.IsNullOrEmpty(expense.PhotoUrl))
            {
                DeletePhoto(expense.PhotoUrl);
            }

            _expenseDatabase.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Month(DateTime? Date)
        {
            (DateTime start, DateTime end) = Date != null ?
                 GenerateMonthDateTimes(Date.Value.Year, Date.Value.Month) :
                 GenerateMonthDateTimes(DateTime.Now.Year, DateTime.Now.Month);

            var expenses = _expenseDatabase.GetExpenses().Where(x => x.Date >= start)
                                                         .Where(x => x.Date <= end)
                                                         .ToList();
            if (expenses.Any())
            {
                var highestExp = expenses.OrderByDescending(x => x.Value).FirstOrDefault();
                var lowestExp = expenses.OrderBy(x => x.Value).FirstOrDefault();
                var MostExpensiveDate = expenses.GroupBy(x => x.Date)
                                                .Select(x => new { Date = x.Key, Value = x.ToList().Sum(x => x.Value) })
                                                .OrderByDescending(x => x.Value)
                                                .FirstOrDefault();

                var groupedCategories = expenses.GroupBy(x => x.Categorie)
                                                .Select(x => new
                                                {
                                                    Categorie = x.Key,
                                                    Value = x.ToList().Sum(x => x.Value)
                                                });

                var MostExpensivCategory = groupedCategories.OrderByDescending(x => x.Value).FirstOrDefault();
                var CheapestCategory = groupedCategories.OrderBy(x => x.Value).FirstOrDefault();

                return View(new ExpenseMonthViewModel
                {
                    Date = start,
                    Highest = new ExpenseDto { Id = highestExp.Id, Description = highestExp.Description, Value = highestExp.Value },
                    Lowest = new ExpenseDto { Id = lowestExp.Id, Description = lowestExp.Description, Value = lowestExp.Value },
                    MostExpensiveDate = new ExpenseDto { Description = MostExpensiveDate.Date.ToShortDateString(), Value = MostExpensiveDate.Value },
                    MostExpensivCategory = new ExpenseDto { Description = MostExpensivCategory.Categorie, Value = MostExpensivCategory.Value },
                    CheapestCategory = new ExpenseDto { Description = CheapestCategory.Categorie, Value = CheapestCategory.Value }
                });
            }

            return View(new ExpenseMonthViewModel { Date = start });
        }

        private string UploadPhoto(IFormFile photo)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
            string pathName = Path.Combine(_hostEnvironment.WebRootPath, _configuration[PhotoFolder]);
            string fileNameWithPath = Path.Combine(pathName, uniqueFileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                photo.CopyTo(stream);
            }

            return uniqueFileName;
        }

        private void DeletePhoto(string photoUrl)
        {
            string path = Path.Combine(_hostEnvironment.WebRootPath, photoUrl.Substring(1));
            System.IO.File.Delete(path);
        }

        private (DateTime Start, DateTime End) GenerateMonthDateTimes(int year, int month)
        {
            return (new DateTime(year, month, 1), new DateTime(year, month, DateTime.DaysInMonth(year, month)));
        }
    }
}

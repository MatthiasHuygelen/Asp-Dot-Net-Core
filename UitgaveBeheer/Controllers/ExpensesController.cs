using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using UitgaveBeheer.Models;
using UitgaveBeheer.Services;

namespace UitgaveBeheer.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly IExpensesService _expensesService;
        private readonly IPhotoService _photoService;
        private readonly IMapper _mapper;

        public ExpensesController(IExpensesService expensesService, IPhotoService photoService, IMapper mapper)
        {
            _expensesService = expensesService;
            _photoService = photoService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var vm = _expensesService.GetMany()
                                     .Select(x =>
                                        new ExpenseListViewModel
                                        {
                                            Id = x.Id,
                                            Description = x.Description,
                                            Date = x.Date,
                                            Value = x.Value
                                        }
                                       );

            return View(vm);
        }

        public IActionResult Detail([FromRoute] int id)
        {
            var expense = _expensesService.GetById(id);
            return View(_mapper.Map<ExpenseDetailViewModel>(expense));
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

            var newExpense = new ExpenseDto
            {
                Description = vm.Description,
                Date = vm.Date,
                Value = vm.Value,
                Categorie = vm.Categorie
            };

            if (vm.Photo != null)
            {
                newExpense.PhotoUrl = _photoService.UploadPhoto(vm.Photo);
            }

            _expensesService.Create(newExpense);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit([FromRoute] int id)
        {
            var expense = _expensesService.GetById(id);

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

            var expense = new ExpenseDto
            {
                Description = vm.Description,
                Date = vm.Date,
                Value = vm.Value,
                Categorie = vm.Categorie,
            };

            var dbExpense = _expensesService.GetById(id);

            if (vm.Photo == null)
            {
                expense.PhotoUrl = dbExpense.PhotoUrl;
            }
            else
            {
                if (!string.IsNullOrEmpty(dbExpense.PhotoUrl))
                {
                    _photoService.DeletePhoto(dbExpense.PhotoUrl);
                }

                expense.PhotoUrl = _photoService.UploadPhoto(vm.Photo);
            }
            _expensesService.Update(id, expense);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete([FromRoute] int id)
        {
            var expense = _expensesService.GetById(id);

            return View(new ExpenseDeleteViewModel
            {
                Id = expense.Id,
                Description = $"{expense.Description} - {expense.Value}€ - {expense.Date}"
            });
        }

        [HttpPost]
        public IActionResult ConfirmDelete([FromRoute] int id)
        {
            var expense = _expensesService.GetById(id);

            if (!string.IsNullOrEmpty(expense.PhotoUrl))
            {
                _photoService.DeletePhoto(expense.PhotoUrl);
            }

            _expensesService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Month(DateTime? Date)
        {
            var monthDetail = _expensesService.GetMonthDetail(Date);

            return View(new ExpenseMonthViewModel
            {
                Date = monthDetail.Date,
                Highest = monthDetail.Highest != null ? new ExpenseVm { Id = monthDetail.Highest.Id, Description = monthDetail.Highest.Description, Value = monthDetail.Highest.Value }: null,
                Lowest = monthDetail.Lowest != null ? new ExpenseVm { Id = monthDetail.Lowest.Id, Description = monthDetail.Lowest.Description, Value = monthDetail.Lowest.Value } : null,
                MostExpensiveDate = monthDetail.MostExpensiveDate != null ? new ExpenseVm { Description = monthDetail.MostExpensiveDate.Date.ToShortDateString(), Value = monthDetail.MostExpensiveDate.Value } : null,
                MostExpensivCategory = monthDetail.MostExpensivCategory != null ? new ExpenseVm { Description = monthDetail.MostExpensivCategory.Categorie, Value = monthDetail.MostExpensivCategory.Value } : null,
                CheapestCategory = monthDetail.CheapestCategory != null ? new ExpenseVm { Description = monthDetail.CheapestCategory.Categorie, Value = monthDetail.CheapestCategory.Value } : null
            });
        }

    }
}

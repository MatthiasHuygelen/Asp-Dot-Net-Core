using System;
using System.Collections.Generic;
using System.Linq;
using UitgaveBeheer.Database;
using UitgaveBeheer.Domain;
using UitgaveBeheer.Models;
using UitgaveBeheer.Util;

namespace UitgaveBeheer.Services
{
    public class ExpensesService : IExpensesService
    {
        private readonly IExpenseDatabase _expenseDatabase;

        public ExpensesService(IExpenseDatabase expenseDatabase)
        {
            _expenseDatabase = expenseDatabase;
        }

        public ExpenseDetailDto GetById(int id)
        {
            var expense = _expenseDatabase.GetExpense(id);

            return new ExpenseDetailDto
            {
                Id = expense.Id,
                Description = expense.Description,
                Date = expense.Date,
                Value = expense.Value,
                Categorie = expense.Categorie.Description,
                PhotoUrl = expense.PhotoUrl
            };
        }

        public IEnumerable<ExpenseListDto> GetMany()
        {
            return _expenseDatabase.GetExpenses()
                                   .Select(x =>
                                   new ExpenseListDto
                                   {
                                       Id = x.Id,
                                       Description = x.Description,
                                       Date = x.Date,
                                       Value = x.Value
                                   });
        }

        public void Create(ExpenseDto expense)
        {
            var newExpense = new Expense
            {
                Description = expense.Description,
                Date = expense.Date,
                Value = expense.Value,
                PhotoUrl = expense.PhotoUrl
            };

            _expenseDatabase.Insert(newExpense);
        }

        public void Update(int id, ExpenseDto expense)
        {
            var updatedExpense = new Expense
            {
                Description = expense.Description,
                Date = expense.Date,
                Value = expense.Value,
                PhotoUrl = expense.PhotoUrl
            };

            _expenseDatabase.Update(id, updatedExpense);
        }

        public void Delete(int id)
        {
            _expenseDatabase.Delete(id);
        }

        public ExpenseMonthDto GetMonthDetail(DateTime? date)
        {
            (DateTime start, DateTime end) = date != null ?
                 DateUtil.GenerateMonthDateTimes(date.Value.Year, date.Value.Month) :
                 DateUtil.GenerateMonthDateTimes(DateTime.Now.Year, DateTime.Now.Month);

            var expenses = _expenseDatabase.GetExpenses().Where(x => x.Date >= start)
                                                         .Where(x => x.Date <= end)
                                                         .ToList();

            if (expenses.Any())
            {
                var highestExp = expenses.OrderByDescending(x => x.Value).FirstOrDefault();
                var lowestExp = expenses.OrderBy(x => x.Value).FirstOrDefault();
                var MostExpensiveDate = expenses.GroupBy(x => x.Date)
                                                .Select(x => new
                                                {
                                                    Date = x.Key,
                                                    Value = x.ToList().Sum(x => x.Value)
                                                })
                                                .OrderByDescending(x => x.Value)
                                                .FirstOrDefault();

                var groupedCategories = expenses.GroupBy(x => x.Categorie.Description)
                                                .Select(x => new
                                                {
                                                    Categorie = x.Key,
                                                    Value = x.ToList().Sum(x => x.Value)
                                                });

                var MostExpensivCategory = groupedCategories.OrderByDescending(x => x.Value).FirstOrDefault();
                var CheapestCategory = groupedCategories.OrderBy(x => x.Value).FirstOrDefault();

                return new ExpenseMonthDto
                {
                    Date = start,
                    Highest = new ExpenseDetailDto { Id = highestExp.Id, Description = highestExp.Description, Value = highestExp.Value },
                    Lowest = new ExpenseDetailDto { Id = lowestExp.Id, Description = lowestExp.Description, Value = lowestExp.Value },
                    MostExpensiveDate = new ExpenseDetailDto { Description = MostExpensiveDate.Date.ToString(), Value = MostExpensiveDate.Value },
                    MostExpensivCategory = new ExpenseDetailDto { Description = MostExpensivCategory.Categorie, Value = MostExpensivCategory.Value },
                    CheapestCategory = new ExpenseDetailDto { Description = CheapestCategory.Categorie, Value = CheapestCategory.Value },
                };
            }
            return new ExpenseMonthDto { Date = start };
        }
    }
}

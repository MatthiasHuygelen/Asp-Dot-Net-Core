using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UitgaveBeheer.Domain;

namespace UitgaveBeheer.Database
{

    public interface IExpenseDatabase
    {
        Expense Insert(Expense Expense);
        IEnumerable<Expense> GetExpenses();
        Expense GetExpense(int id);
        void Delete(int id);
        void Update(int id, Expense Expense);
    }

    public class ExpenseDatabase : IExpenseDatabase
    {
        private readonly ExpenseDbContext _expenseDbContext;

        public ExpenseDatabase(ExpenseDbContext expenseDbContext)
        {
            _expenseDbContext = expenseDbContext;
        }

        public Expense GetExpense(int id)
        {
            return _expenseDbContext.Expenses.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Expense> GetExpenses()
        {
            return _expenseDbContext.Expenses.ToList();
        }

        public Expense Insert(Expense Expense)
        {
            _counter++;
            Expense.Id = _counter;
            _Expenses.Add(Expense);
            return Expense;
        }

        public void Delete(int id)
        {
            var Expense = _Expenses.SingleOrDefault(x => x.Id == id);
            if (Expense != null)
            {
                _Expenses.Remove(Expense);
            }
        }

        public void Update(int id, Expense updatedExpense)
        {
            var Expense = _Expenses.SingleOrDefault(x => x.Id == id);
            if (Expense != null)
            {
                Expense.Description = updatedExpense.Description;
                Expense.Date = updatedExpense.Date;
                Expense.Value = updatedExpense.Value;
                Expense.Categorie = updatedExpense.Categorie;
                Expense.PhotoUrl = updatedExpense.PhotoUrl;
            }
        }
    }
}

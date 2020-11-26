using System.Collections.Generic;
using System.Linq;
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
            _expenseDbContext.Expenses.Add(Expense);
            _expenseDbContext.SaveChanges();
            return Expense;
        }

        public void Delete(int id)
        {
            var Expense = _expenseDbContext.Expenses.SingleOrDefault(x => x.Id == id);
            if (Expense != null)
            {
                _expenseDbContext.Expenses.Remove(Expense);
                _expenseDbContext.SaveChanges();
            }
        }

        public void Update(int id, Expense updatedExpense)
        {
            var Expense = _expenseDbContext.Expenses.SingleOrDefault(x => x.Id == id);
            if (Expense != null)
            {
                Expense.Description = updatedExpense.Description;
                Expense.Date = updatedExpense.Date;
                Expense.Value = updatedExpense.Value;
                Expense.Categorie = updatedExpense.Categorie;
                Expense.PhotoUrl = updatedExpense.PhotoUrl;
                _expenseDbContext.SaveChanges();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UitgaveBeheer.Models;

namespace UitgaveBeheer.Services
{
    public interface IExpensesService
    {
        ExpenseDetailDto GetById(int id);
        IEnumerable<ExpenseListDto> GetMany();
        void Create(ExpenseDto expense);
        void Update(int id, ExpenseDto expense);
        void Delete(int id);
        ExpenseMonthDto GetMonthDetail(DateTime? date);
    }
}

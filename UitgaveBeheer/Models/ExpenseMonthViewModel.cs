using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UitgaveBeheer.Models
{
    public class ExpenseMonthViewModel
    {
        public DateTime Date { get; set; }
        public ExpenseDto Highest { get; set; }
        public ExpenseDto Lowest { get; set; }
        public ExpenseDto MostExpensiveDate { get; set; }
        public ExpenseDto MostExpensivCategory { get; set; }
        public ExpenseDto CheapestCategory { get; set; }
    }
}

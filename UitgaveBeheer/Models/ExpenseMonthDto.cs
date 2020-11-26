﻿using System;

namespace UitgaveBeheer.Models
{
    public class ExpenseMonthDto
    {
        public DateTime Date { get; set; }
        public ExpenseDetailDto Highest { get; set; }
        public ExpenseDetailDto Lowest { get; set; }
        public ExpenseDetailDto MostExpensiveDate { get; set; }
        public ExpenseDetailDto MostExpensivCategory { get; set; }
        public ExpenseDetailDto CheapestCategory { get; set; }
    }
}

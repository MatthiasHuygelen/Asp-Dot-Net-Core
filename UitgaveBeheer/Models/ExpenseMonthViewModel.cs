﻿using System;

namespace UitgaveBeheer.Models
{
    public class ExpenseMonthViewModel
    {
        public DateTime Date { get; set; }
        public ExpenseVm Highest { get; set; }
        public ExpenseVm Lowest { get; set; }
        public ExpenseVm MostExpensiveDate { get; set; }
        public ExpenseVm MostExpensivCategory { get; set; }
        public ExpenseVm CheapestCategory { get; set; }
    }
}

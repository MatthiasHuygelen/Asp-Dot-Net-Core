﻿using System;

namespace UitgaveBeheer.Models
{
    public class ExpenseListViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
    }
}

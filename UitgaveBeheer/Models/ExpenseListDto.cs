﻿using System;

namespace UitgaveBeheer.Models
{
    public class ExpenseListDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
    }
}

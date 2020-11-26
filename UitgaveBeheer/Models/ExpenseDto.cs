using System;

namespace UitgaveBeheer.Models
{
    public class ExpenseDto
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public string Categorie { get; set; }
        public string PhotoUrl { get; set; }
    }
}

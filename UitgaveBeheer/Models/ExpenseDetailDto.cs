using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UitgaveBeheer.Models
{
    public class ExpenseDetailDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public string Categorie { get; set; }
        public string PhotoUrl { get; set; }
    }
}

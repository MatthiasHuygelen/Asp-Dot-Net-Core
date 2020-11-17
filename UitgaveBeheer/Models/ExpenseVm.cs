using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UitgaveBeheer.Models
{
    public class ExpenseVm
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
    }
}

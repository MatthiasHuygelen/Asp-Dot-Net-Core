using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaApp.Models
{
    public class OverviewViewModel
    {
        public DateTime Date { get; set; }
        public IEnumerable<VisitListViewModel> Visits { get; set; }
    }
}

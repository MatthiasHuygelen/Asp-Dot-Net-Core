using System;
using System.Collections.Generic;

namespace CoronaApp.Models
{
    public class OverviewViewModel
    {
        public DateTime Date { get; set; }
        public IEnumerable<VisitListViewModel> Visits { get; set; }
    }
}

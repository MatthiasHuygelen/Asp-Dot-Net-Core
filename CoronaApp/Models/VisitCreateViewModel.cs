using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaApp.Models
{
    public class VisitCreateViewModel
    {
        [DisplayName("Naam")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Bedrijf")]
        [Required]
        public Guid CompanyId { get; set; }

        [DisplayName("Datum")]
        [Required ,DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        public IEnumerable<SelectListItem> Companies { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace UitgaveBeheer.Models
{
    public class ExpenseEditViewModel
    {
        [DisplayName("Omschrijving")]
        public string Description { get; set; }
        [DisplayName("bedrag")]
        public decimal Value { get; set; }
        [DisplayName("Datum")]
        public DateTime Date { get; set; }
        [DisplayName("Categorie")]
        public string Categorie { get; set; }
        [DisplayName("Foto")]
        public IFormFile Photo { get; set; }
        public string PhotoUrl { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>
        {
            new SelectListItem{ Text="Voeding" , Value = "Voeding"},
            new SelectListItem{ Text="Woonlasten" , Value = "Woonlasten"},
            new SelectListItem{ Text="School" , Value = "School"},
            new SelectListItem{ Text="Abonnementen " , Value = "Abonnementen"},
        };
    }
}

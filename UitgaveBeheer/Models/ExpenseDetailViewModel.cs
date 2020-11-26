using System;
using System.ComponentModel;

namespace UitgaveBeheer.Models
{
    public class ExpenseDetailViewModel
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
        public string PhotoUrl { get; set; }
    }
}

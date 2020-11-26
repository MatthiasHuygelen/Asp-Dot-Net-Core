using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UitgaveBeheer.Domain
{
    public class Expense
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public string PhotoUrl { get; set; }
        public string Actor { get; set; }

        [ForeignKey(nameof(Categorie))]
        public int CategorieId { get; set; }
        public Categorie Categorie { get; set; }
    }
}

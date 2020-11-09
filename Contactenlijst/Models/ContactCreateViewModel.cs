using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Contactenlijst.Models
{
    public class ContactCreateViewModel
    {
        [DisplayName("Naam")]
        public string Naam { get; set; }
        [DisplayName("Voornaam")]
        public string Voornaam { get; set; }
        [DisplayName("Geboorte datum")]
        [DataType(DataType.Date)]
        public DateTime Geboortedatum { get; set; }
        [DisplayName("Email")]
        [EmailAddress]
        public string Email { get; set; }
        [DisplayName("Telefoon nummer")]
        [Phone]
        public string TelefoonNr { get; set; }
        [DisplayName("Adres")]
        public string Adres { get; set; }
        [DisplayName("Beschrijving")]
        public string Beschrijving { get; set; }
    }
}

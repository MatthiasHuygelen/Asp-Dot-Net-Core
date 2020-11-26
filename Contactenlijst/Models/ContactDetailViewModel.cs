using System;
using System.ComponentModel;

namespace Contactenlijst.Models
{
    public class ContactDetailViewModel
    {
        [DisplayName("Naam")]
        public string FullName { get; set; }
        [DisplayName("Adres")]
        public string Adress { get; set; }
        public DateTime Birthdate { get; set; }
        [DisplayName("Telefoon")]
        public string Phone { get; set; }
        public string Email { get; set; }
        [DisplayName("Beschrijving")]
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Contactenlijst.Models
{
    public class ContactEditViewModel
    {
        [DisplayName("Naam")]
        public string LastName { get; set; }
        [DisplayName("Voornaam")]
        public string FirstName { get; set; }
        [DisplayName("Geboortedatum")]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
        [DisplayName("Email")]
        [EmailAddress]
        public string Email { get; set; }
        [DisplayName("Telefoon nummer")]
        [Phone]
        public string Phone { get; set; }
        [DisplayName("Adres")]
        public string Adress { get; set; }
        [DisplayName("Beschrijving")]
        public string Description { get; set; }
        [DisplayName("Foto")]
        public IFormFile Photo { get; set; }
        public string PhotoUrl { get; set; }
    }
}

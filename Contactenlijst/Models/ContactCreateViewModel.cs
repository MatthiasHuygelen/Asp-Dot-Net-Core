using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        [DisplayName("Categorie")]
        public string Category { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>()
        {
            new SelectListItem(){Text="Familie", Value="Familie" },
            new SelectListItem(){Text="Vriend", Value="Vriend" },
            new SelectListItem(){Text="Vijand", Value="Vijand" }
        };
    }
}

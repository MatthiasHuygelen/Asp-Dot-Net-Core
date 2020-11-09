using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contactenlijst.Database;
using Contactenlijst.Domain;
using Contactenlijst.Models;
using Microsoft.AspNetCore.Mvc;

namespace Contactenlijst.Controllers
{
    public class ContactenController : Controller
    {
        private readonly IContactenDatabase _contactenDatabase;

        public ContactenController(IContactenDatabase contactenDatabase)
        {
            _contactenDatabase = contactenDatabase;
        }

        public IActionResult Index()
        {
            var vm = _contactenDatabase.GetContacts()
                .Select(contact => new ContactListViewModel { 
                    Id = contact.Id,
                    FullName = $"{contact.Naam} {contact.Voornaam}"
                });


            return View(vm);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ContactCreateViewModel contact)
        {
            if (!TryValidateModel(contact))
            {
                return View(contact);
            }

            Contact newContact = new Contact()
            {
                Naam = contact.Naam,
                Voornaam = contact.Voornaam,
                Geboortedatum = contact.Geboortedatum,
                Email = contact.Email,
                TelefoonNr = contact.TelefoonNr,
                Adres = contact.Adres,
                Beschrijving = contact.Beschrijving
            };
            _contactenDatabase.Insert(newContact);
            return RedirectToAction(nameof(Index));
        }
    }
}

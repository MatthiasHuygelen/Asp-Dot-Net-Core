using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Contactenlijst.Database;
using Contactenlijst.Domain;
using Contactenlijst.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Contactenlijst.Controllers
{
    public class ContactenController : Controller
    {
        private readonly IContactenDatabase _contactenDatabase;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ContactenController(IContactenDatabase contactenDatabase, IWebHostEnvironment hostEnvironment)
        {
            _contactenDatabase = contactenDatabase;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            var vm = _contactenDatabase.GetContacts()
                .Select(contact => new ContactListViewModel
                {
                    Id = contact.Id,
                    FullName = $"{contact.FirstName} {contact.LastName}"
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
                FirstName = contact.LastName,
                LastName = contact.FirstName,
                Birthdate = contact.Birthdate,
                Email = contact.Email,
                Phone = contact.Phone,
                Adress = contact.Adress,
                Description = contact.Description
            };

            if (contact.Photo != null)
            {
                string uniqueFileName = UploadPhoto(contact);

                newContact.PhotoUrl = "/photos/" + uniqueFileName;
            }

            _contactenDatabase.Insert(newContact);
            return RedirectToAction(nameof(Index));
        }

        private string UploadPhoto(ContactCreateViewModel contact)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(contact.Photo.FileName);
            string pathName = Path.Combine(_hostEnvironment.WebRootPath, "photos");
            string fileNameWithPath = Path.Combine(pathName, uniqueFileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                contact.Photo.CopyTo(stream);
            }

            return uniqueFileName;
        }

        public IActionResult Detail([FromRoute] int id)
        {
            var contact = _contactenDatabase.GetContact(id);

            return View(new ContactDetailViewModel
            {
                FullName = $"{contact.FirstName} {contact.LastName}",
                Adress = contact.Adress,
                Birthdate = contact.Birthdate,
                Email = contact.Email,
                Phone = contact.Phone,
                Description = contact.Description,
                PhotoUrl = contact.PhotoUrl
            });
        }

        public IActionResult Edit([FromRoute] int id)
        {
            var contact = _contactenDatabase.GetContact(id);

            return View(new ContactEditViewModel
            {
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Adress = contact.Adress,
                Birthdate = contact.Birthdate,
                Email = contact.Email,
                Phone = contact.Phone,
                Description = contact.Description
            });
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, ContactEditViewModel vm)
        {
            if (!TryValidateModel(vm))
            {
                return View();
            }

            _contactenDatabase.Update(id, new Contact
            {
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Adress = vm.Adress,
                Birthdate = vm.Birthdate,
                Email = vm.Email,
                Phone = vm.Phone,
                Description = vm.Description
            });

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete([FromRoute] int id)
        {
            var contact = _contactenDatabase.GetContact(id);

            return View(new ContactDeleteViewModel
            {
                Id = contact.Id,
                FullName = $"{contact.FirstName} {contact.LastName}"
            });
        }

        [HttpPost]
        public IActionResult ConfirmDelete([FromRoute] int id)
        {
            _contactenDatabase.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

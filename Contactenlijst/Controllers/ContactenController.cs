using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Contactenlijst.Database;
using Contactenlijst.Domain;
using Contactenlijst.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
            throw new DivideByZeroException();
            var vm = _contactenDatabase.GetContacts()
                .Select(contact => new ContactListViewModel
                {
                    Id = contact.Id,
                    FullName = $"{contact.FirstName} {contact.LastName}",
                    Category = contact.Category
                });


            return View(vm);
        }

        public IActionResult Create()
        {
            return View(new ContactCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                Description = contact.Description,
                Category = contact.Category
            };

            if (contact.Photo != null)
            {
                string uniqueFileName = UploadPhoto(contact.Photo);

                newContact.PhotoUrl = "/photos/" + uniqueFileName;
            }

            _contactenDatabase.Insert(newContact);
            return RedirectToAction(nameof(Index));
        }

        private string UploadPhoto(IFormFile photo)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
            string pathName = Path.Combine(_hostEnvironment.WebRootPath, "photos");
            string fileNameWithPath = Path.Combine(pathName, uniqueFileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                photo.CopyTo(stream);
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
                Description = contact.Description,
                PhotoUrl = contact.PhotoUrl
            });
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, ContactEditViewModel vm)
        {
            if (!TryValidateModel(vm))
            {
                return View();
            }

            var contact = new Contact
            {
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Adress = vm.Adress,
                Birthdate = vm.Birthdate,
                Email = vm.Email,
                Phone = vm.Phone,
                Description = vm.Description
            };

            var dbContact = _contactenDatabase.GetContact(id);

            if (vm.Photo == null)
            {
                contact.PhotoUrl = dbContact.PhotoUrl;
            }
            else
            {
                if (!string.IsNullOrEmpty(dbContact.PhotoUrl))
                {
                    DeletePhoto(dbContact.PhotoUrl);
                }

                string uniqueFileName = UploadPhoto(vm.Photo);

                contact.PhotoUrl = "/photos/" + uniqueFileName;
            }
            _contactenDatabase.Update(id, contact);

            return RedirectToAction(nameof(Index));
        }

        private void DeletePhoto(string photoUrl)
        {
            string path = Path.Combine(_hostEnvironment.WebRootPath, photoUrl.Substring(1));
            System.IO.File.Delete(path);
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
            var contact = _contactenDatabase.GetContact(id);

            if (!string.IsNullOrEmpty(contact.PhotoUrl))
            {
                DeletePhoto(contact.PhotoUrl);
            }

            _contactenDatabase.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

using Contactenlijst.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contactenlijst.Database
{

    public interface IContactenDatabase
    {
        Contact Insert(Contact Contact);
        IEnumerable<Contact> GetContacts();
        Contact GetContact(int id);
        void Delete(int id);
        void Update(int id, Contact Contact);
    }

    public class ContactenDatabase : IContactenDatabase
    {
        private int _counter;
        private readonly List<Contact> _Contacts;

        public ContactenDatabase()
        {
            if (_Contacts == null)
            {
                _Contacts = new List<Contact>();
            }
        }

        public Contact GetContact(int id)
        {
            return _Contacts.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Contact> GetContacts()
        {
            return _Contacts;
        }

        public Contact Insert(Contact Contact)
        {
            _counter++;
            Contact.Id = _counter;
            _Contacts.Add(Contact);
            return Contact;
        }

        public void Delete(int id)
        {
            var Contact = _Contacts.SingleOrDefault(x => x.Id == id);
            if (Contact != null)
            {
                _Contacts.Remove(Contact);
            }
        }

        public void Update(int id, Contact updatedContact)
        {
            var Contact = _Contacts.SingleOrDefault(x => x.Id == id);
            if (Contact != null)
            {
                Contact.FirstName = updatedContact.FirstName;
                Contact.LastName = updatedContact.LastName;
                Contact.Phone = updatedContact.Phone;
                Contact.Birthdate = updatedContact.Birthdate;
                Contact.Email = updatedContact.Email;
                Contact.Adress = updatedContact.Adress;
                Contact.Description = updatedContact.Description;
                Contact.PhotoUrl = updatedContact.PhotoUrl;
                Contact.Category = updatedContact.Category;
            }
        }
    }
}

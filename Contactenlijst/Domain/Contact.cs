using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contactenlijst.Domain
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Adress { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public string Category { get; set; }
    }
}

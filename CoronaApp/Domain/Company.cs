using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CoronaApp.Domain
{
    public class Company : IdentityUser
    {
        public string CompanyName { get; set; }
        public List<Visit> Visits { get; set; }
    }
}

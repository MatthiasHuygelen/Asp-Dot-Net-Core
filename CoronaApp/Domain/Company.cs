using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaApp.Domain
{
    public class Company : IdentityUser
    {
        public string CompanyName { get; set; }
        public List<Visit> Visits { get; set; }
    }
}

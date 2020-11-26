using CoronaApp.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoronaApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<Company>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Visit> Visits { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using UitgaveBeheer.Domain;

namespace UitgaveBeheer.Database
{
    public class ExpenseDbContext : DbContext
    {
        public ExpenseDbContext(DbContextOptions<ExpenseDbContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categorie>().HasData(new Categorie { Id=1 , Description = "Voedsel" });
            modelBuilder.Entity<Categorie>().HasData(new Categorie { Id = 2, Description = "School" });
        }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Categorie> Categories { get; set; }
    }
}

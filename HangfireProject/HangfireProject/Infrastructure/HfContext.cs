using Microsoft.EntityFrameworkCore;

namespace HangfireProject.Infrastructure
{
    public class HfContext : DbContext
    {
        public DbSet<Entities.Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (Environment.GetEnvironmentVariable("DB_CONN") != null)
            {
                optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("DB_CONN"), sqlServerOptionsAction: option =>
                {
                    option.EnableRetryOnFailure(2, TimeSpan.FromSeconds(5), null);
                });
            }
            else
                optionsBuilder.UseInMemoryDatabase("FinancialAccountInMemory");

            base.OnConfiguring(optionsBuilder);
        }
    }
}

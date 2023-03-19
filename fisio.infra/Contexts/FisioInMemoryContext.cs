using Microsoft.EntityFrameworkCore;
using fisio.domain.Entities;

namespace fisio.infra.Contexts
{
    public class FisioInMemoryContext : DbContext
    {
        public FisioInMemoryContext(DbContextOptions<FisioInMemoryContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseInMemoryDatabase("FisioInMemoryDatabase");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User("user@email.com", BCrypt.Net.BCrypt.HashPassword("password"), true, "adm"));
        }

        public DbSet<User> Users { get; private set; }
        public DbSet<Patient> Patients { get; private set; }
    }
}
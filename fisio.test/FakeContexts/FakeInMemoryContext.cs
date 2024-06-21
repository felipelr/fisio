using fisio.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace fisio.test.FakeContexts
{
    internal class FakeInMemoryContext : DbContext
    {
        public FakeInMemoryContext(DbContextOptions<FakeInMemoryContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseInMemoryDatabase("FisioInMemoryDatabase");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User("user@email.com", BCrypt.Net.BCrypt.HashPassword("password"), true, "adm"));
        }

        public DbSet<User> Users { get; private set; }
        public DbSet<RefreshToken> RefreshTokens { get; private set; }
        public DbSet<Patient> Patients { get; private set; }
    }

    internal class FakeInMemoryContextFactory
    {

        public static FakeInMemoryContext Create()
        {
            var builder = new DbContextOptionsBuilder<FakeInMemoryContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new FakeInMemoryContext(builder.Options);

            return context;
        }
    }
}

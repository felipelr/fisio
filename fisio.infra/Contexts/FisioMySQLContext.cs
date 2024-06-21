using Microsoft.EntityFrameworkCore;
using fisio.domain.Entities;

namespace fisio.infra.Contexts
{
    public class FisioMySQLContext : DbContext
    {
        public FisioMySQLContext(DbContextOptions<FisioMySQLContext> options) : base(options) { }

        public DbSet<User> Users { get; private set; }
        public DbSet<RefreshToken> RefreshTokens { get; private set; }
        public DbSet<Patient> Patients { get; private set; }
    }
}
using fisio.domain.Repositories;
using fisio.domain.UnitOfWork;
using fisio.infra.Contexts;
using fisio.infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace fisio.infra.ServiceModules
{
    public static class FisioInfraRegistryServices
    {
        public static IServiceCollection AddFisioInfraDependencies(
             this IServiceCollection services,
             IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("mysqlConnection");
            var serverVersion = new MySqlServerVersion(new Version(5, 7, 32));
            services.AddDbContext<FisioMySQLContext>(options => options.UseMySql(connectionString, serverVersion));
            services.AddDbContext<FisioInMemoryContext>();
            services.AddScoped<IUnitOfWork, fisio.infra.UnitOfWork.UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            return services;
        }
    }
}

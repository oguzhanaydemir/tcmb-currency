using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Protein.Tech.TCMB.Core.Application.Interfaces.Repositories;
using Protein.Tech.TCMB.Infrastructure.Persistence.Context;
using Protein.Tech.TCMB.Infrastructure.Persistence.Repositories;
using Protein.Tech.TCMB.Infrastructure.Persistence.Services;

namespace Protein.Tech.TCMB.Infrastructure.Persistence
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("Default")))
            .AddTransient<ICurrencyRepository, CurrencyRepository>()
            .AddTransient<ICurrencyRateRepository, CurrencyRateRepository>()
            .AddScoped<ITodayJob, TodayJob>();

        }
    }
}

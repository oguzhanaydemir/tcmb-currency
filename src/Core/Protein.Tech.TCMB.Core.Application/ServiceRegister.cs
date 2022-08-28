using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Protein.Tech.TCMB.Core.Application
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            return services.AddMediatR(assembly)
                           .AddAutoMapper(assembly);
        }
    }
}

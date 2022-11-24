using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyResolver;

public class DependencyResolverService
{
    public static void RegisterInfrastructure(IServiceCollection services)
    {
        services.AddScoped<IPetsRepository, IPetsRepository>();
    }
} 
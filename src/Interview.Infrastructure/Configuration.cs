using Interview.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Interview.Infrastructure;

public static class Configuration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<InterviewContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("Default")));
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        return services;
    }

    public static async Task EnsureCreated(this IServiceProvider serviceProvider)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<InterviewContext>();
        await context.Database.EnsureCreatedAsync();
    }
}
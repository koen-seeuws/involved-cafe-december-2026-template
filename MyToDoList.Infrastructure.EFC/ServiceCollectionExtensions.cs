using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyToDoList.Application.Abstractions;
using MyToDoList.Application.Abstractions.Persistence;
using MyToDoList.Core.Domain;
using MyToDoList.Infrastructure.EFC.Repositories;

namespace MyToDoList.Infrastructure.EFC;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSqlLite(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ToDoDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("Database")));
        
        services.AddRepositories();
        services.AddQueryHandlers();

        return services;
    }
    
    public static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ToDoDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Database")));
        
        services.AddRepositories();
        services.AddQueryHandlers();

        return services;
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRepository<ToDoList, string>, ToDoListBaseRepository>();

        return services;
    }

    private static IServiceCollection AddQueryHandlers(this IServiceCollection services)
    {
        var registrations = typeof(ServiceCollectionExtensions).Assembly.GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false, IsGenericTypeDefinition: false })
            .SelectMany(t => t.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>))
                .Select(i => (Service: i, Implementation: t)));

        foreach (var (service, implementation) in registrations)
            services.AddScoped(service, implementation);

        return services;
    }
}

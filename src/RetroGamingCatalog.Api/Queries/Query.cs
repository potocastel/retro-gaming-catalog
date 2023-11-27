using System.Reflection;

namespace RetroGamingCatalog.Api.Queries;

public interface IQuery{}

public static class QueriesExtension
{
    public static IServiceCollection AddQueries(this IServiceCollection service)
    {
        var queryTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => !t.IsAbstract && t.GetInterfaces().Any(ti=>ti == typeof(IQuery)));

        foreach (var query in queryTypes)
        {
            service.AddScoped(query);
        }
        return service;
    }
}
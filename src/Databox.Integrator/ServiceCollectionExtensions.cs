using Microsoft.Extensions.DependencyInjection;

namespace Databox.Integrator.Contracts;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGithubIntegrator(this IServiceCollection services)
    {
        return services.AddTransient<IGithubIntegrator, GithubIntegrator>();
    }

    public static IServiceCollection AddTwitterIntegrator(this IServiceCollection services)
    {
        return services.AddTransient<ITwitterIntegrator, TwitterIntegrator>();
    }

    public static IServiceCollection AddScheduler(this IServiceCollection services)
    {
        return services.AddHostedService<Scheduler>();
    }
}

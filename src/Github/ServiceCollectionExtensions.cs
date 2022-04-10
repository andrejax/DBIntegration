using Github.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Octokit;

namespace Github;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGithub(this IServiceCollection services, GithubOptions options)
    {
        return services.AddTransient<IGithubService, GithubService>(_ => new GithubService(new GitHubClient(new ProductHeaderValue(options.AppName))));
    }
}


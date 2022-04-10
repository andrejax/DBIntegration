using Databox.Integrator.Contracts;

namespace Integrations.Host;

public static class IntegrationExtensions
{
    public static RouteHandlerBuilder RegisterIntegrationRoutes(this WebApplication app)
    {
        app.MapGet("/TwitterImport/{query}", async(ITwitterIntegrator integrator, string query) =>
        {
            await integrator.ImportDailyTweets(query);
        })
        .WithName("LoadTweets");

        return app.MapGet("/GithubImport/{username}", async (IGithubIntegrator integrator, string username) =>
        {
            await integrator.ImportUserInfo(username);
        })
        .WithName("LoadUserInfo");
    }
}

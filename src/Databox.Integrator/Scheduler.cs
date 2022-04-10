using Databox.Integrator.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Databox.Integrator;

internal class Scheduler : IHostedService
{
    private readonly ILogger<Scheduler> _logger;
    private readonly IServiceProvider _services;
    private Timer? _timer;

    public Scheduler(ILogger<Scheduler> logger, IServiceProvider services)
    {
        _logger = logger;
        _services = services;
    }

    public async void Start(object? _)
    {
        using var scope = _services.CreateScope();

        var twitterIntegrator =
            scope.ServiceProvider
                .GetRequiredService<ITwitterIntegrator>();

        var githubIntegrator = scope.ServiceProvider
                .GetRequiredService<IGithubIntegrator>();

        try
        {
            await twitterIntegrator.ImportDailyTweets("ptuj");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occured while executing {0}", twitterIntegrator.GetType().Name);
        }

        try
        {
            await githubIntegrator.ImportUserInfo("jskeet");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occured while executing {0}", githubIntegrator.GetType().Name);
        }
    }

    public Task StartAsync(CancellationToken _)
    {
        _logger.LogInformation("Timed Hosted Service running.");

        _timer = new Timer(Start, null, TimeSpan.Zero, TimeSpan.FromHours(24));

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken _)
    {
        _logger.LogInformation("Timed Hosted Service is stopping.");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }
}

using Databox.Contracts.Interfaces;
using Databox.Integrator.Contracts;
using Databox.Integrator.Twitter;
using Microsoft.Extensions.Logging;
using Twitter.Contracts.Interfaces;

namespace Databox.Integrator;

internal class TwitterIntegrator : ITwitterIntegrator
{
    private readonly IDataboxClient _databoxClient;
    private readonly ITwitterService _tweeterService;
    private readonly ILogger<TwitterIntegrator> _logger;

    public TwitterIntegrator(IDataboxClient databoxClient, ITwitterService tweeterService, ILogger<TwitterIntegrator> logger)
    {
        _databoxClient = databoxClient;
        _tweeterService = tweeterService;
        _logger = logger;
    }

    public async Task ImportDailyTweets(string query)
    {
        ArgumentNullException.ThrowIfNull(query, nameof(query));

        var tweetsToday = await _tweeterService.GetTweetsForToday(query);
        var mappedTweets = tweetsToday.Map();
        _logger.LogInformation("{0}  {1}  Sending metrics: {2}. NumberOfKPIs: {3}",
            DateTimeOffset.Now.ToString(), nameof(TwitterIntegrator), string.Join(", ", mappedTweets.Select(x => $"{x.Key}: {x.Value}")), mappedTweets.Count());
        await _databoxClient.Push(mappedTweets);
    }

}

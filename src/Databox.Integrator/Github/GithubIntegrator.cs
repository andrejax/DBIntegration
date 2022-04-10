using Databox.Contracts.Interfaces;
using Databox.Integrator.Contracts;
using Databox.Integrator.Github;
using Databox.Integrator.Twitter;
using Github.Contracts;
using Microsoft.Extensions.Logging;

namespace Databox.Integrator;

internal class GithubIntegrator : IGithubIntegrator
{
    private readonly ILogger<GithubIntegrator> _logger;
    private readonly IDataboxClient _databoxClient;
    private readonly IGithubService _githubService;
    public GithubIntegrator(IDataboxClient databoxClient, IGithubService githubService, ILogger<GithubIntegrator> logger)
    {
        _databoxClient = databoxClient;
        _githubService = githubService;
        _logger = logger;
    }

    public async Task ImportUserInfo(string username)
    {
        ArgumentNullException.ThrowIfNull(username, nameof(username));

        var userInfoToday = await _githubService.GetUserInfo(username);
        var mappedUserInfo = userInfoToday.Map();
        _logger.LogInformation("{0}  {1}  Sending metrics: {2}. NumberOfKPIs: {3}",
            DateTimeOffset.Now.ToString(), nameof(GithubIntegrator), string.Join(", ", mappedUserInfo.Select(x => $"{x.Key}: {x.Value}")), mappedUserInfo.Count());
        await _databoxClient.Push(mappedUserInfo);
    }

}

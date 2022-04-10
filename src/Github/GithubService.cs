using Github.Contracts;
using Octokit;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Github.Tests")]
namespace Github;

internal class GithubService : IGithubService
{
    private readonly IGitHubClient _gitHubClient;

    public GithubService(IGitHubClient gitHubClient)
    {
        _gitHubClient = gitHubClient; 
    }

    public async Task<UserInfoToday> GetUserInfo(string username)
    {
        ArgumentNullException.ThrowIfNull(username, nameof(username));

        var user = await _gitHubClient.User.Get(username);

        return new UserInfoToday
        {
            FollowersCount = user.Following,
            FollowingCount = user.Followers,
            PublicReposCount = user.PublicRepos,
            Date = DateTime.UtcNow,
            User = username
        };
    }
}

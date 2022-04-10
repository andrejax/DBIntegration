using Tweetinvi;
using Tweetinvi.Parameters.V2;
using Twitter.Contracts.Interfaces;
using Twitter.Contracts;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Twitter.Tests")]
namespace Twitter;

internal class TwitterService : ITwitterService
{
    private readonly ITwitterClient _twitterClient;

    public TwitterService(ITwitterClient twitterClient)
    {
        _twitterClient = twitterClient;
    }

    public async Task<TweetsToday> GetTweetsForToday(string query)
    {
        ArgumentNullException.ThrowIfNull(query, nameof(query));

        var currentTime = DateTime.Now;
        var searchParams = new SearchTweetsV2Parameters(query)
        {
            StartTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 0, 0, 0, DateTimeKind.Utc)
        };

        var tweetsResponse = await _twitterClient.SearchV2.SearchTweetsAsync(searchParams);

        var totalRetweets = tweetsResponse.Tweets.Sum(x => x.PublicMetrics.RetweetCount);
        var distinctAuthorsCount = tweetsResponse.Tweets.Select(x => x.AuthorId).Distinct().Count();
        var tweetsToday = new TweetsToday
        {
            Query = query,
            Date = currentTime,
            AuthorsCount = distinctAuthorsCount,
            RetweetsCount = totalRetweets,
            TweetsCount = tweetsResponse.Tweets.Length
        };

        return tweetsToday;
    }
}

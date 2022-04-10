using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Parameters.V2;
using Xunit;

namespace Twitter.Tests;

public class TwitterServiceTests
{
    private readonly Mock<ITwitterClient> _mockTwitterClient;

    public TwitterServiceTests()
    {
        _mockTwitterClient = new Mock<ITwitterClient>();
    }

    [Fact]
    public async void GetTweetsForToday_Returns_TweetsToday()
    {
        var currentTime = DateTime.Now;
        var query = Guid.NewGuid().ToString();
        var retweetCount = 5;
        _mockTwitterClient.Setup(x => x.SearchV2.SearchTweetsAsync(It.IsAny<SearchTweetsV2Parameters>())).Returns(Task.FromResult(new Tweetinvi.Models.V2.SearchTweetsV2Response
        {
            Tweets = new Tweetinvi.Models.V2.TweetV2[] {
            new Tweetinvi.Models.V2.TweetV2
            {
                PublicMetrics = new Tweetinvi.Models.V2.TweetPublicMetricsV2
                {
                    RetweetCount  = retweetCount
                },
                AuthorId = Guid.NewGuid().ToString()
            }
         }
        }));
        var twitterService = new TwitterService(_mockTwitterClient.Object);

        var response = await twitterService.GetTweetsForToday(query);

        response.Should().NotBeNull();
        response.RetweetsCount.Should().Be(retweetCount);
        response.TweetsCount.Should().Be(1);
        response.AuthorsCount.Should().Be(1);
        response.Query.Should().Be(query);
        response.Date.Should().BeAfter(currentTime).And.BeBefore(DateTime.Now);
    }

    [Fact]
    public void GetTweetsForTodayNullInput_Returns_ArgumentNullException()
    {
        var twitterService = new TwitterService(_mockTwitterClient.Object);

        Action act = () => twitterService.GetTweetsForToday(null).GetAwaiter().GetResult();

        act.Should().Throw<ArgumentNullException>();
    }


}

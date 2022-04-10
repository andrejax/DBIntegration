using Microsoft.Extensions.DependencyInjection;
using Tweetinvi;
using Tweetinvi.Models;
using Twitter.Contracts;
using Twitter.Contracts.Interfaces;

namespace Twitter;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTwitter(this IServiceCollection services, TwitterOptions options)
    {
        return services.AddTransient<ITwitterClient, TwitterClient>(services =>
        {
            var consumerOnlyCredentials = new ConsumerOnlyCredentials(options.ClientKey, options.ClientSecret);
            var appClientWithoutBearer = new TwitterClient(consumerOnlyCredentials);

            var bearerToken = appClientWithoutBearer.Auth.CreateBearerTokenAsync().GetAwaiter().GetResult();
            var appCredentials = new ConsumerOnlyCredentials(options.ClientKey, options.ClientSecret)
            {
                BearerToken = bearerToken
            };
            return new TwitterClient(appCredentials);
        }).AddTransient<ITwitterService, TwitterService>(services => new TwitterService(services.GetRequiredService<ITwitterClient>()));
    }
}

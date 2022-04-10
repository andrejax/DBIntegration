using Databox.Contracts;
using Databox.Contracts.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Text;

namespace Databox;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataboxClient(this IServiceCollection services, DataboxOptions options)
    {
        services.AddTransient<IDataboxClient, DataboxClient>();
        services.AddHttpClient<IDataboxClient, DataboxClient>(httpClient =>
        {
            httpClient.Timeout = TimeSpan.FromMilliseconds(5000);
            httpClient.BaseAddress = new Uri(options.BaseUrl);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.databox.v2+json"));
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("am-c#");
            var tokenBytes = Encoding.UTF8.GetBytes(options.Token);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(tokenBytes));
        });
        return services;
    }
}


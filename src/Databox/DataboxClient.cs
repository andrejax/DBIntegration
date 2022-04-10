using Databox.Contracts;
using Databox.Contracts.Interfaces;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("Databox.Tests")]
namespace Databox;

internal class DataboxClient : IDataboxClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<DataboxClient> _logger;
    public DataboxClient(HttpClient httpClient, ILogger<DataboxClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public Task Push(string key, object value)
    {
        ArgumentNullException.ThrowIfNull(key, nameof(key));
        ArgumentNullException.ThrowIfNull(value, nameof(value));

        return Push(new List<KPI> { new KPI { Key = key, Value = value } });
    }

    public async Task Push(IEnumerable<KPI> kpis)
    {
        ArgumentNullException.ThrowIfNull(kpis, nameof(kpis));

        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/");
        var jsonData = new DataboxTransferData(kpis).ToDataTransferObject();

        requestMessage.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        _logger.LogInformation(requestMessage.ToString());

        var responseMessage = await _httpClient.SendAsync(requestMessage);
        var responseString = await responseMessage.Content.ReadAsStringAsync();

        if (responseMessage.StatusCode != System.Net.HttpStatusCode.OK)
        {
            _logger.LogError("Unsuccessful sending! StatusCode: {0}, Error message: {1}", responseMessage.StatusCode, responseString);
        }

        responseMessage.EnsureSuccessStatusCode();
        _logger.LogInformation("Successfuly sent! Response: {0}", responseString);
    }
}

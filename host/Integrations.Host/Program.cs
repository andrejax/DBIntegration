using Databox;
using Databox.Contracts;
using Databox.Integrator.Contracts;
using Github;
using Github.Contracts;
using Integrations.Host;
using Twitter;
using Twitter.Contracts;


var builder = WebApplication.CreateBuilder(args);

var databoxOptions = builder.Configuration.GetRequiredSection(nameof(DataboxOptions)).Get<DataboxOptions>();
ArgumentNullException.ThrowIfNull(databoxOptions.BaseUrl, nameof(databoxOptions.BaseUrl));
ArgumentNullException.ThrowIfNull(databoxOptions.Token, nameof(databoxOptions.Token));
if (!Uri.TryCreate(databoxOptions.BaseUrl, UriKind.Absolute, out var uri))
    throw new ArgumentException("Invalid url!", nameof(databoxOptions.BaseUrl));
builder.Services.AddDataboxClient(databoxOptions);

var twitterOptions = builder.Configuration.GetRequiredSection(nameof(TwitterOptions)).Get<TwitterOptions>();
ArgumentNullException.ThrowIfNull(twitterOptions.ClientKey, nameof(twitterOptions.ClientKey));
ArgumentNullException.ThrowIfNull(twitterOptions.ClientSecret, nameof(twitterOptions.ClientSecret));
builder.Services.AddTwitter(twitterOptions);
builder.Services.AddTwitterIntegrator();

var githubOptions = builder.Configuration.GetRequiredSection(nameof(GithubOptions)).Get<GithubOptions>();
ArgumentNullException.ThrowIfNull(githubOptions.AppName, nameof(githubOptions.AppName));
builder.Services.AddGithub(githubOptions);
builder.Services.AddGithubIntegrator();
builder.Services.AddScheduler();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.RegisterIntegrationRoutes();

app.Run();
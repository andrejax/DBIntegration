namespace Databox.Integrator.Contracts;

public interface ITwitterIntegrator
{
    Task ImportDailyTweets(string query);
}

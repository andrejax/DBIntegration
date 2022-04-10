namespace Twitter.Contracts.Interfaces;

public interface ITwitterService
{
    Task<TweetsToday> GetTweetsForToday(string query);
}

using Databox.Contracts;
using Twitter.Contracts;

namespace Databox.Integrator.Twitter;

internal static class TwitterMapper
{
    internal static IEnumerable<KPI> Map(this TweetsToday tweetsToday)
    {
        yield return new KPI
        {
            Key = $"{tweetsToday.Query}_{nameof(tweetsToday.AuthorsCount)}",
            Value = tweetsToday.AuthorsCount,
            Date = tweetsToday.Date
        };

        yield return new KPI
        {
            Key = $"{tweetsToday.Query}_{nameof(tweetsToday.TweetsCount)}",
            Value = tweetsToday.TweetsCount,
            Date = tweetsToday.Date
        };

        yield return new KPI
        {
            Key = $"{tweetsToday.Query}_{nameof(tweetsToday.RetweetsCount)}",
            Value = tweetsToday.RetweetsCount,
            Date = tweetsToday.Date
        };
    }
}


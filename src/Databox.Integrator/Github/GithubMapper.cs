using Databox.Contracts;
using Github.Contracts;

namespace Databox.Integrator.Github;

internal static class GithubMapper
{
    internal static IEnumerable<KPI> Map(this UserInfoToday userInfoToday)
    {
        yield return new KPI
        {
            Key = $"{userInfoToday.User}_{nameof(userInfoToday.PublicReposCount)}",
            Value = userInfoToday.PublicReposCount,
            Date = userInfoToday.Date
        };

        yield return new KPI
        {
            Key = $"{userInfoToday.User}_{nameof(userInfoToday.FollowingCount)}",
            Value = userInfoToday.FollowingCount,
            Date = userInfoToday.Date
        };

        yield return new KPI
        {
            Key = $"{userInfoToday.User}_{nameof(userInfoToday.FollowersCount)}",
            Value = userInfoToday.FollowersCount,
            Date = userInfoToday.Date
        };
    }
}


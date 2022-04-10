namespace Github.Contracts;

public class UserInfoToday
{
    public int FollowingCount { get; set; }
    public int FollowersCount { get; set; }
    public int PublicReposCount { get; set; }
    public DateTime Date { get; set; }
    public string User { get; set; }
}


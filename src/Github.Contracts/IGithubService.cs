namespace Github.Contracts;

public interface IGithubService
{
    Task<UserInfoToday> GetUserInfo(string user);
}

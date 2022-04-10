namespace Databox.Integrator.Contracts;

public interface IGithubIntegrator
{
    Task ImportUserInfo(string username);
}

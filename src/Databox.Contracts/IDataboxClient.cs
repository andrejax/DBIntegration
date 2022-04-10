namespace Databox.Contracts.Interfaces;

public interface IDataboxClient
{
    public Task Push(string key, object value);
    public Task Push(IEnumerable<KPI> kpis);
}


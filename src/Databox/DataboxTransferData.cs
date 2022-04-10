using Databox.Contracts;

namespace Databox;

internal class DataboxTransferData
{
    public IEnumerable<KPI> Data { get; set; }
    public DataboxTransferData(IEnumerable<KPI> data)
    {
        Data = data;
    }
    public string ToDataTransferObject()
    {
        return $"{{ \"data\" : [ { string.Join(", ", Data.Select(x => ToKPITransferObject(x)))} ] }}";
    }
    private string ToKPITransferObject(KPI kpi)
    {
        ArgumentNullException.ThrowIfNull(kpi, nameof(kpi));
        ArgumentNullException.ThrowIfNull(kpi.Key, nameof(kpi.Key));
        ArgumentNullException.ThrowIfNull(kpi.Value, nameof(kpi.Value));

        return $"{{ \"${kpi.Key}\" : {kpi.Value} , \"date\" : \"{kpi.Date:yyyy-MM-dd HH:mm:ss}\" }}";
    }
}

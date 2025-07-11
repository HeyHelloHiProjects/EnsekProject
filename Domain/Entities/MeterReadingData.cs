namespace Domain.Entities;

public class MeterReadingData
{
    public int DataId { get; set; }
    public int AccountId { get; set; }
    public DateTime MeterReadingDateTime { get; set; }
    public int MeterReadValue { get; set; }
}

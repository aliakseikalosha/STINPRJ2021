using System;

public interface IDataProvider
{
    Action OnNewData { get; }
    void HasDataFor(DateTime day);
    void TryGetNewData(out DayData data);
    void LoadData();
}

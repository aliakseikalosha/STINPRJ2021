using System;

public interface IDataProvider
{
    string PathFor(DateTime day, string fileName);
    Action OnNewData { get; }
    bool HasDataFor(DateTime day);
    StateCaseData CaseDataFor(DateTime day);
    void TryGetNewData(Action<bool> action);
    void SaveData(string csv, DateTime day);
}

public interface IDataProviderFull : IDataProvider
{
    bool HasVacDataFor(DateTime day);
    void SaveVacData(string csv, DateTime day);
    VacinationData VaccinationDataFor(DateTime day);
}
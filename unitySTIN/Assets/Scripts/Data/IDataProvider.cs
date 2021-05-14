using System;

public interface IDataProvider
{
    string PathFor(DateTime day);
    Action OnNewData { get; }
    bool HasDataFor(DateTime day);
    StateCaseData CaseDataFor(DateTime day);
    void TryGetNewData();
    void SaveData(string csv, DateTime day);
}

public interface IDataProviderFull: IDataProvider
{
    VacinationData VaccinationDataFor(DateTime day);
}
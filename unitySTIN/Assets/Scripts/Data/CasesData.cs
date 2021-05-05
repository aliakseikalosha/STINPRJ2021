using System;
public class CasesData
{
    public StateCaseData Mzcr = null;
    public StateCaseData Who = null;
    public DateTime day;
}
public class StateCaseData
{
    public int PerDay = 0;
    public int Total = 0;
    public DateTime updated;
}
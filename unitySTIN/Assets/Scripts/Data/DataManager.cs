using System;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager I { get; private set; }
    public bool HasNextDayData { get; internal set; }
    public bool HasPrevDayData { get; internal set; }
    public DateTime NextDayData { get; internal set; }
    public DateTime PrevDayData { get; internal set; }

    public DateTime CurrentDay => DateTime.Now;

    public DateTime NextDay { get;}
    public DateTime PrevDay { get; }

    public Action OnNewData;

    private void Awake()
    {
        I = this;
    }

    internal DayData DataFor(DateTime day)
    {
        throw new NotImplementedException();
    }

    internal void TryGetNewData(Action p)
    {
        throw new NotImplementedException();
    }
}

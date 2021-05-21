using Assets.Scripts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager I { get; private set; }
    public Action OnNewData;
    public DateTime CurrentDay => DateTime.Now.Date;
    private MzcrDataProvider mzcr = new MzcrDataProvider();
    private WHODataProvider who = new WHODataProvider();
    private DateTime firstDay = DateTime.Parse("2021-05-21T00:00:00.2311892-04:00.").Date;

    private List<DateTime> PrevDayFrom(DateTime day)
    {
        List<DateTime> days = new List<DateTime>();
        day = day.Date.AddDays(-1);
        while (day > firstDay)
        {
            days.Add(day);
            day = day.AddDays(-1);
        }
        return days;
    }

    private List<DateTime> NextDayFrom(DateTime day)
    {
        List<DateTime> days = new List<DateTime>();
        day = day.Date.AddDays(1);
        while (day < CurrentDay)
        {
            days.Add(day);
            day = day.AddDays(1);
        }
        return days;
    }

    public bool HasDataForNextDay(DateTime day)
    {
        return PrevDayFrom(day).Any(c => mzcr.HasDataFor(c) || who.HasDataFor(c));
    }
    public bool HasDataForPrevDay(DateTime day)
    {
        return NextDayFrom(day).Any(c => mzcr.HasDataFor(c) || who.HasDataFor(c));
    }
    public DateTime NextDayWithData(DateTime day)
    {
        throw new NotImplementedException();
    }
    public DateTime PrevDayWithData(DateTime day)
    {
        throw new NotImplementedException();
    }

    private void Awake()
    {
        I = this;
    }

    public DayData DataFor(DateTime day)
    {
        throw new NotImplementedException();
    }

    public void TryGetNewData(Action p)
    {
        throw new NotImplementedException();
    }
}

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

    public bool HasDataFoDay(DateTime day)
    {
        return mzcr.HasDataFor(day) && who.HasDataFor(day) && who.HasVacDataFor(day);
    }
    private List<DateTime> PrevDaysFrom(DateTime day)
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

    private List<DateTime> NextDaysFrom(DateTime day)
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
        return NextDaysFrom(day).Any(c => HasDataFoDay(day));
    }
    public bool HasDataForPrevDay(DateTime day)
    {
        return PrevDaysFrom(day).Any(c => HasDataFoDay(day));
    }
    public DateTime NextDayWithData(DateTime day)
    {
        if (HasDataForNextDay(day))
        {
            return NextDaysFrom(day).First(c=> mzcr.HasDataFor(c) || who.HasDataFor(c));
        }
        return default(DateTime);
    }
    public DateTime PrevDayWithData(DateTime day)
    {
        if (HasDataForPrevDay(day))
        {
            return PrevDaysFrom(day).First(c => mzcr.HasDataFor(c) || who.HasDataFor(c));
        }
        return default(DateTime);
    }

    private void Awake()
    {
        I = this;
        mzcr.OnNewData += GetNewData;
        who.OnNewData += GetNewData;
        TryGetNewData(null);
    }

    private void GetNewData()
    {
        OnNewData?.Invoke();
    }

    public DayData DataFor(DateTime day)
    {
        if (HasDataFoDay(day))
        {
            var vacination = who.VaccinationDataFor(day);
            var mzcrData = mzcr.CaseDataFor(day);
            var whoData = who.CaseDataFor(day);
            return new DayData
            {
                vacination = vacination,
                cases = new CasesData
                {
                    Mzcr = mzcrData,
                    Who = whoData,
                    day = day
                }
            };
        }
        return null;
    }

    public void TryGetNewData(Action p)
    {
        mzcr.TryGetNewData();
        who.TryGetNewData();
    }
}

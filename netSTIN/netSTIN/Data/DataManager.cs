using Assets.Scripts.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DataManager
{
    public static DataManager I { get; private set; }
    public Action OnNewData;
    public DateTime CurrentDay => DateTime.Now.Date;
    private MzcrDataProvider mzcr = new MzcrDataProvider();
    private WHODataProvider who = new WHODataProvider();
    private DateTime firstDay = DateTime.Parse("2021-05-21T00:00:00.2311892-04:00.").Date;
    private Coroutine gettingNewData = null;

    public bool HasDataForDay(DateTime day)
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
        while (day <= CurrentDay)
        {
            days.Add(day);
            day = day.AddDays(1);
        }
        return days;
    }

    public bool HasDataForNextDay(DateTime day)
    {
        return NextDaysFrom(day).Any(c => HasDataForDay(c));
    }
    public bool HasDataForPrevDay(DateTime day)
    {
        return PrevDaysFrom(day).Any(c => HasDataForDay(c));
    }
    public DateTime NextDayWithData(DateTime day)
    {
        return NextDaysFrom(day).FirstOrDefault(c => mzcr.HasDataFor(c) || who.HasDataFor(c));
    }
    public DateTime PrevDayWithData(DateTime day)
    {
        return PrevDaysFrom(day).FirstOrDefault(c => mzcr.HasDataFor(c) || who.HasDataFor(c));
    }

    private void Awake()
    {
        I = this;
        mzcr.OnNewData += GetNewData;
        who.OnNewData += GetNewData;
    }

    private IEnumerator Start()
    {
        SendEvent("StatedApp");
        TryGetNewData();
        while (true)
        {
            var now = DateTime.Now;
            var nextUpdate = new DateTime(now.Year, now.Month, now.Day, 11, 00, 00);
            if (now < nextUpdate)
            {
                yield return new WaitForSeconds((float)(nextUpdate - now).TotalSeconds);
                TryGetNewData();
                SendEvent("Download1100");
            }
            now = DateTime.Now;
            nextUpdate = new DateTime(now.Year, now.Month, now.Day, 22, 00, 00);
            if (now < nextUpdate)
            {
                yield return new WaitForSeconds((float)(nextUpdate - now).TotalSeconds);
                yield return UpdatingData();
                SendEvent("Download2200");
            }
            now = DateTime.Now;
            nextUpdate = new DateTime(now.Year, now.Month, now.Day, 00, 01, 00).AddDays(1);
            if (now < nextUpdate)
            {
                yield return new WaitForSeconds((float)(nextUpdate - now).TotalSeconds);
                TryGetNewData();
                SendEvent("Download0001");
            }
        }
    }

    private void SendEvent(string eventText)
    {
        AnalyticsResult ar = Analytics.CustomEvent(eventText);
        Debug.Log($"Sent event {eventText} with result {ar}");
    }

    private IEnumerator UpdatingData()
    {
        var now = DateTime.Now;
        var stopTime = new DateTime(now.Year, now.Month, now.Day, 23, 30, 00);
        yield return UpdatingProvider(mzcr, stopTime);
        yield return UpdatingProvider(who, stopTime.AddMinutes(10));
    }

    private IEnumerator UpdatingProvider(IDataProvider provider, DateTime stopTime)
    {
        var done = false;
        var newData = false;
        while (!newData && DateTime.Now < stopTime)
        {
            provider.TryGetNewData((b) => { newData = b; done = true; });
            yield return new WaitUntil(() => done);
            yield return new WaitForSeconds(5 * 60);
        }
    }

    private void GetNewData()
    {
        OnNewData?.Invoke();
    }

    public DayData DataFor(DateTime day)
    {
        if (HasDataForDay(day))
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

    public void TryGetNewData(Action<bool> updated = null)
    {
        if (gettingNewData == null)
        {
            gettingNewData = StartCoroutine(WaitWhileGettingNewData(updated));
        }
    }

    private IEnumerator WaitWhileGettingNewData(Action<bool> updated)
    {
        var mzcrDone = false;
        var whoDone = false;
        var newData = false;

        mzcr.TryGetNewData((b) => { newData |= b; mzcrDone = true; });
        who.TryGetNewData((b) => { newData |= b; whoDone = true; });
        yield return new WaitUntil(() => mzcrDone && whoDone);
        gettingNewData = null;
        updated?.Invoke(newData);
        SendEvent("TryGetNewData");
    }
}

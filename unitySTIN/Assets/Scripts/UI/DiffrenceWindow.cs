using System;
using UnityEngine;

public class DiffrenceWindow : UserInterface.Window
{
    [SerializeField] protected DiffrenceWindowElement mzcrElement = null;
    [SerializeField] protected DiffrenceWindowElement whoElement = null;

    protected override void GoToOtherScreen()
    {
        Hide();
        UserInterface.I.VaccinationWindow.Show();
    }
    protected override void ShowDay(DateTime day)
    {
        base.ShowDay(day);
        DayData data = DataManager.I.DataFor(day);
        mzcrElement.Init(data.cases.Mzcr, day);
        whoElement.Init(data.cases.Who, day, data.cases.Mzcr);
    }
}

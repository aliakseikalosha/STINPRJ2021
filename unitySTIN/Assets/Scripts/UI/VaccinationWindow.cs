using System;
using System.Collections.Generic;
using UnityEngine;

public class VaccinationWindow : UserInterface.Window
{
    public static List<VacinationStateData> countries = new List<VacinationStateData>();
    [SerializeField] private List<VacinationWindowElement> elements = null;
    private List<string> selectedCountries = new List<string>();
    private readonly string dataKey = "VaccinationWindowCountries";
    protected override void Awake()
    {
        base.Awake();
        LoadCountries();
        for (int i = 0; i < elements.Count; i++)
        {
            VacinationWindowElement item = elements[i];
            item.OnCountrySelected += SetCountry;
            item.Init(i, selectedCountries[i]);
        }
    }

    private void SaveCoutries()
    {
        string data = string.Empty;
        foreach (var country in selectedCountries)
        {
            data += $"{country},";
        }
        PlayerPrefs.SetString(dataKey, data);
        PlayerPrefs.Save();
    }

    private void LoadCountries()
    {
        selectedCountries = new List<string>(elements.Count);
        var data = PlayerPrefs.GetString(dataKey, string.Empty);
        if (string.IsNullOrEmpty(data))
        {
            return;
        }
        var d = data.Split(',');
        for (int i = 0; i < d.Length; i++)
        {
            if (i >= selectedCountries.Count)
            {
                Debug.LogError($"get more counry then can show  data:{data}");
                continue;
            }
            selectedCountries[i] = d[i];
        }
    }

    private void SetCountry(int index, string country)
    {
        selectedCountries[index] = country;
        SaveCoutries();
    }

    protected override void GoToOtherScreen()
    {
        Hide();
        UserInterface.I.DiffrenceWindow.Show();
    }

    protected override void ShowDay(DateTime day)
    {
        base.ShowDay(day);
        countries = DataManager.I.DataFor(day).vacination.data;
        for (int i = 0; i < elements.Count; i++)
        {
            VacinationWindowElement item = elements[i];
            item.Init(i, selectedCountries[i]);
        }
    }
}

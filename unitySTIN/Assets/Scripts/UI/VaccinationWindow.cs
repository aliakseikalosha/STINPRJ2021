using System;
using System.Collections.Generic;
using UnityEngine;

public class VaccinationWindow : UserInterface.Window
{
    public static List<VacinationStateData> countries = new List<VacinationStateData>();
    [SerializeField] private List<VacinationWindowElement> elements = null;
    private string[] selectedCountries;
    private readonly string dataKey = "VaccinationWindowCountries";
    private readonly string firstCountry = "Czechia";
    protected override void Awake()
    {
        base.Awake();
        LoadCountries();
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
        selectedCountries = new string[elements.Count];
        var data = PlayerPrefs.GetString(dataKey, $"{firstCountry},");
        var d = data.Split(',');
        for (int i = 0; i < d.Length - 1; i++)
        {
            if (i >= selectedCountries.Length)
            {
                Debug.LogError($"get more counry then can show  data:{data}");
                continue;
            }
            selectedCountries[i] = d[i];
        }
        selectedCountries[0] = firstCountry;
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
            if(i == 0)
            {
                item.DisableSelection();
            }
            item.OnCountrySelected -= SetCountry;
            item.OnCountrySelected += SetCountry;
            item.Init(i, selectedCountries[i]);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class VacinationWindowElement : MonoBehaviour
{
    public Action<int, string> OnCountrySelected;

    private static List<string> options = new List<string>();
    [SerializeField] private AutoCompleteComboBox country = null;
    [SerializeField] private TMP_Text czechia = null;
    [SerializeField] private TMP_Text percent = null;
    [SerializeField] private TMP_Text population = null;
    private static string None = "None";
    private int id = -1;
    public void Init(int index, string selected = null)
    {
        id = index;
        if (options.Count == 0)
        {
            options = new List<string> { None };
            options.AddRange(VaccinationWindow.countries.OrderBy(c => c.StateName).Select(c => c.StateName));
        }
        if (country.AvailableOptions.Count != options.Count)
        {
            country.SetAvailableOptions(options);
            country.OnSelectionChanged.AddListener(SelectCountry);
        }
        if (string.IsNullOrEmpty(selected) || selected == None)
        {
            percent.text = string.Empty;
            population.text = string.Empty;
        }
        else
        {
            country.OnValueChanged(selected);
            SelectCountry(selected, true);   
        }
    }

    private void SelectCountry(string text, bool valid)
    {
        if (valid && text != None)
        {
            var data = VaccinationWindow.countries.First(c => c.StateName == text);
            percent.text = $"{data.Percent:F2}%";
            population.text = $"{data.Population}";
        }
        else
        {
            percent.text = string.Empty;
            population.text = string.Empty;
        }
        OnCountrySelected?.Invoke(id, None);
    }

    public void SetInteractive(bool active)
    {
        country.gameObject.SetActive(active);
        czechia.gameObject.SetActive(!active);
    }
}
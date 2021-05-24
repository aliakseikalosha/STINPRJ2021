using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class VacinationWindowElement : MonoBehaviour
{
    public Action<int, string> OnCountrySelected;

    private static List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
    [SerializeField] private TMP_Dropdown country = null;
    [SerializeField] private TMP_InputField countryInput = null;
    [SerializeField] private TMP_Text percent = null;
    [SerializeField] private TMP_Text population = null;
    private static string None = "None";
    private int id = -1;

    private void Awake()
    {
        country.onValueChanged.AddListener(SelectCountry);
        countryInput.onValueChanged.AddListener(ChangeCountrySelection);
    }

    private void ChangeCountrySelection(string text)
    {
        if (text == None)
        {
            country.ClearOptions();
            country.AddOptions(options);
            return;
        }
        var newOptions = options.Where(c => c.text.ToLower().StartsWith(text.ToLower()[0].ToString()) && c.text != None).ToList();
        if (newOptions.Count == 0)
        {
            newOptions.Add(new TMP_Dropdown.OptionData(None));
        }

        country.ClearOptions();
        country.AddOptions(newOptions);
        SelectCountry(0);
    }

    public void Init(int index, string selected = null)
    {
        id = index;
        if (options.Count == 0)
        {
            country.ClearOptions();
            country.AddOptions(new List<TMP_Dropdown.OptionData> { new TMP_Dropdown.OptionData(None) });
            country.AddOptions(VaccinationWindow.countries.OrderBy(c => c.StateName).Select(c => new TMP_Dropdown.OptionData(c.StateName)).ToList());
            options = country.options.ToList();
        }
        if (country.options.Count != options.Count)
        {
            country.ClearOptions();
            country.AddOptions(options);
        }
        if (string.IsNullOrEmpty(selected) || selected == None)
        {
            selected = None;
        }
        var opIndex = options.IndexOf(options.First(c => c.text == selected));
        country.SetValueWithoutNotify(opIndex);
        SelectCountry(opIndex);
    }

    private void SelectCountry(int index)
    {
        var selected = country.options[index].text;
        countryInput.text = selected;
        if (selected != None)
        {
            var data = VaccinationWindow.countries.First(c => c.StateName == selected);
            percent.text = $"{data.Percent:F2}%";
            population.text = $"{data.Population}";
        }
        else
        {
            percent.text = string.Empty;
            population.text = string.Empty;
        }
        OnCountrySelected?.Invoke(id, selected);
    }

    public void DisableSelection()
    {
        country.interactable = false;
        countryInput.interactable = false;
    }
}
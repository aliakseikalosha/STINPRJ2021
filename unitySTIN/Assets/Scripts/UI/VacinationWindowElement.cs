using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class VacinationWindowElement : MonoBehaviour
{
    public Action<int, string> OnCountrySelected;

    private static List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
    [SerializeField] private TMP_Dropdown conutry = null;
    [SerializeField] private TMP_Text percent = null;
    [SerializeField] private TMP_Text population = null;
    private static string None = "None";
    private int id = -1;
    public void Init(int index, string selected = null)
    {
        id = index;
        if (options.Count == 0)
        {
            conutry.ClearOptions();
            conutry.AddOptions(new List<TMP_Dropdown.OptionData> { new TMP_Dropdown.OptionData(None) });
            conutry.AddOptions(VaccinationWindow.countries.OrderBy(c => c.StateName).Select(c => new TMP_Dropdown.OptionData(c.StateName)).ToList());
            options = conutry.options.ToList();
        }
        if (conutry.options.Count != options.Count)
        {
            conutry.ClearOptions();
            conutry.AddOptions(options);
            conutry.onValueChanged.AddListener(SelectCountry);
        }
        if (string.IsNullOrEmpty(selected) || selected == None)
        {
            percent.text = string.Empty;
            population.text = string.Empty;
        }
        else
        {
            var opIndex = options.IndexOf(options.First(c => c.text == selected));
            conutry.SetValueWithoutNotify(opIndex);
            SelectCountry(opIndex);
        }
    }

    private void SelectCountry(int index)
    {
        if (index > 0)
        {
            var data = VaccinationWindow.countries.First(c => c.StateName == options[index].text);
            percent.text = $"{data.Percent:F2}%";
            population.text = $"{data.Population}";
            OnCountrySelected?.Invoke(id, data.StateName);
        }
        else
        {
            percent.text = string.Empty;
            population.text = string.Empty;
            OnCountrySelected?.Invoke(id, None);
        }
    }

    public void DisableSelection()
    {
        conutry.interactable = false;
    }
}
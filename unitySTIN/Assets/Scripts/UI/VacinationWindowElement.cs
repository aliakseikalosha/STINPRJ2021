using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class VacinationWindowElement : MonoBehaviour
{
    public Action<int, string> OnCountrySelected;

    private static List<TMP_Dropdown.OptionData> options = null;
    [SerializeField] private TMP_Dropdown conutry = null;
    [SerializeField] private TMP_Text percent = null;
    [SerializeField] private TMP_Text population = null;
    private int index = -1;
    public void Init(int index, string selected = null)
    {
        this.index = index;
        if (options == null)
        {
            options = VaccinationWindow.countries.Select(c => new TMP_Dropdown.OptionData(c.StateName)).ToList();
        }
        conutry.ClearOptions();
        conutry.AddOptions(options);
        conutry.onValueChanged.AddListener(SelectCountry);
        if (string.IsNullOrEmpty(selected))
        {
            var opIndex = options.IndexOf(options.First(c => c.text == selected));
            conutry.SetValueWithoutNotify(opIndex);
            SelectCountry(opIndex);
        }
        else
        {
            percent.text = string.Empty;
            population.text = string.Empty;
        }
    }

    private void SelectCountry(int index)
    {
        var data = VaccinationWindow.countries.First(c => c.StateName == options[index].text);
        percent.text = $"{data.Percent:X3}%";
        population.text = $"{data.Population}";
        OnCountrySelected?.Invoke(index, data.StateName);
    }
}
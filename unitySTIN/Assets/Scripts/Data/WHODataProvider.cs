using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Data
{
    public class WHODataProvider : DataProvider, IDataProviderFull
    {
        private string vacFileName;
        public WHODataProvider()
        {
            caseFileName = "WHOcases.csv";
            vacFileName = "WHOvaccination.csv";
        }
        public override StateCaseData CaseDataFor(DateTime day)
        {
            if (HasDataFor(day))
            {
                return CSVParserCases(File.ReadAllText(PathFor(day, caseFileName)));
            }
            return null;
        }

        public VacinationData VaccinationDataFor(DateTime day)
        {
            if (HasDataFor(day))
            {
                return CSVParserVaccination(File.ReadAllText(PathFor(day, vacFileName)), day);
            }
            return null;
        }

        public override void TryGetNewData()
        {
            NetworkManager.I.Get("https://covid19.who.int/WHO-COVID-19-global-data.csv", (csv) =>
            {
                var caseData = CSVParserCases(csv);
                if (!HasDataFor(caseData.updated))
                {
                    SaveData(csv, caseData.updated);
                    OnNewData?.Invoke();
                }
            }, Debug.LogError);
            NetworkManager.I.Get("https://covid19.who.int/who-data/vaccination-data.csv", (csv) =>
            {
                var vacData = CSVParserVaccination(csv, DateTime.Now);
                if (!HasDataFor(vacData.day))
                {
                    SaveData(csv, vacData.day);
                    OnNewData?.Invoke();
                }
            }, Debug.LogError);
        }

        private StateCaseData CSVParserCases(string csv)
        {
            var rows = csv.Split('\n');
            var countryCodeIndex = Array.IndexOf(rows[0].Split(','), "Country_code");
            var dateIndex = Array.IndexOf(rows[0].Split(','), "Date_reported");
            var casesTodayIndex = Array.IndexOf(rows[0].Split(','), "New_cases");
            var casesTotalIndex = Array.IndexOf(rows[0].Split(','), "Cumulative_cases");
            for (int i = rows.Length - 1; i > 0; i--)
            {
                var data = rows[i].Split(',');
                if (data[countryCodeIndex] == "CZ")
                {
                    return new StateCaseData
                    {
                        PerDay = int.Parse(data[casesTodayIndex]),
                        Total = int.Parse(data[casesTotalIndex]),
                        updated = DateTime.Parse(data[dateIndex])
                    };
                }
            }
            return null;
        }

        private VacinationData CSVParserVaccination(string csv, DateTime day)
        {
            var vacData = new VacinationData { day = day };
            var rows = csv.Split('\n');
            var countryIndex = Array.IndexOf(rows[0].Split(','), "COUNTRY");
            var totalVacIndex = Array.IndexOf(rows[0].Split(','), "TOTAL_VACCINATIONS");
            var totalVac100Index = Array.IndexOf(rows[0].Split(','), "TOTAL_VACCINATIONS_PER100");
            for (int i = 1; i < rows.Length; i++)
            {
                var data = rows[i].Split(',');
                var percent = float.Parse(data[totalVac100Index]);
                var pop = (int)(int.Parse(data[totalVacIndex]) / percent) * 100;
                vacData.data.Add(new VacinationStateData
                {
                    Percent = percent,
                    Population = pop,
                    StateName = data[countryIndex]
                });
            }
            return vacData;
        }

        public override bool HasDataFor(DateTime day)
        {
            return File.Exists(PathFor(day, caseFileName));
        }

        public bool HasVacDataFor(DateTime day)
        {
            return File.Exists(PathFor(day, vacFileName));
        }

        public override void SaveData(string csv, DateTime day)
        {
            File.WriteAllText(PathFor(day, caseFileName), csv);
        }

        public void SaveVacData(string csv, DateTime day)
        {
            File.WriteAllText(PathFor(day, vacFileName), csv);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace netSTIN.Data
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

        public override void TryGetNewData(Action<bool> updated)
        {
            NetworkManager.I.Get("https://covid19.who.int/WHO-COVID-19-global-data.csv", (csv) => UpdateData(csv, caseFileName, updated), Debug.LogError);
            NetworkManager.I.Get("https://covid19.who.int/who-data/vaccination-data.csv", (csv) => UpdateData(csv, vacFileName, null), Debug.LogError);
        }

        private StateCaseData CSVParserCases(string csv)
        {
            var rows = csv.Split('\n');
            var countryCodeIndex = Array.IndexOf(rows[0].Split(','), "Country_code");
            var dateIndex = 0;//Array.IndexOf(rows[0].Split(','), "Date_reported");
            var casesTodayIndex = Array.IndexOf(rows[0].Split(','), "New_cases");
            var casesTotalIndex = Array.IndexOf(rows[0].Split(','), "Cumulative_cases");
            for (int i = rows.Length - 2; i > 0; i--)
            {
                var data = rows[i].Split(',');
                if (data[countryCodeIndex] == "CZ")
                {
                    return new StateCaseData
                    {
                        PerDay = int.Parse(data[casesTodayIndex]),
                        Total = int.Parse(data[casesTotalIndex]),
                        updated = DateTime.ParseExact(data[dateIndex], "yyyy-MM-dd", CultureInfo.InvariantCulture)
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
            var regex = new Regex("(?<=^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)");
            for (int i = 1; i < rows.Length - 1; i++)
            {
                var data = new List<string>();

                foreach (Match m in regex.Matches(rows[i]))
                {
                    data.Add(m.Value);
                }
                if (string.IsNullOrEmpty(data[totalVac100Index]) || string.IsNullOrEmpty(data[totalVacIndex]))
                {
                    continue;
                }
                var percent = float.Parse(data[totalVac100Index], CultureInfo.GetCultureInfo("en-US"));
                var pop = (int)(int.Parse(data[totalVacIndex]) / percent) * 100;
                vacData.data.Add(new VacinationStateData
                {
                    Percent = percent,
                    Population = pop,
                    StateName = data[countryIndex].Replace("\"", "")
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
            SaveData(csv, day, caseFileName);
        }

        public void SaveVacData(string csv, DateTime day)
        {
            SaveData(csv, day, vacFileName);
        }
    }
}

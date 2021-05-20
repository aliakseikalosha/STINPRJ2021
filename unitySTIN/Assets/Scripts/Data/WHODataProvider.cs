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
    public class WHODataProvider : DataProvider
    {
        private string vacFileName;
        WHODataProvider()
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

        public VacinationData VaccinationDataFor(DateTime day, string state)
        {
            if (HasDataFor(day))
            {
                return CSVParserVaccination(File.ReadAllText(PathFor(day, vacFileName)));
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
                var vacData = CSVParserVaccination(csv);
                //if (!HasDataFor(vacData.updated))
                //{
                //    SaveData(csv, vacData.updated);
                //    OnNewData?.Invoke();
                //}
            }, Debug.LogError);
        }

        private StateCaseData CSVParserCases(string csv)
        {
            var rows = csv.Split('\n');
            var countryCodeIndex = Array.IndexOf(rows[0].Split(','), "Country_code");
            var dateIndex = Array.IndexOf(rows[0].Split(','), "Date_reported");
            var casesTodayIndex = Array.IndexOf(rows[0].Split(','), "New_cases");
            var casesTotalIndex = Array.IndexOf(rows[0].Split(','), "Cumulative_cases");
            for (int i = rows.Length-1; i > 0; i--)
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

        private VacinationData CSVParserVaccination(string csv)
        {
            throw new NotImplementedException();
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

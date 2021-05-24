using System;
using System.Globalization;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Data
{

    public class MzcrDataProvider : DataProvider
    {
        public MzcrDataProvider()
        {
            caseFileName = "MZCR.csv";
        }
        private StateCaseData CSVParser(string csv)
        {
            var rows = csv.Split('\n');
            var dateIndex = Array.IndexOf(rows[0].Split(','), "datum");
            var casesTodayIndex = Array.IndexOf(rows[0].Split(','), "potvrzene_pripady_dnesni_den");
            var casesTotalIndex = Array.IndexOf(rows[0].Split(','), "potvrzene_pripady_celkem");
            var data = rows[1].Split(',');
            return new StateCaseData
            {
                PerDay = int.Parse(data[casesTodayIndex]),
                Total = int.Parse(data[casesTotalIndex]),
                updated = DateTime.Parse(data[dateIndex], new CultureInfo("cs-CZ"))
            };
        }

        public override void TryGetNewData(Action<bool> updated)
        {
            NetworkManager.I.Get("https://onemocneni-aktualne.mzcr.cz/api/v2/covid-19/zakladni-prehled.csv", (csv) => UpdateData(csv, caseFileName, updated), Debug.LogError);
        }

        public override void SaveData(string csv, DateTime day)
        {
            SaveData(csv, day, caseFileName);
        }

        public override StateCaseData CaseDataFor(DateTime day)
        {
            if (HasDataFor(day))
            {
                return CSVParser(File.ReadAllText(PathFor(day, caseFileName)));
            }
            return null;
        }
        public override bool HasDataFor(DateTime day)
        {
            return File.Exists(PathFor(day, caseFileName));
        }
    }
}

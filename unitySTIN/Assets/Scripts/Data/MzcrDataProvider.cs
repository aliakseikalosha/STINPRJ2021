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
    class MzcrDataProvider : IDataProvider
    {

        public Action OnNewData => throw new NotImplementedException();

        public bool HasDataFor(DateTime day)
        {
            return File.Exists(PathFor(day));
        }

        private StateCaseData CSVParser(string csv)
        {
            var rows = csv.Split('\n');
            var dateIndex = Array.IndexOf(rows[0].Split(','),"datum");
            var casesTodayIndex = Array.IndexOf(rows[0].Split(','), "potvrzene_pripady_dnesni_den");
            var casesTotalIndex = Array.IndexOf(rows[0].Split(','), "potvrzene_pripady_celkem");
            var data = rows[1].Split(',');
            return new StateCaseData {
                PerDay = int.Parse(data[casesTodayIndex]),
                Total = int.Parse(data[casesTotalIndex]),
                updated = DateTime.Parse(data[dateIndex], new CultureInfo("cs-CZ"))
            };
        }

        public void TryGetNewData()
        {
            NetworkManager.I.Get("https://onemocneni-aktualne.mzcr.cz/api/v2/covid-19/zakladni-prehled.csv", (csv)=> {
                var caseData = CSVParser(csv);
                if (!HasDataFor(caseData.updated))
                {
                    SaveData(csv, caseData.updated);
                    OnNewData?.Invoke();
                }
            }, Debug.LogError);
        }

        public void SaveData(string csv, DateTime day)
        {
            File.WriteAllText(PathFor(day), csv);
        }

        public StateCaseData CaseDataFor(DateTime day)
        {
            if (HasDataFor(day))
            {
                return CSVParser(File.ReadAllText(PathFor(day)));
            }
            return null;
        }

        public string PathFor(DateTime day)
        {
            return Path.Combine(UnityEngine.Application.persistentDataPath, $"{day:yyyyMMdd}", "MZCR.csv");
        }
    }
}

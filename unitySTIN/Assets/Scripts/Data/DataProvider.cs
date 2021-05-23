using System;
using System.IO;

namespace Assets.Scripts.Data
{
    public abstract class DataProvider : IDataProvider
    {
        protected string caseFileName;
        public Action OnNewData { get; set; }

        public abstract StateCaseData CaseDataFor(DateTime day);
        public abstract bool HasDataFor(DateTime day);
        public abstract void SaveData(string csv, DateTime day);
        public abstract void TryGetNewData();
        public string PathFor(DateTime day, string fileName)
        {
            return Path.Combine(DirFor(day), fileName);
        }
        public string DirFor(DateTime day)
        {
            return Path.Combine(UnityEngine.Application.persistentDataPath, $"{day:yyyyMMdd}");
        }
        protected void SaveData(string csv, DateTime day, string fileName)
        {
            var dir = DirFor(day);
            var pathToFile = PathFor(day,fileName);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            File.WriteAllText(pathToFile, csv);
        }
    }
}

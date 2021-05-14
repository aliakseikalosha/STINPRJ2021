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
            return Path.Combine(UnityEngine.Application.persistentDataPath, $"{day:yyyyMMdd}", fileName);
        }
    }
}

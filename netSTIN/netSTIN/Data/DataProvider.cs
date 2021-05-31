using System;
using System.IO;

namespace netSTIN.Data
{
    public abstract class DataProvider : IDataProvider
    {
        protected string caseFileName;
        public Action OnNewData { get; set; }

        public abstract StateCaseData CaseDataFor(DateTime day);
        public abstract bool HasDataFor(DateTime day);
        public abstract void SaveData(string csv, DateTime day);
        public abstract void TryGetNewData(Action<bool> action);
        public string PathFor(DateTime day, string fileName)
        {
            return Path.Combine(DirFor(day), fileName);
        }
        public string DirFor(DateTime day)
        {
            return Path.Combine(UnityEngine.Application.persistentDataPath, $"{day:yyyyMMdd}");
        }
        public void UpdateData(string csv, string fileName, Action<bool> updated)
        {
            var day = DataManager.I.CurrentDay;
            var path = PathFor(day, fileName);
            if (File.Exists(path) && File.ReadAllText(path) == csv)
            {
                Debug.Log($"{DateTime.Now}\tData are the same data for {day} from file {fileName}");
                updated?.Invoke(false);
                return;
            }
            Debug.Log($"{DateTime.Now}\tSave data for {day} to file {fileName}");
            SaveData(csv, day, fileName);
            OnNewData?.Invoke();
            updated?.Invoke(true);
        }
        protected void SaveData(string csv, DateTime day, string fileName)
        {
            var dir = DirFor(day);
            var pathToFile = PathFor(day, fileName);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            File.WriteAllText(pathToFile, csv);
        }
    }
}

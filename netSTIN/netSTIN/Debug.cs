using System;
using System.IO;

namespace netSTIN
{
    public static class Debug
    {
        private static string CurrentLog => Path.Combine(Program.FilePath, logFileName);
        private static string PrevLog => Path.Combine(Program.FilePath, prevLogFileName);
        private readonly static string logFileName = "netLog.txt";
        private readonly static string prevLogFileName = "netLogPrev.txt";
        private static bool initializaed = false;

        public static void LogError(string text)
        {
            Log($"\nERROR!{text}");
        }

        public static void Log(string text)
        {
            Init();
            File.AppendAllText(CurrentLog, $"\n{DateTime.Now}\t:\t{text}\n");
        }

        public static void Init()
        {
            if (initializaed)
            {
                return;
            }
            if (File.Exists(CurrentLog))
            {
                File.WriteAllText(PrevLog, File.ReadAllText(CurrentLog));
                File.WriteAllText(CurrentLog, string.Empty);
            }
            initializaed = true;
        }
    }
}

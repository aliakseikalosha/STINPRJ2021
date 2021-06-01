using netSTIN.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace netSTIN
{
    static class Program
    {
        public static string FilePath = Path.Combine(Environment.ExpandEnvironmentVariables("%USERPROFILE%"), "AppData\\LocalLow\\KaloshaSafarikKanash\\STIN SP");
        private static CasesWindow cases;
        private static VacinationWindow vacination;
        private static bool casesShown = true;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DataManager.I.TryGetNewData();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            cases = new CasesWindow();
            vacination = new VacinationWindow();
            WaitAndDo(() => DataManager.I.HasDataForDay(DataManager.I.CurrentDay), () => Application.Run(cases));
        }

        public static void ChangeWindow()
        {
            if (casesShown)
            {
                cases.Hide();
                vacination.Show();
                vacination.DesktopLocation = cases.DesktopLocation;
            }
            else
            {
                cases.Show();
                vacination.Hide();
                cases.DesktopLocation = vacination.DesktopLocation;
            }
            casesShown = !casesShown;
        }

        public static void WaitAndDo(Func<bool> waitWhile, Action action)
        {
            while (!waitWhile())
            {
                Thread.Sleep(500);
            }
            action?.Invoke();
        }

        public static void Close()
        {
            cases.Close();
        }
    }
}

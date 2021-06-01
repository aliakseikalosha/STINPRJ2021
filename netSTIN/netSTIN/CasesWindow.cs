using netSTIN.Data;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace netSTIN
{
    public partial class CasesWindow : Form
    {
        protected DateTime currentDay;
        public CasesWindow()
        {
            InitializeComponent();
        }
        private void ChangeScreen_Click(object sender, EventArgs e)
        {
            Program.ChangeWindow();
        }

        private void update_Click(object sender, EventArgs e)
        {
            StartRefresh();
        }

        private void prevDay_Click(object sender, EventArgs e)
        {
            if (DataManager.I.HasDataForPrevDay(currentDay))
            {
                ShowDay(DataManager.I.PrevDayWithData(currentDay));
            }
        }

        private void nextDay_Click(object sender, EventArgs e)
        {
            if (DataManager.I.HasDataForNextDay(currentDay))
            {
                ShowDay(DataManager.I.NextDayWithData(currentDay));
            }
        }

        protected void StartRefresh()
        {
            var waiting = true;
            update.Enabled = false; 
            DataManager.I.TryGetNewData((b) => waiting = false);
            while (waiting)
            {
                Thread.Sleep(500);
            }
            update.Enabled= true;
            ShowDay(DataManager.I.CurrentDay);
        }

        private void ShowDay(DateTime day)
        {
            currentDay = day;
            nextDay.Enabled = DataManager.I.HasDataForNextDay(currentDay);
            nextDay.Text = nextDay.Enabled ? $"{DataManager.I.NextDayWithData(currentDay):dd/MM/yyyy}" : "No Data";
            prevDay.Enabled = DataManager.I.HasDataForPrevDay(currentDay);
            prevDay.Text = prevDay.Enabled ? $"{DataManager.I.PrevDayWithData(currentDay):dd/MM/yyyy}" : "No Data";
            update.Enabled = (day.Date == DataManager.I.CurrentDay.Date);
            DayData data = DataManager.I.DataFor(day);
            UpdateLabel(Czechia, data.cases.Mzcr, day);
            UpdateLabel(WHO, data.cases.Who, day, data.cases.Mzcr);
        }

        public void UpdateLabel(Label text, StateCaseData data, DateTime day, StateCaseData dataDiff = null)
        {
            bool showDiff = dataDiff != null;
            var updated = data.updated;
            text.Text = $"New cases per day : {data.PerDay}{(showDiff ? HighlightDiffecence(data.PerDay, dataDiff.PerDay) : "")}\nTotal count:{data.Total}{(showDiff ? HighlightDiffecence(data.Total, dataDiff.Total) : "")}\nDate {day:dd/MM/yyyy}\nUpdate {updated:dd/MM/yyyy}";
        }
        private string HighlightDiffecence(int a, int b)
        {
            return a != b ? $"<color=\"red\">{(a > b ? "+" : "")}{a - b}</color>" : "";
        }

        private void CasesWindow_Shown(object sender, EventArgs e)
        {
            ShowDay(DataManager.I.CurrentDay);
        }
    }
}

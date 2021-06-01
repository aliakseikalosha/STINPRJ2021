using netSTIN.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace netSTIN
{
    public partial class VacinationWindow : Form
    {
        private static List<VacinationStateData> countries = null;
        private Dictionary<ComboBox, Label> comboToLabel = new Dictionary<ComboBox, Label>();
        private string[] selectedCountries;
        protected DateTime currentDay;
        public VacinationWindow()
        {
            InitializeComponent();
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


            Init(combo1, label1);

            LoadCountries();
        }

        private void Init(ComboBox cb, Label label)
        {
            if (comboToLabel.ContainsKey(cb))
            {
                return;
            }
            comboToLabel.Add(cb, label);
            cb.BeginUpdate();
            cb.Items.Clear();
            cb.Items.AddRange(countries.Select(c => (object)c.StateName).ToArray());
            cb.EndUpdate();
        }

        private void LoadCountries()
        {
            if (countries == null)
            {
                countries = DataManager.I.DataFor(DataManager.I.CurrentDay).vacination.data;
            }
            //selectedCountries = new string[elements.Count];
            //var data = PlayerPrefs.GetString(dataKey, $"{firstCountry},");
            //var d = data.Split(',');
            //for (int i = 0; i < d.Length - 1; i++)
            //{
            //    if (i >= selectedCountries.Length)
            //    {
            //        Debug.LogError($"get more counry then can show  data:{data}");
            //        continue;
            //    }
            //    selectedCountries[i] = d[i];
            //}
            //selectedCountries[0] = firstCountry;
        }

        private void ChangeScreen_Click(object sender, EventArgs e)
        {
            Program.ChangeWindow();
        }

        private void prevDay_Click(object sender, EventArgs e)
        {

        }

        private void update_Click(object sender, EventArgs e)
        {

        }

        private void nextDay_Click(object sender, EventArgs e)
        {

        }

        private void SelectionChangeCommitted(object sender, EventArgs e)
        {
            var cb = (ComboBox)sender;
            var label = comboToLabel[cb];
            var selected = (string)cb.SelectedItem;
            var data = countries.First(c => c.StateName == selected);
            label.Text = $"{data.Percent:F2}%\t\t{data.Population}";
        }

        private void VacinationWindow_Shown(object sender, EventArgs e)
        {
            ShowDay(DataManager.I.CurrentDay);
        }
    }
}

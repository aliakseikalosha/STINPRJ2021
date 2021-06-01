using netSTIN.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace netSTIN
{
    public partial class VacinationWindow : Form
    {
        private static List<VacinationStateData> countries = null;
        private Dictionary<ComboBox, Label> comboToLabel = new Dictionary<ComboBox, Label>();
        private string[] selectedCountries;
        private string selectedCountriesPath = Path.Combine(Program.FilePath, "SelectedCountries.txt");
        private Dictionary<ComboBox, int> comboToIndexes = new Dictionary<ComboBox, int>();
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
            LoadCountries();
            UpdateDataCzech(labelCzech);
            Init(combo1, label1, 0);
            UpdateData(combo1);
            Init(combo2, label2, 1);
            UpdateData(combo2);
            Init(combo3, label3, 2);
            UpdateData(combo3);
            Init(combo4, label4, 3);
            UpdateData(combo4);
        }

        private void Init(ComboBox cb, Label label, int index)
        {
            if (!comboToIndexes.ContainsKey(cb))
            {
                comboToIndexes.Add(cb, index);
            }
            if (comboToLabel.ContainsKey(cb))
            {
                return;
            }
            comboToLabel.Add(cb, label);
            cb.BeginUpdate();
            cb.Items.Clear();
            cb.Items.AddRange(countries.Select(c => (object)c.StateName).ToArray());
            cb.EndUpdate();
            cb.SelectedIndex = cb.Items.IndexOf(selectedCountries[comboToIndexes[cb]]);
        }

        private void SaveCountries()
        {
            string data = string.Empty;
            foreach (var country in selectedCountries)
            {
                data += $"{country},";
            }
            File.WriteAllText(selectedCountriesPath, data);
        }

        private void SetCountry(int index, string country)
        {
            selectedCountries[index] = country;
            SaveCountries();
        }

        private void LoadCountries()
        {
            countries = DataManager.I.DataFor(currentDay).vacination.data;
            selectedCountries = new string[4];
            if (!File.Exists(selectedCountriesPath))
            {
                return;
            }
            var data = File.ReadAllText(selectedCountriesPath);
            var dataSplit = data.Split(',');
            for (int i = 0; i < dataSplit.Length - 1; i++)
            {
                if (i >= selectedCountries.Length)
                {
                    Debug.LogError($"get more counry then can show  data:{data}");
                    continue;
                }
                selectedCountries[i] = dataSplit[i];
            }
        }

        private void ChangeScreen_Click(object sender, EventArgs e)
        {
            Program.ChangeWindow();
        }

        private void prevDay_Click(object sender, EventArgs e)
        {
            if (DataManager.I.HasDataForPrevDay(currentDay))
            {
                ShowDay(DataManager.I.PrevDayWithData(currentDay));
            }
        }

        private void update_Click(object sender, EventArgs e)
        {
            StartRefresh();
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
            update.Enabled = true;
            ShowDay(DataManager.I.CurrentDay);
        }

        private void nextDay_Click(object sender, EventArgs e)
        {
            if (DataManager.I.HasDataForNextDay(currentDay))
            {
                ShowDay(DataManager.I.NextDayWithData(currentDay));
            }
        }

        private void SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateData((ComboBox)sender);
        }

        private void UpdateData(ComboBox cb)
        {
            SetCountry(comboToIndexes[cb], (string)cb.SelectedItem);
            if (cb.SelectedItem == null)
            {
                return;
            }
            var label = comboToLabel[cb];
            var selected = (string)cb.SelectedItem;
            var data = countries.First(c => c.StateName == selected);
            label.Text = $@"{data.StateName}:   {data.Percent:F2}%        {data.Population}";
        }

        private void UpdateDataCzech(Label label)
        {
            var data = countries.First(c => c.StateName == "Czechia");
            label.Text = $@"{data.StateName}:   {data.Percent:F2}%        {data.Population}";
        }

        private void VacinationWindow_Shown(object sender, EventArgs e)
        {
            ShowDay(DataManager.I.CurrentDay);
        }
    }
}

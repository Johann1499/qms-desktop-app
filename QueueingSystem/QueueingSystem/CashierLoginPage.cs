using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace QueueingSystem
{
    public partial class CashierLoginPage : Form
    {
        private static readonly HttpClient client = new HttpClient { BaseAddress = new Uri("http://localhost:8080/api/") };
        private string selectedCashierID = "";
        private string selectedCashierName = "";
        private JArray allCashiers;

        public CashierLoginPage()
        {
            InitializeComponent();
            InitializeComboBoxes();
            LoadAllCashiersAsync().ConfigureAwait(false); // ConfigureAwait to avoid deadlock
        }

        private void InitializeComboBoxes()
        {
            cmbDept.Items.AddRange(new[] { "Basic Education", "College" });
            cmbCashier.Enabled = false;
            btnNext.Enabled = false;
        }

        private async Task LoadAllCashiersAsync()
        {
            try
            {
                string response = await client.GetStringAsync("cashiers/inactive");
                allCashiers = JArray.Parse(response);
            }
            catch (Exception ex)
            {
                ShowError("Failed to load cashiers.", ex);
            }
        }

        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCashierComboBox();
        }

        private void UpdateCashierComboBox()
        {
            cmbCashier.Items.Clear();

            var selectedDept = cmbDept.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedDept) || allCashiers == null)
            {
                cmbCashier.Enabled = false;
                btnNext.Enabled = false;
                return;
            }

            var cashiers = allCashiers
                .Where(cashier => cashier["department"]?.ToString() == selectedDept)
                .Select(cashier => cashier["name"]?.ToString())
                .ToArray();

            if (cashiers.Any())
            {
                cmbCashier.Items.AddRange(cashiers);
                cmbCashier.Enabled = true;
            }
            else
            {
                MessageBox.Show("No available cashiers for the selected department.");
            }
        }

        private void cmbCashier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCashier.SelectedIndex < 0) return;

            var selectedName = cmbCashier.SelectedItem?.ToString();
            var selectedCashier = allCashiers?.FirstOrDefault(cashier => cashier["name"]?.ToString() == selectedName);

            if (selectedCashier != null)
            {
                selectedCashierID = selectedCashier["id"]?.ToString();
                selectedCashierName = selectedCashier["name"]?.ToString();
                btnNext.Enabled = !string.IsNullOrEmpty(selectedCashierID);
            }
        }

        private async void btnNext_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedCashierID))
            {
                MessageBox.Show("Please select a cashier.");
                return;
            }

            try
            {
                if (await CheckCashierStatusAsync(selectedCashierID))
                {
                    MessageBox.Show($"{selectedCashierName} is already active.");
                }
                else if (await SetCashierActiveAsync(selectedCashierID))
                {
                    var cashierOperate = new CashierOperate(cmbDept.SelectedItem.ToString(), selectedCashierID, selectedCashierName);
                    Hide();
                    cashierOperate.Show();
                }
            }
            catch (Exception ex)
            {
                ShowError("An error occurred during the operation.", ex);
            }
        }

        private async Task<bool> CheckCashierStatusAsync(string cashierId)
        {
            try
            {
                var response = await client.GetStringAsync($"cashiers/{cashierId}/status");
                var result = JObject.Parse(response);
                return result["status"]?.ToString() == "1"; // Assuming status '1' means active
            }
            catch (Exception ex)
            {
                ShowError("Error checking cashier status.", ex);
                return false;
            }
        }

        private async Task<bool> SetCashierActiveAsync(string cashierId)
        {
            var updateData = new { status = 1 }; // Set status to active (1)
            var content = new StringContent(JObject.FromObject(updateData).ToString(), Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PutAsync($"cashiers/{cashierId}/status", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                ShowError("Error setting cashier active.", ex);
                return false;
            }
        }

        private void ShowError(string message, Exception ex)
        {
            MessageBox.Show($"{message}\nDetails: {ex.Message}");
        }
    }
}

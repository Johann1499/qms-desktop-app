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
            LoadAllCashiersAsync();
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
                var response = await client.GetStringAsync("cashiers/inactive");
                allCashiers = JArray.Parse(response);
                UpdateCashierComboBox(); // Update combo box with the latest data
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
            var selectedDept = cmbDept.SelectedItem?.ToString();
            cmbCashier.Items.Clear();

            if (string.IsNullOrEmpty(selectedDept) || allCashiers == null)
                return;

            var cashiers = allCashiers
                .Where(cashier => cashier["department"]?.ToString() == selectedDept)
                .Select(cashier => cashier["name"]?.ToString())
                .ToList();

            if (cashiers.Count > 0)
            {
                cmbCashier.Items.AddRange(cashiers.ToArray());
                cmbCashier.Enabled = true;
            }
            else
            {
                MessageBox.Show("No available cashiers for the selected department.");
            }
        }

        private void cmbCashier_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Ensure there are items in cmbCashier before trying to access the selected item
            if (cmbCashier.SelectedIndex >= 0 && cmbCashier.SelectedIndex < cmbCashier.Items.Count)
            {
                var selectedName = cmbCashier.SelectedItem?.ToString();
                var selectedCashier = allCashiers?
                    .FirstOrDefault(cashier => cashier["name"]?.ToString() == selectedName);

                if (selectedCashier != null)
                {
                    selectedCashierID = selectedCashier["id"]?.ToString();
                    selectedCashierName = selectedCashier["name"]?.ToString();
                    btnNext.Enabled = !string.IsNullOrEmpty(selectedCashierID);
                }
            }
        }

        private async void btnNext_Click(object sender, EventArgs e)
        {
            var selectedDept = cmbDept.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedDept) || string.IsNullOrEmpty(selectedCashierID))
            {
                MessageBox.Show("Please select a department and a cashier.");
                return;
            }

            // Check if the cashier is already active
            if (await CheckCashierStatusAsync(selectedCashierID))
            {
                MessageBox.Show($"{selectedCashierName} is already active.");
                return;
            }

            // Set the cashier to active if not already active
            var isSuccessful = await SetCashierActiveAsync(selectedCashierID);
            if (isSuccessful)
            {
                var cashierOperate = new CashierOperate(selectedDept, selectedCashierID, selectedCashierName);
                Hide();
                cashierOperate.Show();
            }
        }

        private async Task<bool> CheckCashierStatusAsync(string cashierId)
        {
            try
            {
                var response = await client.GetAsync($"cashiers/{cashierId}/status");
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var result = JObject.Parse(responseBody);
                    var status = result["status"]?.ToString();

                    // Assuming that status '1' means active
                    return status == "1";
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Error checking cashier status: {response.StatusCode} - {errorMessage}");
                    return false;
                }
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
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Welcome {selectedCashierName}.");
                    return true;
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Error setting cashier active: {response.StatusCode} - {errorMessage}");
                    return false;
                }
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

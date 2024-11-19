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
        private static readonly HttpClient client = new HttpClient { BaseAddress = new Uri("https://www.dctqueue.info/api/") };
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
            int retryCount = 0;
            const int maxRetries = 5;
            const int baseDelay = 2000; // 2 seconds initial delay

            while (retryCount < maxRetries)
            {
                try
                {
                    string response = await client.GetStringAsync("cashiers/inactive");
                    allCashiers = JArray.Parse(response);
                    break; // Exit loop if successful
                }
                catch (HttpRequestException e)
                {
                    ShowError("Failed to load cashiers.", e);
                    break; // Exit if network or HTTP error occurs
                }
                catch (Exception ex)
                {
                    ShowError("Unexpected error occurred while loading cashiers.", ex);
                    break; // Exit for unexpected errors
                }

                retryCount++;
                if (retryCount < maxRetries)
                {
                    await Task.Delay(baseDelay * (int)Math.Pow(2, retryCount - 1)); // Exponential backoff
                }
            }

            if (retryCount == maxRetries)
            {
                MessageBox.Show("Maximum retry attempts reached. Please try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            btnNext.Enabled = false;
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
            int retryCount = 0;
            const int maxRetries = 5;
            const int baseDelay = 2000; // 2 seconds initial delay

            while (retryCount < maxRetries)
            {
                try
                {
                    var response = await client.GetStringAsync($"cashiers/{cashierId}/status");
                    var result = JObject.Parse(response);
                    return result["status"]?.ToString() == "1"; // Assuming status '1' means active
                }
                catch (HttpRequestException e)
                {
                    ShowError("Error checking cashier status.", e);
                    break; // Exit if network or HTTP error occurs
                }
                catch (Exception ex)
                {
                    ShowError("Unexpected error occurred while checking cashier status.", ex);
                    break; // Exit for unexpected errors
                }

                retryCount++;
                if (retryCount < maxRetries)
                {
                    await Task.Delay(baseDelay * (int)Math.Pow(2, retryCount - 1)); // Exponential backoff
                }
            }

            if (retryCount == maxRetries)
            {
                MessageBox.Show("Maximum retry attempts reached. Please try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return false;
        }

        private async Task<bool> SetCashierActiveAsync(string cashierId)
        {
            int retryCount = 0;
            const int maxRetries = 5;
            const int baseDelay = 2000; // 2 seconds initial delay

            var updateData = new { status = 1 }; // Set status to active (1)
            var content = new StringContent(JObject.FromObject(updateData).ToString(), Encoding.UTF8, "application/json");

            while (retryCount < maxRetries)
            {
                try
                {
                    var response = await client.PutAsync($"cashiers/{cashierId}/status", content);
                    return response.IsSuccessStatusCode;
                }
                catch (HttpRequestException e)
                {
                    ShowError("Error setting cashier active.", e);
                    break; // Exit if network or HTTP error occurs
                }
                catch (Exception ex)
                {
                    ShowError("Unexpected error occurred while setting cashier active.", ex);
                    break; // Exit for unexpected errors
                }

                retryCount++;
                if (retryCount < maxRetries)
                {
                    await Task.Delay(baseDelay * (int)Math.Pow(2, retryCount - 1)); // Exponential backoff
                }
            }

            if (retryCount == maxRetries)
            {
                MessageBox.Show("Maximum retry attempts reached. Please try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return false;
        }

        private void ShowError(string message, Exception ex)
        {
            MessageBox.Show($"{message}\nDetails: {ex.Message}");
        }
    }
}

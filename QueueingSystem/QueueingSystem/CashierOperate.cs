using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Speech.Synthesis;
using Newtonsoft.Json;
using System.Text;

namespace QueueingSystem
{
    public partial class CashierOperate : Form
    {
        private static readonly HttpClient client = new HttpClient { BaseAddress = new Uri("http://localhost:8080/api/") };
        private const string QueueEndpoint = "queues";
        private const string CashierEndpoint = "cashiers";
        private readonly string selectedDepartment;
        private readonly string cashierId;
        private readonly string cashierName;
        private readonly Timer updateTimer = new Timer { Interval = 15000 };
        private readonly SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();

        public CashierOperate(string department, string selectedCashier, string selectedCashierName)
        {
            InitializeComponent();
            selectedDepartment = department;
            cashierId = selectedCashier;
            cashierName = selectedCashierName;

            // Set up the timer and event handlers
            updateTimer.Tick += async (sender, e) => await LoadQueueData();
            Resize += (sender, e) => AdjustListViewColumnWidths();
        }

        private async void CashierOperate_Load(object sender, EventArgs e)
        {
            SetupListView();
            await LoadQueueData();
            updateTimer.Start();
            lblCashier.Text = cashierName;
        }

        private void SetupListView()
        {
            listLiveQueue.Clear();
            listLiveQueue.GridLines = true;
            listLiveQueue.View = View.Details;
            listLiveQueue.FullRowSelect = true;
            listLiveQueue.Columns.AddRange(new[]
            {
                new ColumnHeader { Text = "ID", Width = -2 },
                new ColumnHeader { Text = "Name", Width = -2 },
                new ColumnHeader { Text = "Student Number", Width = -2 },
                new ColumnHeader { Text = "Queue Number", Width = -2 }
            });
            lblQueue.Text = "No queue number";
        }

        private async Task LoadQueueData()
        {
            listLiveQueue.Items.Clear();
            try
            {
                var response = await client.GetStringAsync($"{QueueEndpoint}?department={selectedDepartment}");
                var queueData = JArray.Parse(response);

                if (queueData.Count > 0)
                {
                    foreach (var item in queueData)
                    {
                        var lv = new ListViewItem(item["id"].ToString())
                        {
                            SubItems =
                            {
                                item["name"].ToString(),
                                item["student_number"].ToString(),
                                item["queue_number"].ToString()
                            }
                        };
                        listLiveQueue.Items.Add(lv);
                    }
                    lblQueue.Text = queueData[0]["queue_number"].ToString();
                }
                else
                {
                    lblQueue.Text = "No queue number";
                }

                AdjustListViewColumnWidths();
            }
            catch (Exception ex)
            {
                ShowError("Error loading queue data.", ex);
            }
        }

        private void AdjustListViewColumnWidths()
        {
            int listViewWidth = listLiveQueue.ClientSize.Width;
            listLiveQueue.Columns[0].Width = (int)(listViewWidth * 0.1);
            listLiveQueue.Columns[1].Width = (int)(listViewWidth * 0.3);
            listLiveQueue.Columns[2].Width = (int)(listViewWidth * 0.3);
            listLiveQueue.Columns[3].Width = (int)(listViewWidth * 0.3);
        }

        private async void btnRefresh_Click(object sender, EventArgs e) => await LoadQueueData();

        private async void btnNext_Click(object sender, EventArgs e)
        {
            if (listLiveQueue.Items.Count == 0)
            {
                MessageBox.Show("No more queue.");
                return;
            }
            lblQueue.Text = "";
            // Get the latest item
            var latestItem = listLiveQueue.Items[0];
            var latestItemId = latestItem.SubItems[0].Text;
            string customerQueueNumber;

            if (listLiveQueue.Items.Count > 1)
            {
                // If there's more than one item, get the next one in line
                var nextItem = listLiveQueue.Items[1];
                customerQueueNumber = nextItem.SubItems[3].Text;
                // Announce the next customer
                AnnounceNextCustomer(customerQueueNumber);
            }
            else
            {
                // If only one item is left, no next item exists
                customerQueueNumber = latestItem.SubItems[3].Text;
            }

            try
            {
                // Attempt to delete the latest item
                var response = await client.DeleteAsync($"{QueueEndpoint}/{latestItemId}");
                response.EnsureSuccessStatusCode();

                // Remove the latest item from the list
                listLiveQueue.Items.Remove(latestItem);
                MessageBox.Show("Record removed.");



                // Reload the queue data
                await LoadQueueData();
            }
            catch (Exception ex)
            {
                ShowError("Error deleting record.", ex);
            }
        }


        private void AnnounceNextCustomer(string customerQueueNumber) =>
            speechSynthesizer.SpeakAsync($"{customerQueueNumber} please proceed to the counter.");
        private void AnnounceNotifyCustomer(string customerQueueNumber) =>
            speechSynthesizer.SpeakAsync($"{customerQueueNumber} please proceed to the counter.");
        private async Task SetCashierInactiveAsync(string cashierId)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(new { status = false }), Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"{CashierEndpoint}/{cashierId}/status", content);
                response.EnsureSuccessStatusCode();
                Close();
            }
            catch (Exception ex)
            {
                ShowError("Error setting cashier inactive.", ex);
            }
        }

        private async void CashierOperate_FormClosing(object sender, FormClosingEventArgs e)
        {
            updateTimer.Stop();
            if (MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }

            await SetCashierInactiveAsync(cashierId);
            speechSynthesizer.Dispose();
        }

        private void ShowError(string message, Exception ex) =>
            MessageBox.Show($"{message}\nDetails: {ex.Message}");

        private void btnNotify_Click(object sender, EventArgs e)
        {
            if (listLiveQueue.Items.Count == 0)
            {
                MessageBox.Show("No records to delete.");
                return;
            }

            var latestItem = listLiveQueue.Items[0];
            var customerQueueNumber = latestItem.SubItems[3].Text;
            AnnounceNotifyCustomer(customerQueueNumber);
        }
    }
}

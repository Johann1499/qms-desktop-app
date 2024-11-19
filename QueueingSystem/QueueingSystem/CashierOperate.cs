using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Linq;

namespace QueueingSystem
{
    public partial class CashierOperate : Form
    {
        private static readonly HttpClient client = new HttpClient { BaseAddress = new Uri("https://www.dctqueue.info/api/") };
        private const string QueueEndpoint = "queues";
        private const string CashierEndpoint = "cashiers";
        private const string NotifyEndpoint = "notify";
        private readonly string selectedDepartment;
        private readonly string cashierId;
        private readonly string cashierName;
        private readonly Timer updateTimer = new Timer { Interval = 15000 };

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
            Console.WriteLine("Console: " + selectedDepartment);
            Console.WriteLine("Console: " + cashierId);
            Console.WriteLine("Console: " + cashierName);
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
            int retryCount = 0;
            const int maxRetries = 5;
            const int baseDelay = 2000; // Start with 2 seconds delay

            while (retryCount < maxRetries)
            {
                try
                {
                    var response = await client.GetAsync($"{QueueEndpoint}?department={selectedDepartment}");

                    // Check if the response status is 429 (Too Many Requests)
                    if (response.StatusCode == (System.Net.HttpStatusCode)429)
                    {
                        retryCount++;
                        // Check for Retry-After header and wait accordingly
                        if (response.Headers.TryGetValues("Retry-After", out var values) && int.TryParse(values.FirstOrDefault(), out int retryAfter))
                        {
                            await Task.Delay(retryAfter * 1000);
                        }
                        else
                        {
                            // Exponential backoff if Retry-After is not specified
                            await Task.Delay(baseDelay * (int)Math.Pow(2, retryCount - 1));
                        }
                        continue; // Retry the loop
                    }

                    // Ensure the response is successful if no 429 error occurred
                    response.EnsureSuccessStatusCode();
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var queueData = JArray.Parse(responseContent);

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
                            if (InvokeRequired)
                            {
                                Invoke(new Action(() => listLiveQueue.Items.Add(lv)));
                            }
                            else
                            {
                                listLiveQueue.Items.Add(lv);
                            }
                        }
                        lblQueue.Text = queueData[0]["queue_number"].ToString();
                    }
                    else
                    {
                        lblQueue.Text = "No queue number";
                    }

                    AdjustListViewColumnWidths();
                    break; // Exit the loop if successful
                }
                catch (HttpRequestException httpEx)
                {
                    ShowError("Error loading queue data. Check the network or API availability.", httpEx);
                    break; // Exit if it's a network or other HTTP error
                }
                catch (Exception ex)
                {
                    ShowError("Unexpected error occurred while loading queue data.", ex);
                    break; // Exit for unexpected errors
                }
            }

            if (retryCount == maxRetries)
            {
                ShowError("Maximum retry attempts reached. Please try again later.", null);
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

            // Get the latest item
            var latestItem = listLiveQueue.Items[0];
            var latestItemId = latestItem.SubItems[0].Text;

            // Show confirmation dialog
            var confirmResult = MessageBox.Show("Are you sure you want to remove the record from the queue?",
                                                "Confirm Deletion",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    // Attempt to delete the latest item
                    var response = await client.DeleteAsync($"{QueueEndpoint}/{latestItemId}");
                    response.EnsureSuccessStatusCode();

                    // Remove the latest item from the list
                    if (InvokeRequired)
                    {
                        Invoke(new Action(() => listLiveQueue.Items.Remove(latestItem)));
                    }
                    else
                    {
                        listLiveQueue.Items.Remove(latestItem);
                    }

                    MessageBox.Show("Record removed.");

                    // Reload the queue data
                    await LoadQueueData();
                }
                catch (Exception ex)
                {
                    ShowError("Error deleting record.", ex);
                }
            }
        }


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
            updateTimer.Dispose();

            if (MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }

            await SetCashierInactiveAsync(cashierId);
        }

        private void ShowError(string message, Exception ex) =>
            MessageBox.Show($"{message}\nDetails: {ex.Message}");

        private void btnNotify_Click(object sender, EventArgs e)
        {
            if (listLiveQueue.Items.Count == 0)
            {
                MessageBox.Show("No records to notify.");
                return;
            }
            else{
                AnnounceQueueNumber(lblQueue.Text);
            }

        }
        public void AnnounceQueueNumber(string queueNumber)
        {
            using (var synthesizer = new System.Speech.Synthesis.SpeechSynthesizer())
            {
                synthesizer.Speak($"{queueNumber} please proceed to the counter.");
            }
        }
    }
}

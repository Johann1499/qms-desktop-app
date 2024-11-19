using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace QueueingSystem
{
    public partial class AdminQueue : Form
    {
        private static readonly HttpClient client = new HttpClient();
        private const string apiUrl = "https://www.dctqueue.info/api/queues";
        private Timer refreshTimer; // Timer to refresh data

        public AdminQueue()
        {
            InitializeComponent();
            this.Resize += new EventHandler(Form_Resize); // Handle form resize events

            // Initialize and configure the timer
            refreshTimer = new Timer();
            refreshTimer.Interval = 30000; // Set interval to 30 seconds (30000 ms)
            refreshTimer.Tick += new EventHandler(OnTimerTick);
        }

        private async void AdminQueue_Load(object sender, EventArgs e)
        {
            listQueue.Clear();
            listQueue.GridLines = true;
            listQueue.View = View.Details;
            listQueue.FullRowSelect = true;

            listQueue.Columns.Add("ID");
            listQueue.Columns.Add("Name");
            listQueue.Columns.Add("Student Number");
            listQueue.Columns.Add("Department");
            listQueue.Columns.Add("Queue Number");

            // Set initial column widths
            SetColumnWidths();

            // Load data initially
            await LoadQueueData();

            // Start the timer
            refreshTimer.Start();
        }

        private async void OnTimerTick(object sender, EventArgs e)
        {
            await LoadQueueData(); // Reload data periodically
        }

        private async Task LoadQueueData()
        {
            int retryCount = 0;
            const int maxRetries = 5;
            const int baseDelay = 2000; // 2 seconds initial delay

            while (retryCount < maxRetries)
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    // Check if response status is 429 (Too Many Requests)
                    if (response.StatusCode == (System.Net.HttpStatusCode)429)
                    {
                        retryCount++;
                        if (response.Headers.TryGetValues("Retry-After", out var values) && int.TryParse(values.FirstOrDefault(), out int retryAfter))
                        {
                            await Task.Delay(retryAfter * 1000); // Wait based on the Retry-After header
                        }
                        else
                        {
                            await Task.Delay(baseDelay * (int)Math.Pow(2, retryCount - 1)); // Exponential backoff
                        }
                        continue; // Retry the loop
                    }

                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    JArray queues = JArray.Parse(responseBody);

                    listQueue.Items.Clear(); // Clear existing items before adding new data

                    foreach (var queue in queues)
                    {
                        ListViewItem lv = new ListViewItem(queue["id"].ToString());
                        lv.SubItems.Add(queue["name"].ToString());
                        lv.SubItems.Add(queue["student_number"].ToString());
                        lv.SubItems.Add(queue["department"].ToString());
                        lv.SubItems.Add(queue["queue_number"].ToString());
                        listQueue.Items.Add(lv);
                    }
                    break; // Exit loop if successful
                }
                catch (HttpRequestException e)
                {
                    MessageBox.Show($"Error loading queue data. Please check the network or API availability.\n\n{e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break; // Exit if network or HTTP error occurs
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Unexpected error occurred while loading queue data.\n\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break; // Exit for unexpected errors
                }
            }

            if (retryCount == maxRetries)
            {
                MessageBox.Show("Maximum retry attempts reached. Please try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async Task<JArray> FetchQueueData()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            return JArray.Parse(responseBody);
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            // Adjust column widths when the form is resized
            SetColumnWidths();
        }


        private void SetColumnWidths()
        {
            int listViewWidth = listQueue.ClientSize.Width;

            listQueue.Columns[0].Width = (int)(listViewWidth * 0.1); // ID (10%)
            listQueue.Columns[1].Width = (int)(listViewWidth * 0.2); // Name (20%)
            listQueue.Columns[2].Width = (int)(listViewWidth * 0.2); // Student Number (20%)
            listQueue.Columns[3].Width = (int)(listViewWidth * 0.2); // Department (20%)
            listQueue.Columns[4].Width = (int)(listViewWidth * 0.2); // Queue Number (20%)
        }
    }
}

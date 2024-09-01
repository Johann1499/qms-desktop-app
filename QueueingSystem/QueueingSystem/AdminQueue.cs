using System;
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
        private const string apiUrl = "http://localhost:8080/api/queues";
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
            listQueue.Items.Clear(); // Clear existing items before loading new data

            try
            {
                // Fetch data from Laravel API
                var queues = await FetchQueueData();

                foreach (var queue in queues)
                {
                    ListViewItem lv = new ListViewItem(queue["id"].ToString());
                    lv.SubItems.Add(queue["name"].ToString());
                    lv.SubItems.Add(queue["student_number"].ToString());
                    lv.SubItems.Add(queue["department"].ToString());
                    lv.SubItems.Add(queue["queue_number"].ToString());
                    listQueue.Items.Add(lv);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching data: " + ex.Message);
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

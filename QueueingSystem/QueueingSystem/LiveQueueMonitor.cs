using System;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace QueueingSystem
{
    public partial class LiveQueueMonitor : Form
    {
        private string department = "Basic Education";
        private static readonly HttpClient client = new HttpClient();
        private const string apiUrl = "http://localhost:8080/api/queues"; // Update this URL to match your API
        private bool isFullScreen = false;
        private FormBorderStyle previousBorderStyle;
        private FormWindowState previousWindowState;
        private Point previousLocation;
        private Size previousSize;
        private string videoPath;
        private Timer refreshTimer;

        public LiveQueueMonitor(string videoPath)
        {
            InitializeComponent();
            this.videoPath = videoPath;
            this.KeyDown += new KeyEventHandler(Form_KeyDown); // Attach key down event handler
            this.FormClosing += new FormClosingEventHandler(LiveQueueMonitor_FormClosing); // Attach form closing event handler

            // Initialize and configure the timer
            refreshTimer = new Timer();
            refreshTimer.Interval = 3000; // 3 seconds
            refreshTimer.Tick += new EventHandler(OnTimerTick);
        }

        private async void LiveQueueMonitor_Load(object sender, EventArgs e)
        {
            lblQueueNumber.Text = "No queue.";
            axWindowsMediaPlayer1.URL = videoPath;
            axWindowsMediaPlayer1.uiMode = "none";
            axWindowsMediaPlayer1.Ctlcontrols.play();
            axWindowsMediaPlayer1.Dock = DockStyle.Fill;

            await LoadQueueData();
            refreshTimer.Start(); // Start the timer to fetch data periodically
        }

        private async void OnTimerTick(object sender, EventArgs e)
        {
            await LoadQueueData(); // Fetch live data on each timer tick
        }

        private async Task LoadQueueData()
        {
            try
            {
                string urlWithDepartment = $"{apiUrl}?department={department}";
                HttpResponseMessage response = await client.GetAsync(urlWithDepartment);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                JArray queueData = JArray.Parse(responseBody);

                if (queueData.Count > 0)
                {
                    lblQueueNumber.Text = queueData[0]["queue_number"].ToString();
                }
                else
                {
                    lblQueueNumber.Text = "No queue.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading queue data: {ex.Message}");
            }
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11 || e.KeyCode == Keys.F)
            {
                ToggleFullScreen();
            }
        }

        private void EnterFullScreen()
        {
            previousBorderStyle = this.FormBorderStyle;
            previousWindowState = this.WindowState;
            previousLocation = this.Location;
            previousSize = this.Size;

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.TopMost = true;

            isFullScreen = true;
        }

        private void ExitFullScreen()
        {
            this.FormBorderStyle = previousBorderStyle;
            this.WindowState = previousWindowState;
            this.Location = previousLocation;
            this.Size = previousSize;
            this.TopMost = false;

            isFullScreen = false;
        }

        private void ToggleFullScreen()
        {
            if (isFullScreen)
            {
                ExitFullScreen();
            }
            else
            {
                EnterFullScreen();
            }
        }

        private void LiveQueueMonitor_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to close this form?",
                "Confirm Exit",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}

using System;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Speech.Synthesis;
using System.Collections.Generic;

namespace QueueingSystem
{
    public partial class LiveQueueMonitor : Form
    {
        private static readonly HttpClient client = new HttpClient();
        private const string apiUrl = "https://dctqueue.info/api";
        private bool isFullScreen = false;
        private FormBorderStyle previousBorderStyle;
        private FormWindowState previousWindowState;
        private Point previousLocation;
        private Size previousSize;
        private readonly string videoPath;
        private readonly int volume;
        private Timer refreshTimer, timerDate;
        private readonly SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();
        private readonly CashierOperate cashierOperate;
        private string lastAnnouncedBasicQueueNumber = "";
        private string lastAnnouncedCollegeQueueNumber = "";

        public LiveQueueMonitor(string videoPath, int volume)
        {
            InitializeComponent();
            this.videoPath = videoPath;
            this.volume = Clamp(volume, 0, 10);
            this.KeyDown += Form_KeyDown;

            cashierOperate = new CashierOperate("", "", "");
            InitializeTimers();
            SetupMediaPlayer();
        }

        private static int Clamp(int value, int min, int max) => Math.Max(min, Math.Min(value, max));

        private void InitializeTimers()
        {
            refreshTimer = new Timer { Interval = 3000 };
            refreshTimer.Tick += OnTimerTick;

            timerDate = new Timer { Interval = 3000 };
            timerDate.Tick += (sender, e) => UpdateDateTimeLabels();
            timerDate.Start();
        }

        private void SetupMediaPlayer()
        {
            axWindowsMediaPlayer1.URL = videoPath;
            axWindowsMediaPlayer1.settings.volume = volume * 10;
            axWindowsMediaPlayer1.uiMode = "none";
            axWindowsMediaPlayer1.Dock = DockStyle.Fill;
            axWindowsMediaPlayer1.settings.setMode("loop", true);
        }

        private void UpdateDateTimeLabels()
        {
            lblTime.Text = DateTime.Now.ToString("h:mm tt");
            lblDate.Text = DateTime.Now.ToString("d");
        }

        private async void LiveQueueMonitor_Load(object sender, EventArgs e)
        {
            lblQueueNumberCollege1.Text = "00000";
            lblQueueNumberBasic1.Text = "00000";
            await LoadQueueData();
            refreshTimer.Start();
        }

        private async void OnTimerTick(object sender, EventArgs e)
        {
            await LoadQueueData();
        }

        private async Task LoadQueueData()
        {
            const int maxRetries = 5;
            for (int retryCount = 0; retryCount < maxRetries; retryCount++)
            {
                try
                {
                    var queueData = await FetchQueueDataAsync();
                    ProcessQueueData(queueData);
                    break;
                }
                catch (HttpRequestException ex) when (ex.Message.Contains("429"))
                {
                    await HandleTooManyRequests(retryCount);
                }
                catch (Exception ex)
                {
                    ShowErrorMessage("Unexpected error loading queue data", ex);
                    break;
                }
            }
        }

        private async Task HandleTooManyRequests(int retryCount)
        {
            int retryAfterSeconds = (int)Math.Pow(2, retryCount) * 2;
            Console.WriteLine($"Received 429 Too Many Requests. Retrying in {retryAfterSeconds} seconds.");
            await Task.Delay(retryAfterSeconds * 1000);
        }

        private async Task<JArray> FetchQueueDataAsync()
        {
            HttpResponseMessage response = await client.GetAsync(apiUrl + "/queues");

            if (response.StatusCode == (System.Net.HttpStatusCode)429)
            {
                var retryAfter = response.Headers.RetryAfter?.Delta?.Seconds ?? 60;
                Console.WriteLine($"429 Too Many Requests. Retrying in {retryAfter} seconds.");
                await Task.Delay(retryAfter * 1000);
                return await FetchQueueDataAsync();
            }

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return JArray.Parse(responseBody);
        }

        private void ProcessQueueData(JArray queueData)
        {
            var basicQueue = queueData.Where(q => q["department"].ToString() == "Basic Education")
                                       .OrderBy(q => q["queue_number"]).ToList();
            var collegeQueue = queueData.Where(q => q["department"].ToString() == "College")
                                         .OrderBy(q => q["queue_number"]).ToList();

            UpdateNextFiveQueueNumbers(queueData);
            UpdateQueueNumberDisplay(basicQueue, "Basic Education", lblQueueNumberBasic1, ref lastAnnouncedBasicQueueNumber);
            UpdateQueueNumberDisplay(collegeQueue, "College", lblQueueNumberCollege1, ref lastAnnouncedCollegeQueueNumber);
        }

        private void UpdateNextFiveQueueNumbers(JArray queueData)
        {
            UpdateDepartmentQueueDisplay(queueData, "Basic Education", "lblQueueNumberBasic");
            UpdateDepartmentQueueDisplay(queueData, "College", "lblQueueNumberCollege");
        }

        private void UpdateDepartmentQueueDisplay(JArray queueData, string department, string labelPrefix)
        {
            var queue = queueData.Where(q => q["department"].ToString() == department)
                                 .OrderBy(q => q["queue_number"])
                                 .Take(6)
                                 .Select(q => q["queue_number"]?.ToString() ?? "No queue")
                                 .ToList();

            for (int i = 0; i < queue.Count; i++)
            {
                if (panel1.Controls[$"{labelPrefix}{i + 1}"] is Label label)
                    label.Text = queue[i];
            }
        }

        private void UpdateQueueNumberDisplay(List<JToken> queue, string department, Label label, ref string lastAnnouncedQueueNumber)
        {
            string currentQueueNumber = queue.Any() ? queue.First()["queue_number"].ToString() : "No queue";
            if (label.InvokeRequired)
                label.Invoke(new Action(() => label.Text = currentQueueNumber));
            else
                label.Text = currentQueueNumber;

            if (currentQueueNumber != lastAnnouncedQueueNumber)
            {
                AnnounceCurrentQueueNumber(currentQueueNumber);
                lastAnnouncedQueueNumber = currentQueueNumber;
            }
        }

        private void AnnounceCurrentQueueNumber(string queueNumber)
        {
            speechSynthesizer.SpeakAsync($"{queueNumber} please proceed to the counter.");
            cashierOperate?.AnnounceQueueNumber(queueNumber);
        }

        private void ShowErrorMessage(string message, Exception ex)
        {
            MessageBox.Show($"{message}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11 || e.KeyCode == Keys.F)
                ToggleFullScreen();
        }

        private void EnterFullScreen()
        {
            previousBorderStyle = FormBorderStyle;
            previousWindowState = WindowState;
            previousLocation = Location;
            previousSize = Size;

            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            Location = Screen.PrimaryScreen.WorkingArea.Location;
            Size = Screen.PrimaryScreen.WorkingArea.Size;
            TopMost = true;
            isFullScreen = true;
        }

        private void ExitFullScreen()
        {
            FormBorderStyle = previousBorderStyle;
            WindowState = previousWindowState;
            Location = previousLocation;
            Size = previousSize;
            TopMost = false;
            isFullScreen = false;
        }

        private void ToggleFullScreen()
        {
            if (isFullScreen) ExitFullScreen();
            else EnterFullScreen();
        }
    }
}

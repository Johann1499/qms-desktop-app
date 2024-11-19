using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace QueueingSystem
{
    public partial class AdminHome : Form
    {
        // HttpClient should be static or used as a singleton to avoid socket exhaustion
        private static readonly HttpClient client = new HttpClient { BaseAddress = new Uri("https://www.dctqueue.info/api/") };
        private Timer timer;

        public AdminHome()
        {
            InitializeComponent();

            // Set up the timer
            timer = new Timer();
            timer.Interval = 15000; // Set the interval to 60 sec
            timer.Tick += Timer_Tick;
            timer.Start();

            // Initial load
            _ = LoadQueueCountByDepartment();
            _ = LoadCashierCountByDepartment();
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            await LoadQueueCountByDepartment();
            await LoadCashierCountByDepartment();
            timer.Start();
        }


        private async Task LoadQueueCountByDepartment()
        {
            string apiUrl = "queues/count-by-department";
            int retryCount = 0;
            const int maxRetries = 5;
            const int baseDelay = 2000; // 2 seconds initial delay

            while (retryCount < maxRetries)
            {
                try
                {
                    var response = await client.GetAsync(apiUrl);

                    if (response.StatusCode == (System.Net.HttpStatusCode)429)
                    {
                        retryCount++;
                        if (response.Headers.TryGetValues("Retry-After", out var values) && int.TryParse(values.FirstOrDefault(), out int retryAfter))
                        {
                            await Task.Delay(retryAfter * 1000);
                        }
                        else
                        {
                            await Task.Delay(baseDelay * (int)Math.Pow(2, retryCount - 1));
                        }
                        continue; // Retry the loop
                    }

                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    JArray jsonArray = JArray.Parse(responseBody);

                    foreach (var item in jsonArray)
                    {
                        string department = item["department"].ToString();
                        int totalQueues = item["total"].Value<int>();

                        if (department == "College")
                        {
                            lblCollege.Text = $"{totalQueues}";
                        }
                        else if (department == "Basic Education")
                        {
                            lblBasic.Text = $"{totalQueues}";
                        }
                    }
                    break; // Exit loop if successful
                }
                catch (HttpRequestException e)
                {
                    ShowError("Error loading queue count data. Check the network or API availability.", e);
                    break;
                }
                catch (Exception ex)
                {
                    ShowError("Unexpected error occurred while loading queue count data.", ex);
                    break;
                }
            }

            if (retryCount == maxRetries)
            {
                ShowError("Maximum retry attempts reached for loading queue count data. Please try again later.", null);
            }
        }

        private async Task LoadCashierCountByDepartment()
        {
            string apiUrl = "cashiers/total-by-department-status";
            int retryCount = 0;
            const int maxRetries = 5;
            const int baseDelay = 2000; // 2 seconds initial delay

            while (retryCount < maxRetries)
            {
                try
                {
                    var response = await client.GetAsync(apiUrl);

                    if (response.StatusCode == (System.Net.HttpStatusCode)429)
                    {
                        retryCount++;
                        if (response.Headers.TryGetValues("Retry-After", out var values) && int.TryParse(values.FirstOrDefault(), out int retryAfter))
                        {
                            await Task.Delay(retryAfter * 1000);
                        }
                        else
                        {
                            await Task.Delay(baseDelay * (int)Math.Pow(2, retryCount - 1));
                        }
                        continue; // Retry the loop
                    }

                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    JArray jsonArray = JArray.Parse(responseBody);

                    foreach (var item in jsonArray)
                    {
                        string department = item["department"].ToString();
                        string status = item["status"].ToString();
                        int totalCashiers = item["total"].Value<int>();

                        if (department == "Basic Education")
                        {
                            if (status == "1")
                            {
                                lblActiveBasic.Text = $"{totalCashiers}";
                            }
                            else if (status == "0")
                            {
                                lblInactiveBasic.Text = $"{totalCashiers}";
                            }
                        }
                        else if (department == "College")
                        {
                            if (status == "1")
                            {
                                lblActiveCollege.Text = $"{totalCashiers}";
                            }
                            else if (status == "0")
                            {
                                lblInactiveCollege.Text = $"{totalCashiers}";
                            }
                        }
                    }
                    break; // Exit loop if successful
                }
                catch (HttpRequestException e)
                {
                    ShowError("Error loading cashier count data. Check the network or API availability.", e);
                    break;
                }
                catch (Exception ex)
                {
                    ShowError("Unexpected error occurred while loading cashier count data.", ex);
                    break;
                }
            }

            if (retryCount == maxRetries)
            {
                ShowError("Maximum retry attempts reached for loading cashier count data. Please try again later.", null);
            }
        }

        private void ShowError(string message, Exception ex)
        {
            string errorMessage = ex != null ? $"{message}\n\n{ex.Message}" : message;
            MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }



    }
}

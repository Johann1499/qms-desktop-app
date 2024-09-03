using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace QueueingSystem
{
    public partial class AdminHome : Form
    {
        // HttpClient should be static or used as a singleton to avoid socket exhaustion
        private static readonly HttpClient client = new HttpClient { BaseAddress = new Uri("http://localhost:8080/api/") };
        private Timer timer;

        public AdminHome()
        {
            InitializeComponent();

            // Set up the timer
            timer = new Timer();
            timer.Interval = 60000; // Set the interval to 60 sec
            timer.Tick += Timer_Tick;
            timer.Start();

            // Initial load
            _ = LoadQueueCountByDepartment();
            _ = LoadCashierCountByDepartment();
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            await LoadQueueCountByDepartment();
            await LoadCashierCountByDepartment();
        }

        private async Task LoadQueueCountByDepartment()
        {
            string apiUrl = "queues/count-by-department"; // Replace with your actual Laravel API endpoint path

            try
            {
                // Make the GET request to the Laravel API
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                // Ensure the request was successful
                response.EnsureSuccessStatusCode();

                // Read the response content as a string
                string responseBody = await response.Content.ReadAsStringAsync();

                // Parse the JSON response to extract queue counts by department
                JArray jsonArray = JArray.Parse(responseBody);

                // Loop through the JSON array and display counts
                foreach (var item in jsonArray)
                {
                    string department = item["department"].ToString();
                    int totalQueues = item["total"].Value<int>();
                    // Display the counts in a suitable way, for example in labels
                    if (department == "College")
                    {
                        lblCollege.Text = $"{totalQueues}";
                    }
                    else if (department == "Basic Education")
                    {
                        lblBasic.Text = $"{totalQueues}";
                    }
                }
            }
            catch (HttpRequestException e)
            {
                // Handle exceptions related to HTTP requests
                MessageBox.Show($"Request error: {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadCashierCountByDepartment()
        {
            string apiUrl = "cashiers/total-by-department-status"; // Replace with your actual Laravel API endpoint path

            try
            {
                // Make the GET request to the Laravel API
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                // Ensure the request was successful
                response.EnsureSuccessStatusCode();

                // Read the response content as a string
                string responseBody = await response.Content.ReadAsStringAsync();

                // Parse the JSON response to extract cashier counts by department and status
                JArray jsonArray = JArray.Parse(responseBody);

                // Initialize counts

                // Loop through the JSON array and update counts
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
            }
            catch (HttpRequestException e)
            {
                // Handle exceptions related to HTTP requests
                MessageBox.Show($"Request error: {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblInactiveBasic_Click(object sender, EventArgs e)
        {

        }

 
    }
}

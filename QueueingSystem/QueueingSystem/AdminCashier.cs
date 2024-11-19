using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace QueueingSystem
{
    public partial class AdminCashier : Form
    {
        private static readonly HttpClient client = new HttpClient();
        private const string apiUrl = "https://www.dctqueue.info/api/cashiers";
        private Timer refreshTimer;

        public AdminCashier()
        {
            InitializeComponent();
            this.Resize += new EventHandler(Form_Resize);

            refreshTimer = new Timer();
            refreshTimer.Interval = 30000; // Set interval to 30 seconds
            refreshTimer.Tick += new EventHandler(OnTimerTick);
        }

        private async void AdminCashier_Load(object sender, EventArgs e)
        {
            listCashier.Clear();
            listCashier.GridLines = true;
            listCashier.View = View.Details;
            listCashier.FullRowSelect = true;

            // Remove ID column
            listCashier.Columns.Add("Name");
            listCashier.Columns.Add("Department");
            listCashier.Columns.Add("Status");

            SetColumnWidths();

            await LoadCashierData();
            refreshTimer.Start();
        }

        private async void OnTimerTick(object sender, EventArgs e)
        {
            await LoadCashierData();
        }

        private async Task LoadCashierData()
        {
            int retryCount = 0;
            const int maxRetries = 5;
            const int baseDelay = 2000;

            while (retryCount < maxRetries)
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

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
                        continue;
                    }

                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    JArray cashiers = JArray.Parse(responseBody);

                    listCashier.Items.Clear();

                    foreach (var item in cashiers)
                    {
                        ListViewItem lv = new ListViewItem(item["name"].ToString())
                        {
                            // Store ID in Tag
                            Tag = item["id"].ToString(),
                            SubItems =
                            {
                                item["department"].ToString(),
                                (bool)item["status"] ? "Active" : "Inactive"
                            }
                        };
                        listCashier.Items.Add(lv);
                    }
                    break;
                }
                catch (HttpRequestException e)
                {
                    MessageBox.Show($"Error loading cashier data. Please check the network or API availability.\n\n{e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Unexpected error occurred while loading cashier data.\n\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
            }

            if (retryCount == maxRetries)
            {
                MessageBox.Show("Maximum retry attempts reached. Please try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (listCashier.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listCashier.SelectedItems[0];
                string id = selectedItem.Tag.ToString(); // Retrieve ID from Tag
                string name = selectedItem.SubItems[0].Text;
                string dept = selectedItem.SubItems[1].Text;

                EditCashier edit = new EditCashier(id, name, dept)
                {
                    RefreshList = async () => await LoadCashierData()
                };
                edit.Show();
            }
            else
            {
                MessageBox.Show("Please select an item from the list.");
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (listCashier.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listCashier.SelectedItems[0];
                string id = selectedItem.Tag.ToString(); // Retrieve ID from Tag

                DialogResult result = MessageBox.Show("Are you sure you want to delete this cashier?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        HttpResponseMessage response = await client.DeleteAsync($"{apiUrl}/{id}");

                        if (response.IsSuccessStatusCode)
                        {
                            listCashier.Items.Remove(selectedItem);
                        }
                        else
                        {
                            MessageBox.Show($"Failed to delete cashier. Error: {response.ReasonPhrase}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a cashier to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            AddNewCashier add = new AddNewCashier
            {
                RefreshList = async () => await LoadCashierData()
            };
            add.Show();
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            SetColumnWidths();
        }

        private void SetColumnWidths()
        {
            int listViewWidth = listCashier.ClientSize.Width;

            listCashier.Columns[0].Width = (int)(listViewWidth * 0.4); // Name (40%)
            listCashier.Columns[1].Width = (int)(listViewWidth * 0.3); // Department (30%)
            listCashier.Columns[2].Width = (int)(listViewWidth * 0.3); // Status (30%)
        }
    }
}

using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace QueueingSystem
{
    public partial class AdminCashier : Form
    {
        private static readonly HttpClient client = new HttpClient();
        private const string apiUrl = "http://localhost:8080/api/cashiers";
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

            listCashier.Columns.Add("ID");
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
            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                JArray cashiers = JArray.Parse(responseBody);

                listCashier.Items.Clear();

                foreach (var item in cashiers)
                {
                    ListViewItem lv = new ListViewItem(item["id"].ToString());
                    lv.SubItems.Add(item["name"].ToString());
                    lv.SubItems.Add(item["department"].ToString());
                    lv.SubItems.Add((bool)item["status"] ? "Active" : "Inactive");
                    listCashier.Items.Add(lv);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (listCashier.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listCashier.SelectedItems[0];
                string id = selectedItem.SubItems[0].Text;
                string name = selectedItem.SubItems[1].Text;
                string dept = selectedItem.SubItems[2].Text;

                // Create an instance of EditCashier and pass the callback
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
                string id = selectedItem.SubItems[0].Text;

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
            // Create an instance of AddNewCashier and pass the callback
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

            listCashier.Columns[0].Width = (int)(listViewWidth * 0.1); // ID (10%)
            listCashier.Columns[1].Width = (int)(listViewWidth * 0.3); // Name (30%)
            listCashier.Columns[2].Width = (int)(listViewWidth * 0.3); // Department (30%)
            listCashier.Columns[3].Width = (int)(listViewWidth * 0.3); // Status (30%)
        }
    }
}

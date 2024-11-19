using System;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace QueueingSystem
{
    public partial class AddNewCashier : Form
    {
        private static readonly HttpClient client = new HttpClient();
        private const string apiUrl = "https://www.dctqueue.info/api/cashiers";

        // Define a delegate for the callback
        public delegate void RefreshListCallback();
        public RefreshListCallback RefreshList { get; set; }

        public AddNewCashier()
        {
            InitializeComponent();
        }

        private void AddNew_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
        }

        private void AddNewCashier_Load(object sender, EventArgs e)
        {
            // Populate the ComboBox with predefined values
            cmbDept.Items.Add("Basic Education");
            cmbDept.Items.Add("College");

            // Clear the TextBox and ComboBox
            txtCashierName.Clear();
            cmbDept.SelectedIndex = -1; // Deselect any selected item
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            string cashierName = txtCashierName.Text;
            string dept = cmbDept.Text;

            if (String.IsNullOrEmpty(cashierName))
            {
                MessageBox.Show("Please Enter Name");
            }
            else if (String.IsNullOrEmpty(dept))
            {
                MessageBox.Show("Please Select Department");
            }
            else
            {
                try
                {
                    // Create the data payload
                    var newCashier = new
                    {
                        name = cashierName,
                        department = dept,
                        status = 0 // Assuming '0' means inactive by default
                    };

                    // Convert the payload to JSON
                    var jsonContent = JsonConvert.SerializeObject(newCashier);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    // Send the POST request to the Laravel API
                    var response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Cashier Added Successfully");
                        txtCashierName.Clear();
                        cmbDept.SelectedIndex = -1;

                        // Invoke the callback to refresh the list
                        RefreshList?.Invoke();
                    }
                    else
                    {
                        MessageBox.Show($"Error: {response.ReasonPhrase}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            txtCashierName.Clear();
            cmbDept.SelectedIndex = -1;
        }

        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCashierName.Enabled = true;
        }

        private void txtCashierName_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }
    }
}

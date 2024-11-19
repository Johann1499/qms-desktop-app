using System;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace QueueingSystem
{
    public partial class EditCashier : Form
    {
        private static readonly HttpClient client = new HttpClient();
        private string primaryKeyValue;

        // Define a delegate for the callback
        public delegate void RefreshListCallback();
        public RefreshListCallback RefreshList { get; set; }

        public EditCashier(string id, string name, string dept)
        {
            InitializeComponent();
            txtCashierName.Text = name;
            cmbDept.Text = dept;
            this.primaryKeyValue = id;
        }

        private async void btnOkay_Click(object sender, EventArgs e)
        {
            string cashierName = txtCashierName.Text;
            string dept = cmbDept.Text;

            if (String.IsNullOrEmpty(cashierName))
            {
                MessageBox.Show("Please Enter Name");
            }
            else
            {
                try
                {
                    // Create the data payload
                    var updateData = new
                    {
                        name = cashierName,
                        department = dept
                    };

                    // Convert the payload to JSON
                    var jsonContent = JsonConvert.SerializeObject(updateData);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    // Send the PUT request to the Laravel API
                    var response = await client.PutAsync($"https://www.dctqueue.info/api/cashiers/{primaryKeyValue}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Saved Changes", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();

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
        }
    }
}

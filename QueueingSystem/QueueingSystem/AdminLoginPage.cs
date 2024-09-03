using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace QueueingSystem
{
    public partial class AdminLoginPage : Form
    {
        public AdminLoginPage()
        {
            InitializeComponent();

        }


        private async void btnLogin_Click_1(object sender, EventArgs e)
        {
            // Disable the login button to prevent double clicks
            btnLogin.Enabled = false;

            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {
                MessageBox.Show("No empty fields allowed");
                btnLogin.Enabled = true; // Re-enable the button
                return;
            }

            try
            {
                if (await AuthenticateAdmin(username, password))
                {
                    Visible = false;
                    AdminPage admin = new AdminPage();
                    admin.Show();
                }
                else
                {
                    MessageBox.Show("Invalid credentials");
                    btnLogin.Enabled = true; // Re-enable the button if authentication fails
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                btnLogin.Enabled = true; // Re-enable the button in case of an error
            }

        }
        private async Task<bool> AuthenticateAdmin(string username, string password)
        {
            using (var client = new HttpClient())
            {
                var payload = new
                {   
                    username = username,
                    password = password
                };

                string json = JsonConvert.SerializeObject(payload);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("http://localhost:8080/api/admin/login", content);

                if (response.IsSuccessStatusCode)
                {
                    // Optionally, you can parse the response content here if needed
                    return true;
                }
                return false;
            }
        }

        private void AdminLoginPage_Load(object sender, EventArgs e)
        {

        }

        private void AdminLoginPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }
    }
}

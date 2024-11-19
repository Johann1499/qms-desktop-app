using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json; // Install Newtonsoft.Json if needed

namespace QueueingSystem
{
    public partial class AdminSetting : Form
    {
        private const string apiUrl = "https://www.dctqueue.info/api/admin";

        public AdminSetting()
        {
            InitializeComponent();
        }

        // Make this method async and return a boolean indicating success or failure
        private async Task<bool> ChangePasswordAsync(string currentPassword, string newPassword)
        {
            using (var client = new HttpClient())
            {
                // Set the API base URL
                client.BaseAddress = new Uri(apiUrl);

                // Prepare the data for the POST request
                var content = new FormUrlEncodedContent(new[]
                {
            new KeyValuePair<string, string>("current_password", currentPassword),
            new KeyValuePair<string, string>("new_password", newPassword),
            new KeyValuePair<string, string>("new_password_confirmation", newPassword)
        });

                try
                {
                    // Send the POST request
                    var response = await client.PutAsync("/change-password", content);

                    // Log the response for debugging
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Response: " + responseBody);

                    // Check if the response is successful
                    return response.IsSuccessStatusCode;
                }
                catch (Exception ex)
                {
                    // Log any exceptions that occur
                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
            }
        }


        // Asynchronous button click handler
        private async void btnSave_Click(object sender, EventArgs e)
        {
            var currentPassword = txtOldPassword.Text;
            var newPassword = txtNewPassword.Text;
            var confirmPassword = txtConfirmPassword.Text;

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("The new passwords do not match.");
                return;
            }

            // Call the async ChangePasswordAsync method and await the result
            bool result = await ChangePasswordAsync(currentPassword, newPassword);

            if (result)
            {
                MessageBox.Show("Password changed successfully.");
            }
            else
            {
                MessageBox.Show("Failed to change password. Please check your current password.");
            }
        }

        private void btnOldPassword_Click(object sender, EventArgs e)
        {
            // Toggle the visibility of the password
            if (txtOldPassword.UseSystemPasswordChar)
            {
                txtOldPassword.UseSystemPasswordChar = false;  // Show the password (no masking)
                btnOldPassword.Text = "Hide";  // Change button text to "Hide"
            }
            else
            {
                txtOldPassword.UseSystemPasswordChar = true;  // Mask the password (show '*')
                btnOldPassword.Text = "Show";  // Change button text to "Show"
            }
        }

        private void txtOldPassword_TextChanged(object sender, EventArgs e)
        {
            btnOldPassword.Text = "Show";
        }

        private void txtNewPassword_TextChanged(object sender, EventArgs e)
        {
            btnNewPassword.Text = "Show";
        }

        private void btnNewPassword_Click(object sender, EventArgs e)
        {
            if (txtNewPassword.UseSystemPasswordChar)
            {
                txtNewPassword.UseSystemPasswordChar = false;  // Show the password (no masking)
                btnNewPassword.Text = "Hide";  // Change button text to "Hide"
            }
            else
            {
                txtNewPassword.UseSystemPasswordChar = true;  // Mask the password (show '*')
                btnNewPassword.Text = "Show";  // Change button text to "Show"
            }
        }

        private void txtConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            btnConfirmPassword.Text = "Show";
        }

        private void btnConfirmPassword_Click(object sender, EventArgs e)
        {
            if (txtConfirmPassword.UseSystemPasswordChar)
            {
                txtConfirmPassword.UseSystemPasswordChar = false;  // Show the password (no masking)
                btnConfirmPassword.Text = "Hide";  // Change button text to "Hide"
            }
            else
            {
                txtConfirmPassword.UseSystemPasswordChar = true;  // Mask the password (show '*')
                btnConfirmPassword.Text = "Show";  // Change button text to "Show"
            }
        }
    }
}

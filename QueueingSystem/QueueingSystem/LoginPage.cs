using System;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace QueueingSystem
{
    public partial class loginPage : Form
    {
        public loginPage()
        {
            InitializeComponent();
            loadForm(new AdminLoginPage());
        }

        public void loadForm(object Form)
        {
            if (this.pnlLogin.Controls.Count > 0)
                this.pnlLogin.Controls.RemoveAt(0);
            Form form = Form as Form;
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            this.pnlLogin.Controls.Add(form);
            this.pnlLogin.Tag = form;
            form.Show();

            btnAdmin.Enabled = false;
        }

        private Button lastPressedButton = null;

        private void loginPage_Load(object sender, EventArgs e)
        {
            // Show AdminLoginPage initially
            //loadForm(new AdminLoginPage());

            // Optionally, change the appearance of the Admin button as if it was clicked
            btnAdmin.BackColor = SystemColors.ScrollBar;
            lastPressedButton = btnAdmin;
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn != null)
            {
                if (lastPressedButton != null)
                {
                    lastPressedButton.BackColor = SystemColors.Control;
                }

                btn.BackColor = SystemColors.ScrollBar;
                lastPressedButton = btn;
            }

            // Load the AdminLoginPage form
            loadForm(new AdminLoginPage());
            btnCashier.Enabled = true;
            btnAdmin.Enabled = false;
            btnLive.Enabled = true;
        }

        private void btnCashier_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn != null)
            {
                if (lastPressedButton != null)
                {
                    lastPressedButton.BackColor = SystemColors.Control;
                }

                btn.BackColor = SystemColors.ScrollBar;
                lastPressedButton = btn;
            }

            // Load another form or perform another action
            loadForm(new CashierLoginPage());
            btnCashier.Enabled = false;
            btnAdmin.Enabled = true;
            btnLive.Enabled = true;

        }

        private void btnLive_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn != null)
            {
                if (lastPressedButton != null)
                {
                    lastPressedButton.BackColor = SystemColors.Control;
                }

                btn.BackColor = SystemColors.ScrollBar;
                lastPressedButton = btn;
            }

            // Load another form or perform another action
            loadForm(new LiveQueueLogin());
            btnLive.Enabled = false;
            btnAdmin.Enabled = true;
            btnCashier.Enabled = true;
        }

        private void loginPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace QueueingSystem
{
    public partial class AdminPage : Form
    {
        public AdminPage()
        {
            InitializeComponent();
            loadForm(new AdminHome());
        }
        public void loadForm(object Form)
        {
            if (this.pnlMain.Controls.Count > 0)
                this.pnlMain.Controls.RemoveAt(0);
            Form form = Form as Form;
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            this.pnlMain.Controls.Add(form);
            this.pnlMain.Tag = form;
            form.Show();
            
        }
        private void btnAdminQueue_Click(object sender, EventArgs e)
        {
            loadForm(new AdminQueue());
            btnAdminQueue.Enabled = false;
            btnAdminHome.Enabled = true;
            btnAdminCashier.Enabled = true;
        }

        private void btnAdminCashier_Click(object sender, EventArgs e)
        {
            loadForm(new AdminCashier());

            btnAdminQueue.Enabled = true;
            btnAdminHome.Enabled = true;
            btnAdminCashier.Enabled = false;

        }
        private void btnAdminHome_Click(object sender, EventArgs e)
        {
            loadForm(new AdminHome());
            btnAdminQueue.Enabled = true;
            btnAdminHome.Enabled = false;
            btnAdminCashier.Enabled = true;
        }
        private void btnSetting_Click(object sender, EventArgs e)
        {
            loadForm(new AdminSetting());
        }


        private void Admin_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to logout?",
                "Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
                this.Close();
        }

       
    }
}

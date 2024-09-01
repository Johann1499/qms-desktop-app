
namespace QueueingSystem
{
    partial class AdminPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlSide = new System.Windows.Forms.Panel();
            this.btnHome = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnAdminQueue = new System.Windows.Forms.Button();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.btnAdminCashier = new System.Windows.Forms.Button();
            this.pnlSide.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSide
            // 
            this.pnlSide.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pnlSide.Controls.Add(this.btnAdminCashier);
            this.pnlSide.Controls.Add(this.btnHome);
            this.pnlSide.Controls.Add(this.btnLogout);
            this.pnlSide.Controls.Add(this.btnAdminQueue);
            this.pnlSide.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSide.Location = new System.Drawing.Point(0, 0);
            this.pnlSide.Name = "pnlSide";
            this.pnlSide.Size = new System.Drawing.Size(200, 450);
            this.pnlSide.TabIndex = 0;
            // 
            // btnHome
            // 
            this.btnHome.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnHome.FlatAppearance.BorderSize = 0;
            this.btnHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHome.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHome.Image = global::QueueingSystem.Properties.Resources.home;
            this.btnHome.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHome.Location = new System.Drawing.Point(3, 68);
            this.btnHome.Name = "btnHome";
            this.btnHome.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnHome.Size = new System.Drawing.Size(194, 38);
            this.btnHome.TabIndex = 4;
            this.btnHome.Text = "             Home";
            this.btnHome.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHome.UseVisualStyleBackColor = false;
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLogout.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.Image = global::QueueingSystem.Properties.Resources.logout;
            this.btnLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.Location = new System.Drawing.Point(3, 385);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnLogout.Size = new System.Drawing.Size(194, 38);
            this.btnLogout.TabIndex = 3;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnAdminQueue
            // 
            this.btnAdminQueue.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnAdminQueue.FlatAppearance.BorderSize = 0;
            this.btnAdminQueue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdminQueue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdminQueue.Image = global::QueueingSystem.Properties.Resources.queue;
            this.btnAdminQueue.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdminQueue.Location = new System.Drawing.Point(3, 158);
            this.btnAdminQueue.Name = "btnAdminQueue";
            this.btnAdminQueue.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnAdminQueue.Size = new System.Drawing.Size(194, 38);
            this.btnAdminQueue.TabIndex = 2;
            this.btnAdminQueue.Text = "             Queuing List";
            this.btnAdminQueue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdminQueue.UseVisualStyleBackColor = false;
            this.btnAdminQueue.Click += new System.EventHandler(this.btnAdminQueue_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMain.Location = new System.Drawing.Point(200, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(600, 450);
            this.pnlMain.TabIndex = 2;
            // 
            // btnAdminCashier
            // 
            this.btnAdminCashier.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnAdminCashier.FlatAppearance.BorderSize = 0;
            this.btnAdminCashier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdminCashier.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdminCashier.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnAdminCashier.Image = global::QueueingSystem.Properties.Resources.cashier;
            this.btnAdminCashier.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdminCashier.Location = new System.Drawing.Point(3, 112);
            this.btnAdminCashier.Name = "btnAdminCashier";
            this.btnAdminCashier.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnAdminCashier.Size = new System.Drawing.Size(194, 40);
            this.btnAdminCashier.TabIndex = 1;
            this.btnAdminCashier.Text = "             Cashier List";
            this.btnAdminCashier.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdminCashier.UseVisualStyleBackColor = false;
            this.btnAdminCashier.Click += new System.EventHandler(this.btnAdminCashier_Click);
            // 
            // AdminPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlSide);
            this.Name = "AdminPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ADMINISTRATOR";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Admin_FormClosing);
            this.pnlSide.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSide;
        private System.Windows.Forms.Button btnAdminQueue;
        private System.Windows.Forms.Button btnAdminCashier;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnHome;
    }
}
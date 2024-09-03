
namespace QueueingSystem
{
    partial class loginPage
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
            this.btnAdmin = new System.Windows.Forms.Button();
            this.btnLive = new System.Windows.Forms.Button();
            this.btnCashier = new System.Windows.Forms.Button();
            this.pnlLogin = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAdmin
            // 
            this.btnAdmin.BackColor = System.Drawing.SystemColors.Control;
            this.btnAdmin.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnAdmin.FlatAppearance.BorderSize = 0;
            this.btnAdmin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.btnAdmin.Location = new System.Drawing.Point(6, 10);
            this.btnAdmin.Name = "btnAdmin";
            this.btnAdmin.Size = new System.Drawing.Size(130, 50);
            this.btnAdmin.TabIndex = 1;
            this.btnAdmin.Text = "Admin";
            this.btnAdmin.UseVisualStyleBackColor = false;
            this.btnAdmin.Click += new System.EventHandler(this.btnAdmin_Click);
            // 
            // btnLive
            // 
            this.btnLive.BackColor = System.Drawing.SystemColors.Control;
            this.btnLive.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnLive.FlatAppearance.BorderSize = 0;
            this.btnLive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLive.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.btnLive.Location = new System.Drawing.Point(278, 10);
            this.btnLive.Name = "btnLive";
            this.btnLive.Size = new System.Drawing.Size(130, 50);
            this.btnLive.TabIndex = 8;
            this.btnLive.Text = "Live";
            this.btnLive.UseVisualStyleBackColor = false;
            this.btnLive.Click += new System.EventHandler(this.btnLive_Click);
            // 
            // btnCashier
            // 
            this.btnCashier.BackColor = System.Drawing.SystemColors.Control;
            this.btnCashier.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCashier.FlatAppearance.BorderSize = 0;
            this.btnCashier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCashier.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.btnCashier.Location = new System.Drawing.Point(142, 10);
            this.btnCashier.Name = "btnCashier";
            this.btnCashier.Size = new System.Drawing.Size(130, 50);
            this.btnCashier.TabIndex = 9;
            this.btnCashier.Text = "Cashier";
            this.btnCashier.UseVisualStyleBackColor = false;
            this.btnCashier.Click += new System.EventHandler(this.btnCashier_Click);
            // 
            // pnlLogin
            // 
            this.pnlLogin.Controls.Add(this.label1);
            this.pnlLogin.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlLogin.Location = new System.Drawing.Point(0, 58);
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.Size = new System.Drawing.Size(414, 253);
            this.pnlLogin.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(176, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // loginPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 311);
            this.Controls.Add(this.pnlLogin);
            this.Controls.Add(this.btnCashier);
            this.Controls.Add(this.btnLive);
            this.Controls.Add(this.btnAdmin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "loginPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.loginPage_FormClosing);
            this.Load += new System.EventHandler(this.loginPage_Load);
            this.pnlLogin.ResumeLayout(false);
            this.pnlLogin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnAdmin;
        private System.Windows.Forms.Button btnLive;
        private System.Windows.Forms.Button btnCashier;
        private System.Windows.Forms.Panel pnlLogin;
        private System.Windows.Forms.Label label1;
    }
}


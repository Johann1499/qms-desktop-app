
namespace QueueingSystem
{
    partial class LiveQueueMonitor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LiveQueueMonitor));
            this.panelQueuing = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.lblQueueNumber = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelVideo = new System.Windows.Forms.Panel();
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.panelTime = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.panelQueuing.SuspendLayout();
            this.panelVideo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.panelTime.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelQueuing
            // 
            this.panelQueuing.BackColor = System.Drawing.Color.Thistle;
            this.panelQueuing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelQueuing.Controls.Add(this.listView1);
            this.panelQueuing.Controls.Add(this.label3);
            this.panelQueuing.Controls.Add(this.lblQueueNumber);
            this.panelQueuing.Controls.Add(this.label1);
            this.panelQueuing.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelQueuing.Location = new System.Drawing.Point(0, 0);
            this.panelQueuing.Name = "panelQueuing";
            this.panelQueuing.Size = new System.Drawing.Size(533, 525);
            this.panelQueuing.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(59, 287);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 46);
            this.label3.TabIndex = 2;
            this.label3.Text = "Next";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblQueueNumber
            // 
            this.lblQueueNumber.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblQueueNumber.AutoSize = true;
            this.lblQueueNumber.BackColor = System.Drawing.Color.Violet;
            this.lblQueueNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 60F);
            this.lblQueueNumber.Location = new System.Drawing.Point(128, 155);
            this.lblQueueNumber.Name = "lblQueueNumber";
            this.lblQueueNumber.Size = new System.Drawing.Size(273, 91);
            this.lblQueueNumber.TabIndex = 1;
            this.lblQueueNumber.Text = "C0000";
            this.lblQueueNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 50.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(54, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(421, 76);
            this.label1.TabIndex = 0;
            this.label1.Text = "Now Serving";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelVideo
            // 
            this.panelVideo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelVideo.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panelVideo.Controls.Add(this.axWindowsMediaPlayer1);
            this.panelVideo.Location = new System.Drawing.Point(533, 0);
            this.panelVideo.Name = "panelVideo";
            this.panelVideo.Size = new System.Drawing.Size(419, 384);
            this.panelVideo.TabIndex = 1;
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(0, 0);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(419, 384);
            this.axWindowsMediaPlayer1.TabIndex = 8;
            // 
            // panelTime
            // 
            this.panelTime.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.panelTime.Controls.Add(this.label9);
            this.panelTime.Controls.Add(this.label10);
            this.panelTime.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelTime.Location = new System.Drawing.Point(533, 381);
            this.panelTime.Name = "panelTime";
            this.panelTime.Size = new System.Drawing.Size(419, 144);
            this.panelTime.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.label9.Location = new System.Drawing.Point(149, 65);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(135, 31);
            this.label9.TabIndex = 9;
            this.label9.Text = "8/22/2024";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.label10.Location = new System.Drawing.Point(164, 34);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(111, 31);
            this.label10.TabIndex = 8;
            this.label10.Text = "2:48 pm";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(67, 336);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(408, 176);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // LiveQueueMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 525);
            this.Controls.Add(this.panelTime);
            this.Controls.Add(this.panelVideo);
            this.Controls.Add(this.panelQueuing);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "LiveQueueMonitor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LiveQueueMonitor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.LiveQueueMonitor_Load);
            this.panelQueuing.ResumeLayout(false);
            this.panelQueuing.PerformLayout();
            this.panelVideo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.panelTime.ResumeLayout(false);
            this.panelTime.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelQueuing;
        private System.Windows.Forms.Panel panelVideo;
        private System.Windows.Forms.Panel panelTime;
        private System.Windows.Forms.Label lblQueueNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private System.Windows.Forms.ListView listView1;
    }
}
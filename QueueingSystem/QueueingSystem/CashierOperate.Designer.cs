
namespace QueueingSystem
{
    partial class CashierOperate
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblQueue = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnNotify = new System.Windows.Forms.Button();
            this.listLiveQueue = new System.Windows.Forms.ListView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblCashier = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F);
            this.label1.Location = new System.Drawing.Point(82, 65);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(333, 63);
            this.label1.TabIndex = 0;
            this.label1.Text = "Now Serving";
            // 
            // lblQueue
            // 
            this.lblQueue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblQueue.AutoSize = true;
            this.lblQueue.BackColor = System.Drawing.Color.Thistle;
            this.lblQueue.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F);
            this.lblQueue.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblQueue.Location = new System.Drawing.Point(137, 182);
            this.lblQueue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQueue.Name = "lblQueue";
            this.lblQueue.Size = new System.Drawing.Size(218, 76);
            this.lblQueue.TabIndex = 1;
            this.lblQueue.Text = "00000";
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.btnNext.Location = new System.Drawing.Point(58, 327);
            this.btnNext.Margin = new System.Windows.Forms.Padding(4);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(380, 74);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnNotify
            // 
            this.btnNotify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNotify.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.btnNotify.Location = new System.Drawing.Point(189, 409);
            this.btnNotify.Margin = new System.Windows.Forms.Padding(4);
            this.btnNotify.Name = "btnNotify";
            this.btnNotify.Size = new System.Drawing.Size(249, 74);
            this.btnNotify.TabIndex = 5;
            this.btnNotify.Text = "Notify";
            this.btnNotify.UseVisualStyleBackColor = true;
            this.btnNotify.Click += new System.EventHandler(this.btnNotify_Click);
            // 
            // listLiveQueue
            // 
            this.listLiveQueue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listLiveQueue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listLiveQueue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listLiveQueue.GridLines = true;
            this.listLiveQueue.HideSelection = false;
            this.listLiveQueue.Location = new System.Drawing.Point(480, 150);
            this.listLiveQueue.Margin = new System.Windows.Forms.Padding(4);
            this.listLiveQueue.Name = "listLiveQueue";
            this.listLiveQueue.Size = new System.Drawing.Size(477, 333);
            this.listLiveQueue.TabIndex = 6;
            this.listLiveQueue.UseCompatibleStateImageBehavior = false;
            this.listLiveQueue.View = System.Windows.Forms.View.Details;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.btnRefresh.Location = new System.Drawing.Point(58, 409);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(123, 74);
            this.btnRefresh.TabIndex = 7;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblCashier
            // 
            this.lblCashier.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCashier.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblCashier.Location = new System.Drawing.Point(679, 36);
            this.lblCashier.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCashier.Name = "lblCashier";
            this.lblCashier.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCashier.Size = new System.Drawing.Size(282, 20);
            this.lblCashier.TabIndex = 8;
            this.lblCashier.Text = "Name";
            this.lblCashier.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CashierOperate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(995, 516);
            this.Controls.Add(this.lblCashier);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.listLiveQueue);
            this.Controls.Add(this.btnNotify);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.lblQueue);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CashierOperate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Queuing";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CashierOperate_FormClosing);
            this.Load += new System.EventHandler(this.CashierOperate_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblQueue;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnNotify;
        private System.Windows.Forms.ListView listLiveQueue;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblCashier;
    }
}
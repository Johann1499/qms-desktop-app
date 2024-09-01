using System;
using System.Windows.Forms;

namespace QueueingSystem
{
    public partial class LiveQueueLogin : Form
    {
        public LiveQueueLogin()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Video Files|*.mp4;*.avi;*.wmv;*.mov"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Display the file path in the TextBox
                txtVideoPath.Text = openFileDialog.FileName;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // Use the path from the TextBox
            string videoPath = txtVideoPath.Text;

            if (!string.IsNullOrEmpty(videoPath))
            {
                LiveQueueMonitor videoPlayerForm = new LiveQueueMonitor(videoPath);
                videoPlayerForm.Show();
            }
            else
            {
                MessageBox.Show("Please select a video file first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

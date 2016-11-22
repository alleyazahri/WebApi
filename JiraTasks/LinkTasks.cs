using System;
using System.Windows.Forms;

namespace JiraTasks
{
    public partial class LinkTasks : Form
    {
        public string LinkedTask { get; set; }
        public string MainTask { get; set; }

        public LinkTasks()
        {
            InitializeComponent();
        }

        public LinkTasks(string mainTask)
        {
            InitializeComponent();
            tbMainTask.Text = mainTask;
        }

        private void linkTask_Click(object sender, EventArgs e)
        {
            MainTask = tbMainTask.Text;
            LinkedTask = tbLinkTask.Text;
            Close();
        }

        private void bCancelLinkingTasks_Click(object sender, EventArgs e)
        {
            MainTask = null;
            LinkedTask = null;
            Close();
        }
    }
}
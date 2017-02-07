using System;
using System.Windows.Forms;

namespace JiraTasks
{
    public partial class LinkTasks : Form
    {
        public string LinkedTask { get; set; }
        public string MainTask { get; set; }
        private string PrevLinkedTaskValue { get; set; }

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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (cbNoTask.Checked)
            {
                rbComplete.Visible = true;
                rbInProgress.Visible = true;
                rbNotStarted.Visible = true;
                rbNotStarted.Checked = true;
                PrevLinkedTaskValue = tbLinkTask.Text;
                tbLinkTask.Text = "~C-NotStarted";
                tbLinkTask.Visible = false;
            }
            else
            {
                tbLinkTask.Text = PrevLinkedTaskValue;
                tbLinkTask.Visible = true;

                rbComplete.Visible = false;
                rbInProgress.Visible = false;
                rbNotStarted.Visible = false;
            }
        }

        private void rbNotStarted_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNotStarted.Checked)
            {
                rbComplete.Checked = false;
                rbInProgress.Checked = false;
                tbLinkTask.Text = "~C-NotStarted";
            }
            else
            {
                if (!rbComplete.Checked && !rbInProgress.Checked)
                    rbNotStarted.Checked = true;
            }
        }

        private void rbInProgress_CheckedChanged(object sender, EventArgs e)
        {
            if (rbInProgress.Checked)
            {
                rbComplete.Checked = false;
                rbNotStarted.Checked = false;
                tbLinkTask.Text = "~C-InProgress";
            }
            else
            {
                if (!rbComplete.Checked && !rbNotStarted.Checked)
                    rbInProgress.Checked = true;
            }
        }

        private void rbComplete_CheckedChanged(object sender, EventArgs e)
        {
            if (rbComplete.Checked)
            {
                rbInProgress.Checked = false;
                rbNotStarted.Checked = false;
                tbLinkTask.Text = "~C-Complete";
            }
            else
            {
                if (!rbNotStarted.Checked && !rbInProgress.Checked)
                    rbComplete.Checked = true;
            }
        }
    }
}
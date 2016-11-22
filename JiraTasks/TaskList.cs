using Atlassian.Jira;
using JiraApi;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace JiraTasks
{
    public partial class TaskList : Form
    {
        private Login LoginWindow { get; set; }
        private List<Issue> Tasks { get; set; }
        private TaskController TaskCont { get; set; }
        private Dictionary<string, string> TaskUserPrefs { get; set; }

        /// <summary>
        /// Initializes the TaskList window
        /// </summary>
        public TaskList()
        {
            InitializeComponent();
            LoginWindow = new Login();
            if (LoginWindow.loginSettings.SavePassword != CheckState.Checked)
                LoginWindow.ShowDialog();
        }

        private void dgJiraTaskList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var dataGrid = sender as DataGridView;
                if (dataGrid == null)
                    throw new Exception("the data grid is null, something went seriously wrong O.o");

                if (e.ColumnIndex == 1 && dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != "")
                    System.Diagnostics.Process.Start(
                        $"https://epm.verisk.com/jira/browse/{dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value}");
                else if (e.ColumnIndex == 1)
                    linkTaskToolStripMenuItem_Click(dataGrid.Rows[e.RowIndex].Cells[0].Value, null);
                else
                    System.Diagnostics.Process.Start(
                        $"https://epm.verisk.com/jira/browse/{dataGrid.Rows[e.RowIndex].Cells[0].Value}");
                Console.WriteLine(dataGrid?.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            }
        }

        private void dgJiraTaskList_SelectionChanged(object sender, EventArgs e)
        {
            dgJiraTaskList.ClearSelection();
        }

        private void TaskList_Load(object sender, EventArgs e)
        {
        }

        private async void TaskList_Shown(object sender, EventArgs e)
        {
            ContextMenuStrip mnu = new ContextMenuStrip();
            ToolStripMenuItem mnuLogWork = new ToolStripMenuItem("Log Work");
            ToolStripMenuItem mnuAddLink = new ToolStripMenuItem("Add Link");
            ToolStripMenuItem mnuRemoveLink = new ToolStripMenuItem("Remove Link");
            //Assign event handlers
            mnuRemoveLink.Click += dgvtaskContextMenu_RemoveLink;
            mnuAddLink.Click += dgvtaskContextMenu_AddLink;
            mnuLogWork.Click += dvgTaskContextMenu_LogWork;

            //Add to main context menu
            mnu.Items.AddRange(new ToolStripItem[] { mnuLogWork, mnuAddLink, mnuRemoveLink });

            //Assign to datagridview
            dgJiraTaskList.ContextMenuStrip = mnu;

            if (LoginWindow.LogCont != null && LoginWindow.LogCont.IsLoggedIn())
                await LoadingJiraTasks();
            else
                pLoginToViewTasks.Visible = true;
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dgJiraTaskList.Rows.Clear();
            LoginWindow.LogCont = null;
            TaskCont = null;
            LoginWindow.loginSettings.ClearUser();
            LoginWindow.loginSettings.Save(LoginWindow.SettingsPath, LoginWindow.SettingsFile);
        }

        private async void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pLoginToViewTasks.Visible = false;
            LoginWindow.ShowDialog();
            if (LoginWindow.LogCont != null && LoginWindow.LogCont.IsLoggedIn())
                await LoadingJiraTasks();
            else
                pLoginToViewTasks.Visible = true;
        }

        private void addProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException(
                "Can add and remove projects to filter with methinks.....dynamically that need to be later saved :/ good luck woman!");
        }

        private void TaskList_FormClosing(object sender, FormClosingEventArgs e)
        {
            //throw new NotImplementedException("Would you like to save your current filter options? (compare to saved filters first)");
        }

        private void linkTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinkTasks linkTasksWindow = null;
            if (sender is string)
            {
                linkTasksWindow = new LinkTasks(sender.ToString());
            }
            else
            {
                linkTasksWindow = new LinkTasks();
            }
            linkTasksWindow.ShowDialog();
            var linkCreated = CreateLinkBetweenTasks(linkTasksWindow.MainTask, linkTasksWindow.LinkedTask);
        }

        private void dgvtaskContextMenu_RemoveLink(object sender, EventArgs e)
        {
            var item = sender as ToolStripItem;
            RemoveLink("MainTask", "LinkedTask");
            throw new NotImplementedException();
        }

        private void dgvtaskContextMenu_AddLink(object sender, EventArgs e)
        {
            var item = sender as ToolStripItem;
            var asdf = sender.GetType();
            RemoveLink("MainTask", "LinkedTask");
            throw new NotImplementedException();
        }

        private void dvgTaskContextMenu_LogWork(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
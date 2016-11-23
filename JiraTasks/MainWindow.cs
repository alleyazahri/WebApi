using Atlassian.Jira;
using JiraApi;
using JiraTasks.Data;
using JiraTasks.MainWindowBusi;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JiraTasks
{
    public partial class MainWindow : Form
    {
        private Login LoginWindow { get; set; }
        private List<Issue> Tasks { get; set; }
        private List<CompoundIssue> IssueList { get; set; }
        private UserPrefs UserPreferences { get; set; }
        private TaskBusi TaskBusi { get; set; }

        /// <summary>
        /// Initializes the TaskList window
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            LoginWindow = new Login();
            IssueList = new List<CompoundIssue>();
            UserPreferences = new UserPrefs(LoginWindow.SettingsPath, "UPJTFO23.settings");
            UserPreferences.Load();
            TaskBusi = new TaskBusi(LoginWindow.LogCont);
            LoadDataGridViewContextMenu();
            if (LoginWindow.loginSettings.SavePassword != CheckState.Checked)
                LoginWindow.ShowDialog();
        }

        #region Events

        private void dgJiraTaskList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var dataGrid = sender as DataGridView;
            if (dataGrid == null)
                throw new Exception("the data grid is null, something went seriously wrong O.o");
            DataGridViewCellDoubleClicked(e.RowIndex, e.ColumnIndex, dataGrid);
        }

        private void dgJiraTaskList_SelectionChanged(object sender, EventArgs e)
        {
            dgJiraTaskList.ClearSelection();
        }

        private void TaskList_Shown(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            //TODO: Add a 'Loading' Label :)
            if (LoginWindow.LogCont != null && LoginWindow.LogCont.IsLoggedIn())
                LoadDataGridView();
            else
                pLoginToViewTasks.Visible = true;
            Cursor.Current = Cursors.Default;
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logout();
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pLoginToViewTasks.Visible = false;
            LoginWindow.ShowDialog();
            if (LoginWindow.LogCont != null && LoginWindow.LogCont.IsLoggedIn())
                LoadDataGridView();
            else
                pLoginToViewTasks.Visible = true;
        }

        private void addProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not yet implemented!");
        }

        private void TaskList_FormClosing(object sender, FormClosingEventArgs e)
        {
            UserPreferences.Save();
            //TODO: Just save the current filter options anyways and open the next time with those prefs
        }

        private void linkTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateLinkWindowCreation();
        }

        private void dgvtaskContextMenu_RemoveLink(object sender, EventArgs e)
        {
            MessageBox.Show("Not Yet Implemented!");
            //TODO: Cast e and see if that gives me what I want
            //TaskBusi.RemoveLink("MainTask", "LinkedTask");
        }

        private void dgvtaskContextMenu_AddLink(object sender, EventArgs e)
        {
            MessageBox.Show("Not Yet Implemented!");
            //TODO: Cast e and see if that gives me what I want
            //TaskBusi.RemoveLink("MainTask", "LinkedTask");
        }

        private void dvgTaskContextMenu_LogWork(object sender, EventArgs e)
        {
            MessageBox.Show("Not Yet Implemented!");
        }

        #endregion Events

        #region Data Grid View Logic

        private void LoadDataGridViewContextMenu()
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
        }

        private async void LoadDataGridView()
        {
            dgJiraTaskList.Columns.Add("DevTask", "Task to Follow");
            dgJiraTaskList.Columns.Add("MyTask", "My Task");
            dgJiraTaskList.Columns.Add("Status", "Status");
            dgJiraTaskList.Columns.Add("TaskName", "Summary");
            dgJiraTaskList.Columns.Add("TaskDescription", "Description");

            dgJiraTaskList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgJiraTaskList.Columns[3].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgJiraTaskList.Columns[4].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dgJiraTaskList.AllowUserToAddRows = false;
            dgJiraTaskList.AllowUserToOrderColumns = true;
            dgJiraTaskList.AllowUserToDeleteRows = false;

            dgJiraTaskList.Show();

            var task = new Task<List<Issue>>(TaskBusi.PopulateJiraTasks);
            task.Start();
            await task.ConfigureAwait(true);
            await task;
            Tasks = task.Result;
            if (UserPreferences.LinkedTaskList == null || UserPreferences.LinkedTaskList.Count == 0)
            {
                for (int i = 0; i < IssueList.Count; i++)
                {
                    dgJiraTaskList.Rows.Add(IssueList[i].ToStringArray());
                    ColorDataGridViewCell(i, 0, IssueList[i].DevTask.Status.Name);
                    ColorDataGridViewCell(i, 1, IssueList[i].LinkedTask?.Status?.Name);
                }
            }
            else
            {
                IssueList = TaskBusi.CompareTasksToUserPrefs(Tasks, UserPreferences.LinkedTaskList);
                for (int i = 0; i < IssueList.Count; i++)
                {
                    dgJiraTaskList.Rows.Add(IssueList[i].ToStringArray());
                    ColorDataGridViewCell(i, 0, IssueList[i].DevTask.Status.Name);
                    ColorDataGridViewCell(i, 1, IssueList[i].LinkedTask?.Status?.Name);
                }
            }
        }

        private void DataGridViewCellDoubleClicked(int row, int column, DataGridView dataGrid)
        {
            if (row >= 0 && column >= 0)
            {
                if (column == 1 && (string)dataGrid.Rows[row].Cells[column].Value != "")
                    System.Diagnostics.Process.Start(
                        $"https://epm.verisk.com/jira/browse/{dataGrid.Rows[row].Cells[column].Value}");
                else if (column == 1)
                    CreateLinkWindowCreation(dataGrid.Rows[row].Cells[0].Value.ToString());
                else
                    System.Diagnostics.Process.Start(
                        $"https://epm.verisk.com/jira/browse/{dataGrid.Rows[row].Cells[0].Value}");
                Console.WriteLine(dataGrid?.Rows[row].Cells[column].Value);
            }
        }

        /// <summary>
        /// Colors the datagrid view cell based on the status of the task
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="statusName"></param>
        internal void ColorDataGridViewCell(int row, int column, string statusName)
        {
            if (string.IsNullOrEmpty(statusName))
            {
                return;
            }
            switch (statusName)
            {
                case "Integration Testing":
                case "Functional Testing":
                case "In Progress":
                case "Acceptance Testing":
                case "Code Review":
                    dgJiraTaskList.Rows[row].Cells[column].Style.BackColor = Color.Turquoise;
                    break;

                case "Closed":
                    dgJiraTaskList.Rows[row].Cells[column].Style.BackColor = Color.DarkSeaGreen;
                    break;

                default:
                    dgJiraTaskList.Rows[row].Cells[column].Style.BackColor = Color.Crimson;
                    break;
            }
        }

        internal bool CreateLinkBetweenTasks(string mainTask, string linkedTask)
        {
            //TODO: After adding above functionality, make sure to return the unliked task to the task list as it's own task
            //TODO: Add functionality to remember which tasks are linked for a specific user.
            //TODO: Add functionality to tell user when a linked task does not exist.
            Console.WriteLine($"{mainTask} {linkedTask}");
            if (string.IsNullOrEmpty(mainTask) || string.IsNullOrEmpty(linkedTask))
                return false;
            for (int i = 0; i < dgJiraTaskList.RowCount; i++)
            {
                if (dgJiraTaskList.Rows[i].Cells[0].Value.ToString() == mainTask.ToUpper())// && (dgJiraTaskList.Rows[i].Cells[1] == null || dgJiraTaskList.Rows[i].Cells[1].Value == ""))
                {
                    if (UserPreferences.LinkedTaskList.ContainsKey(mainTask.ToUpper()))
                    {
                        //TODO: Check if a task is already linked, then ask the person if they want to unlink that in favor of the new task
                    }
                    try
                    {
                        var issue = TaskBusi.TaskController.GetIssue(linkedTask);
                        dgJiraTaskList.Rows[i].Cells[1].Value = linkedTask;
                        UserPreferences.LinkedTaskList[mainTask.ToUpper()] = linkedTask.ToUpper();
                        ColorDataGridViewCell(i, 1, issue.Status.Name);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("there was an error in finding this task...");
                    }
                }
                if (dgJiraTaskList.Rows[i].Cells[0].Value.ToString() == linkedTask.ToUpper())
                {
                    dgJiraTaskList.Rows.RemoveAt(i);
                }
            }
            return true;
        }

        #endregion Data Grid View Logic

        private void Logout()
        {
            dgJiraTaskList.Rows.Clear();
            LoginWindow.LogCont = new LoginController();
            LoginWindow.loginSettings.ClearUser();
            LoginWindow.loginSettings.Save(LoginWindow.SettingsPath, LoginWindow.SettingsFile);
            pLoginToViewTasks.Visible = true;
        }

        private void CreateLinkWindowCreation(string task = null)
        {
            var linkTasksWindow = task != null ? new LinkTasks(task) : new LinkTasks();
            linkTasksWindow.ShowDialog();
            CreateLinkBetweenTasks(linkTasksWindow.MainTask, linkTasksWindow.LinkedTask);
        }
    }
}
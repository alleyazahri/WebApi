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
        private AddRemoveProjects AddRemoveProjectsWindow { get; set; }
        private RowColumn CurrentRowColumn { get; set; }

        /// <summary>
        /// Initializes the TaskList window
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            LoginWindow = new Login();
            IssueList = new List<CompoundIssue>();
            CurrentRowColumn = new RowColumn();
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
            AddAndRemoveProjects();
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
            dgJiraTaskList.ContextMenuStrip = null;
            //TODO: Cast e and see if that gives me what I want
            //TaskBusi.RemoveLink("MainTask", "LinkedTask");
        }

        private void dgvtaskContextMenu_AddLink(object sender, EventArgs e)
        {
            MessageBox.Show($"Not Yet Implemented! {CurrentRowColumn.Row} {CurrentRowColumn.Column}");
            dgJiraTaskList.ContextMenuStrip = null;
            //TODO: Cast e and see if that gives me what I want
            //TaskBusi.RemoveLink("MainTask", "LinkedTask");
        }

        private void dvgTaskContextMenu_LogWork(object sender, EventArgs e)
        {
            MessageBox.Show("Not Yet Implemented!");
            dgJiraTaskList.ContextMenuStrip = null;
        }

        private void dvgTaskContextMenu_MarkIrrelevant(object sender, EventArgs e)
        {
            var currentCell = dgJiraTaskList.Rows[CurrentRowColumn.Row].Cells[0];
            if (currentCell.Style.BackColor == UserPreferences.ColorLegend.IrrelevantTasks)
            {
                //Change cell color back by getting the task, and it's status, then coloring
                UserPreferences.IrrelevantTasks.RemoveAll(x => x == currentCell.Value.ToString());
            }
            else
            {
                for (int i = 0; i < dgJiraTaskList.Rows[CurrentRowColumn.Row].Cells.Count; i++)
                {
                    ColorDataGridViewCell(CurrentRowColumn.Row, i, UserPreferences.ColorLegend.IrrelevantTasks);
                }
                UserPreferences.IrrelevantTasks.Add(currentCell.Value.ToString());
                dgJiraTaskList.Rows[CurrentRowColumn.Row].Cells[2].Value =
                    $"7 - {dgJiraTaskList.Rows[CurrentRowColumn.Row].Cells[2].Value}";
            }
            dgJiraTaskList.ContextMenuStrip = null;
        }

        #endregion Events

        #region Data Grid View Logic

        private void LoadDataGridViewContextMenu()
        {
            //Assign to datagridview
            //dgJiraTaskList.ContextMenuStrip = mnu;
        }

        private async void LoadDataGridView()
        {
            dgJiraTaskList.Columns.Add("DevTask", "Task to Follow");
            dgJiraTaskList.Columns.Add("MyTask", "My Task");
            dgJiraTaskList.Columns.Add("Status", "Status");
            dgJiraTaskList.Columns.Add("TaskName", "Summary");
            dgJiraTaskList.Columns.Add("TaskDescription", "Description");
            dgJiraTaskList.Columns.Add("Notes", "Notes");

            dgJiraTaskList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgJiraTaskList.Columns[3].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgJiraTaskList.Columns[3].Width = (int)((Width - 30) * .2);
            dgJiraTaskList.Columns[4].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgJiraTaskList.Columns[4].Width = (int)((Width - 30) * .4);

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
                for (int i = 0; i < Tasks.Count; i++)
                {
                    dgJiraTaskList.Rows.Add(Tasks[i].ToStringArray());
                    ColorDataGridViewCell(i, 0, Tasks[i]);
                }
            }
            else
            {
                IssueList = TaskBusi.CompareTasksToUserPrefs(Tasks, UserPreferences.LinkedTaskList);
                for (int i = 0; i < IssueList.Count; i++)
                {
                    dgJiraTaskList.Rows.Add(IssueList[i].ToStringArray());
                    ColorDataGridViewCell(i, 0, IssueList[i].DevTask);
                    ColorDataGridViewCell(i, 1, IssueList[i].LinkedTask);
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
        internal void ColorDataGridViewCell(int row, int column, Issue issue)
        {
            if (issue == null)
            {
                return;
            }
            if (UserPreferences.IrrelevantTasks.Contains(issue.Key.Value))
            {
                for (int i = 0; i < dgJiraTaskList.Rows[row].Cells.Count; i++)
                {
                    dgJiraTaskList.Rows[row].Cells[i].Style.BackColor = UserPreferences.ColorLegend.IrrelevantTasks;
                }
                dgJiraTaskList.Rows[row].Cells[2].Value = "7 - " + dgJiraTaskList.Rows[row].Cells[2].Value;
            }
            switch (issue.Status.Name)
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

        /// <summary>
        /// Colors the datagrid view cell based on the given color
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="color"></param>
        internal void ColorDataGridViewCell(int row, int column, Color color)
        {
            dgJiraTaskList.Rows[row].Cells[column].Style.BackColor = color;
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
                        ColorDataGridViewCell(i, 1, issue);
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
            UserPreferences.Save();
            return true;
        }

        #endregion Data Grid View Logic

        #region Login/Logout Logic

        private void Logout()
        {
            dgJiraTaskList.Rows.Clear();
            LoginWindow.LogCont = new LoginController();
            LoginWindow.loginSettings.ClearUser();
            LoginWindow.loginSettings.Save(LoginWindow.SettingsPath, LoginWindow.SettingsFile);
            pLoginToViewTasks.Visible = true;
        }

        #endregion Login/Logout Logic

        #region Create Link Window Logic

        private void CreateLinkWindowCreation(string task = null)
        {
            var linkTasksWindow = task != null ? new LinkTasks(task) : new LinkTasks();
            linkTasksWindow.ShowDialog();
            CreateLinkBetweenTasks(linkTasksWindow.MainTask, linkTasksWindow.LinkedTask);
        }

        #endregion Create Link Window Logic

        #region Add/Remove Project Window Logic

        private void AddAndRemoveProjects()
        {
            AddRemoveProjectsWindow = new AddRemoveProjects(UserPreferences.Projects);
            AddRemoveProjectsWindow.ShowDialog();
        }

        #endregion Add/Remove Project Window Logic

        private void dgJiraTaskList_ColumnSortModeChanged(object sender, DataGridViewColumnEventArgs e)
        {
            var asdf = dgJiraTaskList.SortOrder;
            var fdsa = dgJiraTaskList.SortedColumn;
        }

        private void dgJiraTaskList_Sorted(object sender, EventArgs e)
        {
            var asdf = dgJiraTaskList.SortOrder;
            var fdsa = dgJiraTaskList.SortedColumn;
        }

        private void dgJiraTaskList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                ContextMenuStrip mnu = new ContextMenuStrip();
                ToolStripMenuItem mnuLogWork = new ToolStripMenuItem("Log Work");
                ToolStripMenuItem mnuAddLink = new ToolStripMenuItem("Add Link");
                ToolStripMenuItem mnuRemoveLink = new ToolStripMenuItem("Remove Link");
                ToolStripMenuItem mnuMarkIrrelevant = new ToolStripMenuItem("Mark Irrelevant");
                //Assign event handlers
                mnuRemoveLink.Click += dgvtaskContextMenu_RemoveLink;
                mnuAddLink.Click += dgvtaskContextMenu_AddLink;
                mnuLogWork.Click += dvgTaskContextMenu_LogWork;
                mnuMarkIrrelevant.Click += dvgTaskContextMenu_MarkIrrelevant;

                CurrentRowColumn.Row = e.RowIndex;
                CurrentRowColumn.Column = e.ColumnIndex;

                //Add to main context menu
                mnu.Items.AddRange(new ToolStripItem[] { mnuMarkIrrelevant, mnuLogWork, mnuAddLink, mnuRemoveLink });
                //mnu.Show(this,new Point(e.X,e.Y));
                dgJiraTaskList.ContextMenuStrip = mnu;
            }
        }

        private void colorKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorKeyWindow asdf = new ColorKeyWindow();
            asdf.ShowDialog();
        }

        private void dgJiraTaskList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show("Will be implementing soon...");
        }
    }
}
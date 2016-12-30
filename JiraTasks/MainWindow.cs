using Atlassian.Jira;
using JiraApi;
using JiraTasks.Data;
using JiraTasks.MainWindowBusi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JiraTasks
{
    public partial class MainWindow : Form
    {
        private Login LoginWindow { get; set; }
        private List<Issue> Tasks { get; set; }
        private List<CompoundIssue> IssueList { get; set; }
        private UserPrefs UserPreferences { get; }
        private TaskBusi TaskBusi { get; }
        private AddRemoveProjects AddRemoveProjectsWindow { get; set; }
        private RowColumn CurrentRowColumn { get; }

        private int WorkingWidth => Width - dgJiraTaskList.Columns[0].Width - dgJiraTaskList.Columns[1].Width - dgJiraTaskList.Columns[2].Width - 58;

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
            LoadProjectMenu();
            TaskBusi = new TaskBusi(LoginWindow.LogCont);
            if (LoginWindow.loginSettings.SavePassword != CheckState.Checked)
                LoginWindow.ShowDialog();
        }

        #region Events

        //Main Menu Item Events
        private void colorKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorKeyWindow asdf = new ColorKeyWindow();
            asdf.ShowDialog();
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

        private void linkTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateLinkWindowCreation();
        }

        //Sub Menu Item Events
        private void projectSubMenuItem_Click(object sender, EventArgs e)
        {
            var toolStripItem = sender as ToolStripMenuItem;
            FilterProjects(toolStripItem);
        }

        //Main Window Events
        private void MainWindow_Shown(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            //TODO: Add a 'Loading' Label :)
            if (LoginWindow.LogCont != null && LoginWindow.LogCont.IsLoggedIn())
                LoadDataGridView();
            else
                pLoginToViewTasks.Visible = true;
            Cursor.Current = Cursors.Default;
        }

        private void MainWindow_ResizeEnd(object sender, EventArgs e)
        {
            AutoAdjustColumnWidths();
        }

        //Data Grid View Events
        private void dgJiraTaskList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewCellDoubleClicked(e.RowIndex, e.ColumnIndex, dgJiraTaskList);
        }

        private void dgJiraTaskList_SelectionChanged(object sender, EventArgs e)
        {
            dgJiraTaskList.ClearSelection();
        }

        private void dgJiraTaskList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                SaveNotesToTask(
                    dgJiraTaskList.Rows[e.RowIndex].Cells[0].Value.ToString(),
                    dgJiraTaskList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString());
            }
        }

        private void dgJiraTaskList_Sorted(object sender, EventArgs e)
        {
            SaveSortPreferences();
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

        //Data Grid View Sub Menu Item Events
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
            UserPreferences.Save();
            dgJiraTaskList.ContextMenuStrip = null;
        }

        //Miscellaneous Events
        private void bRefresh_Click(object sender, EventArgs e)
        {
            LoadDataGridView(true);
        }

        #endregion Events

        #region Data Grid View Logic

        private void SaveSortPreferences()
        {
            UserPreferences.TaskSortOrder = dgJiraTaskList.SortOrder;
            UserPreferences.SortedColumn = dgJiraTaskList.SortedColumn.DisplayIndex;
            UserPreferences.Save();
        }

        private async void LoadDataGridView(bool clearOriginalGrid = false)
        {
            bRefresh.Visible = false;
            if (clearOriginalGrid)
            {
                dgJiraTaskList.Rows.Clear();
                dgJiraTaskList.Refresh();
            }
            else
            {
                dgJiraTaskList.Columns.Add("DevTask", "Task to Follow");
                dgJiraTaskList.Columns.Add("MyTask", "My Task");
                dgJiraTaskList.Columns.Add("Status", "Status");
                dgJiraTaskList.Columns.Add("TaskName", "Summary");
                dgJiraTaskList.Columns.Add("TaskDescription", "Description");
                dgJiraTaskList.Columns.Add("Notes", "Notes");
                dgJiraTaskList.RowHeadersVisible = false;

                dgJiraTaskList.Columns[0].Width = 116;
                dgJiraTaskList.Columns[1].Width = 116;
                dgJiraTaskList.Columns[2].Width = 109;

                //Set all columns to read only except the last one
                dgJiraTaskList.Columns[0].ReadOnly = true;
                dgJiraTaskList.Columns[1].ReadOnly = true;
                dgJiraTaskList.Columns[2].ReadOnly = true;
                dgJiraTaskList.Columns[3].ReadOnly = true;
                dgJiraTaskList.Columns[4].ReadOnly = true;

                dgJiraTaskList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dgJiraTaskList.Columns[3].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgJiraTaskList.Columns[4].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgJiraTaskList.Columns[5].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                AutoAdjustColumnWidths();

                dgJiraTaskList.AllowUserToAddRows = false;
                dgJiraTaskList.AllowUserToOrderColumns = true;
                dgJiraTaskList.AllowUserToDeleteRows = false;

                dgJiraTaskList.Show();
            }

            var filter = GetCurrentFilterOptions();
            var task = Task.Factory.StartNew(() => TaskBusi.PopulateJiraTasks(filter)); // new Task<List<Issue>>(TaskBusi.PopulateJiraTasks);
            await task.ConfigureAwait(true);
            await task;
            Tasks = task.Result;
            if (UserPreferences.LinkedTaskList == null || UserPreferences.LinkedTaskList.Count == 0)
            {
                for (int i = 0; i < Tasks.Count; i++)
                {
                    dgJiraTaskList.Rows.Add(Tasks[i].ToObjectArray(UserPreferences.Notes));
                    ColorDataGridViewCell(i, 0, Tasks[i]);
                }
            }
            else
            {
                IssueList = TaskBusi.CompareTasksToUserPrefs(Tasks, UserPreferences.LinkedTaskList);
                for (int i = 0; i < IssueList.Count; i++)
                {
                    dgJiraTaskList.Rows.Add(IssueList[i].ToObjectArray(UserPreferences.Notes));
                    ColorDataGridViewCell(i, 0, IssueList[i].DevTask);
                    ColorDataGridViewCell(i, 1, IssueList[i].LinkedTask);
                }
            }
            if (UserPreferences.TaskSortOrder != SortOrder.None && UserPreferences.SortedColumn != -1)
                dgJiraTaskList.Sort(
                    dgJiraTaskList.Columns[UserPreferences.SortedColumn],
                    UserPreferences.TaskSortOrder == SortOrder.Ascending
                        ? ListSortDirection.Ascending
                        : ListSortDirection.Descending);
            LoadRefreshButton();
        }

        private TaskFilter GetCurrentFilterOptions()
        {
            var filter = new TaskFilter()
            {
                Project = UserPreferences.Projects.Where(x => x.ProjectIsSelected).Select(x => x.ProjectName).ToList(),
                UpdatedSince = DateTime.Now.AddMonths(-7),
                ResolutionDateAfter = DateTime.Now.AddMonths(-3)
            };
            return filter;
        }

        private void DataGridViewCellDoubleClicked(int row, int column, DataGridView dataGrid)
        {
            if (row >= 0 && column == 5)
            {
                dgJiraTaskList.CurrentCell = dgJiraTaskList.Rows[row].Cells[column];
                dgJiraTaskList.BeginEdit(true);
            }
            else if (row >= 0 && column >= 0)
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
        /// <param name="issue"></param>
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
                    dgJiraTaskList.Rows[row].Cells[column].Style.BackColor = Color.Cyan;
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

        private void AutoAdjustColumnWidths()
        {
            dgJiraTaskList.Columns[3].Width = (int)(WorkingWidth * .2);
            dgJiraTaskList.Columns[4].Width = (int)(WorkingWidth * .45);
            dgJiraTaskList.Columns[5].Width = (int)(WorkingWidth * .35);
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
            AddRemoveProjectsWindow = new AddRemoveProjects(UserPreferences, TaskBusi, this.Left, this.Top);
            if (AddRemoveProjectsWindow.ShowDialog() == DialogResult.OK)
            {
                AddProjectMenuItems();
            }
        }

        private void AddProjectMenuItems()
        {
            //Add the user prefs
            UserPreferences.Save();

            //Determine which projects need to be added and which need to be removed
            var prefs = UserPreferences.DeepCopy();
            var projectsToAdd = prefs.Projects;
            var projectsToRemove = new List<ToolStripMenuItem>();
            foreach (ToolStripMenuItem dropDownItem in projectsToolStripMenuItem.DropDownItems)
            {
                var proj = projectsToAdd.FirstOrDefault(x => x.ProjectName == dropDownItem.ToString());
                if (proj != null)
                    projectsToAdd.Remove(proj);
                else if (dropDownItem.ToString() != "Add/Remove Project")
                    projectsToRemove.Add(dropDownItem);
            }

            //Add any new projects to the menu
            foreach (var project in projectsToAdd)
            {
                var projectMenuItem = new ToolStripMenuItem(project.ProjectName);
                projectMenuItem.Click += projectSubMenuItem_Click;
                projectMenuItem.Checked = project.ProjectIsSelected;
                projectsToolStripMenuItem.DropDownItems.Add(projectMenuItem);
            }

            //Remove any old projects from the menu
            foreach (var project in projectsToRemove)
            {
                projectsToolStripMenuItem.DropDownItems.Remove(project);
            }
        }

        private void LoadProjectMenu()
        {
            //Add any new projects to the menu
            foreach (var project in UserPreferences.Projects)
            {
                var projectMenuItem = new ToolStripMenuItem(project.ProjectName);
                projectMenuItem.Click += projectSubMenuItem_Click;
                projectMenuItem.Checked = project.ProjectIsSelected;
                projectsToolStripMenuItem.DropDownItems.Add(projectMenuItem);
            }
        }

        #endregion Add/Remove Project Window Logic

        private void LoadRefreshButton()
        {
            bRefresh.Visible = true;
            bRefresh.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void SaveNotesToTask(string task, string value)
        {
            if (string.IsNullOrEmpty(value))
                UserPreferences.Notes.Remove(task);
            else
                UserPreferences.Notes[task] = value;
            UserPreferences.Save();
        }

        private void FilterProjects(ToolStripMenuItem toolStripItem)
        {
            toolStripItem.Checked = !toolStripItem.Checked;
            var project = UserPreferences.Projects.FirstOrDefault(x => x.ProjectName == toolStripItem.Text);
            project.ProjectIsSelected = toolStripItem.Checked;
            LoadDataGridView(true);
        }
    }
}
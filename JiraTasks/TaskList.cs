using Atlassian.Jira;
using JiraApi;
using JiraTasks.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JiraTasks
{
    public partial class TaskList : Form
    {
        private Login LoginWindow { get; set; }
        private List<Issue> Tasks { get; set; }
        private TaskController TaskCont { get; set; }

        public TaskList()
        {
            InitializeComponent();
            LoginWindow = new Login();
            if (LoginWindow.loginSettings.SavePassword != CheckState.Checked)
                LoginWindow.ShowDialog();
        }

        private void PopulateJiraTasks(string username, string password)
        {
            TaskCont = new TaskController(LoginWindow.LogCont);
            var filter = new TaskFilter()
            {
                Project = new List<string>() { "XWESVC" },
                UpdatedSince = DateTime.Now.AddMonths(-7),
                ResolutionDateAfter = DateTime.Now.AddMonths(-3)
            };
            Tasks = TaskCont.GetIssues(filter: filter);
        }

        private void dgJiraTaskList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var dataGrid = sender as DataGridView;
                if (dataGrid == null)
                    throw new Exception("the data grid is null, something went seriously wrong O.o");
                if (e.ColumnIndex == 1 && dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                    System.Diagnostics.Process.Start($"https://epm.verisk.com/jira/browse/{dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value}");
                else
                {
                    System.Diagnostics.Process.Start($"https://epm.verisk.com/jira/browse/{dataGrid.Rows[e.RowIndex].Cells[0].Value}");
                }
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

        private async Task LoadingJiraTasks()
        {
            dgJiraTaskList.Columns.Add("DevTask", "Task to Follow");
            dgJiraTaskList.Columns.Add("MyTask", "My Task");
            dgJiraTaskList.Columns.Add("Status", "Status");
            dgJiraTaskList.Columns.Add("TaskDescription", "Description");
            dgJiraTaskList.Columns.Add("TaskName", "Summary");

            dgJiraTaskList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgJiraTaskList.Columns[4].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dgJiraTaskList.AllowUserToAddRows = false;
            dgJiraTaskList.AllowUserToOrderColumns = true;
            dgJiraTaskList.AllowUserToDeleteRows = false;

            dgJiraTaskList.Show();
            //Console.WriteLine($"{Thread.CurrentContext} right after initialization of columns");

            //var jc = new JiraController("https://epm.verisk.com/jira/", loginWindow.username, loginWindow.password);
            //tasks = jc.ParseJiraTasks();
            Task task = new Task(() => { PopulateJiraTasks(LoginWindow.loginSettings.Username, LoginWindow.loginSettings.Password); });
            task.Start();
            await task.ConfigureAwait(true);
            await task;
            //Console.WriteLine($"{Thread.CurrentContext} right before initialization of rows");

            for (int i = 0; i < Tasks.Count; i++)
            {
                dgJiraTaskList.Rows.Add(Tasks[i].ToStringArray());
                if (Tasks[i].Status.Name.Contains("Integration") || Tasks[i].Status.Name.Contains("Functional") || Tasks[i].Status.Name.Contains("In Progress") ||
                    Tasks[i].Status.Name.Contains("Code Review") || Tasks[i].Status.Name.Contains("Acceptance"))
                {
                    dgJiraTaskList.Rows[i].Cells[0].Style.BackColor = Color.Turquoise;
                }
                else if (Tasks[i].Status.Name.Contains("Closed"))
                {
                    dgJiraTaskList.Rows[i].Cells[0].Style.BackColor = Color.DarkSeaGreen;
                }
                else
                {
                    dgJiraTaskList.Rows[i].Cells[0].Style.BackColor = Color.Crimson;
                }
            }
        }

        private async void TaskList_Shown(object sender, EventArgs e)
        {
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
            throw new NotImplementedException("Can add and remove projects to filter with methinks.....dynamically that need to be later saved :/ good luck woman!");
        }

        private void TaskList_FormClosing(object sender, FormClosingEventArgs e)
        {
            throw new NotImplementedException("Would you like to save your current filter options? (compare to saved filters first)");
        }
    }
}
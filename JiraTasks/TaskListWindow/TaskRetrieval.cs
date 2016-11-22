using JiraApi;
using JiraTasks.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JiraTasks
{
    partial class TaskList
    {
        private void PopulateJiraTasks()
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

        private async Task LoadingJiraTasks()
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
            //Console.WriteLine($"{Thread.CurrentContext} right after initialization of columns");

            //var jc = new JiraController("https://epm.verisk.com/jira/", loginWindow.username, loginWindow.password);
            //tasks = jc.ParseJiraTasks();
            Task task = new Task(PopulateJiraTasks);
            task.Start();
            await task.ConfigureAwait(true);
            await task;
            CompareTasksToUserPrefs();
            //Console.WriteLine($"{Thread.CurrentContext} right before initialization of rows");

            for (int i = 0; i < Tasks.Count; i++)
            {
                dgJiraTaskList.Rows.Add(Tasks[i].ToStringArray());
                ColorTask(i, 0, Tasks[i].Status.Name);
            }
        }

        private void CompareTasksToUserPrefs()
        {
            throw new NotImplementedException();
        }

        private void ColorTask(int row, int column, string statusName)
        {
            if (statusName.Contains("Integration") || statusName.Contains("Functional") || statusName.Contains("In Progress") ||
                statusName.Contains("Code Review") || statusName.Contains("Acceptance"))
            {
                dgJiraTaskList.Rows[row].Cells[column].Style.BackColor = Color.Turquoise;
            }
            else if (statusName.Contains("Closed"))
            {
                dgJiraTaskList.Rows[row].Cells[column].Style.BackColor = Color.DarkSeaGreen;
            }
            else
            {
                dgJiraTaskList.Rows[row].Cells[column].Style.BackColor = Color.Crimson;
            }
        }

        private bool CreateLinkBetweenTasks(string mainTask, string linkedTask)
        {
            //TODO: Add functionality to check if a task is already linked, then ask the person if they want to unlink that task
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
                    try
                    {
                        var issue = TaskCont.GetIssue(linkedTask);
                        dgJiraTaskList.Rows[i].Cells[1].Value = linkedTask;
                        ColorTask(i, 1, issue.Status.Name);
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

        private void RemoveLink(string maintask, string linkedtask)
        {
            Console.WriteLine(maintask);
            //throw new NotImplementedException();
        }
    }
}
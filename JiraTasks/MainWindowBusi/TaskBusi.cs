using Atlassian.Jira;
using JiraApi;
using JiraTasks.Data;
using System;
using System.Collections.Generic;

namespace JiraTasks.MainWindowBusi
{
    internal class TaskBusi : BaseBusi
    {
        public TaskBusi(LoginController loginController) : base(loginController)
        {
        }

        internal List<Issue> PopulateJiraTasks()
        {
            var filter = new TaskFilter()
            {
                Project = new List<string>() { "XWESVC" },
                UpdatedSince = DateTime.Now.AddMonths(-7),
                ResolutionDateAfter = DateTime.Now.AddMonths(-3)
            };
            return TaskController.GetIssues(filter: filter);
        }

        internal List<CompoundIssue> CompareTasksToUserPrefs(List<Issue> tasks, Dictionary<string, string> linkedTasks)
        {
            var issueList = new List<CompoundIssue>();
            var removeIssues = new List<string>();
            foreach (var issue in tasks)
            {
                if (linkedTasks.ContainsKey(issue.Key.Value))
                {
                    issueList.Add(new CompoundIssue() { DevTask = issue, LinkedTask = TaskController.GetIssue(linkedTasks[issue.Key.Value]) });
                }
                else
                {
                    issueList.Add(new CompoundIssue() { DevTask = issue });
                }
            }
            foreach (var removeIssue in removeIssues)
            {
                issueList.RemoveAll(x => x.DevTask.Key.Value == removeIssue);
            }
            return issueList;
        }

        internal void RemoveLink(string maintask, string linkedtask)
        {
            Console.WriteLine(maintask);
            //throw new NotImplementedException();
        }
    }
}
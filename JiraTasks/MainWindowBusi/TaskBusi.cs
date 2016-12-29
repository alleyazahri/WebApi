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

        internal List<Issue> PopulateJiraTasks(TaskFilter filter)
        {
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
                    removeIssues.Add(linkedTasks[issue.Key.Value]);
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

        public List<Issue> RemoveIrrelevantTasks(List<Issue> tasks, List<string> irrelevantTasks)
        {
            foreach (var irrelevantTask in irrelevantTasks)
            {
                tasks.RemoveAll(x => x.Key.Value == irrelevantTask);
            }
            return tasks;
        }

        public bool VerifyProjectExists(string projectName)
        {
            var filter = new TaskFilter()
            {
                Project = new List<string>() { projectName.ToUpper() }
            };
            try
            {
                TaskController.GetIssues(1, filter);
                return true;
            }
            catch (AggregateException)
            {
                return false;
            }
        }
    }
}
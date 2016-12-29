using Atlassian.Jira;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JiraApi
{
    public class TaskController
    {
        private LoginController Lc { get; }
        private const int TaskNum = 9999;

        public TaskController(LoginController loginController)
        {
            Lc = loginController;
        }

        public List<Issue> GetIssues(int numIssues = TaskNum, TaskFilter filter = null)
        {
            var propsNotNull = filter?.PropertiesNotNull();
            if (filter == null || propsNotNull == "")
            {
                return Lc.JiraConnection.Issues.Queryable.Select(x => x).Take(numIssues).ToList();
            }
            List<Issue> tasks = FilterProjects(filter);
            tasks = FilterByUpdatedDate(filter, tasks);
            tasks = FilteredByResolutionDate(filter, tasks);
            tasks = FilterByAssignee(filter, tasks);
            tasks = FilterByStatus(filter, tasks);

            return tasks.Take(numIssues).ToList();
        }

        public Issue GetIssue(string issueId)
        {
            return Lc.JiraConnection.Issues.Queryable.FirstOrDefault(i => i.Key == issueId.ToUpper());
        }

        public List<Issue> GetIssuesInProject(string projectName, int numIssues)
        {
            return Lc.JiraConnection.Issues.Queryable.Where(i => i.Project == projectName).Take(numIssues).ToList();
        }

        private List<Issue> FilterByStatus(TaskFilter filter, List<Issue> tasks)
        {
            if (filter.Statuses == null || filter.Statuses.Count <= 0 || tasks?.Count == 0)
                return tasks;
            var filteredTasks = new List<Issue>();
            if (tasks == null)
            {
                foreach (var status in filter.Statuses)
                {
                    filteredTasks.AddRange(
                        Lc
                            .JiraConnection
                            .Issues
                            .Queryable
                            .Where(
                                i =>
                                    i.Status.Name.ToLower() == status.ToLower())
                            .Take(TaskNum));
                }
            }
            else
            {
                foreach (var status in filter.Statuses)
                {
                    filteredTasks.AddRange(tasks.Where(i => i.Status.Name.ToLower() == status.ToLower()));
                }
            }
            return filteredTasks;
        }

        private List<Issue> FilterByAssignee(TaskFilter filter, List<Issue> tasks)
        {
            if (filter.AssigneeIds == null || filter.AssigneeIds.Count <= 0)
                return tasks;
            var filteredTasks = new List<Issue>();
            if (tasks == null)
            {
                foreach (var assignee in filter.AssigneeIds)
                {
                    filteredTasks.AddRange(
                        Lc
                            .JiraConnection
                            .Issues
                            .Queryable
                            .Where(
                                i =>
                                    i.Assignee.ToLower() == assignee.ToLower())
                            .Take(TaskNum)
                            .ToList());
                }
            }
            else
            {
                foreach (var assignee in filter.AssigneeIds)
                {
                    filteredTasks.AddRange(tasks.Where(i => i.Assignee.ToLower() == assignee.ToLower()));
                }
            }
            return filteredTasks;
        }

        private List<Issue> FilteredByResolutionDate(TaskFilter filter, List<Issue> tasks)
        {
            if (filter.ResolutionDateAfter == null)
                return tasks;
            var filteredTasks = new List<Issue>();
            if (tasks == null)
            {
                filteredTasks.AddRange(
                    Lc
                        .JiraConnection
                        .Issues
                        .Queryable
                        .Where(i => i.ResolutionDate == null || DateTime.Compare(i.ResolutionDate.Value, filter.ResolutionDateAfter.Value) >= 0)
                        .Take(TaskNum));
            }
            else
            {
                filteredTasks.AddRange(tasks.Where(i => i.ResolutionDate == null ||
                             DateTime.Compare(i.ResolutionDate.Value, filter.ResolutionDateAfter.Value) >= 0));
            }
            return filteredTasks;
        }

        private List<Issue> FilterByUpdatedDate(TaskFilter filter, List<Issue> tasks)
        {
            if (filter.UpdatedSince == null)
                return tasks;
            var filteredTasks = new List<Issue>();
            if (tasks == null)
            {
                var initialList =
                    Lc
                        .JiraConnection
                        .Issues
                        .Queryable
                        .Where(i => i.Updated != null)
                        .Take(TaskNum).ToList();
                filteredTasks.AddRange(initialList.Where(i => DateTime.Compare(i.Updated.Value, filter.UpdatedSince.Value) >= 0));
            }
            else
            {
                filteredTasks.AddRange(tasks.Where(i => i.Updated != null &&
                             DateTime.Compare(i.Updated.Value, filter.UpdatedSince.Value) >= 0));
            }
            return filteredTasks;
        }

        private List<Issue> FilterProjects(TaskFilter filter)
        {
            if (filter.Project == null || filter.Project.Count <= 0)
                return null;
            var tasks = new List<Issue>();
            foreach (var project in filter.Project)
            {
                tasks.AddRange(Lc.JiraConnection.Issues.Queryable.Where(x => x.Project == project.ToUpper()).Take(TaskNum));
            }
            return tasks;
        }

        public List<Issue> GetIssues(int numIssues, List<Issue> issues, TaskFilter filter = null)
        {
            return null;
        }
    }
}
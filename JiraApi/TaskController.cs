using Atlassian.Jira;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JiraApi
{
    public class TaskController
    {
        private LoginController Lc { get; }

        public TaskController(LoginController loginController)
        {
            Lc = loginController;
        }

        public List<Issue> GetIssues(int numIssues = 9999, TaskFilter filter = null)
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

            var asdf = tasks.FirstOrDefault(x => x.Key.Value == "XWESVC-7");

            return tasks.Take(numIssues).ToList();
        }

        public Issue GetIssue(string issueId)
        {
            return Lc.JiraConnection.Issues.Queryable.FirstOrDefault(i => i.Key == issueId.ToUpper());
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
                            .Take(9999));
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
                            .Take(9999)
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
                        .Take(9999));
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
                filteredTasks.AddRange(
                    Lc
                        .JiraConnection
                        .Issues
                        .Queryable
                        .Where(i => i.Updated != null && DateTime.Compare(i.Updated.Value, filter.UpdatedSince.Value) >= 0)
                        .Take(9999));
            }
            else
            {
                var asdf = tasks.FirstOrDefault(i => i.Key.Value == "XWESVC-991");
                var fdsa = tasks.FirstOrDefault(i => i.Key.Value == "XWESVC-956");
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
                tasks.AddRange(Lc.JiraConnection.Issues.Queryable.Where(x => x.Project == project.ToUpper()).Take(9999));
            }
            return tasks;
        }

        public List<Issue> GetIssues(int numIssues, List<Issue> issues, TaskFilter filter = null)
        {
            return null;
        }
    }
}
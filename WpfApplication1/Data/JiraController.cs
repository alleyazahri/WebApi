using Atlassian.Jira;
using Atlassian.Jira.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WpfApplication1.Data
{
	internal class JiraController
	{
		private Jira jiraConn { get; set; }

		public JiraController()
		{
			var settings = new JiraRestClientSettings()
			{
				EnableRequestTrace = true
			};
			var thingie =
				new IssueStatus(new RemoteStatus()
				{
					id = "10510",
					description = "",
					iconUrl = "https://epm.verisk.com/jira/images/icons/status_generic.gif",
					name = "Acceptance Testing"
				});
			jiraConn = Jira.CreateRestClient("https://epm.verisk.com/jira/", "I59098",
				File.ReadAllText(@"C:\Users\i59098\Google Drive\pword.txt"), settings);
		}

		private List<Issue> GetJiraTasks(string project)
		{
			return jiraConn.Issues.Queryable.Where(i => i.Project == project).Take(1000000000).ToList();
		}

		public Issue GetIssue(string issueName)
		{
			//return jiraConn.GetIssue(issueName);
			return null;
		}

		internal List<JiraIssue> ParseJiraTasks()
		{
			List<Issue> tasks = GetJiraTasks("XWESVC");

			var jiraIssues = new List<JiraIssue>();
			var issues = new Dictionary<string, JiraIssue>();
			var subtasks = new List<Issue>();
			foreach (var issue in tasks)
			{
				if (issue.Type.IsSubTask && (issue.Assignee == "I59098" || issue.Assignee == "I55711"))
				{
					subtasks.Add(issue);
				}
				else
				{
					string status = GetStatusIdentifier(issue.Status);
					if (
						(status == "5 - Closed"
						 && issue.ResolutionDate != null
						 && DateTime.Compare(issue.ResolutionDate.Value, DateTime.Now.AddMonths(-6)) < 0)
						|| status == "ARCHIVED")
					{
						continue;
					}

					var stuffs = new JiraIssue()
					{
						DevTask = issue.Key.Value,
						Status = status,
						TaskDescription = issue.Description,
						TaskName = issue.Summary
					};
					jiraIssues.Add(stuffs);
					issues.Add(issue.Key.Value, stuffs);
				}
			}
			foreach (var subtask in subtasks)
			{
				if (issues.ContainsKey(subtask.ParentIssueKey))
					issues[subtask.ParentIssueKey].MyTask = subtask.Key.Value;
				else
				{
					jiraIssues.Add(new JiraIssue()
					{
						DevTask = subtask.Key.Value,
						Status = "6 - Independent Task",
						TaskDescription = subtask.Description,
						TaskName = subtask.Summary
					});
				}
			}
			SortTasks(jiraIssues);
			return jiraIssues;
		}

		private void SortTasks(List<JiraIssue> jiraIssues)
		{
			jiraIssues.Sort((first, second) =>
			{
				int? blankCompare = null;
				if (first.Status == "" && second.Status == "")
					blankCompare = 0;
				else if (first.Status == "")
					blankCompare = 1;
				else if (second.Status == "")
					blankCompare = -1;
				return blankCompare ?? first.Status.CompareTo(second.Status);
			});
		}

		private string GetStatusIdentifier(IssueStatus issueStatus)
		{
			var status = "";
			switch (issueStatus.Name)
			{
				case "Acceptance Testing":
					status = "1 - Beta";
					break;

				case "Integration Testing":
				case "Functional Testing":
					status = "2 - Test";
					break;

				case "Code Review":
					status = "3 - Code Review";
					break;

				case "In Progress":
					status = "4 - In Progress";
					break;

				case "Closed":
					status = "6 - Closed";
					break;

				case "Reopened":
				case "Approved":
				case "Open":
					status = $"5 - {issueStatus.Name}";
					break;

				case "Archived":
					status = "7 - Archived";
					break;

				case "Ready to Merge":
					status = "0 - Ready to Merge";
					break;

				default:
					status = $"-1 - Unknown Status: {issueStatus.Name}";
					break;
			}
			return status;
		}

		//Status: In Progress           Count: 10
		//Status: Approved              Count: 12
		//Status: Functional Testing    Count: 1       Test
		//Status: Code Review           Count: 5
		//Status: Closed                Count: 812
		//Status: Acceptance Testing    Count: 49      Test
		//Status: Integration Testing   Count: 9       Beta
		//Status: Open                  Count: 12
		//Status: Reopened              Count: 4
		//Status: Archived              Count: 1
	}
}
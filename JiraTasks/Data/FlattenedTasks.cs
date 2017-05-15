using Atlassian.Jira;
using JiraApi;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace JiraTasks.Data
{
	public class FlattenedTasks
	{
		public List<FlattenedTask> Tasks { get; set; }
		private UserPrefs UserPreferences { get; }
		private TaskController TaskController { get; }

		public FlattenedTasks(UserPrefs userPreferences, TaskController taskController)
		{
			Tasks = new List<FlattenedTask>();
			UserPreferences = userPreferences;
			TaskController = taskController;
		}

		public void FlattenTasks(List<Issue> issues)
		{
			Tasks = new List<FlattenedTask>();
			foreach (var issue in issues)
			{
				if (IssueShouldBeAdded(issues, issue.Key.Value))
				{
					var linkedTask = GetLinkTaskName(issue.Key.Value);
					var flatTask = new FlattenedTask
					{
						DevTaskName = issue.Key.Value,
						DevTaskColor = GetTaskColor(issue),
						Description = issue.Description,
						Status = issue.Status.Name,
						Summary = issue.Summary,
						LinkedTaskName = linkedTask,
						LinkedTaskColor = GetTaskColor(linkedTask),
						Notes = UserPreferences.Notes.ContainsKey(issue.Key.Value) ? UserPreferences.Notes[issue.Key.Value] : "",
						IsIrreleventTask = UserPreferences.IrrelevantTasks.Contains(issue.Key.Value)
					};
					Tasks.Add(flatTask);
				}
			}
		}

		private bool IssueShouldBeAdded(List<Issue> issues, string issue)
		{
			var linkedTasks = UserPreferences.LinkedTaskList.FirstOrDefault(t => t.Value == issue);
			return linkedTasks.Key == null || linkedTasks.Value == null || issues.All(i => i.Key.Value != linkedTasks.Key);
		}

		private string GetLinkTaskName(string issueKey)
		{
			return UserPreferences.LinkedTaskList.ContainsKey(issueKey) ? UserPreferences.LinkedTaskList[issueKey] : null;
		}

		/// <summary>	Gets the task color based on a string value. </summary>
		/// <param name="value">	The task name or task-less status. </param>
		/// <returns>	The task color. </returns>
		private Color? GetTaskColor(string value)
		{
			if (value == null)
				return null;
			switch (value)
			{
				case "~C-InProgress":
					return UserPreferences.ColorLegend.InProgressTasks;

				case "~C-CodeReview":
					return UserPreferences.ColorLegend.CodeReviewTasks;

				case "~C-Complete":
					return UserPreferences.ColorLegend.CompletedTasks;

				case "~C-NotStarted":
					return UserPreferences.ColorLegend.NotStartedTasks;
			}
			return GetTaskColor(TaskController.GetIssue(value));
		}

		/// <summary>
		/// Gets the proper color associated with the given task
		/// </summary>
		/// <param name="task"></param>
		private Color? GetTaskColor(Issue task)
		{
			if (task == null)
				return null;
			switch (task.Status.Name)
			{
				case "Integration Testing":
				case "Functional Testing":
					return UserPreferences.ColorLegend.TasksInTest;

				case "In Progress":
					return UserPreferences.ColorLegend.InProgressTasks;

				case "Acceptance Testing":
					return UserPreferences.ColorLegend.TasksInBeta;

				case "Code Review":
					return UserPreferences.ColorLegend.CodeReviewTasks;

				case "Ready to Merge":
				case "Closed":
					return UserPreferences.ColorLegend.CompletedTasks;

				default:
					return UserPreferences.ColorLegend.NotStartedTasks;
			}
		}

		public FlattenedTask LinkNewTask(string mainTask, string linkedTask)
		{
			var task = Tasks.FirstOrDefault(t => t.DevTaskName.ToUpper() == mainTask.ToUpper());
			if (task == null)
				return null;
			task.LinkedTaskName = linkedTask;
			task.LinkedTaskColor = GetTaskColor(linkedTask);
			return task;
		}

		public void AddTask(Issue issue)
		{
			var linkedTask = GetLinkTaskName(issue.Key.Value);
			Tasks.Add(new FlattenedTask
			{
				DevTaskName = issue.Key.Value,
				DevTaskColor = GetTaskColor(issue),
				Description = issue.Description,
				Status = issue.Status.Name,
				Summary = issue.Summary,
				LinkedTaskName = linkedTask,
				LinkedTaskColor = GetTaskColor(linkedTask),
				Notes = UserPreferences.Notes.ContainsKey(issue.Key.Value) ? UserPreferences.Notes[issue.Key.Value] : "",
				IsIrreleventTask = UserPreferences.IrrelevantTasks.Contains(issue.Key.Value)
			});
		}
	}

	public class FlattenedTask
	{
		public string DevTaskName { get; set; }
		public Color? DevTaskColor { get; set; }
		public DateTime? DevTaskLastUpdated { get; set; }
		public string LinkedTaskName { get; set; }
		public Color? LinkedTaskColor { get; set; }
		public DateTime? LinkedTaskLastUpdated { get; set; }
		public string Status { get; set; }
		public string Summary { get; set; }
		public string Description { get; set; }
		public string Notes { get; set; }
		public bool IsIrreleventTask { get; set; }

		public object[] ToObjectArray()
		{
			string status = GetIssueSummaryEquivalent(Status);
			return new object[]
			{
				DevTaskName,
				LinkedTaskName == null || LinkedTaskName.Contains("~C-") ? "" : LinkedTaskName,
				status,
				Summary,
				Description?.Substring(0, Description.Length > 300 ? 300 : Description.Length),
				Notes
			};
		}

		public static string GetIssueSummaryEquivalent(string issueSummary)
		{
			string status;
			switch (issueSummary)
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
					status = $"5 - {issueSummary}";
					break;

				case "Archived":
					status = "7 - Archived";
					break;

				case "Ready to Merge":
					status = "0 - Ready to Merge";
					break;

				default:
					status = $"-1 - Unknown Status: {issueSummary}";
					break;
			}
			return status;
		}
	}
}
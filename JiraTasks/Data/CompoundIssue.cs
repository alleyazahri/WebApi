using Atlassian.Jira;
using System.Collections.Generic;

namespace JiraTasks.Data
{
	internal class CompoundIssue
	{
		public Issue DevTask { get; set; }
		public Issue LinkedTask { get; set; }
		public string NoTaskStatus { get; set; }

		public object[] ToObjectArray(Dictionary<string, string> userNotes)
		{
			string status = GetIssueSummaryEquivalent(DevTask.Status.Name);
			return new object[]
			{
				DevTask.Key.Value,
				LinkedTask?.Key?.Value ?? "",
				status,
				DevTask.Summary,
				DevTask.Description?.Substring(0, DevTask.Description.Length > 300 ? 300 : DevTask.Description.Length),
				userNotes.ContainsKey(DevTask.Key.Value) ? userNotes[DevTask.Key.Value] : ""
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
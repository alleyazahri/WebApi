using Atlassian.Jira;
using System.Collections.Generic;

namespace JiraTasks.Data
{
    public static class IssueExtension
    {
        public static object[] ToObjectArray(this Issue issue, Dictionary<string, string> userNotes)
        {
            string status = CompoundIssue.GetIssueSummaryEquivalent(issue.Status.Name);
            return new object[]
            {
                issue.Key.Value,
                "",
                status,
                issue.Summary,
                issue.Description?.Substring(0, issue.Description.Length > 300 ? 300 : issue.Description.Length),
                userNotes.ContainsKey(issue.Key.Value) ? userNotes[issue.Key.Value] : ""
            };
        }
    }
}
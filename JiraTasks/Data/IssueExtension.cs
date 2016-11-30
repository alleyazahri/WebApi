using Atlassian.Jira;

namespace JiraTasks.Data
{
    public static class IssueExtension
    {
        public static object[] ToStringArray(this Issue issue)
        {
            string status = CompoundIssue.GetIssueSummaryEquivalent(issue.Status.Name);
            return new object[] { issue.Key.Value, "", status, issue.Summary, issue.Description?.Substring(0, issue.Description.Length > 300 ? 300 : issue.Description.Length) };
        }
    }
}
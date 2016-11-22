using Atlassian.Jira;

namespace JiraTasks.Data
{
    public static class IssueExtension
    {
        public static object[] ToStringArray(this Issue issue)
        {
            return new object[] { issue.Key.Value, "", issue.Status.Name, issue.Summary, issue.Description };
        }
    }
}
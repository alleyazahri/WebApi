using Atlassian.Jira;

namespace JiraTasks.Data
{
    public static class IssueExtension
    {
        public static object[] ToStringArray(this Issue issue)
        {
            string status = GetIssueSummaryEquivalent(issue.Status.Name);
            return new object[] { issue.Key.Value, "", status, issue.Summary, issue.Description };
        }

        private static string GetIssueSummaryEquivalent(string issueSummary)
        {
            if (issueSummary == "Functional Testing" || issueSummary == "Integration Testing")
                return "2 - Test";
            if (issueSummary == "Acceptance Testing")
                return "1 - Beta";
            if (issueSummary == "Code Review")
                return $"3 - {issueSummary}";
            if (issueSummary == "In Progress")
                return $"4 - {issueSummary}";
            if (issueSummary == "Closed")
                return $"5 - {issueSummary}";
            return issueSummary;
        }
    }
}
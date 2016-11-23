using Atlassian.Jira;

namespace JiraTasks.Data
{
    internal class CompoundIssue
    {
        public Issue DevTask { get; set; }
        public Issue LinkedTask { get; set; }

        public object[] ToStringArray()
        {
            string status = GetIssueSummaryEquivalent(DevTask.Status.Name);
            return new object[] { DevTask.Key.Value, LinkedTask?.Key?.Value ?? "", status, DevTask.Summary, DevTask.Description };
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
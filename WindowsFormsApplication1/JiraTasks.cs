using Atlassian.Jira;
using Atlassian.Jira.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WindowsFormsApplication1
{
    internal class JiraTasks
    {
        public static string filepath { get; } = "C:\\Users\\i59098\\Google Drive\\jira.txt";

        private static List<Object> GetJiraTasks()
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
            var jiraConn = Jira.CreateRestClient("https://epm.verisk.com/jira/", "I59098",
                File.ReadAllText(@"C:\Users\i59098\Google Drive\pword.txt"), settings);

            var tasks = jiraConn.Issues.Queryable.Where(i => i.Project == "XWESVC").Take(1000000000).ToList();
            //var jiraenum = query.GetEnumerator();
            //Issue asdf = jiraenum.Current;
            //List<Issue> jiraIssues = new List<Issue>
            //{
            //    jiraenum.Current
            //};
            //while (jiraenum.MoveNext())
            //{
            //    jiraIssues.Add(jiraenum.Current);
            //}
            //foreach (var issue in jiraIssues)
            //{
            //    var s = issue.Project;
            //}
            var issueStatuses = new Dictionary<string, int>();
            foreach (var issue in tasks)
            {
                //if (issue.Assignee == "I59098" || issue.Assignee == "I55711")
                //{
                //    Console.WriteLine(issue.Key.Value + " " + issue.Summary + "\n" + issue.Description);
                //}
                //if (issue.Assignee != null && jiraConn.Users.GetUserAsync(issue.Assignee).Result.DisplayName == "McPherson, Bryan D")
                //    Console.WriteLine(issue.Assignee);
                //else
                //    Console.WriteLine(issue.Key.Value);
                if (issueStatuses.ContainsKey(issue.Status.Name))
                {
                    issueStatuses[issue.Status.Name]++;
                }
                else
                {
                    issueStatuses[issue.Status.Name] = 1;
                }
            }
            foreach (var issueStatus in issueStatuses)
            {
                Console.WriteLine($"Status: {issueStatus.Key} Count: {issueStatus.Value}");
            }

            //Issue thing = jiraConn.GetIssue("XWESVC-974");
            //var user = jiraConn.Users.GetUserAsync(thing.Assignee).Result;
            return null;
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
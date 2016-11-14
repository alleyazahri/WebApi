using Atlassian.Jira;
using Atlassian.Jira.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WpfApplication1.Data;

namespace WebApi
{
    internal class JiraTaskParser
    {
        public static string filepath { get; } = "C:\\Users\\i59098\\Google Drive\\jira.txt";

        private static void Main(string[] args)
        {
            var stuffs = GetJiraTasks();
        }

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

            var jiraIssues = new List<JiraIssue>();
            var issues = new List<Issue>();
            var subtasks = new List<Issue>();
            foreach (var issue in tasks)
            {
                if (issue.Type.IsSubTask && (issue.Assignee == "I59098" || issue.Assignee == "I55711"))
                {
                    subtasks.Add(issue);
                }
                else
                {
                    var status = "";
                    jiraIssues.Add(new JiraIssue() { DevTask = issue.Key.Value, });
                }
            }
            foreach (var issueStatus in subtasks)
            {
                Console.WriteLine($@"Status: {issueStatus.Key} Count: {issueStatus.Key.Value}");
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
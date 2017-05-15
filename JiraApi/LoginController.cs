using Atlassian.Jira;
using System;
using System.Linq;

namespace JiraApi
{
	public class LoginController
	{
		internal Jira JiraConnection { get; set; }

		public bool Login(string url, string username, string password)
		{
			JiraConnection = Jira.CreateRestClient(url, username, password);
			try
			{
				var jiraUser = JiraConnection.Issues.Queryable.FirstOrDefault(x => x.Key == "XWECSS-913");
				return true;
			}
			catch (Exception)
			{
				JiraConnection = null;
				return false;
			}
		}

		public void Logout()
		{
			JiraConnection = null;
		}

		public bool IsLoggedIn()
		{
			return JiraConnection != null;
		}
	}
}
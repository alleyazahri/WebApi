using JiraApi;
using JiraApi.DataClasses;
using NUnit.Framework;
using System;
using System.IO;

namespace JiraApiTests.FilteringTests
{
	internal class FilterByCreatedDateTests
	{
		private TaskController TC { get; set; }
		private Random Random { get; set; }

		[OneTimeSetUp]
		public void LoginToJira()
		{
			Random = new Random();
			LoginController lc = new LoginController();
			Assert.IsTrue(lc.Login("https://epm.verisk.com/jira/", "i59098", File.ReadAllText(@"C:\Users\i59098\Google Drive\pword.txt")));
			TC = new TaskController(lc);
		}

		[Test]
		public void FilterByCreatedDate()
		{
			var startTime = DateTime.Now.AddMonths(-Random.Next(5, 7));
			var endTime = DateTime.Now.AddMonths(-Random.Next(1, 4));
			var filter = new TaskFilter
			{
				CreatedDateRange = new DateTimeRange(startTime, endTime)
			};
			var createdDateIssues = TC.GetIssues(Random.Next(10, 9999), filter);
			foreach (var issue in createdDateIssues)
			{
				Assert.IsNotNull(issue.Created, $"Created date was null for issue {issue.Key.Value}");
				Assert.IsTrue(
					DateTime.Compare(issue.Created.Value, startTime) >= 0 &&
					DateTime.Compare(issue.Created.Value, endTime) <= 0,
					$"Issue Created Date: {issue.Created.Value:d} Expected date range: {startTime:d} - {endTime:d}");
			}
		}

		[Test]
		public void FilterFromDateUntilNowOnCreatedDate()
		{
			var startTime = DateTime.Now.AddMonths(-Random.Next(1, 5));
			var filter = new TaskFilter
			{
				CreatedDateRange = new DateTimeRange(startTime, null)
			};
			var createdDateIssues = TC.GetIssues(Random.Next(10, 9999), filter);
			foreach (var issue in createdDateIssues)
			{
				Assert.IsNotNull(issue.Created, $"Created date was null for issue {issue.Key.Value}");
				Assert.IsTrue(
					DateTime.Compare(issue.Created.Value, startTime) >= 0,
					$"Issue Created Date: {issue.Created.Value:d} Expected date range: {startTime:d} - Now");
			}
		}

		[Test]
		public void FilterToEndDateOnCreatedDate()
		{
			var endTime = DateTime.Now.AddMonths(-Random.Next(1, 4));
			var filter = new TaskFilter
			{
				CreatedDateRange = new DateTimeRange(null, endTime)
			};
			var createdDateIssues = TC.GetIssues(Random.Next(10, 9999), filter);
			foreach (var issue in createdDateIssues)
			{
				Assert.IsNotNull(issue.Created, $"Created date was null for issue {issue.Key.Value}");
				Assert.IsTrue(
					DateTime.Compare(issue.Created.Value, endTime) <= 0,
					$"Issue Created Date: {issue.Created.Value:d} Expected date range: Beginning of Time? - {endTime:d}");
			}
		}
	}
}
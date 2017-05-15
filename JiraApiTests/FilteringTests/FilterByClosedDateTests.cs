using JiraApi;
using JiraApi.DataClasses;
using NUnit.Framework;
using System;
using System.IO;

namespace JiraApiTests.FilteringTests
{
	internal class FilterByClosedDateTests
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
		public void FilterByClosedDate()
		{
			var startTime = DateTime.Now.AddMonths(-Random.Next(5, 7));
			var endTime = DateTime.Now.AddMonths(-Random.Next(1, 4));
			var filter = new TaskFilter
			{
				ResolutionDateRange = new DateTimeRange(startTime, endTime)
			};
			var updatedDateIssues = TC.GetIssues(Random.Next(10, 9999), filter);
			foreach (var issue in updatedDateIssues)
			{
				Assert.IsNotNull(issue.ResolutionDate, $"Created date was null for issue {issue.Key.Value}");
				Assert.IsTrue(
					DateTime.Compare(issue.ResolutionDate.Value, startTime) >= 0 &&
					DateTime.Compare(issue.ResolutionDate.Value, endTime) <= 0,
					$"Issue Created Date: {issue.ResolutionDate.Value:d} Expected date range: {startTime:d} - {endTime:d}");
			}
		}

		[Test]
		public void FilterFromDateUntilNowOnClosedDate()
		{
			var startTime = DateTime.Now.AddMonths(-Random.Next(1, 5));
			var filter = new TaskFilter
			{
				ResolutionDateRange = new DateTimeRange(startTime, null)
			};
			var updatedDateIssues = TC.GetIssues(Random.Next(10, 9999), filter);
			foreach (var issue in updatedDateIssues)
			{
				Assert.IsTrue(issue.ResolutionDate == null ||
					DateTime.Compare(issue.ResolutionDate.Value, startTime) >= 0,
					$"Issue Created Date: {issue.ResolutionDate?.ToString("d") ?? "NULL"} Expected date range: {startTime:d} - Now");
			}
		}

		[Test]
		public void FilterToEndDateOnClosedDate()
		{
			var endTime = DateTime.Now.AddMonths(-Random.Next(1, 4));
			var filter = new TaskFilter
			{
				ResolutionDateRange = new DateTimeRange(null, endTime)
			};
			var updatedDateIssues = TC.GetIssues(Random.Next(10, 9999), filter);
			foreach (var issue in updatedDateIssues)
			{
				Assert.IsNotNull(issue.ResolutionDate, $"Created date was null for issue {issue.Key.Value}");
				Assert.IsTrue(
					DateTime.Compare(issue.ResolutionDate.Value, endTime) <= 0,
					$"Issue Created Date: {issue.ResolutionDate.Value:d} Expected date range: Beginning of Time? - {endTime:d}");
			}
		}
	}
}
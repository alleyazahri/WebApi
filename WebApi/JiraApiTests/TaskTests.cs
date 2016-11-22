using JiraApi;
using NUnit.Framework;

namespace JiraApiTests
{
    internal class TaskTests
    {
        [Test]
        public void GetSpecifiedTask()
        {
            LoginController lc = new LoginController();

            Assert.IsTrue(lc.Login("https://epm.verisk.com/jira/", "i59098", ""));
            var tc = new TaskController(lc);
            var asdf = tc.GetIssue("XWESVC-955");
        }
    }
}
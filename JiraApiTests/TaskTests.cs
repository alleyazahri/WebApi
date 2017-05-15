using JiraApi;
using NUnit.Framework;
using System.IO;

namespace JiraApiTests
{
    internal class TaskTests
    {
        [Test]
        public void GetSpecifiedTask()
        {
            LoginController lc = new LoginController();

            Assert.IsTrue(lc.Login("https://epm.verisk.com/jira/", "i59098", File.ReadAllText(@"C:\Users\i59098\Google Drive\pword.txt")));
            var tc = new TaskController(lc);
            var asdf = tc.GetIssue("XWESVC-955");
            Assert.AreEqual(asdf.Key.Value, "XWESVC-955");
        }
    }
}
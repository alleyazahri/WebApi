using JiraApi;
using NUnit.Framework;

namespace JiraApiTests
{
    public class LoginTests
    {
        [Test]
        public void TestBadLogin()
        {
            var loginC = new LoginController();
            var isLoggedIn = loginC.Login("https://epm.verisk.com/jira/", "asdf", "123");
            Assert.IsFalse(isLoggedIn, "Shouldn't be logged in, but it thinks I am...");
        }
    }
}
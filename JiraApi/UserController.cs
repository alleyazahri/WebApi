namespace JiraApi
{
    public class UserController
    {
        private LoginController Lc { get; }

        public UserController(LoginController loginController)
        {
            Lc = loginController;
        }

        public string GetDisplayName(string userId)
        {
            //return Lc.JiraConnection.Users.GetUserAsync(userId).Result.DisplayName;
            return null;
        }

        //public string GetUserId(string displayName)
        //{
        //    return Lc.JiraConnection.Users.GetUserAsync(displayName).Result.Username;
        //}
    }
}
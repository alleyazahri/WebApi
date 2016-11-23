using JiraApi;

namespace JiraTasks.MainWindowBusi
{
    internal class BaseBusi
    {
        internal TaskController TaskController { get; set; }
        internal UserController UserController { get; set; }

        public BaseBusi(LoginController loginController)
        {
            TaskController = new TaskController(loginController);
            UserController = new UserController(loginController);
        }
    }
}
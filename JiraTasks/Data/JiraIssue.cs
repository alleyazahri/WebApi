namespace WpfApplication1.Data
{
    public class JiraIssue
    {
        public string DevTask { get; set; }
        public string MyTask { get; set; }
        public string Status { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }

        public object[] ToStringArray()
        {
            return new object[] { DevTask, MyTask, Status, TaskName, TaskDescription };
        }
    }
}
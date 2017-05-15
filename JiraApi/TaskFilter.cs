using JiraApi.DataClasses;
using System.Collections.Generic;

namespace JiraApi
{
    public class TaskFilter
    {
        /// <summary>
        /// Sorts on status name only.
        /// </summary>
        public List<string> Statuses { get; set; }

        /// <summary>
        /// i.e. I12345
        /// </summary>
        public List<string> AssigneeIds { get; set; }

        /// <summary>
        /// i.e. Lasname, Firstname MiddleInitial.
        /// </summary>
        //public List<string> AssigneeDisplayName { get; set; }

        /// <summary>
        /// Task description (key phrase or words in a task description)
        /// </summary>
        //public List<string> Descriptions { get; set; }

        //public List<string> Environments { get; set; }
        public DateTimeRange ResolutionDateRange { get; set; }

        public DateTimeRange UpdatedDateRange { get; set; }

        public DateTimeRange CreatedDateRange { get; set; }

        /// <summary>
        /// i.e. XWESVC (the first portion of the task number)
        /// </summary>
        public List<string> Project { get; set; }

        public string PropertiesNotNull()
        {
            var nnprops = "";
            if (Statuses != null && Statuses.Count > 0)
                nnprops += "s";
            if (AssigneeIds != null && AssigneeIds.Count > 0)
                nnprops += "a";
            if (ResolutionDateRange != null)
                nnprops += "r";
            if (UpdatedDateRange != null)
                nnprops += "u";
            if (CreatedDateRange != null)
                nnprops += "c";
            if (Project != null && Project.Count > 0)
                nnprops += "p";
            if (nnprops.Length == 5)
                nnprops = "A";
            return nnprops;
        }

        //private void Thingie()
        //{
        //    string asf = stuff.proj.upd.Environment;//.Assignee.Status;
        //}
    }
}
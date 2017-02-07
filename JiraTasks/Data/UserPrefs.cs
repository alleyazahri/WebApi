using JiraApi.DataClasses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace JiraTasks.Data
{
    public class UserPrefs
    {
        private string Path { get; }
        private string Filename { get; }

        public List<ProjectMenuItem> Projects { get; set; }
        public List<string> IrrelevantTasks { get; set; }
        public Dictionary<string, string> LinkedTaskList { get; set; }
        public ColorKey ColorLegend { get; set; }
        public Dictionary<string, string> Notes { get; set; }
        public SortOrder TaskSortOrder { get; set; }
        public int SortedColumn { get; set; }
        public DateTimeRange CreatedDateRange { get; set; }
        public DateTimeRange ModifiedDateRange { get; set; }
        public DateTimeRange ClosedDateRange { get; set; }

        public UserPrefs(string path, string filename)
        {
            Path = path;
            Filename = filename;
            LinkedTaskList = new Dictionary<string, string>();
            Projects = new List<ProjectMenuItem>();
            IrrelevantTasks = new List<string>();
            ColorLegend = new ColorKey();
            Notes = new Dictionary<string, string>();
            TaskSortOrder = SortOrder.None;
            SortedColumn = -1;
            CreatedDateRange = new DateTimeRange();
            ModifiedDateRange = new DateTimeRange();
            ClosedDateRange = new DateTimeRange();
        }

        public UserPrefs DeepCopy()
        {
            return new UserPrefs(Path, Filename)
            {
                ColorLegend = ColorLegend.DeepCopy(),
                IrrelevantTasks = new List<string>(IrrelevantTasks),
                LinkedTaskList = new Dictionary<string, string>(LinkedTaskList),
                Notes = new Dictionary<string, string>(Notes),
                Projects = new List<ProjectMenuItem>(Projects.Select(p => new ProjectMenuItem() { ProjectName = p.ProjectName, ProjectIsSelected = p.ProjectIsSelected })),
                SortedColumn = SortedColumn,
                TaskSortOrder = TaskSortOrder,
                CreatedDateRange = CreatedDateRange.DeepCopy(),
                ModifiedDateRange = ModifiedDateRange.DeepCopy(),
                ClosedDateRange = ClosedDateRange.DeepCopy()
            };
        }

        public bool Load()
        {
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
                File.Create($@"{Path}/{Filename}").Dispose();
                return false;
            }
            try
            {
                var fileText = File.ReadAllText($@"{Path}/{Filename}");
                var deserialized = JsonConvert.DeserializeObject<UserPrefs>(fileText);
                LinkedTaskList = deserialized.LinkedTaskList;
                Projects = deserialized.Projects;
                IrrelevantTasks = deserialized.IrrelevantTasks;
                ColorLegend = deserialized.ColorLegend;
                Notes = deserialized.Notes;
                TaskSortOrder = deserialized.TaskSortOrder;
                SortedColumn = deserialized.SortedColumn;
                CreatedDateRange = deserialized.CreatedDateRange;
                ModifiedDateRange = deserialized.ModifiedDateRange;
                ClosedDateRange = deserialized.ClosedDateRange;
                return true;
            }
            catch (Exception ef)
            {
                MessageBox.Show(ef.Message);
                LinkedTaskList = new Dictionary<string, string>();
                Projects = new List<ProjectMenuItem>();
                IrrelevantTasks = new List<string>();
                ColorLegend = new ColorKey();
                Notes = new Dictionary<string, string>();
                TaskSortOrder = SortOrder.None;
                SortedColumn = -1;
                CreatedDateRange = new DateTimeRange();
                ModifiedDateRange = new DateTimeRange();
                ClosedDateRange = new DateTimeRange();
                Save();
                return false;
            }
        }

        public void Save()
        {
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
            string serialized = JsonConvert.SerializeObject(this);
            File.WriteAllText($@"{Path}/{Filename}", serialized);
        }

        public void SaveBackup()
        {
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
            string serialized = JsonConvert.SerializeObject(this);
            File.WriteAllText($@"{Path}/BUUPSF{DateTime.Now:YYMMDD}.settings", serialized);
        }
    }

    public class ProjectMenuItem
    {
        public string ProjectName { get; set; }
        public bool ProjectIsSelected { get; set; }

        public override bool Equals(object obj)
        {
            var otherObj = obj as ProjectMenuItem;
            return otherObj != null && otherObj.ProjectName == ProjectName;
        }

        protected bool Equals(ProjectMenuItem other)
        {
            return string.Equals(ProjectName, other.ProjectName);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((ProjectName?.GetHashCode() ?? 0) * 397) ^ ProjectIsSelected.GetHashCode();
            }
        }
    }
}
using JiraTasks.Data;
using JiraTasks.MainWindowBusi;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace JiraTasks
{
    internal partial class AddRemoveProjects : Form
    {
        public UserPrefs UserPrefs { get; set; }
        private List<string> ProjectNames { get; set; }
        private TaskBusi TaskBusi { get; set; }
        private int X { get; set; }
        private int Y { get; set; }
        private int InitWidth => Width - 40;
        private string CellValue { get; set; }

        public AddRemoveProjects(UserPrefs userPrefs, TaskBusi taskBusi, int left, int top)
        {
            X = left;
            Y = top;
            UserPrefs = userPrefs;
            ProjectNames = new List<string>();
            foreach (var project in UserPrefs.Projects)
            {
                ProjectNames.Add(project.ProjectName);
            }
            TaskBusi = taskBusi;
            InitializeComponent();
            LoadDataGridView();
        }

        #region Events

        private void bSaveChanges_Click(object sender, System.EventArgs e)
        {
            //Get values that are selected
            var newList = new List<ProjectMenuItem>();
            //Compare them to the changes
            foreach (var projectName in ProjectNames)
            {
                var proj = UserPrefs.Projects.FirstOrDefault(p => p.ProjectName == projectName);
                newList.Add(proj ?? new ProjectMenuItem() { ProjectName = projectName, ProjectIsSelected = false });
            }
            UserPrefs.Projects = newList;
            DialogResult = DialogResult.OK;
        }

        private void bDiscardChanges_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void dgvAddRemoveProjects_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            EditProjectInList(e.RowIndex, e.ColumnIndex);
        }

        private void AddRemoveProjects_Load(object sender, System.EventArgs e)
        {
            this.Location = new Point(X + 10, Y + 10);
        }

        private void dgvAddRemoveProjects_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                AddDeleteGivenProject(e.RowIndex, e.ColumnIndex);
            }
        }

        private void dgvAddRemoveProjects_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            CellValue = dgvAddRemoveProjects.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
        }

        #endregion Events

        private void LoadDataGridView()
        {
            dgvAddRemoveProjects.Columns.Add("project", "Project");
            dgvAddRemoveProjects.Columns[0].Width = (int)(InitWidth * .7);
            DataGridViewButtonColumn deleteProjects = new DataGridViewButtonColumn
            {
                Name = "addDeleteColumn",
                HeaderText = "Add/Delete"
            };
            dgvAddRemoveProjects.Columns.Add(deleteProjects);
            dgvAddRemoveProjects.Columns[1].Width = (int)(InitWidth * .3);
            dgvAddRemoveProjects.RowHeadersVisible = false;
            dgvAddRemoveProjects.AllowUserToAddRows = false;

            dgvAddRemoveProjects.AllowUserToOrderColumns = true;
            dgvAddRemoveProjects.Show();
            foreach (var project in UserPrefs.Projects)
            {
                dgvAddRemoveProjects.Rows.Add(project.ProjectName, "Delete");
            }
            dgvAddRemoveProjects.Rows.Add("", "Add");
        }

        private void AddDeleteGivenProject(int row, int column)
        {
            var addDelete = dgvAddRemoveProjects.Rows[row].Cells[column].Value.ToString();
            if (addDelete == "Delete")
            {
                DeleteProjectFromList(row, column - 1);
            }
            else if (addDelete == "Add")
            {
                AddProjectToList(row, column - 1);
            }
            else
            {
                MessageBox.Show("An error has occurred. If this continues, please contact someone O.o", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteProjectFromList(int row, int column)
        {
            var projectName = dgvAddRemoveProjects.Rows[row].Cells[column].Value?.ToString().ToUpper();
            ProjectNames.Remove(projectName);
            dgvAddRemoveProjects.Rows.RemoveAt(row);
        }

        private void AddProjectToList(int row, int column)
        {
            var projectName = dgvAddRemoveProjects.Rows[row].Cells[column].Value?.ToString().ToUpper();
            if (projectName != null && ProjectIsUsableAndAlertUser(projectName))
            {
                dgvAddRemoveProjects.Rows[row].Cells[column].Value = projectName;
                dgvAddRemoveProjects.Rows[row].Cells[column + 1].Value = "Delete";
                ProjectNames.Add(projectName);
                dgvAddRemoveProjects.Rows.Add("", "Add");
            }
            else
            {
                dgvAddRemoveProjects.Rows[row].Cells[column].Value = "";
                dgvAddRemoveProjects.Rows[row].Cells[column + 1].Value = "Add";
            }
        }

        private void EditProjectInList(int row, int column)
        {
            var projectName = dgvAddRemoveProjects.Rows[row].Cells[column].Value?.ToString().ToUpper();
            if (projectName != null)
            {
                dgvAddRemoveProjects.Rows[row].Cells[column].Value = projectName;
                if (projectName == CellValue)
                    return;
                var val = dgvAddRemoveProjects.Rows[row].Cells[column + 1].Value.ToString();

                if (val != "Add" && ProjectIsUsableAndAlertUser(projectName))
                {
                    ProjectNames.Add(projectName);
                    ProjectNames.Remove(CellValue);
                }
                else
                {
                    dgvAddRemoveProjects.Rows[row].Cells[column].Value = CellValue;
                }
            }
            CellValue = null;
        }

        private bool ProjectIsUsableAndAlertUser(string projectName)
        {
            if (ProjectNames.Contains(projectName))
            {
                MessageBox.Show(this, $"Project {projectName} already exists", "", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            var project = TaskBusi.VerifyProjectExists(projectName);
            if (!project)
            {
                MessageBox.Show(this, $"Project {projectName} is not a valid project", "", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
    }
}
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
        private int WorkingWidth => Width - 40 - dgvAddRemoveProjects.Columns[0].Width;

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

        private void LoadDataGridView()
        {
            dgvAddRemoveProjects.Columns.Add("project", "Project");
            dgvAddRemoveProjects.Columns[0].Width = (int)(InitWidth * .7);
            DataGridViewButtonColumn deleteProjects = new DataGridViewButtonColumn
            {
                Name = "delete_column",
                Text = "Add/Delete"
            };
            dgvAddRemoveProjects.Columns.Add(deleteProjects);
            dgvAddRemoveProjects.Columns[1].Width = (int)(InitWidth * .3);
            dgvAddRemoveProjects.RowHeadersVisible = false;
            dgvAddRemoveProjects.AllowUserToAddRows = false;

            //    DataGridViewButtonColumn uninstallButtonColumn = new DataGridViewButtonColumn();
            //uninstallButtonColumn.Name = "uninstall_column";
            //uninstallButtonColumn.Text = "Uninstall";
            //int columnIndex = 2;

            //            if (dataGridViewSoftware.Columns["uninstall_column"] == null)
            //            {
            //                dataGridViewSoftware.Columns.Insert(columnIndex, uninstallButtonColumn);
            //            }
            //            Of course you will have to handle the CellClick event of the grid to do anything with the button.

            //            Add this somewhere in your DataGridView Initialization code

            //               dataGridViewSoftware.CellClick += dataGridViewSoftware_CellClick;
            //        Then create the handler:

            //private void dataGridViewSoftware_CellClick(object sender, DataGridViewCellEventArgs e)
            //        {
            //            if (e.ColumnIndex == dataGridViewSoftware.Columns["uninstall_button"].Index)
            //            {
            //                //Do Something with your button.
            //            }
            //        }

            dgvAddRemoveProjects.AllowUserToOrderColumns = true;
            dgvAddRemoveProjects.Show();
            foreach (var project in UserPrefs.Projects)
            {
                dgvAddRemoveProjects.Rows.Add(project.ProjectName, "Delete");
            }
            dgvAddRemoveProjects.Rows.Add("", "Add");
        }

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
            //Maybe make sure the project is a real project, otherwise remove it. Plus make it all caps
            var projectName = dgvAddRemoveProjects.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString().ToUpper();
            if (projectName != null)
            {
                dgvAddRemoveProjects.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = projectName;
                dgvAddRemoveProjects.Rows[e.RowIndex].Cells[1].Value = "Delete";
                if (ProjectNames.Contains(projectName))
                {
                    MessageBox.Show(this, $"Project {projectName} already exists", "", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    dgvAddRemoveProjects.Rows.Remove(dgvAddRemoveProjects.Rows[e.RowIndex]);
                    return;
                }
                var project = TaskBusi.VerifyProjectExists(projectName);
                if (!project)
                {
                    MessageBox.Show(this, $"Project {projectName} is not a valid project", "", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    dgvAddRemoveProjects.Rows.Remove(dgvAddRemoveProjects.Rows[e.RowIndex]);
                    return;
                }
                ProjectNames.Add(projectName);
            }
        }

        private void AddRemoveProjects_Load(object sender, System.EventArgs e)
        {
            this.Location = new Point(X + 10, Y + 10);
        }

        private void dgvAddRemoveProjects_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show($"{e.RowIndex},{e.ColumnIndex}");
            if (e.ColumnIndex == 1)
            {
                DeleteGivenProject(e.RowIndex, e.ColumnIndex);
            }
        }

        private void DeleteGivenProject(int row, int column)
        {
            MessageBox.Show(dgvAddRemoveProjects.Rows[row].Cells[column].Value.ToString());
        }
    }
}
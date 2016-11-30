using JiraTasks.Data;
using System.Collections.Generic;
using System.Windows.Forms;

namespace JiraTasks
{
    public partial class AddRemoveProjects : Form
    {
        public List<ProjectMenuItem> Projects { get; set; }

        public AddRemoveProjects(List<ProjectMenuItem> projects)
        {
            Projects = projects;
            InitializeComponent();
            LoadDataGridView();
        }

        private void LoadDataGridView()
        {
            dgvAddRemoveProjects.Columns.Add("project", "Project");
            dgvAddRemoveProjects.Columns[0].Width = Width - 100;

            dgvAddRemoveProjects.AllowUserToOrderColumns = true;
            dgvAddRemoveProjects.Show();
            foreach (var project in Projects)
            {
                dgvAddRemoveProjects.Rows.Add(project.ProjectName);
            }
        }

        private void bSaveChanges_Click(object sender, System.EventArgs e)
        {
        }

        private void bDiscardChanges_Click(object sender, System.EventArgs e)
        {
        }

        private void dgvAddRemoveProjects_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show(sender.GetType().ToString());
        }
    }
}
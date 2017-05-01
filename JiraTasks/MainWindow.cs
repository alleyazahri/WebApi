using Atlassian.Jira;
using JiraApi;
using JiraTasks.Data;
using JiraTasks.MainWindowBusi;
using JiraTasks.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JiraTasks
{
	//TODO: Add error to tell user they cannot link tasks or add notes while the grid view is still loading
	public partial class MainWindow : Form
	{
		#region Properties

		private Login LoginWindow { get; set; }
		private FlattenedTasks Tasks { get; set; }
		private UserPrefs UserPreferences { get; }
		private LoadedTaskLists LoadedTaskLists { get; }
		private TaskBusi TaskBusi { get; }
		private AddRemoveProjects AddRemoveProjectsWindow { get; set; }
		private RowColumn CurrentRowColumn { get; set; }
		private bool IsLoading { get; set; }

		//Column Variables
		private int DevTaskColumnIndex { get; set; }

		private int MyTaskColumnIndex { get; set; }
		private int StatusColumnIndex { get; set; }
		private int SummaryColumnIndex { get; set; }
		private int DescriptionColumnIndex { get; set; }
		private int NotesColumnIndex { get; set; }

		private int WorkingWidth => Width - dgJiraTaskList.Columns[DevTaskColumnIndex].Width - dgJiraTaskList.Columns[MyTaskColumnIndex].Width - dgJiraTaskList.Columns[StatusColumnIndex].Width - 58;

		#endregion Properties

		/// <summary>
		/// Initializes the TaskList window, login window, user preferences, loaded task list,
		/// project menu, and task busi.
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();
			LoginWindow = new Login();
			UserPreferences = new UserPrefs(LoginWindow.SettingsPath, "UPJTFO23.settings");
			UserPreferences.Load();
			LoadedTaskLists = new LoadedTaskLists(Path.Combine(LoginWindow.SettingsPath, "Tasks"), "JTL0384.jgtlf");
			LoadedTaskLists.Load();
			LoadProjectMenu();
			TaskBusi = new TaskBusi(LoginWindow.LogCont);
			Tasks = new FlattenedTasks(UserPreferences, TaskBusi.TaskController);
			if (LoginWindow.loginSettings.SavePassword != CheckState.Checked)
				LoginWindow.ShowDialog();
		}

		#region Events

		//Main Menu Item Events
		private void colorKeyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ColorKeyWindow asdf = new ColorKeyWindow(UserPreferences);
			asdf.ShowDialog();
		}

		private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Logout();
		}

		private void loginToolStripMenuItem_Click(object sender, EventArgs e)
		{
			pLoginToViewTasks.Visible = false;
			LoginWindow.ShowDialog();
			if (LoginWindow.LogCont != null && LoginWindow.LogCont.IsLoggedIn())
				LoadDataGridView();
			else
				pLoginToViewTasks.Visible = true;
		}

		private void addProjectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddAndRemoveProjects();
		}

		private void linkTaskToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CreateLinkWindowCreation();
		}

		//Sub Menu Item Events
		private void projectSubMenuItem_Click(object sender, EventArgs e)
		{
			var toolStripItem = sender as ToolStripMenuItem;
			FilterProjects(toolStripItem);
		}

		//Main Window Events
		private void MainWindow_Shown(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			//TODO: Add a 'Loading' Label :)
			if (LoginWindow.LogCont != null && LoginWindow.LogCont.IsLoggedIn())
				LoadDataGridView(loadFromSavedSettings: true);
			else
				pLoginToViewTasks.Visible = true;
			Cursor.Current = Cursors.Default;
		}

		private void MainWindow_ResizeEnd(object sender, EventArgs e)
		{
			AutoAdjustColumnWidths();
		}

		//Data Grid View Events
		private void dgJiraTaskList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.RowIndex >= 0 && e.ColumnIndex == NotesColumnIndex)
				EditNotes(e.RowIndex, e.ColumnIndex);
			else if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
				DataGridViewCellDoubleClicked(e.RowIndex, e.ColumnIndex, dgJiraTaskList);
		}

		private void EditNotes(int row, int column)
		{
			dgJiraTaskList.CurrentCell = dgJiraTaskList.Rows[row].Cells[column];
			dgJiraTaskList.BeginEdit(true);
		}

		private void dgJiraTaskList_SelectionChanged(object sender, EventArgs e)
		{
			dgJiraTaskList.ClearSelection();
		}

		private void dgJiraTaskList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == NotesColumnIndex && IsLoading)
			{
				MessageBox.Show("Cannot add notes while data grid view is loading", "Load Error", MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}
			else if (e.ColumnIndex == NotesColumnIndex)
			{
				SaveNotesToTask(
					dgJiraTaskList.Rows[e.RowIndex].Cells[DevTaskColumnIndex].Value.ToString(),
					dgJiraTaskList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString());
			}
		}

		private void dgJiraTaskList_Sorted(object sender, EventArgs e)
		{
			SaveSortPreferences();
		}

		private void dgJiraTaskList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			CurrentRowColumn = new RowColumn { Column = e.ColumnIndex, Row = e.RowIndex };
			if (e.Button == MouseButtons.Right && CurrentRowColumn.Row > -1 && CurrentRowColumn.Column > -1)
			{
				ContextMenuStrip mnu = new ContextMenuStrip();
				ToolStripMenuItem mnuLogWork = new ToolStripMenuItem("Log Work");
				ToolStripMenuItem mnuAddLink = new ToolStripMenuItem("Add Link");
				ToolStripMenuItem mnuRemoveLink = new ToolStripMenuItem("Remove Link");
				ToolStripMenuItem mnuMarkIrrelevant = new ToolStripMenuItem("Mark Irrelevant");
				//Assign event handlers
				mnuRemoveLink.Click += dgvtaskContextMenu_RemoveLink;
				mnuAddLink.Click += dgvtaskContextMenu_AddLink;
				mnuLogWork.Click += dvgTaskContextMenu_LogWork;
				mnuMarkIrrelevant.Click += dvgTaskContextMenu_MarkIrrelevant;

				//Add to main context menu
				mnu.Items.AddRange(new ToolStripItem[] { mnuMarkIrrelevant, mnuLogWork, mnuAddLink, mnuRemoveLink });
				//mnu.Show(dgJiraTaskList, new Point(e.ColumnIndex, e.RowIndex));
				var r = dgJiraTaskList.GetCellDisplayRectangle(CurrentRowColumn.Column, CurrentRowColumn.Row, false);
				mnu.Show(new Point(r.X + r.Width, r.Y));
			}
		}

		//Data Grid View Sub Menu Item Events
		private void dgvtaskContextMenu_RemoveLink(object sender, EventArgs e)
		{
			RemoveLinkBetweenTasks(CurrentRowColumn.Row);
			MessageBox.Show("Not Yet Implemented!");
			dgJiraTaskList.ContextMenuStrip = null;
			//TODO: Cast e and see if that gives me what I want
			//TaskBusi.RemoveLink("MainTask", "LinkedTask");
		}

		private void dgvtaskContextMenu_AddLink(object sender, EventArgs e)
		{
			CreateLinkWindowCreation(dgJiraTaskList.Rows[CurrentRowColumn.Row].Cells[DevTaskColumnIndex].Value.ToString());
		}

		private void dvgTaskContextMenu_LogWork(object sender, EventArgs e)
		{
			MessageBox.Show("Not Yet Implemented!");
			dgJiraTaskList.ContextMenuStrip = null;
		}

		private void dvgTaskContextMenu_MarkIrrelevant(object sender, EventArgs e)
		{
			if (IsLoading)
			{
				MessageBox.Show("Cannot change a task status while data grid view is loading", "Load Error", MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				return;
			}
			var devTaskCell = dgJiraTaskList.Rows[CurrentRowColumn.Row].Cells[DevTaskColumnIndex];
			var currentCell = dgJiraTaskList.Rows[CurrentRowColumn.Row].Cells[MyTaskColumnIndex];
			if (currentCell.Style.BackColor == UserPreferences.ColorLegend.IrrelevantTasks)
			{
				//Change cell color back by getting the task, and it's status, then coloring
				UserPreferences.IrrelevantTasks.RemoveAll(x => x == currentCell.Value.ToString());
			}
			else
			{
				for (int i = 0; i < dgJiraTaskList.Rows[CurrentRowColumn.Row].Cells.Count; i++)
				{
					ColorDataGridViewCell(CurrentRowColumn.Row, i, UserPreferences.ColorLegend.IrrelevantTasks);
				}
				UserPreferences.IrrelevantTasks.Add(devTaskCell.Value.ToString());
				dgJiraTaskList.Rows[CurrentRowColumn.Row].Cells[StatusColumnIndex].Value =
					$"7 - {dgJiraTaskList.Rows[CurrentRowColumn.Row].Cells[StatusColumnIndex].Value}";
			}
			UserPreferences.Save();
			dgJiraTaskList.ContextMenuStrip = null;
		}

		//Miscellaneous Events
		private void bRefresh_Click(object sender, EventArgs e)
		{
			LoadDataGridView(true);
		}

		#endregion Events

		#region Data Grid View Logic

		private void SaveSortPreferences()
		{
			UserPreferences.TaskSortOrder = dgJiraTaskList.SortOrder;
			UserPreferences.SortedColumn = dgJiraTaskList.SortedColumn.DisplayIndex;
			UserPreferences.Save();
		}

		private async void LoadDataGridView(bool clearOriginalGrid = false, bool loadFromSavedSettings = false)
		{
			loadingPanel.Visible = true;
			bRefresh.Visible = false;
			IsLoading = true;
			if (!clearOriginalGrid)
				InitializeGridHeadersAndOptions();

			//'Quickly' load tasks saved from previous session
			if (loadFromSavedSettings && LoadedTaskLists.JiraIssueTaskLists != null && LoadedTaskLists.JiraIssueTaskLists.ContainsKey("LatestLoad"))
			{
				Tasks.Tasks = LoadedTaskLists.JiraIssueTaskLists["LatestLoad"];
				LoadTasksAndSortOrderToGridView();
			}

			//Now reload all tasks based on the current filter options
			var filter = GetCurrentFilterOptions();
			var getJiraTasks = Task.Factory.StartNew(() => TaskBusi.PopulateJiraTasks(filter));
			await getJiraTasks.ConfigureAwait(true);
			await getJiraTasks;
			var flattenJiraTasks = Task.Factory.StartNew(() => Tasks.FlattenTasks(getJiraTasks.Result));
			await flattenJiraTasks.ConfigureAwait(true);
			await flattenJiraTasks;

			dgJiraTaskList.Rows.Clear();
			dgJiraTaskList.Refresh();
			LoadTasksAndSortOrderToGridView();

			LoadedTaskLists.ReplaceTasks("LatestLoad", Tasks.Tasks);
			LoadRefreshButton();
			loadingPanel.Visible = false;
			IsLoading = false;
		}

		private void LoadTasksAndSortOrderToGridView()
		{
			for (int i = 0; i < Tasks.Tasks.Count; i++)
			{
				dgJiraTaskList.Rows.Add(Tasks.Tasks[i].ToObjectArray());
				ColorDataGridViewCell(i, DevTaskColumnIndex, Tasks.Tasks[i].DevTaskColor);
				ColorDataGridViewCell(i, MyTaskColumnIndex, Tasks.Tasks[i].LinkedTaskColor);
				if (Tasks.Tasks[i].IsIrreleventTask)
					ColorRowAsIrrelevent(i);
			}
			if (UserPreferences.TaskSortOrder != SortOrder.None && UserPreferences.SortedColumn != -1)
				dgJiraTaskList.Sort(
					dgJiraTaskList.Columns[UserPreferences.SortedColumn],
					UserPreferences.TaskSortOrder == SortOrder.Ascending
						? ListSortDirection.Ascending
						: ListSortDirection.Descending);
			dgJiraTaskList.Refresh();
		}

		private void InitializeGridHeadersAndOptions()
		{
			dgJiraTaskList.Columns.Add("DevTask", Resources.DevTaskHeader);
			dgJiraTaskList.Columns.Add("MyTask", Resources.MyTaskHeader);
			dgJiraTaskList.Columns.Add(Resources.StatusHeader, Resources.StatusHeader);
			dgJiraTaskList.Columns.Add("TaskName", Resources.SummaryHeader);
			dgJiraTaskList.Columns.Add("TaskDescription", Resources.DescriptionHeader);
			dgJiraTaskList.Columns.Add(Resources.NotesHeader, Resources.NotesHeader);
			dgJiraTaskList.RowHeadersVisible = false;

			LoadColumnIndexProperties();

			dgJiraTaskList.Columns[DevTaskColumnIndex].Width = 116;
			dgJiraTaskList.Columns[MyTaskColumnIndex].Width = 116;
			dgJiraTaskList.Columns[StatusColumnIndex].Width = 109;

			//Set all columns to ReadOnly except the Notes column
			dgJiraTaskList.Columns[DevTaskColumnIndex].ReadOnly = true;
			dgJiraTaskList.Columns[MyTaskColumnIndex].ReadOnly = true;
			dgJiraTaskList.Columns[StatusColumnIndex].ReadOnly = true;
			dgJiraTaskList.Columns[SummaryColumnIndex].ReadOnly = true;
			dgJiraTaskList.Columns[DescriptionColumnIndex].ReadOnly = true;

			dgJiraTaskList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
			dgJiraTaskList.Columns[SummaryColumnIndex].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
			dgJiraTaskList.Columns[DescriptionColumnIndex].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
			dgJiraTaskList.Columns[NotesColumnIndex].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
			AutoAdjustColumnWidths();

			dgJiraTaskList.AllowUserToAddRows = false;
			dgJiraTaskList.AllowUserToOrderColumns = true;
			dgJiraTaskList.AllowUserToDeleteRows = false;

			dgJiraTaskList.Show();
		}

		private void ColorRowAsIrrelevent(int row)
		{
			dgJiraTaskList.Rows[row].Cells[StatusColumnIndex].Value = "x " + dgJiraTaskList.Rows[row].Cells[StatusColumnIndex].Value;
			dgJiraTaskList.Rows[row].Cells[MyTaskColumnIndex].Style.BackColor = UserPreferences.ColorLegend.IrrelevantTasks;
			dgJiraTaskList.Rows[row].Cells[StatusColumnIndex].Style.BackColor = UserPreferences.ColorLegend.IrrelevantTasks;
			dgJiraTaskList.Rows[row].Cells[SummaryColumnIndex].Style.BackColor = UserPreferences.ColorLegend.IrrelevantTasks;
			dgJiraTaskList.Rows[row].Cells[DescriptionColumnIndex].Style.BackColor = UserPreferences.ColorLegend.IrrelevantTasks;
			dgJiraTaskList.Rows[row].Cells[NotesColumnIndex].Style.BackColor = UserPreferences.ColorLegend.IrrelevantTasks;
		}

		private TaskFilter GetCurrentFilterOptions()
		{
			var filter = new TaskFilter()
			{
				Project = UserPreferences.Projects.Where(x => x.ProjectIsSelected).Select(x => x.ProjectName).ToList(),
				UpdatedDateRange = UserPreferences.ModifiedDateRange,
				ResolutionDateRange = UserPreferences.ClosedDateRange,
				CreatedDateRange = UserPreferences.CreatedDateRange
			};
			return filter;
		}

		private void DataGridViewCellDoubleClicked(int row, int column, DataGridView dataGrid)
		{
			if (column == MyTaskColumnIndex && (string)dataGrid.Rows[row].Cells[column].Value != "")
				System.Diagnostics.Process.Start(
					$"https://epm.verisk.com/jira/browse/{dataGrid.Rows[row].Cells[column].Value}");
			else if (column == MyTaskColumnIndex)
				CreateLinkWindowCreation(dataGrid.Rows[row].Cells[DevTaskColumnIndex].Value.ToString());
			else
				System.Diagnostics.Process.Start(
					$"https://epm.verisk.com/jira/browse/{dataGrid.Rows[row].Cells[DevTaskColumnIndex].Value}");
			Console.WriteLine(dataGrid?.Rows[row].Cells[column].Value);
		}

		/// <summary>
		/// Colors the datagrid view cell based on the given color
		/// </summary>
		/// <param name="row"></param>
		/// <param name="column"></param>
		/// <param name="color"></param>
		internal void ColorDataGridViewCell(int row, int column, Color? color)
		{
			if (color == null)
				return;
			dgJiraTaskList.Rows[row].Cells[column].Style.BackColor = color.Value;
		}

		internal bool CreateLinkBetweenTasks(string mainTask, string linkedTask)
		{
			//TODO: After adding above functionality, make sure to return the unliked task to the task list as it's own task
			//TODO: Add functionality to remember which tasks are linked for a specific user.
			//TODO: Add functionality to tell user when a linked task does not exist.
			Console.WriteLine($"{mainTask} {linkedTask}");
			if (string.IsNullOrEmpty(mainTask) || string.IsNullOrEmpty(linkedTask))
				return false;
			for (int i = 0; i < dgJiraTaskList.RowCount; i++)
			{
				if (dgJiraTaskList.Rows[i].Cells[DevTaskColumnIndex].Value.ToString() == mainTask.ToUpper())// && (dgJiraTaskList.Rows[i].Cells[MyTaskColumnIndex] == null || dgJiraTaskList.Rows[i].Cells[MyTaskColumnIndex].Value == ""))
				{
					if (UserPreferences.LinkedTaskList.ContainsKey(mainTask.ToUpper()))
					{
						//TODO: Check if a task is already linked, then ask the person if they want to unlink that in favor of the new task
					}

					dgJiraTaskList.Rows[i].Cells[MyTaskColumnIndex].Value = linkedTask.Contains("~C") ? "" : linkedTask;
					var updated = Tasks.LinkNewTask(mainTask, linkedTask);
					ColorDataGridViewCell(i, MyTaskColumnIndex, updated.LinkedTaskColor);
					LoadedTaskLists.ReplaceTasks("LatestLoad", Tasks.Tasks);
					UserPreferences.LinkedTaskList[mainTask.ToUpper()] = linkedTask;
				}
				if (dgJiraTaskList.Rows[i].Cells[DevTaskColumnIndex].Value.ToString() == linkedTask.ToUpper())
				{
					dgJiraTaskList.Rows.RemoveAt(i);
				}
			}
			UserPreferences.Save();
			return true;
		}

		private void RemoveLinkBetweenTasks(int row)
		{
			var mainTask = dgJiraTaskList.Rows[row].Cells[DevTaskColumnIndex].Value.ToString();
			var linkedTask = dgJiraTaskList.Rows[row].Cells[MyTaskColumnIndex].Value.ToString();
			//If linked task meets filtered criteria, we need to add it back to the dgv as it's own task
			if (TaskBusi.TaskMatchesFilter(linkedTask))
			{
				Tasks.AddTask(new Issue(new Jira(new ServiceLocator()), ""));
			}
			// Otherwise, just remove it from the main task
			// We'll need to also remove the link in the userprefs
			dgJiraTaskList.Rows[row].Cells[MyTaskColumnIndex].Value = "";
			UserPreferences.LinkedTaskList.Remove(mainTask);
			UserPreferences.Save();
		}

		private void AutoAdjustColumnWidths()
		{
			dgJiraTaskList.Columns[SummaryColumnIndex].Width = (int)(WorkingWidth * .2);
			dgJiraTaskList.Columns[DescriptionColumnIndex].Width = (int)(WorkingWidth * .45);
			dgJiraTaskList.Columns[NotesColumnIndex].Width = (int)(WorkingWidth * .35);
		}

		#endregion Data Grid View Logic

		#region Login/Logout Logic

		private void Logout()
		{
			LoadedTaskLists.JiraIssueTaskLists = null;
			LoadedTaskLists.Save();
			dgJiraTaskList.Rows.Clear();
			LoginWindow.LogCont = new LoginController();
			LoginWindow.loginSettings.ClearUser();
			LoginWindow.loginSettings.Save(LoginWindow.SettingsPath, LoginWindow.SettingsFile);
			pLoginToViewTasks.Visible = true;
		}

		#endregion Login/Logout Logic

		#region Create Link Window Logic

		private void CreateLinkWindowCreation(string task = null)
		{
			if (IsLoading)
			{
				MessageBox.Show("Cannot link tasks while data grid view is loading", "Load Error", MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				return;
			}
			var linkTasksWindow = task != null ? new LinkTasks(task) : new LinkTasks();
			linkTasksWindow.ShowDialog();

			CreateLinkBetweenTasks(linkTasksWindow.MainTask, linkTasksWindow.LinkedTask);
		}

		#endregion Create Link Window Logic

		#region Add/Remove Project Window Logic

		private void AddAndRemoveProjects()
		{
			AddRemoveProjectsWindow = new AddRemoveProjects(UserPreferences, TaskBusi, this.Left, this.Top);
			if (AddRemoveProjectsWindow.ShowDialog() == DialogResult.OK)
			{
				AddProjectMenuItems();
			}
		}

		private void AddProjectMenuItems()
		{
			//Add the user prefs
			UserPreferences.Save();

			//Determine which projects need to be added and which need to be removed
			var prefs = UserPreferences.DeepCopy();
			var projectsToAdd = prefs.Projects;
			var projectsToRemove = new List<ToolStripMenuItem>();
			foreach (ToolStripMenuItem dropDownItem in projectsToolStripMenuItem.DropDownItems)
			{
				var proj = projectsToAdd.FirstOrDefault(x => x.ProjectName == dropDownItem.ToString());
				if (proj != null)
					projectsToAdd.Remove(proj);
				else if (dropDownItem.ToString() != "Add/Remove Project")
					projectsToRemove.Add(dropDownItem);
			}

			//Add any new projects to the menu
			foreach (var project in projectsToAdd)
			{
				var projectMenuItem = new ToolStripMenuItem(project.ProjectName);
				projectMenuItem.Click += projectSubMenuItem_Click;
				projectMenuItem.Checked = project.ProjectIsSelected;
				projectsToolStripMenuItem.DropDownItems.Add(projectMenuItem);
			}

			//Remove any old projects from the menu
			foreach (var project in projectsToRemove)
			{
				projectsToolStripMenuItem.DropDownItems.Remove(project);
			}
		}

		private void LoadProjectMenu()
		{
			//Add any new projects to the menu
			foreach (var project in UserPreferences.Projects)
			{
				var projectMenuItem = new ToolStripMenuItem(project.ProjectName);
				projectMenuItem.Click += projectSubMenuItem_Click;
				projectMenuItem.Checked = project.ProjectIsSelected;
				projectsToolStripMenuItem.DropDownItems.Add(projectMenuItem);
			}
		}

		#endregion Add/Remove Project Window Logic

		private void LoadRefreshButton()
		{
			bRefresh.Visible = true;
			bRefresh.BackgroundImageLayout = ImageLayout.Stretch;
		}

		private void SaveNotesToTask(string task, string value)
		{
			if (string.IsNullOrEmpty(value))
				UserPreferences.Notes.Remove(task);
			else
				UserPreferences.Notes[task] = value;
			UserPreferences.Save();
		}

		private void FilterProjects(ToolStripMenuItem toolStripItem)
		{
			toolStripItem.Checked = !toolStripItem.Checked;
			var project = UserPreferences.Projects.FirstOrDefault(x => x.ProjectName == toolStripItem.Text);
			project.ProjectIsSelected = toolStripItem.Checked;
			LoadDataGridView(true);
		}

		private void LoadColumnIndexProperties()
		{
			DevTaskColumnIndex = dgJiraTaskList.Columns.FirstOrDefault(x => x.HeaderText == Resources.DevTaskHeader).Index;
			MyTaskColumnIndex = dgJiraTaskList.Columns.FirstOrDefault(x => x.HeaderText == Resources.MyTaskHeader).Index;
			StatusColumnIndex = dgJiraTaskList.Columns.FirstOrDefault(x => x.HeaderText == Resources.StatusHeader).Index;
			SummaryColumnIndex = dgJiraTaskList.Columns.FirstOrDefault(x => x.HeaderText == Resources.SummaryHeader).Index;
			DescriptionColumnIndex = dgJiraTaskList.Columns.FirstOrDefault(x => x.HeaderText == Resources.DescriptionHeader).Index;
			NotesColumnIndex = dgJiraTaskList.Columns.FirstOrDefault(x => x.HeaderText == Resources.NotesHeader).Index;
		}

		private void datesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//Closed, Modified, and Created date window
			var asdf = new FilterByDate(UserPreferences);
			asdf.ShowDialog(this);
			if (asdf.ValueChanged)
				LoadDataGridView(true);
		}
	}
}
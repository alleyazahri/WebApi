using JiraTasks.Data;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace JiraTasks
{
	public partial class ColorKeyWindow : Form
	{
		private UserPrefs UserPreferences { get; set; }
		public bool ChangesOccurred { get; private set; }

		public ColorKeyWindow(UserPrefs userPrefs)
		{
			UserPreferences = userPrefs;
			InitializeComponent();
		}

		private void bCompleted_Click(object sender, EventArgs e)
		{
			SetColor(bCompleted, UserPreferences.ColorLegend.CompletedTasks);
		}

		private void bBeta_Click(object sender, EventArgs e)
		{
			SetColor(bBeta, UserPreferences.ColorLegend.TasksInBeta);
		}

		private void bTest_Click(object sender, EventArgs e)
		{
			SetColor(bTest, UserPreferences.ColorLegend.TasksInTest);
		}

		private void bCodeReview_Click(object sender, EventArgs e)
		{
			SetColor(bCodeReview, UserPreferences.ColorLegend.CodeReviewTasks);
		}

		private void bInProgress_Click(object sender, EventArgs e)
		{
			SetColor(bInProgress, UserPreferences.ColorLegend.InProgressTasks);
		}

		private void bNotStarted_Click(object sender, EventArgs e)
		{
			SetColor(bNotStarted, UserPreferences.ColorLegend.NotStartedTasks);
		}

		private void bIrrelevent_Click(object sender, EventArgs e)
		{
			SetColor(bIrrelevent, UserPreferences.ColorLegend.IrrelevantTasks);
		}

		private void ColorKeyWindow_Load(object sender, EventArgs e)
		{
			bCompleted.BackColor = UserPreferences.ColorLegend.CompletedTasks;
			bBeta.BackColor = UserPreferences.ColorLegend.TasksInBeta;
			bTest.BackColor = UserPreferences.ColorLegend.TasksInTest;
			bCodeReview.BackColor = UserPreferences.ColorLegend.CodeReviewTasks;
			bInProgress.BackColor = UserPreferences.ColorLegend.InProgressTasks;
			bNotStarted.BackColor = UserPreferences.ColorLegend.NotStartedTasks;
			bIrrelevent.BackColor = UserPreferences.ColorLegend.IrrelevantTasks;
		}

		private void SetColor(Button button, Color originalColor)
		{
			cdColorPicker.Color = originalColor;
			cdColorPicker.AllowFullOpen = true;
			cdColorPicker.ShowDialog(this);
			button.BackColor = cdColorPicker.Color;
		}
	}
}
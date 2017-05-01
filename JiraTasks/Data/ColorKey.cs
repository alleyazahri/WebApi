using System.Drawing;

namespace JiraTasks.Data
{
	public class ColorKey
	{
		public Color IrrelevantTasks { get; set; }
		public Color InProgressTasks { get; set; }
		public Color CompletedTasks { get; set; }
		public Color CodeReviewTasks { get; set; }
		public Color TasksInTest { get; set; }
		public Color TasksInBeta { get; set; }
		public Color NotStartedTasks { get; set; }

		public ColorKey()
		{
			CompletedTasks = Color.DarkGreen;
			TasksInBeta = Color.DarkOliveGreen;
			TasksInTest = Color.DarkSeaGreen;
			CodeReviewTasks = Color.Aquamarine;
			InProgressTasks = Color.Cyan;
			NotStartedTasks = Color.Crimson;
			IrrelevantTasks = Color.Gray;
		}

		public ColorKey DeepCopy()
		{
			return new ColorKey()
			{
				IrrelevantTasks = IrrelevantTasks,
				InProgressTasks = InProgressTasks,
				CompletedTasks = CompletedTasks,
				NotStartedTasks = NotStartedTasks
			};
		}
	}
}
using System.Drawing;

namespace JiraTasks.Data
{
	public class ColorKey
	{
		public Color IrrelevantTasks { get; set; }
		public Color InProgressTasks { get; set; }
		public Color CompletedTasks { get; set; }
		public Color NotStartedTasks { get; set; }

		public ColorKey()
		{
			IrrelevantTasks = Color.Gray;
			InProgressTasks = Color.Cyan;
			CompletedTasks = Color.DarkSeaGreen;
			NotStartedTasks = Color.Crimson;
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
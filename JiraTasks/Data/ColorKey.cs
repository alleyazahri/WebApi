using System.Drawing;

namespace JiraTasks.Data
{
    public class ColorKey
    {
        public Color IrrelevantTasks { get; set; }

        public ColorKey()
        {
            IrrelevantTasks = Color.Gray;
        }
    }
}
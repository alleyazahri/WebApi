using System;
using System.Windows.Forms;

namespace JiraTasks
{
    public partial class ColorKeyWindow : Form
    {
        public ColorKeyWindow()
        {
            InitializeComponent();
        }

        private void bInProgress_Click(object sender, EventArgs e)
        {
            cdColorPicker.AllowFullOpen = true;
            cdColorPicker.ShowDialog(this);
            bInProgress.BackColor = cdColorPicker.Color;
        }
    }
}
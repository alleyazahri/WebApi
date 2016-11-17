using System;
using System.Windows.Forms;

namespace JiraTasks
{
    public partial class Login : Form
    {
        internal string username { get; set; }
        internal string password { get; set; }

        public Login()
        {
            InitializeComponent();
        }

        private void bLogin_Click(object sender, EventArgs e)
        {
            username = tbUsername.Text;
            password = tbPassword.Text;
            this.Close();
        }
    }
}
using JiraApi;
using JiraTasks.Data;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace JiraTasks
{
	public partial class Login : Form
	{
		internal LoginSettingsDocument loginSettings { get; set; }
		internal string SettingsPath { get; set; }
		internal string SettingsFile => "LSJTP27.settings";

		private string JiraUrl => "https://epm.verisk.com/jira/";
		internal LoginController LoginController { get; set; }

		public Login()
		{
			bool result = false;
			loginSettings = new LoginSettingsDocument();
			LoginController = new LoginController();
			SettingsPath = Environment.GetEnvironmentVariable("AppData");
			SettingsPath = SettingsPath.Replace("Roaming", "Local") + "\\JiraUtil";
			var loaded = loginSettings.Load(SettingsPath, SettingsFile);
			if (!loaded || loginSettings.SavePassword != CheckState.Checked)
				InitializeComponent();
			else
				result = LoginToJira();
			if (!result)
				InitializeComponent();
		}

		private void bLogin_Click(object sender, EventArgs e)
		{
			bLogin.BackColor = Color.Beige;
			bLogin.ForeColor = Color.Blue;
			loginSettings.Username = tbUsername.Text;
			loginSettings.Password = tbPassword.Text;
			loginSettings.SavePassword = cbLoginAutomatically.CheckState;
			var result = LoginToJira();
			if (result)
			{
				loginSettings.Save(SettingsPath, SettingsFile);
				this.Close();
			}
			else
			{
				bLogin.BackColor = Color.AliceBlue;
				bLogin.ForeColor = Color.Black;
				tbUsername.Text = "";
				tbPassword.Text = "";
				cbLoginAutomatically.CheckState = CheckState.Unchecked;
				pLoginFailed.Visible = true;
			}
		}

		private void cbLoginAutomatically_CheckedChanged(object sender, EventArgs e)
		{
			Console.WriteLine(sender.GetType());
			var checkbox = sender as CheckBox;
			loginSettings.SavePassword = checkbox?.CheckState ?? CheckState.Indeterminate;
			panel1.Visible = checkbox.Checked;
			if (checkbox?.CheckState == CheckState.Checked)
			{
			}
		}

		private void tbUsername_TextChanged(object sender, EventArgs e)
		{
			pLoginFailed.Visible = false;
		}

		private void tbPassword_TextChanged(object sender, EventArgs e)
		{
			pLoginFailed.Visible = false;
		}

		private void Login_FormClosed(object sender, FormClosedEventArgs e)
		{
			pLoginFailed.Visible = false;
			tbUsername.Text = "";
			tbPassword.Text = "";
			cbLoginAutomatically.CheckState = CheckState.Unchecked;
		}

		private bool LoginToJira()
		{
			var result = false;
			if (!string.IsNullOrEmpty(loginSettings.Username) && !string.IsNullOrEmpty(loginSettings.Password))
				result = LoginController.Login(JiraUrl, loginSettings.Username, loginSettings.Password);
			return result;
		}
	}
}
﻿namespace JiraTasks
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.usernameLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.bLogin = new System.Windows.Forms.Button();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.cbLoginAutomatically = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lWarning = new System.Windows.Forms.Label();
            this.pLoginFailed = new System.Windows.Forms.Panel();
            this.lLoginFailed = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.pLoginFailed.SuspendLayout();
            this.SuspendLayout();
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(35, 40);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(83, 20);
            this.usernameLabel.TabIndex = 0;
            this.usernameLabel.Text = "Username";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(35, 92);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(78, 20);
            this.passwordLabel.TabIndex = 1;
            this.passwordLabel.Text = "Password";
            // 
            // tbUsername
            // 
            this.tbUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbUsername.Location = new System.Drawing.Point(143, 40);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(519, 26);
            this.tbUsername.TabIndex = 2;
            this.tbUsername.TextChanged += new System.EventHandler(this.tbUsername_TextChanged);
            // 
            // bLogin
            // 
            this.bLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bLogin.Location = new System.Drawing.Point(516, 147);
            this.bLogin.Name = "bLogin";
            this.bLogin.Size = new System.Drawing.Size(146, 42);
            this.bLogin.TabIndex = 4;
            this.bLogin.Text = "Login";
            this.bLogin.UseVisualStyleBackColor = true;
            this.bLogin.Click += new System.EventHandler(this.bLogin_Click);
            // 
            // tbPassword
            // 
            this.tbPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPassword.Location = new System.Drawing.Point(143, 89);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(518, 26);
            this.tbPassword.TabIndex = 3;
            this.tbPassword.TextChanged += new System.EventHandler(this.tbPassword_TextChanged);
            // 
            // cbLoginAutomatically
            // 
            this.cbLoginAutomatically.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbLoginAutomatically.AutoSize = true;
            this.cbLoginAutomatically.Location = new System.Drawing.Point(82, 130);
            this.cbLoginAutomatically.Name = "cbLoginAutomatically";
            this.cbLoginAutomatically.Size = new System.Drawing.Size(172, 24);
            this.cbLoginAutomatically.TabIndex = 5;
            this.cbLoginAutomatically.Text = "Login Automatically";
            this.cbLoginAutomatically.UseVisualStyleBackColor = true;
            this.cbLoginAutomatically.CheckedChanged += new System.EventHandler(this.cbLoginAutomatically_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.lWarning);
            this.panel1.Location = new System.Drawing.Point(39, 158);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(459, 42);
            this.panel1.TabIndex = 6;
            this.panel1.Visible = false;
            // 
            // lWarning
            // 
            this.lWarning.AutoSize = true;
            this.lWarning.ForeColor = System.Drawing.Color.DarkRed;
            this.lWarning.Location = new System.Drawing.Point(3, 11);
            this.lWarning.Name = "lWarning";
            this.lWarning.Size = new System.Drawing.Size(434, 20);
            this.lWarning.TabIndex = 0;
            this.lWarning.Text = "WARNING: Password is stored locally which can be insecure";
            // 
            // pLoginFailed
            // 
            this.pLoginFailed.Controls.Add(this.lLoginFailed);
            this.pLoginFailed.Location = new System.Drawing.Point(467, 120);
            this.pLoginFailed.Name = "pLoginFailed";
            this.pLoginFailed.Size = new System.Drawing.Size(229, 27);
            this.pLoginFailed.TabIndex = 7;
            this.pLoginFailed.Visible = false;
            // 
            // lLoginFailed
            // 
            this.lLoginFailed.AutoSize = true;
            this.lLoginFailed.ForeColor = System.Drawing.Color.Red;
            this.lLoginFailed.Location = new System.Drawing.Point(71, 5);
            this.lLoginFailed.Name = "lLoginFailed";
            this.lLoginFailed.Size = new System.Drawing.Size(95, 20);
            this.lLoginFailed.TabIndex = 0;
            this.lLoginFailed.Text = "Login Failed";
            // 
            // Login
            // 
            this.AcceptButton = this.bLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 212);
            this.Controls.Add(this.pLoginFailed);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cbLoginAutomatically);
            this.Controls.Add(this.bLogin);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbUsername);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.usernameLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Login";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Login_FormClosed);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pLoginFailed.ResumeLayout(false);
            this.pLoginFailed.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.Button bLogin;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.CheckBox cbLoginAutomatically;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lWarning;
        private System.Windows.Forms.Panel pLoginFailed;
        private System.Windows.Forms.Label lLoginFailed;
    }
}
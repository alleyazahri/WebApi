namespace JiraTasks
{
    partial class MainWindow
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
            this.dgJiraTaskList = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xWESVCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savedFiltersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCurrentFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tasksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linkTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unlinkTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pLoginToViewTasks = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgJiraTaskList)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.pLoginToViewTasks.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgJiraTaskList
            // 
            this.dgJiraTaskList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgJiraTaskList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgJiraTaskList.Location = new System.Drawing.Point(46, 129);
            this.dgJiraTaskList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgJiraTaskList.Name = "dgJiraTaskList";
            this.dgJiraTaskList.RowTemplate.Height = 28;
            this.dgJiraTaskList.Size = new System.Drawing.Size(1257, 855);
            this.dgJiraTaskList.TabIndex = 0;
            this.dgJiraTaskList.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgJiraTaskList_CellMouseDoubleClick);
            this.dgJiraTaskList.SelectionChanged += new System.EventHandler(this.dgJiraTaskList_SelectionChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.filterToolStripMenuItem,
            this.savedFiltersToolStripMenuItem,
            this.tasksToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1344, 33);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginToolStripMenuItem,
            this.logoutToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(50, 29);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // loginToolStripMenuItem
            // 
            this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            this.loginToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.L)));
            this.loginToolStripMenuItem.Size = new System.Drawing.Size(214, 30);
            this.loginToolStripMenuItem.Text = "&Login";
            this.loginToolStripMenuItem.Click += new System.EventHandler(this.loginToolStripMenuItem_Click);
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.O)));
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(214, 30);
            this.logoutToolStripMenuItem.Text = "L&ogout";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click);
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.projectsToolStripMenuItem});
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(62, 29);
            this.filterToolStripMenuItem.Text = "Filter";
            // 
            // projectsToolStripMenuItem
            // 
            this.projectsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xWESVCToolStripMenuItem,
            this.addProjectToolStripMenuItem});
            this.projectsToolStripMenuItem.Name = "projectsToolStripMenuItem";
            this.projectsToolStripMenuItem.Size = new System.Drawing.Size(159, 30);
            this.projectsToolStripMenuItem.Text = "Projects";
            // 
            // xWESVCToolStripMenuItem
            // 
            this.xWESVCToolStripMenuItem.Checked = true;
            this.xWESVCToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.xWESVCToolStripMenuItem.Name = "xWESVCToolStripMenuItem";
            this.xWESVCToolStripMenuItem.Size = new System.Drawing.Size(261, 30);
            this.xWESVCToolStripMenuItem.Text = "XWESVC";
            // 
            // addProjectToolStripMenuItem
            // 
            this.addProjectToolStripMenuItem.Name = "addProjectToolStripMenuItem";
            this.addProjectToolStripMenuItem.Size = new System.Drawing.Size(261, 30);
            this.addProjectToolStripMenuItem.Text = "Add/Remove Project";
            this.addProjectToolStripMenuItem.Click += new System.EventHandler(this.addProjectToolStripMenuItem_Click);
            // 
            // savedFiltersToolStripMenuItem
            // 
            this.savedFiltersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveCurrentFilterToolStripMenuItem});
            this.savedFiltersToolStripMenuItem.Name = "savedFiltersToolStripMenuItem";
            this.savedFiltersToolStripMenuItem.Size = new System.Drawing.Size(123, 29);
            this.savedFiltersToolStripMenuItem.Text = "Saved Filters";
            // 
            // saveCurrentFilterToolStripMenuItem
            // 
            this.saveCurrentFilterToolStripMenuItem.Name = "saveCurrentFilterToolStripMenuItem";
            this.saveCurrentFilterToolStripMenuItem.Size = new System.Drawing.Size(252, 30);
            this.saveCurrentFilterToolStripMenuItem.Text = "+Save Current Filter";
            // 
            // tasksToolStripMenuItem
            // 
            this.tasksToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.linkTaskToolStripMenuItem,
            this.unlinkTaskToolStripMenuItem});
            this.tasksToolStripMenuItem.Name = "tasksToolStripMenuItem";
            this.tasksToolStripMenuItem.Size = new System.Drawing.Size(65, 29);
            this.tasksToolStripMenuItem.Text = "Tasks";
            // 
            // linkTaskToolStripMenuItem
            // 
            this.linkTaskToolStripMenuItem.Name = "linkTaskToolStripMenuItem";
            this.linkTaskToolStripMenuItem.Size = new System.Drawing.Size(184, 30);
            this.linkTaskToolStripMenuItem.Text = "Link Task";
            this.linkTaskToolStripMenuItem.Click += new System.EventHandler(this.linkTaskToolStripMenuItem_Click);
            // 
            // unlinkTaskToolStripMenuItem
            // 
            this.unlinkTaskToolStripMenuItem.Name = "unlinkTaskToolStripMenuItem";
            this.unlinkTaskToolStripMenuItem.Size = new System.Drawing.Size(184, 30);
            this.unlinkTaskToolStripMenuItem.Text = "Unlink Task";
            // 
            // pLoginToViewTasks
            // 
            this.pLoginToViewTasks.Controls.Add(this.label1);
            this.pLoginToViewTasks.Location = new System.Drawing.Point(368, 469);
            this.pLoginToViewTasks.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pLoginToViewTasks.Name = "pLoginToViewTasks";
            this.pLoginToViewTasks.Size = new System.Drawing.Size(644, 44);
            this.pLoginToViewTasks.TabIndex = 2;
            this.pLoginToViewTasks.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(196, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(326, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please Log In To View Tasks";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 1011);
            this.Controls.Add(this.pLoginToViewTasks);
            this.Controls.Add(this.dgJiraTaskList);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainWindow";
            this.Text = "Jira Task List";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TaskList_FormClosing);
            this.Shown += new System.EventHandler(this.TaskList_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgJiraTaskList)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pLoginToViewTasks.ResumeLayout(false);
            this.pLoginToViewTasks.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgJiraTaskList;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
        private System.Windows.Forms.Panel pLoginToViewTasks;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem filterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projectsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xWESVCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem savedFiltersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveCurrentFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tasksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem linkTaskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unlinkTaskToolStripMenuItem;
    }
}


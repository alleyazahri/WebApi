namespace JiraTasks
{
    partial class LinkTasks
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LinkTasks));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.linkTask = new System.Windows.Forms.Button();
			this.bCancelLinkingTasks = new System.Windows.Forms.Button();
			this.tbMainTask = new System.Windows.Forms.TextBox();
			this.tbLinkTask = new System.Windows.Forms.TextBox();
			this.cbNoTask = new System.Windows.Forms.CheckBox();
			this.rbNotStarted = new System.Windows.Forms.RadioButton();
			this.rbInProgress = new System.Windows.Forms.RadioButton();
			this.rbComplete = new System.Windows.Forms.RadioButton();
			this.rbCodeReview = new System.Windows.Forms.RadioButton();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(40, 46);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104, 25);
			this.label1.TabIndex = 0;
			this.label1.Text = "Main Task";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(40, 98);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(151, 25);
			this.label2.TabIndex = 1;
			this.label2.Text = "My Linked Task";
			// 
			// linkTask
			// 
			this.linkTask.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.linkTask.Location = new System.Drawing.Point(171, 225);
			this.linkTask.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.linkTask.Name = "linkTask";
			this.linkTask.Size = new System.Drawing.Size(133, 56);
			this.linkTask.TabIndex = 2;
			this.linkTask.Text = "Save";
			this.linkTask.UseVisualStyleBackColor = true;
			this.linkTask.Click += new System.EventHandler(this.linkTask_Click);
			// 
			// bCancelLinkingTasks
			// 
			this.bCancelLinkingTasks.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bCancelLinkingTasks.Location = new System.Drawing.Point(340, 225);
			this.bCancelLinkingTasks.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.bCancelLinkingTasks.Name = "bCancelLinkingTasks";
			this.bCancelLinkingTasks.Size = new System.Drawing.Size(133, 56);
			this.bCancelLinkingTasks.TabIndex = 3;
			this.bCancelLinkingTasks.Text = "Cancel";
			this.bCancelLinkingTasks.UseVisualStyleBackColor = true;
			this.bCancelLinkingTasks.Click += new System.EventHandler(this.bCancelLinkingTasks_Click);
			// 
			// tbMainTask
			// 
			this.tbMainTask.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbMainTask.Location = new System.Drawing.Point(244, 42);
			this.tbMainTask.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tbMainTask.Name = "tbMainTask";
			this.tbMainTask.Size = new System.Drawing.Size(241, 30);
			this.tbMainTask.TabIndex = 4;
			// 
			// tbLinkTask
			// 
			this.tbLinkTask.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbLinkTask.Location = new System.Drawing.Point(244, 92);
			this.tbLinkTask.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tbLinkTask.Name = "tbLinkTask";
			this.tbLinkTask.Size = new System.Drawing.Size(241, 30);
			this.tbLinkTask.TabIndex = 5;
			// 
			// cbNoTask
			// 
			this.cbNoTask.AutoSize = true;
			this.cbNoTask.Location = new System.Drawing.Point(45, 141);
			this.cbNoTask.Name = "cbNoTask";
			this.cbNoTask.Size = new System.Drawing.Size(93, 24);
			this.cbNoTask.TabIndex = 6;
			this.cbNoTask.Text = "No Task";
			this.cbNoTask.UseVisualStyleBackColor = true;
			this.cbNoTask.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// rbNotStarted
			// 
			this.rbNotStarted.AutoSize = true;
			this.rbNotStarted.Location = new System.Drawing.Point(12, 178);
			this.rbNotStarted.Name = "rbNotStarted";
			this.rbNotStarted.Size = new System.Drawing.Size(116, 24);
			this.rbNotStarted.TabIndex = 7;
			this.rbNotStarted.TabStop = true;
			this.rbNotStarted.Text = "Not Started";
			this.rbNotStarted.UseVisualStyleBackColor = true;
			this.rbNotStarted.Visible = false;
			this.rbNotStarted.CheckedChanged += new System.EventHandler(this.rbNotStarted_CheckedChanged);
			// 
			// rbInProgress
			// 
			this.rbInProgress.AutoSize = true;
			this.rbInProgress.Location = new System.Drawing.Point(146, 178);
			this.rbInProgress.Name = "rbInProgress";
			this.rbInProgress.Size = new System.Drawing.Size(115, 24);
			this.rbInProgress.TabIndex = 8;
			this.rbInProgress.TabStop = true;
			this.rbInProgress.Text = "In Progress";
			this.rbInProgress.UseVisualStyleBackColor = true;
			this.rbInProgress.Visible = false;
			this.rbInProgress.CheckedChanged += new System.EventHandler(this.rbInProgress_CheckedChanged);
			// 
			// rbComplete
			// 
			this.rbComplete.AutoSize = true;
			this.rbComplete.Location = new System.Drawing.Point(426, 178);
			this.rbComplete.Name = "rbComplete";
			this.rbComplete.Size = new System.Drawing.Size(102, 24);
			this.rbComplete.TabIndex = 9;
			this.rbComplete.TabStop = true;
			this.rbComplete.Text = "Complete";
			this.rbComplete.UseVisualStyleBackColor = true;
			this.rbComplete.Visible = false;
			this.rbComplete.CheckedChanged += new System.EventHandler(this.rbComplete_CheckedChanged);
			// 
			// rbCodeReview
			// 
			this.rbCodeReview.AutoSize = true;
			this.rbCodeReview.Location = new System.Drawing.Point(280, 178);
			this.rbCodeReview.Name = "rbCodeReview";
			this.rbCodeReview.Size = new System.Drawing.Size(127, 24);
			this.rbCodeReview.TabIndex = 10;
			this.rbCodeReview.TabStop = true;
			this.rbCodeReview.Text = "Code Review";
			this.rbCodeReview.UseVisualStyleBackColor = true;
			this.rbCodeReview.Visible = false;
			this.rbCodeReview.CheckedChanged += new System.EventHandler(this.rbCodeReview_CheckedChanged);
			// 
			// LinkTasks
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(540, 305);
			this.Controls.Add(this.rbCodeReview);
			this.Controls.Add(this.rbComplete);
			this.Controls.Add(this.rbInProgress);
			this.Controls.Add(this.rbNotStarted);
			this.Controls.Add(this.cbNoTask);
			this.Controls.Add(this.tbLinkTask);
			this.Controls.Add(this.tbMainTask);
			this.Controls.Add(this.bCancelLinkingTasks);
			this.Controls.Add(this.linkTask);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "LinkTasks";
			this.Text = "LinkTasks";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button linkTask;
        private System.Windows.Forms.Button bCancelLinkingTasks;
        private System.Windows.Forms.TextBox tbMainTask;
        private System.Windows.Forms.TextBox tbLinkTask;
        private System.Windows.Forms.CheckBox cbNoTask;
        private System.Windows.Forms.RadioButton rbNotStarted;
        private System.Windows.Forms.RadioButton rbInProgress;
        private System.Windows.Forms.RadioButton rbComplete;
		private System.Windows.Forms.RadioButton rbCodeReview;
	}
}
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
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(36, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Main Task";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(36, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "My Linked Task";
            // 
            // linkTask
            // 
            this.linkTask.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkTask.Location = new System.Drawing.Point(152, 180);
            this.linkTask.Name = "linkTask";
            this.linkTask.Size = new System.Drawing.Size(118, 45);
            this.linkTask.TabIndex = 2;
            this.linkTask.Text = "Link";
            this.linkTask.UseVisualStyleBackColor = true;
            this.linkTask.Click += new System.EventHandler(this.linkTask_Click);
            // 
            // bCancelLinkingTasks
            // 
            this.bCancelLinkingTasks.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bCancelLinkingTasks.Location = new System.Drawing.Point(302, 180);
            this.bCancelLinkingTasks.Name = "bCancelLinkingTasks";
            this.bCancelLinkingTasks.Size = new System.Drawing.Size(118, 45);
            this.bCancelLinkingTasks.TabIndex = 3;
            this.bCancelLinkingTasks.Text = "Cancel";
            this.bCancelLinkingTasks.UseVisualStyleBackColor = true;
            this.bCancelLinkingTasks.Click += new System.EventHandler(this.bCancelLinkingTasks_Click);
            // 
            // tbMainTask
            // 
            this.tbMainTask.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMainTask.Location = new System.Drawing.Point(217, 56);
            this.tbMainTask.Name = "tbMainTask";
            this.tbMainTask.Size = new System.Drawing.Size(215, 26);
            this.tbMainTask.TabIndex = 4;
            // 
            // tbLinkTask
            // 
            this.tbLinkTask.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLinkTask.Location = new System.Drawing.Point(217, 107);
            this.tbLinkTask.Name = "tbLinkTask";
            this.tbLinkTask.Size = new System.Drawing.Size(215, 26);
            this.tbLinkTask.TabIndex = 5;
            // 
            // LinkTasks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 244);
            this.Controls.Add(this.tbLinkTask);
            this.Controls.Add(this.tbMainTask);
            this.Controls.Add(this.bCancelLinkingTasks);
            this.Controls.Add(this.linkTask);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
    }
}
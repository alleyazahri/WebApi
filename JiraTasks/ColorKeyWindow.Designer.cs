namespace JiraTasks
{
    partial class ColorKeyWindow
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColorKeyWindow));
			this.cdColorPicker = new System.Windows.Forms.ColorDialog();
			this.lInProgress = new System.Windows.Forms.Label();
			this.bInProgress = new System.Windows.Forms.Button();
			this.bCompleted = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.bBeta = new System.Windows.Forms.Button();
			this.bTest = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.bCodeReview = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.bNotStarted = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.bIrrelevent = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lInProgress
			// 
			this.lInProgress.AutoSize = true;
			this.lInProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lInProgress.Location = new System.Drawing.Point(133, 243);
			this.lInProgress.Name = "lInProgress";
			this.lInProgress.Size = new System.Drawing.Size(111, 25);
			this.lInProgress.TabIndex = 0;
			this.lInProgress.Text = "In Progress";
			// 
			// bInProgress
			// 
			this.bInProgress.BackColor = System.Drawing.Color.Aqua;
			this.bInProgress.Location = new System.Drawing.Point(15, 237);
			this.bInProgress.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.bInProgress.Name = "bInProgress";
			this.bInProgress.Size = new System.Drawing.Size(99, 48);
			this.bInProgress.TabIndex = 1;
			this.bInProgress.UseVisualStyleBackColor = false;
			this.bInProgress.Click += new System.EventHandler(this.bInProgress_Click);
			// 
			// bCompleted
			// 
			this.bCompleted.BackColor = System.Drawing.Color.Aqua;
			this.bCompleted.Location = new System.Drawing.Point(15, 13);
			this.bCompleted.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.bCompleted.Name = "bCompleted";
			this.bCompleted.Size = new System.Drawing.Size(99, 48);
			this.bCompleted.TabIndex = 2;
			this.bCompleted.UseVisualStyleBackColor = false;
			this.bCompleted.Click += new System.EventHandler(this.bCompleted_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(133, 23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(107, 25);
			this.label1.TabIndex = 3;
			this.label1.Text = "Completed";
			// 
			// bBeta
			// 
			this.bBeta.BackColor = System.Drawing.Color.Aqua;
			this.bBeta.Location = new System.Drawing.Point(15, 69);
			this.bBeta.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.bBeta.Name = "bBeta";
			this.bBeta.Size = new System.Drawing.Size(99, 48);
			this.bBeta.TabIndex = 4;
			this.bBeta.UseVisualStyleBackColor = false;
			this.bBeta.Click += new System.EventHandler(this.bBeta_Click);
			// 
			// bTest
			// 
			this.bTest.BackColor = System.Drawing.Color.Aqua;
			this.bTest.Location = new System.Drawing.Point(15, 125);
			this.bTest.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.bTest.Name = "bTest";
			this.bTest.Size = new System.Drawing.Size(99, 48);
			this.bTest.TabIndex = 5;
			this.bTest.UseVisualStyleBackColor = false;
			this.bTest.Click += new System.EventHandler(this.bTest_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(133, 79);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(52, 25);
			this.label2.TabIndex = 6;
			this.label2.Text = "Beta";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(133, 135);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(51, 25);
			this.label3.TabIndex = 7;
			this.label3.Text = "Test";
			// 
			// bCodeReview
			// 
			this.bCodeReview.BackColor = System.Drawing.Color.Aqua;
			this.bCodeReview.Location = new System.Drawing.Point(15, 181);
			this.bCodeReview.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.bCodeReview.Name = "bCodeReview";
			this.bCodeReview.Size = new System.Drawing.Size(99, 48);
			this.bCodeReview.TabIndex = 8;
			this.bCodeReview.UseVisualStyleBackColor = false;
			this.bCodeReview.Click += new System.EventHandler(this.bCodeReview_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(133, 191);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(128, 25);
			this.label4.TabIndex = 9;
			this.label4.Text = "Code Review";
			// 
			// bNotStarted
			// 
			this.bNotStarted.BackColor = System.Drawing.Color.Aqua;
			this.bNotStarted.Location = new System.Drawing.Point(15, 293);
			this.bNotStarted.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.bNotStarted.Name = "bNotStarted";
			this.bNotStarted.Size = new System.Drawing.Size(99, 48);
			this.bNotStarted.TabIndex = 10;
			this.bNotStarted.UseVisualStyleBackColor = false;
			this.bNotStarted.Click += new System.EventHandler(this.bNotStarted_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(133, 303);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(110, 25);
			this.label5.TabIndex = 11;
			this.label5.Text = "Not Started";
			// 
			// bIrrelevent
			// 
			this.bIrrelevent.BackColor = System.Drawing.Color.Aqua;
			this.bIrrelevent.Location = new System.Drawing.Point(15, 349);
			this.bIrrelevent.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.bIrrelevent.Name = "bIrrelevent";
			this.bIrrelevent.Size = new System.Drawing.Size(99, 48);
			this.bIrrelevent.TabIndex = 12;
			this.bIrrelevent.UseVisualStyleBackColor = false;
			this.bIrrelevent.Click += new System.EventHandler(this.bIrrelevent_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(130, 359);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(92, 25);
			this.label6.TabIndex = 13;
			this.label6.Text = "Irrelevent";
			// 
			// ColorKeyWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(276, 409);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.bIrrelevent);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.bNotStarted);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.bCodeReview);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.bTest);
			this.Controls.Add(this.bBeta);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.bCompleted);
			this.Controls.Add(this.bInProgress);
			this.Controls.Add(this.lInProgress);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "ColorKeyWindow";
			this.Text = "ColorKeyWindow";
			this.Load += new System.EventHandler(this.ColorKeyWindow_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ColorDialog cdColorPicker;
        private System.Windows.Forms.Label lInProgress;
        private System.Windows.Forms.Button bInProgress;
		private System.Windows.Forms.Button bCompleted;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button bBeta;
		private System.Windows.Forms.Button bTest;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button bCodeReview;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button bNotStarted;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button bIrrelevent;
		private System.Windows.Forms.Label label6;
	}
}
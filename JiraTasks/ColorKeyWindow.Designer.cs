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
            this.SuspendLayout();
            // 
            // lInProgress
            // 
            this.lInProgress.AutoSize = true;
            this.lInProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lInProgress.Location = new System.Drawing.Point(118, 19);
            this.lInProgress.Name = "lInProgress";
            this.lInProgress.Size = new System.Drawing.Size(95, 20);
            this.lInProgress.TabIndex = 0;
            this.lInProgress.Text = "In Progress";
            // 
            // bInProgress
            // 
            this.bInProgress.BackColor = System.Drawing.Color.Aqua;
            this.bInProgress.Location = new System.Drawing.Point(13, 14);
            this.bInProgress.Name = "bInProgress";
            this.bInProgress.Size = new System.Drawing.Size(88, 38);
            this.bInProgress.TabIndex = 1;
            this.bInProgress.UseVisualStyleBackColor = false;
            this.bInProgress.Click += new System.EventHandler(this.bInProgress_Click);
            // 
            // ColorKeyWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(227, 305);
            this.Controls.Add(this.bInProgress);
            this.Controls.Add(this.lInProgress);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ColorKeyWindow";
            this.Text = "ColorKeyWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ColorDialog cdColorPicker;
        private System.Windows.Forms.Label lInProgress;
        private System.Windows.Forms.Button bInProgress;
    }
}
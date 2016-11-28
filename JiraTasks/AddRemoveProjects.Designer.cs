namespace JiraTasks
{
    partial class AddRemoveProjects
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
            this.dgvAddRemoveProjects = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAddRemoveProjects)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAddRemoveProjects
            // 
            this.dgvAddRemoveProjects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAddRemoveProjects.Location = new System.Drawing.Point(12, 29);
            this.dgvAddRemoveProjects.Name = "dgvAddRemoveProjects";
            this.dgvAddRemoveProjects.RowTemplate.Height = 24;
            this.dgvAddRemoveProjects.Size = new System.Drawing.Size(656, 220);
            this.dgvAddRemoveProjects.TabIndex = 0;
            // 
            // AddRemoveProjects
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 261);
            this.Controls.Add(this.dgvAddRemoveProjects);
            this.Name = "AddRemoveProjects";
            this.Text = "Add/Remove Projects";
            ((System.ComponentModel.ISupportInitialize)(this.dgvAddRemoveProjects)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAddRemoveProjects;
    }
}
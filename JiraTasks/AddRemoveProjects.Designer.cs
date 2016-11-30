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
            this.bSaveChanges = new System.Windows.Forms.Button();
            this.bDiscardChanges = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAddRemoveProjects)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAddRemoveProjects
            // 
            this.dgvAddRemoveProjects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAddRemoveProjects.Location = new System.Drawing.Point(12, 29);
            this.dgvAddRemoveProjects.Name = "dgvAddRemoveProjects";
            this.dgvAddRemoveProjects.RowTemplate.Height = 24;
            this.dgvAddRemoveProjects.Size = new System.Drawing.Size(656, 182);
            this.dgvAddRemoveProjects.TabIndex = 0;
            this.dgvAddRemoveProjects.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAddRemoveProjects_CellEndEdit);
            // 
            // bSaveChanges
            // 
            this.bSaveChanges.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bSaveChanges.Location = new System.Drawing.Point(526, 217);
            this.bSaveChanges.Name = "bSaveChanges";
            this.bSaveChanges.Size = new System.Drawing.Size(142, 32);
            this.bSaveChanges.TabIndex = 1;
            this.bSaveChanges.Text = "Save Changes";
            this.bSaveChanges.UseVisualStyleBackColor = true;
            this.bSaveChanges.Click += new System.EventHandler(this.bSaveChanges_Click);
            // 
            // bDiscardChanges
            // 
            this.bDiscardChanges.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bDiscardChanges.Location = new System.Drawing.Point(357, 217);
            this.bDiscardChanges.Name = "bDiscardChanges";
            this.bDiscardChanges.Size = new System.Drawing.Size(154, 32);
            this.bDiscardChanges.TabIndex = 2;
            this.bDiscardChanges.Text = "Discard Changes";
            this.bDiscardChanges.UseVisualStyleBackColor = true;
            this.bDiscardChanges.Click += new System.EventHandler(this.bDiscardChanges_Click);
            // 
            // AddRemoveProjects
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 261);
            this.Controls.Add(this.bDiscardChanges);
            this.Controls.Add(this.bSaveChanges);
            this.Controls.Add(this.dgvAddRemoveProjects);
            this.Name = "AddRemoveProjects";
            this.Text = "Add/Remove Projects";
            ((System.ComponentModel.ISupportInitialize)(this.dgvAddRemoveProjects)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAddRemoveProjects;
        private System.Windows.Forms.Button bSaveChanges;
        private System.Windows.Forms.Button bDiscardChanges;
    }
}
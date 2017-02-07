namespace JiraTasks
{
    partial class FilterByDate
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
            this.dateTimeCreatedStart = new System.Windows.Forms.DateTimePicker();
            this.dateTimeCreatedEnd = new System.Windows.Forms.DateTimePicker();
            this.dateTimeModifiedStart = new System.Windows.Forms.DateTimePicker();
            this.dateTimeModifiedEnd = new System.Windows.Forms.DateTimePicker();
            this.dateTimeClosedStart = new System.Windows.Forms.DateTimePicker();
            this.dateTimeClosedEnd = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.bClearCreatedDate = new System.Windows.Forms.Button();
            this.bClearModifiedDate = new System.Windows.Forms.Button();
            this.bClearClosedDate = new System.Windows.Forms.Button();
            this.bSaveDates = new System.Windows.Forms.Button();
            this.bCancelDates = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dateTimeCreatedStart
            // 
            this.dateTimeCreatedStart.Location = new System.Drawing.Point(137, 43);
            this.dateTimeCreatedStart.Name = "dateTimeCreatedStart";
            this.dateTimeCreatedStart.Size = new System.Drawing.Size(200, 26);
            this.dateTimeCreatedStart.TabIndex = 0;
            this.dateTimeCreatedStart.Value = new System.DateTime(2017, 1, 30, 0, 0, 0, 0);
            this.dateTimeCreatedStart.ValueChanged += new System.EventHandler(this.dateTimeCreatedStart_ValueChanged);
            // 
            // dateTimeCreatedEnd
            // 
            this.dateTimeCreatedEnd.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimeCreatedEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimeCreatedEnd.Location = new System.Drawing.Point(343, 43);
            this.dateTimeCreatedEnd.Name = "dateTimeCreatedEnd";
            this.dateTimeCreatedEnd.Size = new System.Drawing.Size(200, 30);
            this.dateTimeCreatedEnd.TabIndex = 1;
            this.dateTimeCreatedEnd.Value = new System.DateTime(2017, 1, 30, 0, 0, 0, 0);
            this.dateTimeCreatedEnd.ValueChanged += new System.EventHandler(this.dateTimeCreatedEnd_ValueChanged);
            // 
            // dateTimeModifiedStart
            // 
            this.dateTimeModifiedStart.Location = new System.Drawing.Point(137, 93);
            this.dateTimeModifiedStart.Name = "dateTimeModifiedStart";
            this.dateTimeModifiedStart.Size = new System.Drawing.Size(200, 26);
            this.dateTimeModifiedStart.TabIndex = 2;
            this.dateTimeModifiedStart.Value = new System.DateTime(2017, 1, 30, 0, 0, 0, 0);
            this.dateTimeModifiedStart.ValueChanged += new System.EventHandler(this.dateTimeModifiedStart_ValueChanged);
            // 
            // dateTimeModifiedEnd
            // 
            this.dateTimeModifiedEnd.Location = new System.Drawing.Point(343, 93);
            this.dateTimeModifiedEnd.Name = "dateTimeModifiedEnd";
            this.dateTimeModifiedEnd.Size = new System.Drawing.Size(200, 26);
            this.dateTimeModifiedEnd.TabIndex = 3;
            this.dateTimeModifiedEnd.Value = new System.DateTime(2017, 1, 30, 0, 0, 0, 0);
            this.dateTimeModifiedEnd.ValueChanged += new System.EventHandler(this.dateTimeModifiedEnd_ValueChanged);
            // 
            // dateTimeClosedStart
            // 
            this.dateTimeClosedStart.Location = new System.Drawing.Point(137, 139);
            this.dateTimeClosedStart.Name = "dateTimeClosedStart";
            this.dateTimeClosedStart.Size = new System.Drawing.Size(200, 26);
            this.dateTimeClosedStart.TabIndex = 4;
            this.dateTimeClosedStart.Value = new System.DateTime(2017, 1, 30, 0, 0, 0, 0);
            this.dateTimeClosedStart.ValueChanged += new System.EventHandler(this.dateTimeClosedStart_ValueChanged);
            // 
            // dateTimeClosedEnd
            // 
            this.dateTimeClosedEnd.Location = new System.Drawing.Point(343, 139);
            this.dateTimeClosedEnd.Name = "dateTimeClosedEnd";
            this.dateTimeClosedEnd.Size = new System.Drawing.Size(200, 26);
            this.dateTimeClosedEnd.TabIndex = 5;
            this.dateTimeClosedEnd.Value = new System.DateTime(2017, 1, 30, 0, 0, 0, 0);
            this.dateTimeClosedEnd.ValueChanged += new System.EventHandler(this.dateTimeClosedEnd_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Created Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Modified Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Closed Date";
            // 
            // bClearCreatedDate
            // 
            this.bClearCreatedDate.Location = new System.Drawing.Point(549, 38);
            this.bClearCreatedDate.Name = "bClearCreatedDate";
            this.bClearCreatedDate.Size = new System.Drawing.Size(75, 41);
            this.bClearCreatedDate.TabIndex = 9;
            this.bClearCreatedDate.Text = "Clear";
            this.bClearCreatedDate.UseVisualStyleBackColor = true;
            this.bClearCreatedDate.Click += new System.EventHandler(this.bClearCreatedDate_Click);
            // 
            // bClearModifiedDate
            // 
            this.bClearModifiedDate.Location = new System.Drawing.Point(549, 132);
            this.bClearModifiedDate.Name = "bClearModifiedDate";
            this.bClearModifiedDate.Size = new System.Drawing.Size(75, 39);
            this.bClearModifiedDate.TabIndex = 10;
            this.bClearModifiedDate.Text = "Clear";
            this.bClearModifiedDate.UseVisualStyleBackColor = true;
            this.bClearModifiedDate.Click += new System.EventHandler(this.bClearModifiedDate_Click);
            // 
            // bClearClosedDate
            // 
            this.bClearClosedDate.Location = new System.Drawing.Point(549, 85);
            this.bClearClosedDate.Name = "bClearClosedDate";
            this.bClearClosedDate.Size = new System.Drawing.Size(75, 41);
            this.bClearClosedDate.TabIndex = 11;
            this.bClearClosedDate.Text = "Clear";
            this.bClearClosedDate.UseVisualStyleBackColor = true;
            this.bClearClosedDate.Click += new System.EventHandler(this.bClearClosedDate_Click);
            // 
            // bSaveDates
            // 
            this.bSaveDates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bSaveDates.Location = new System.Drawing.Point(468, 190);
            this.bSaveDates.Name = "bSaveDates";
            this.bSaveDates.Size = new System.Drawing.Size(75, 39);
            this.bSaveDates.TabIndex = 12;
            this.bSaveDates.Text = "Save";
            this.bSaveDates.UseVisualStyleBackColor = true;
            this.bSaveDates.Click += new System.EventHandler(this.bSaveDates_Click);
            // 
            // bCancelDates
            // 
            this.bCancelDates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancelDates.Location = new System.Drawing.Point(549, 190);
            this.bCancelDates.Name = "bCancelDates";
            this.bCancelDates.Size = new System.Drawing.Size(75, 39);
            this.bCancelDates.TabIndex = 13;
            this.bCancelDates.Text = "Cancel";
            this.bCancelDates.UseVisualStyleBackColor = true;
            this.bCancelDates.Click += new System.EventHandler(this.bCancelDates_Click);
            // 
            // FilterByDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 247);
            this.Controls.Add(this.bCancelDates);
            this.Controls.Add(this.bSaveDates);
            this.Controls.Add(this.bClearClosedDate);
            this.Controls.Add(this.bClearModifiedDate);
            this.Controls.Add(this.bClearCreatedDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimeClosedEnd);
            this.Controls.Add(this.dateTimeClosedStart);
            this.Controls.Add(this.dateTimeModifiedEnd);
            this.Controls.Add(this.dateTimeModifiedStart);
            this.Controls.Add(this.dateTimeCreatedEnd);
            this.Controls.Add(this.dateTimeCreatedStart);
            this.Name = "FilterByDate";
            this.Text = "FilterByDate";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimeCreatedStart;
        private System.Windows.Forms.DateTimePicker dateTimeCreatedEnd;
        private System.Windows.Forms.DateTimePicker dateTimeModifiedStart;
        private System.Windows.Forms.DateTimePicker dateTimeModifiedEnd;
        private System.Windows.Forms.DateTimePicker dateTimeClosedStart;
        private System.Windows.Forms.DateTimePicker dateTimeClosedEnd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bClearCreatedDate;
        private System.Windows.Forms.Button bClearModifiedDate;
        private System.Windows.Forms.Button bClearClosedDate;
        private System.Windows.Forms.Button bSaveDates;
        private System.Windows.Forms.Button bCancelDates;
    }
}
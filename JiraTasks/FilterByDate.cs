using JiraTasks.Data;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace JiraTasks
{
    public partial class FilterByDate : Form
    {
        private Font StandardFont = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular);
        private UserPrefs UserPreferences { get; set; }

        public FilterByDate(UserPrefs userPreferences)
        {
            InitializeComponent();
            InitializeDateTime();
            UserPreferences = userPreferences;
            InitializeWindowWithUserPrefs();
        }

        private void InitializeDateTime()
        {
            dateTimeCreatedStart.Format = DateTimePickerFormat.Custom;
            ModifyDateTimeFormat(dateTimeCreatedStart, true);
            dateTimeCreatedStart.Font = StandardFont;
            dateTimeCreatedEnd.Format = DateTimePickerFormat.Custom;
            ModifyDateTimeFormat(dateTimeCreatedEnd, true);
            dateTimeCreatedEnd.Font = StandardFont;

            dateTimeModifiedStart.Format = DateTimePickerFormat.Custom;
            ModifyDateTimeFormat(dateTimeModifiedStart, true);
            dateTimeModifiedStart.Font = StandardFont;
            dateTimeModifiedEnd.Format = DateTimePickerFormat.Custom;
            ModifyDateTimeFormat(dateTimeModifiedEnd, true);
            dateTimeModifiedEnd.Font = StandardFont;

            dateTimeClosedStart.Format = DateTimePickerFormat.Custom;
            ModifyDateTimeFormat(dateTimeClosedStart, true);
            dateTimeClosedStart.Font = StandardFont;
            dateTimeClosedEnd.Format = DateTimePickerFormat.Custom;
            ModifyDateTimeFormat(dateTimeClosedEnd, true);
            dateTimeClosedEnd.Font = StandardFont;
        }

        //Clear Dates Methods:
        private void bClearCreatedDate_Click(object sender, EventArgs e)
        {
            ModifyDateTimeFormat(dateTimeCreatedStart, true);
            ModifyDateTimeFormat(dateTimeCreatedEnd, true);
        }

        private void bClearModifiedDate_Click(object sender, EventArgs e)
        {
            ModifyDateTimeFormat(dateTimeModifiedStart, true);
            ModifyDateTimeFormat(dateTimeModifiedEnd, true);
        }

        private void bClearClosedDate_Click(object sender, EventArgs e)
        {
            ModifyDateTimeFormat(dateTimeClosedStart, true);
            ModifyDateTimeFormat(dateTimeClosedEnd, true);
        }

        //Save and Cancel
        private void bSaveDates_Click(object sender, EventArgs e)
        {
            SaveDatesToUserPrefs();
            Close();
        }

        private void bCancelDates_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Modified Date Values
        private void dateTimeCreatedEnd_ValueChanged(object sender, EventArgs e)
        {
            ModifyDateTimeFormat(dateTimeCreatedEnd, false);
        }

        private void dateTimeCreatedStart_ValueChanged(object sender, EventArgs e)
        {
            ModifyDateTimeFormat(dateTimeCreatedStart, false);
        }

        private void dateTimeModifiedEnd_ValueChanged(object sender, EventArgs e)
        {
            ModifyDateTimeFormat(dateTimeModifiedEnd, false);
        }

        private void dateTimeModifiedStart_ValueChanged(object sender, EventArgs e)
        {
            ModifyDateTimeFormat(dateTimeModifiedStart, false);
        }

        private void dateTimeClosedStart_ValueChanged(object sender, EventArgs e)
        {
            ModifyDateTimeFormat(dateTimeClosedStart, false);
        }

        private void dateTimeClosedEnd_ValueChanged(object sender, EventArgs e)
        {
            ModifyDateTimeFormat(dateTimeClosedEnd, false);
        }

        //Initialize Filter Window
        private void InitializeWindowWithUserPrefs()
        {
            //Created Dates
            if (UserPreferences.CreatedDateRange != null)
            {
                if (UserPreferences.CreatedDateRange.Start != null)
                    ModifyDateTimeFormat(dateTimeCreatedStart, false);
                if (UserPreferences.CreatedDateRange.End != null)
                    ModifyDateTimeFormat(dateTimeCreatedEnd, false);
            }

            //Modified Dates
            if (UserPreferences.ModifiedDateRange != null)
            {
                if (UserPreferences.ModifiedDateRange.Start != null)
                    ModifyDateTimeFormat(dateTimeModifiedStart, false);
                if (UserPreferences.ModifiedDateRange.Start != null)
                    ModifyDateTimeFormat(dateTimeModifiedEnd, false);
            }

            //Closed Dates
            if (UserPreferences.ClosedDateRange != null)
            {
                if (UserPreferences.ClosedDateRange.Start != null)
                    ModifyDateTimeFormat(dateTimeClosedStart, false);
                if (UserPreferences.ClosedDateRange.End != null)
                    ModifyDateTimeFormat(dateTimeClosedEnd, false);
            }
        }

        #region Support Methods

        private void ModifyDateTimeFormat(DateTimePicker dateTimePicker, bool clearFormat)
        {
            dateTimePicker.CustomFormat = clearFormat ? " " : "MM-dd-yyyy";
        }

        private void SaveDatesToUserPrefs()
        {
            //Created Dates
            if (dateTimeCreatedStart.CustomFormat != " ")
                UserPreferences.CreatedDateRange.Start = dateTimeCreatedStart.Value.Date;
            else
                UserPreferences.CreatedDateRange.Start = null;
            if (dateTimeCreatedEnd.CustomFormat != " ")
                UserPreferences.CreatedDateRange.End = dateTimeCreatedEnd.Value.Date;
            else
                UserPreferences.CreatedDateRange.End = null;

            //Modified Dates
            if (dateTimeModifiedStart.CustomFormat != " ")
                UserPreferences.ModifiedDateRange.Start = dateTimeModifiedStart.Value.Date;
            else
                UserPreferences.ModifiedDateRange.Start = null;
            if (dateTimeModifiedEnd.CustomFormat != " ")
                UserPreferences.ModifiedDateRange.End = dateTimeModifiedEnd.Value.Date;
            else
                UserPreferences.ModifiedDateRange.Start = null;

            //Closed Dates
            if (dateTimeClosedStart.CustomFormat != " ")
                UserPreferences.ClosedDateRange.Start = dateTimeClosedStart.Value.Date;
            else
                UserPreferences.ClosedDateRange.Start = null;
            if (dateTimeClosedEnd.CustomFormat != " ")
                UserPreferences.ClosedDateRange.End = dateTimeClosedEnd.Value.Date;
            else
                UserPreferences.ClosedDateRange.Start = null;

            UserPreferences.Save();
        }

        #endregion Support Methods
    }
}
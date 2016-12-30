using System;
using System.Windows.Forms;

namespace JiraTasks.MainWindowBusi
{
    internal static class DataGridBusi
    {
        internal static DataGridViewColumn FirstOrDefault(this DataGridViewColumnCollection collection, Func<DataGridViewColumn, bool> lambda)
        {
            foreach (var col in collection)
            {
                var column = col as DataGridViewColumn;
                if (lambda(column))
                {
                    return column;
                }
            }
            return null;
        }
    }
}
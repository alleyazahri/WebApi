using System;

namespace JiraApi.DataClasses
{
    public class DateTimeRange
    {
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }

        public DateTimeRange()
        {
            Start = null;
            End = null;
        }

        public DateTimeRange(DateTime? start, DateTime? end)
        {
            Start = start;
            End = end;
        }

        public DateTimeRange DeepCopy()
        {
            return new DateTimeRange(Start, End);
        }
    }
}
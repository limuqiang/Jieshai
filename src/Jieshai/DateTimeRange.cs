using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jieshai
{
    [Serializable]
    public class DateTimeRange
    {
        public DateTimeRange()
        {
        }

        public DateTimeRange(DateTime? start, DateTime? end)
        {
            this.Start = start;
            this.End = end;
        }

        public DateTime? Start { set; get; }

        public DateTime? End { set; get; }

        public bool InRange(DateTime? date)
        {
            if (!date.HasValue)
            {
                return false;
            }

            if (this.End.HasValue && this.Start.HasValue)
            {
                if (date.Value < Start.Value || date.Value > this.End.Value)
                {
                    return false;
                }
            }
            else if (this.End.HasValue)
            {
                if (date.Value > this.End.Value)
                {
                    return false;
                }
            }
            else if (this.Start.HasValue)
            {
                if (date.Value < Start.Value)
                {
                    return false;
                }
            }
            return true;
        }

        public bool InRange(DateTimeRange range)
        {
            if (this.InRange(range.Start))
            {
                return true;
            }
            if (this.InRange(range.End))
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            if (this.Start.HasValue && this.End.HasValue)
            {
                return string.Format("{0}到{1}", this.Start.Value.ToString("yyyy-MM-dd HH:mm"), this.End.Value.ToString("yyyy-MM-dd HH:mm"));
            }
            else if (this.Start.HasValue)
            {
                return string.Format("{0}以后", this.Start.Value.ToString("yyyy-MM-dd HH:mm"));
            }
            else if (this.End.HasValue)
            {
                return string.Format("{0}以前", this.End.Value.ToString("yyyy-MM-dd HH:mm"));
            }
            return "";
        }
    }
}

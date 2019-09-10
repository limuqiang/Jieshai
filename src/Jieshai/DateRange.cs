﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jieshai
{
    [Serializable]
    public class DateRange
    {
        public DateRange()
        {
        }

        public DateRange(DateTime? start, DateTime? end)
        {
            this.start = start;
            this.end = end;
        }

        public DateTime? start { set; get; }

        public DateTime? end { set; get; }

        public bool InRange(DateTime? date)
        {
            if (!date.HasValue)
            {
                return false;
            }

            if (this.end.HasValue && this.start.HasValue)
            {
                if (date.Value.Date < start.Value.Date || date.Value.Date > this.end.Value.Date)
                {
                    return false;
                }
            }
            else if (this.end.HasValue)
            {
                if (date.Value.Date > this.end.Value.Date)
                {
                    return false;
                }
            }
            else if (this.start.HasValue)
            {
                if (date.Value.Date < start.Value.Date)
                {
                    return false;
                }
            }
            return true;
        }

        public bool InRange(DateRange range)
        {
            if (this.InRange(range.start))
            {
                return true;
            }
            if (this.InRange(range.end))
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            if (this.start.HasValue && this.end.HasValue)
            {
                return string.Format("{0}到{1}", this.start.Value.ToString("yyyy-MM-dd"), this.end.Value.ToString("yyyy-MM-dd"));
            }
            else if (this.start.HasValue)
            {
                return string.Format("{0}以后", this.start.Value.ToString("yyyy-MM-dd"));
            }
            else if (this.end.HasValue)
            {
                return string.Format("{0}以前", this.end.Value.ToString("yyyy-MM-dd"));
            }
            return "";
        }
    }
}

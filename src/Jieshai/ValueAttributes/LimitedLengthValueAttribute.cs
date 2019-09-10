using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jieshai
{
    public class LimitedLengthValueAttribute : Attribute
    {
        public LimitedLengthValueAttribute(int length)
        {
            this.Length = length;
        }

        public int Length { set; get; }
    }
}

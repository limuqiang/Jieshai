using System;
using System.Collections.Generic;
using System.Text;

namespace Jieshai.Core
{
    public class IncomeCalculateArgs
    {
        public IncomeCalculateArgs()
        {
            this.InvestorName = "";
        }

        public DateTimeRange IncomeDateRange { set; get; }

        public string InvestorName { set; get; }
    }
}

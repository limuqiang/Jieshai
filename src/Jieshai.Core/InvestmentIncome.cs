using System;
using System.Collections.Generic;
using System.Text;

namespace Jieshai.Core
{
    public class InvestmentIncome
    {
        public Investment Investment { set; get; }

        public float Amount { set; get; }

        public DateTime IncomeDate { set; get; }
    }
}

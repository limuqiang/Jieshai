using System;
using System.Collections.Generic;
using System.Text;

namespace Jieshai.Core
{
    public class OrderIncome
    {
        public Investment Investment { set; get; }

        public float Money { set; get; }

        public int ToadyQuantity { set; get; }

        public int TotalQuantity { set; get; }

        public DateTime IncomeDate { set; get; }
    }
}

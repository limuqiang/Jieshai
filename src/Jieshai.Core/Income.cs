using System;
using System.Collections.Generic;
using System.Text;

namespace Jieshai.Core
{
    public class Income
    {
        public Investment Investment { set; get; }

        public float OrderIncomeMoney { set; get; }

        public int ToadyOrderQuantity { set; get; }

        public int TotalOrderQuantity { set; get; }

        public float FixIncomeMoney { set; get; }

        public DateTime IncomeDate { set; get; }
    }
}

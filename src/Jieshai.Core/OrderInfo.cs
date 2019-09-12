using System;
using System.Collections.Generic;
using System.Text;

namespace Jieshai.Core
{
    public class OrderCreateInfo
    {
        public Investor Investor { set; get; }

        public int Quantity { set; get; }

        public DateTime ReceivingDate { set; get; }
    }
}

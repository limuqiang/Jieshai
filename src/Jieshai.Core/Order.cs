using System;
using System.Collections.Generic;
using System.Text;

namespace Jieshai.Core
{
    public class Order: IIdProvider
    {
        public int Id { set; get; }

        public Investment Investment { set; get; }

        public int Quantity { set; get; }

        public DateTime ReceivingDate { set; get; }
    }
}

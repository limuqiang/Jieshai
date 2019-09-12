using System;
using System.Collections.Generic;
using System.Text;

namespace Jieshai.Core
{
    public class InvestmentCreateInfo
    {
        public string Name { set; get; }

        public Investor Investor { set; get; }

        public float Money { set; get; }

        public DateTime InvestDate { set; get; }

        public string Remark { set; get; }
    }
}

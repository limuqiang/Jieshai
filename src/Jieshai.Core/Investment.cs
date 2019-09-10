using System;
using System.Collections.Generic;
using System.Text;

namespace Jieshai.Core
{
    public class Investment: IIdProvider
    {
        public int Id { set; get; }

        public string Name { set; get; }

        public Investor Investor { set; get; }

        public float Amount { set; get; }

        public DateTime InvestDate { set; get; }

        public string Remark { set; get; }
    }
}

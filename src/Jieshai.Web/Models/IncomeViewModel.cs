using System;

namespace Jieshai.Web.Models
{
    public class IncomeViewModel
    {
        public string InvestorName { set; get; }

        public DateTime IncomeDate { set; get; }

        public string InvertmentName { set; get; }

        public float FixIncomeMoney { set; get; }

        public int ToadyQuantity { set; get; }

        public int TotalQuantity { set; get; }

        public float OrderIncomeMoney { set; get; }

        public float TotalIncomeMoney { get { return this.FixIncomeMoney + this.OrderIncomeMoney; } }
    }
}
using System;

namespace Jieshai.Web.Models
{
    public class IncomeSeachModel
    {
        public IncomeSeachModel()
        {
            IncomeDateRange = new DateTimeRange(DateTime.Today.AddDays(-100), DateTime.Today);
        }

        public DateTimeRange IncomeDateRange { set; get; }

        public string InvestorName { set; get; }

        public DateTime IncomeDate { set; get; }

        public string InvertmentName { set; get; }

        public float FixIncomeMoney { set; get; }

        public int ToadyQuantity { set; get; }

        public int TotalQuantity { set; get; }

        public float OrderIncomeMoney { set; get; }
    }
}
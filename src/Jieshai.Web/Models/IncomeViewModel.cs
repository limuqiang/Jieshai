using Jieshai.Core;
using System;

namespace Jieshaincome.Web.Models
{
    public class IncomeViewModel
    {
        public IncomeViewModel()
        {

        }
        public IncomeViewModel(Income income)
        {
            this.IncomeDate = income.IncomeDate;
            this.InvestorName = income.Investment.Investor.Name;
            this.InvertmentName = income.Investment.Name;
            this.OrderIncomeMoney = income.OrderIncomeMoney;
            this.FixIncomeMoney = income.FixIncomeMoney;
            this.ToadyQuantity = income.ToadyOrderQuantity;
            this.TotalQuantity = income.TotalOrderQuantity;
        }

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
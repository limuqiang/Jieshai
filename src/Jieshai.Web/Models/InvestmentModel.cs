using Jieshai.Core;
using System;

namespace Jieshai.Web.Models
{
    public class InvestmentViewModel
    {
        public InvestmentViewModel()
        {

        }
        public InvestmentViewModel(Investment investment)
        {
            ObjectMapperHelper.Map(this, investment);
            this.InvestorId = investment.Investor.Id;
        }

        public int Id { set; get; }

        public string Name { set; get; }

        public int InvestorId { set; get; }

        public float Money { set; get; }

        public DateTime InvestDate { set; get; }

        public string Remark { set; get; }
    }

    public class InvestmentCreateModel
    {
        public InvestmentCreateModel()
        {

        }

        public string Name { set; get; }

        public int InvestorId { set; get; }

        public float Money { set; get; }

        public DateTime InvestDate { set; get; }

        public string Remark { set; get; }
    }

    public class InvestmentSearchModel
    {
        public InvestmentSearchModel()
        {
            this.InvestorName = "";
            this.InvestDateRange = new DateRange(DateTime.Today.AddDays(-100), DateTime.Today);
        }

        public string InvestorName { set; get; }

        public DateRange InvestDateRange { set; get; }
    }
}
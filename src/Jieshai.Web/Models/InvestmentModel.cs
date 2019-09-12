using Jieshai.Core;
using System;

namespace Jieshaincome.Web.Models
{
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
}
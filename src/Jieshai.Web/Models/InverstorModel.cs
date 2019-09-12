using Jieshai.Core;
using System;

namespace Jieshai.Web.Models
{
    public class InverstorCreateModel
    {
        public InverstorCreateModel()
        {

        }

        public string Name { set; get; }

        public string Remark { set; get; }
    }

    public class InvestorSearchModel
    {
        public InvestorSearchModel()
        {
            this.Name = "";
        }

        public string Name { set; get; }
    }


    public class InvestorViewModel
    {
        public InvestorViewModel()
        {

        }
        public InvestorViewModel(Investor investor)
        {
            ObjectMapperHelper.Map(this, investor);
        }

        public int Id { set; get; }

        public string Name { set; get; }

        public string Remark { set; get; }
    }
}
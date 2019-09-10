using Jieshai.Cache.CacheManagers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Jieshai.Core
{
    public class InvestmentManager : ByIdCacheManager<Investment>
    {
        public List<Investment> GetInvestments(DateTime startDate, DateTime endDate)
        {
            return this.Where(i => i.InvestDate <= endDate && i.InvestDate >= startDate);
        }

        public Investment GetInvestment(Investor investor, DateTime date)
        {
            var investments = this.Where(i => i.Investor == investor && i.InvestDate <= date);

            return investments.OrderBy(i => i.InvestDate).FirstOrDefault();
        }
    }
}

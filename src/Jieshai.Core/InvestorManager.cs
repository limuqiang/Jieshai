using Jieshai.Cache.CacheManagers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Jieshai.Core
{
    public class InvestorManager : ByIdCacheManager<Investor>
    {
        public Investor Create(InvestorCreateInfo createInfo)
        {
            if (createInfo.Name == null)
            {
                throw new ArgumentNullException("createInfo.Name");
            }

            Investor investor = new Investor();
            investor.Id = this.CacheList.Max(i => i.Id) + 1;
            investor.Name = createInfo.Name;

            this.Add(investor);

            return investor;
        }
    }
}

﻿using Jieshai.Cache.CacheManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jieshai.Cache.CacheIndexes
{
    public class ByNameCacheIndex<T>: DicationaryCacheIndex<T, string>
        where T : class, INameProvider
    {
        public ByNameCacheIndex(CacheManager<T> cacheManager):
            base(cacheManager)
        {
            
        }

        protected override string GetKey(T cache)
        {
            return cache.Name;
        }
    }
}

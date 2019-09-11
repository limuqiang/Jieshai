using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jieshai.Cache
{
    public class CacheChangedArgs<CacheType>
    {
        public CacheType Snapshot { set; get; }

        public CacheType Cache { set; get; }
    }
}

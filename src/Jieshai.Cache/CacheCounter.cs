using Jieshai.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jieshai.Cache
{
    public abstract class CacheCounter<T>
    {
        public abstract bool Count(T t);

        public abstract string Serialize();
    }
}

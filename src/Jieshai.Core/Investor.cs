using System;
using System.Collections.Generic;
using System.Text;

namespace Jieshai.Core
{
    public class Investor: IIdProvider
    {
        public int Id { set; get; }

        public string Name { set; get; }
    }
}

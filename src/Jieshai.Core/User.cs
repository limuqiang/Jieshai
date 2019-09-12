using System;
using System.Collections.Generic;
using System.Text;

namespace Jieshai.Core
{
    public class User : IIdProvider
    {
        public int Id { set; get; }

        public string Name { set; get; }

        public string Account { set; get; }

        public string Password { set; get; }
    }
}

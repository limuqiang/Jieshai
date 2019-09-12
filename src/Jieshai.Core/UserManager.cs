using Jieshai.Cache.CacheManagers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Jieshai.Core
{
    public class UserManager : ByIdCacheManager<User>
    {
        public UserManager()
        {
            this._tokens = new Dictionary<string, User>();
        }

        Dictionary<string, User> _tokens;

        public string Login(string account, string password)
        {
            var user = this.Where(u => u.Account == account && u.Password == password).First();
            if(user == null)
            {
                throw new Exception("用户名或密码错误");
            }
            string token = Guid.NewGuid().ToString();

            this._tokens.Add(token, user);

            return token;
        }

        public bool ExistToken(string token)
        {
            return this._tokens.ContainsKey(token);
        }
    }
}

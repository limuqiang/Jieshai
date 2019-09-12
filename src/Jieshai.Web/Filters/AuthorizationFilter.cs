using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;

namespace Jieshai.Web
{
    /// <summary>
    /// 授权过滤
    /// </summary>
    public class AuthorizationFilter : IAuthorizationFilter
    {
        public AuthorizationFilter()
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <remarks>用于统一过滤登录用户的状态是否有效，如果无效，拒绝请求</remarks>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var httpCtx = context.HttpContext;
            //跳过的请求
            if (SkipRequest(httpCtx.Request.Path))
            {
                return;
            }
            var hasToken = httpCtx.Request.Cookies.TryGetValue("token", out string token);

            if (hasToken)
            {
                var existToken = Jieshai.Core.JieshaiManager.Instace.UserManager.ExistToken(token);
                if (existToken)
                {
                    return;
                }
            }

            var ulr = new UserLoginResult(-1)
            {
                Message = "请重新登录"
            };
            context.Result = new JsonResult(ulr);
        }
        /// <summary>
        /// 是否跳过请求url，比如登录请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private bool SkipRequest(string url)
        {
            List<string> skipUrls = new List<string>()
            {
                "/login"
            };
            url = url.ToLowerInvariant();
            return skipUrls.Any(p => url.IndexOf(p)==0);
        }
    }
}

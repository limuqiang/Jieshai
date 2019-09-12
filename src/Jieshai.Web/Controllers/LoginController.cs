using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jieshai.Core;
using Jieshaincome.Web.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Jieshai.Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [HttpGet]
        public IActionResult Login(string account, string password)
        {
            var token = JieshaiManager.Instace.UserManager.Login(account, password);
            this.Response.Cookies.Append("token", token);


            return this.RedirectToAction("Home", "Index");
        }
    }
}

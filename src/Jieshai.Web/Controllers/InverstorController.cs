using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jieshai.Core;
using Jieshai.Web.Models;
using Jieshaincome.Web.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Jieshai.Web.Controllers
{
    public class InverstorController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [HttpGet]
        public IActionResult GetList(InvestorSearchModel searchModel)
        {
            var viewModels = JieshaiManager.Instace.InvestorManager
                .Where(i => i.Name.IndexOf(searchModel.Name, StringComparison.OrdinalIgnoreCase) > -1)
                .Select(i => new InvestorViewModel(i))
                .OrderBy(i => i.Name)
                .ToList();

            return this.Json(viewModels);
        }

        [HttpPost]
        [HttpGet]
        public IActionResult Create(InverstorCreateModel createModel)
        {
            var createInfo = ObjectMapperHelper.Map<InvestorCreateInfo>(createModel);
            var investor = JieshaiManager.Instace.InvestorManager.Create(createInfo);


            return this.RedirectToAction("Index");
        }
    }
}

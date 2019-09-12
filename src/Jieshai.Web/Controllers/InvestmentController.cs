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
    public class InvestmentController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [HttpGet]
        public IActionResult GetList(InvestmentSearchModel searchModel)
        {
            var viewModels = JieshaiManager.Instace.InvestmentManager
                .Where(i => i.Investor.Name.IndexOf(searchModel.InvestorName, StringComparison.OrdinalIgnoreCase) > -1
                    && searchModel.InvestDateRange.InRange(i.InvestDate))
                .OrderByDescending(i => i.InvestDate)
                .Select(i => new InvestmentViewModel(i))
                .ToList();

            return this.Json(viewModels);
        }

        [HttpPost]
        [HttpGet]
        public IActionResult Create(InvestmentCreateModel createModel)
        {
            var createInfo = ObjectMapperHelper.Map<InvestmentCreateInfo>(createModel);
            createInfo.Investor = JieshaiManager.Instace.InvestorManager.GetById(createModel.InvestorId);
            var investor = JieshaiManager.Instace.InvestmentManager.Create(createInfo);


            return this.RedirectToAction("Index");
        }
    }
}

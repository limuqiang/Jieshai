using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Jieshai.Web.Models;
using Jieshai.Core;
using Jieshaincome.Web.Models;

namespace Jieshai.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [HttpGet]
        public IActionResult GetIncomeList(IncomeSeachModel searchModel)
        {
            
            IncomeCalculateArgs incomeCalculateArgs = ObjectMapperHelper.Map<IncomeCalculateArgs>(searchModel);
            var incomes = JieshaiManager.Instace.IncomeManager.CalculateIncome(incomeCalculateArgs);

            var incomeModels = incomes
                .Select(i => ObjectMapperHelper.Map<IncomeViewModel>(i));

            return Json(new { incomes = incomeModels });
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

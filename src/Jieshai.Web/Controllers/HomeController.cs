using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Jieshai.Web.Models;
using Jieshai.Core;

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

            OrderIncomeCalculator orderIncomeCalculator = 
                new OrderIncomeCalculator(JieshaiManager.Instace, searchModel.IncomeDateRange.start.Value, searchModel.IncomeDateRange.end.Value);

            var incomes = orderIncomeCalculator.Calculate();

            var incomeModels = incomes
                .Select(i => new IncomeViewModel { IncomeDate = i.IncomeDate, InvestorName = i.Investment.Investor.Name, InvertmentName = i.Investment.Name,
                    OrderIncomeMoney = i.Money, ToadyQuantity = i.ToadyQuantity, TotalQuantity = i.TotalQuantity });

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

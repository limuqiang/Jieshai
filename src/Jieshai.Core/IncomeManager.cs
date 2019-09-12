using System;
using System.Collections.Generic;
using System.Text;

namespace Jieshai.Core
{
    public class IncomeManager
    {
        public IncomeManager(JieshaiManager jieshaiManager)
        {
            this._jieshaiManager = jieshaiManager;
        }

        JieshaiManager _jieshaiManager;

        public List<Income> CalculateIncome(IncomeCalculateArgs args)
        {
            var investments = this._jieshaiManager.InvestmentManager
                .Where(i => args.IncomeDateRange.InRange(i.InvestDate) 
                    && i.Investor.Name.IndexOf(args.InvestorName, StringComparison.OrdinalIgnoreCase) > -1);

            List<Income> incomes = new List<Income>();
            foreach (var investment in investments)
            {
                var orders = this._jieshaiManager.OrderManager.GetOrders(investment, args.IncomeDateRange);
                if(orders.Count > 0)
                {
                    IncomeCalculator incomeCalculator = new IncomeCalculator(investment, orders);
                    var investmentIncomes = incomeCalculator.Calculate();

                    incomes.AddRange(investmentIncomes);
                }
            }

            return incomes;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Jieshai.Core
{
    public class OrderIncomeCalculator
    {
        public OrderIncomeCalculator(JieshaiManager jieshaiManager, DateTime startDate, DateTime endDate)
        {
            this._jieshaiManager = jieshaiManager;
            this._startDate = startDate;
            this._endDate = endDate;
        }

        JieshaiManager _jieshaiManager;
        DateTime _startDate;
        DateTime _endDate;

        public List<OrderIncome> Calculate()
        {
            var investments = this._jieshaiManager.InvestmentManager.GetInvestments(this._startDate, this._endDate);

            return this.Calculate(investments);
        }

        public List<OrderIncome> Calculate(List<Investment> investments)
        {
            List<OrderIncome> orderIncomes = new List<OrderIncome>();
            foreach (var investment in investments)
            {
                orderIncomes.AddRange(this.Calculate(investment));
            }

            return orderIncomes;
        }

        public List<OrderIncome> Calculate(Investment investment)
        {
            List<OrderIncome> orderIncomes = new List<OrderIncome>();
            DateTime incomeDate = this._startDate;
            while (incomeDate <= this._endDate)
            {
                var orderTotalQuantity = this._jieshaiManager.OrderManager.GetOrderQuantity(investment, investment.InvestDate, incomeDate);
                var toadyTotalQuantity = this._jieshaiManager.OrderManager.GetOrderQuantity(investment, incomeDate);

                if (orderTotalQuantity > 0)
                {
                    OrderIncome orderIncome = new OrderIncome();
                    orderIncome.Money = this.CalculateTodayOrderIncomeMoney(orderTotalQuantity);
                    orderIncome.ToadyQuantity = toadyTotalQuantity;
                    orderIncome.TotalQuantity = orderTotalQuantity;
                    orderIncome.IncomeDate = incomeDate;
                    orderIncome.Investment = investment;

                    orderIncomes.Add(orderIncome);
                }

                incomeDate = incomeDate.AddDays(1);
            }

            return orderIncomes;
        }

        public float CalculateTodayOrderIncomeMoney(int orderTotalQuantity)
        {
            float money = 0;
            if (orderTotalQuantity > 3000)
            {
                money = (orderTotalQuantity - 3000) * 80;

                return money + this.CalculateTodayOrderIncomeMoney(3000);
            }
            else if (orderTotalQuantity > 1000)
            {
                money = (orderTotalQuantity - 1000) * 60;

                return money + this.CalculateTodayOrderIncomeMoney(1000);
            }
            else if (orderTotalQuantity > 300)
            {
                money = (orderTotalQuantity - 300) * 50;

                return money + this.CalculateTodayOrderIncomeMoney(300);
            }
            else if (orderTotalQuantity > 100)
            {
                money = (orderTotalQuantity - 100) * 40;

                return money + this.CalculateTodayOrderIncomeMoney(100);
            }
            else if (orderTotalQuantity > 10)
            {
                money = (orderTotalQuantity -10) * 30;
            }

            return money;
        }
    }
}

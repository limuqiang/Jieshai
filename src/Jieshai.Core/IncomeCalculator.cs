using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Jieshai.Core
{
    public class IncomeCalculator
    {
        public IncomeCalculator(Investment investment, List<Order> orders)
        {
            this._investment = investment;
            this._orders = orders;
        }

        Investment _investment;
        List<Order> _orders;


        public List<Income> Calculate()
        {
            List<Income> orderIncomes = new List<Income>();
            DateTime incomeDate = this._orders.Min(o => o.ReceivingDate);
            var orderMaxReceivingDate = this._orders.Max(o => o.ReceivingDate);
            while (incomeDate <= orderMaxReceivingDate)
            {
                var orderTotalQuantity = this._orders.Where(o => o.ReceivingDate >= this._investment.InvestDate && o.ReceivingDate <= incomeDate).Sum(o => o.Quantity);
                var toadyTotalQuantity = this._orders.Where(o => o.ReceivingDate == incomeDate).Sum(o => o.Quantity);

                if (orderTotalQuantity > 0)
                {
                    Income orderIncome = new Income();
                    orderIncome.OrderIncomeMoney = this.CalculateTodayOrderIncomeMoney(orderTotalQuantity);
                    orderIncome.FixIncomeMoney = this.CalculateInvestmentIncomeMoney();
                    orderIncome.ToadyOrderQuantity = toadyTotalQuantity;
                    orderIncome.TotalOrderQuantity = orderTotalQuantity;
                    orderIncome.IncomeDate = incomeDate;
                    orderIncome.Investment = this._investment;

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

        public float CalculateInvestmentIncomeMoney()
        {
            return (float)(this._investment.Money * 0.03);
        }
    }
}

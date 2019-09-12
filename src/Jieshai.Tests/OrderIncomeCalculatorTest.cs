using Jieshai.Core;
using System;
using Xunit;
using System.Linq;

namespace Jieshai.Tests
{
    public class OrderIncomeCalculatorTest
    {
        [Fact]
        public void Calculate()
        {
            JieshaiManager jieshaiManager = new JieshaiManager();

            jieshaiManager.InvestorManager.Add(new Investor { Id = 1, Name = "张三" });
            jieshaiManager.InvestorManager.Add(new Investor { Id = 2, Name = "李四" });
            jieshaiManager.InvestmentManager.Add(new Investment { Id = 1, Money = 30000, InvestDate = DateTime.Today.AddDays(-30), Investor = jieshaiManager.InvestorManager.GetById(1) });
            jieshaiManager.InvestmentManager.Add(new Investment { Id = 2, Money = 40000, InvestDate = DateTime.Today.AddDays(-28), Investor = jieshaiManager.InvestorManager.GetById(1) });
            jieshaiManager.InvestmentManager.Add(new Investment { Id = 3, Money = 50000, InvestDate = DateTime.Today.AddDays(-26), Investor = jieshaiManager.InvestorManager.GetById(2) });
            jieshaiManager.OrderManager.Add(new Order { Id = 1, Investment = jieshaiManager.InvestmentManager.GetById(1), Quantity = 50, ReceivingDate = DateTime.Today.AddDays(-29)  });
            jieshaiManager.OrderManager.Add(new Order { Id = 2, Investment = jieshaiManager.InvestmentManager.GetById(1), Quantity = 50, ReceivingDate = DateTime.Today.AddDays(-28) });
            jieshaiManager.OrderManager.Add(new Order { Id = 7, Investment = jieshaiManager.InvestmentManager.GetById(3), Quantity = 50, ReceivingDate = DateTime.Today.AddDays(-28) });
            jieshaiManager.OrderManager.Add(new Order { Id = 3, Investment = jieshaiManager.InvestmentManager.GetById(1), Quantity = 50, ReceivingDate = DateTime.Today.AddDays(-27) });
            jieshaiManager.OrderManager.Add(new Order { Id = 4, Investment = jieshaiManager.InvestmentManager.GetById(1), Quantity = 50, ReceivingDate = DateTime.Today.AddDays(-26) });

            
            var orderIncomes = jieshaiManager.IncomeManager.CalculateIncome(new IncomeCalculateArgs { IncomeDateRange = new DateTimeRange(DateTime.Today.AddDays(-30), DateTime.Today.AddDays(-30)) });

            Assert.Equal(0, orderIncomes.Count);

            orderIncomes = jieshaiManager.IncomeManager.CalculateIncome(new IncomeCalculateArgs { IncomeDateRange = new DateTimeRange(DateTime.Today.AddDays(-30), DateTime.Today.AddDays(-27)) });

            Assert.Equal(3, orderIncomes.Count);
            Assert.Equal(1200, orderIncomes[0].OrderIncomeMoney);
            Assert.Equal(1200 + 1500, orderIncomes[1].OrderIncomeMoney);
            Assert.Equal(1200 + 1500 + 2000, orderIncomes[2].OrderIncomeMoney);
        }

        [Fact]
        public void CalculateTodayOrderIncomeMoney()
        {
            JieshaiManager jieshaiManager = new JieshaiManager();

            IncomeCalculator orderIncomeCalculator = new IncomeCalculator(null, null);
            var money1 = orderIncomeCalculator.CalculateTodayOrderIncomeMoney(99);
            Assert.Equal((99 -10) * 30, money1);

            var money2 = orderIncomeCalculator.CalculateTodayOrderIncomeMoney(160);
            Assert.Equal((100 - 10) * 30 + (160 - 100) * 40, money2);

            var money3 = orderIncomeCalculator.CalculateTodayOrderIncomeMoney(360);
            Assert.Equal((100 - 10) * 30 + (300 - 100) * 40 + (360 - 300) * 50, money3);

            var money4 = orderIncomeCalculator.CalculateTodayOrderIncomeMoney(2600);
            Assert.Equal((100 - 10) * 30 + (300 - 100) * 40 + (1000 - 300) * 50 + (2600 - 1000) * 60, money4);

            var money5 = orderIncomeCalculator.CalculateTodayOrderIncomeMoney(6600);
            Assert.Equal((100 - 10) * 30 + (300 - 100) * 40 + (1000 - 300) * 50 + (3000 - 1000) * 60 + (6600 - 3000) * 80, money5);
        }
    }
}

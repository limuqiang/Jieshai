using System;
using System.Collections.Generic;
using System.Text;

namespace Jieshai.Core
{
    public class JieshaiManager
    {
        public static JieshaiManager Instace { private set; get; }

        static JieshaiManager()
        {
            Instace = new JieshaiManager();
        }

        public JieshaiManager()
        {
            this.InvestorManager = new InvestorManager();
            this.InvestmentManager = new InvestmentManager();
            this.OrderManager = new OrderManager();
            this.IncomeManager = new IncomeManager(this);
        }

        public InvestorManager InvestorManager { set; get; }

        public InvestmentManager InvestmentManager { set; get; }

        public OrderManager OrderManager { set; get; }

        public IncomeManager IncomeManager { set; get; }

        public void Load()
        {
            this.LoadInvestor();
            this.LoadInvestment();
            this.LoadOrder();
        }

        public void LoadInvestor()
        {
            this.InvestorManager.Add(new Investor { Id = 1, Name = "张三" });
            this.InvestorManager.Add(new Investor { Id = 2, Name = "李四" });
        }

        public void LoadInvestment()
        {
            var zhangshan = this.InvestorManager.GetById(1);
            this.InvestmentManager.Add(new Investment { Id = 1, Name = "一期", Amount = 30000, InvestDate = DateTime.Today.AddDays(-80), Investor = zhangshan });
            this.InvestmentManager.Add(new Investment { Id = 2, Name = "二期", Amount = 40000, InvestDate = DateTime.Today.AddDays(-60), Investor = zhangshan });

            var lisi = this.InvestorManager.GetById(2);
            this.InvestmentManager.Add(new Investment { Id = 3, Name = "一期", Amount = 50000, InvestDate = DateTime.Today.AddDays(-60), Investor = lisi });
        }

        public void LoadOrder()
        {
            Random random = new Random();
            var zhangshan = this.InvestorManager.GetById(1);

            for(int i = 1; i < 80; i ++)
            {
                var receivingDate = DateTime.Today.AddDays(i - 80);
                var investment = this.InvestmentManager.GetInvestment(zhangshan, receivingDate);
                if(investment == null)
                {
                    throw new ArgumentNullException("investment");
                }

                this.OrderManager.Add(new Order { Id = i, Investment = investment, Quantity = random.Next(1, 50), ReceivingDate = receivingDate });
            }

            var lisi = this.InvestorManager.GetById(2);

            for (int i = 1; i < 60; i++)
            {
                var receivingDate = DateTime.Today.AddDays(i - 60);
                var investment = this.InvestmentManager.GetInvestment(lisi, receivingDate);
                if (investment == null)
                {
                    throw new ArgumentNullException("investment");
                }

                this.OrderManager.Add(new Order { Id = i + 100, Investment = investment, Quantity = random.Next(1, 50), ReceivingDate = receivingDate });
            }
        }
    }
}

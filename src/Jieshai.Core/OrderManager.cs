using Jieshai.Cache.CacheManagers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Jieshai.Core
{
    public class OrderManager : ByIdCacheManager<Order>
    {
        public OrderManager(JieshaiManager jieshaiManager)
        {
            this._jieshaiManager = jieshaiManager;
        }

        JieshaiManager _jieshaiManager;

        public int GetOrderQuantity(Investment investment, DateTime startDate, DateTime endDate)
        {
            var orders = this.Where(o => o.Investment == investment && o.ReceivingDate >= startDate && o.ReceivingDate <= endDate);
            return orders.Sum(o => o.Quantity);
        }

        public int GetOrderQuantity(Investment investment, DateTime date)
        {
            var orders = this.Where(o => o.Investment == investment && o.ReceivingDate == date);
            return orders.Sum(o => o.Quantity);
        }

        public List<Order> GetOrders(Investment investment, DateTime startDate, DateTime endDate)
        {
            return this.Where(o => o.Investment == investment && o.ReceivingDate >= startDate && o.ReceivingDate <= endDate);
        }

        public List<Order> GetOrders(Investment investment, DateTime date)
        {
            return this.Where(o => o.Investment == investment && o.ReceivingDate == date);
        }

        public List<Order> GetOrders(Investment investment, DateTimeRange dateRange)
        {
            return this.Where(o => o.Investment == investment && dateRange.InRange(o.ReceivingDate));
        }

        public Order Create(OrderCreateInfo createInfo)
        {
            if(createInfo.Investor == null)
            {
                throw new ArgumentNullException("没有找到投资人");
            }

            var investment = this._jieshaiManager.InvestmentManager.GetInvestment(createInfo.Investor, createInfo.ReceivingDate);

            if(investment == null)
            {
                throw new Exception("没有找到投资人的投资记录");
            }

            Order order = new Order();
            order.Id = this.CacheList.Max(o => o.Id) + 1;
            order.Investment = investment;
            order.Quantity = createInfo.Quantity;
            order.ReceivingDate = createInfo.ReceivingDate;

            this.Add(order);

            return order;
        }
    }
}

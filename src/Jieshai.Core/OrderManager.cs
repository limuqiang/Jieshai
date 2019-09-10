using Jieshai.Cache.CacheManagers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Jieshai.Core
{
    public class OrderManager : ByIdCacheManager<Order>
    {
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
    }
}

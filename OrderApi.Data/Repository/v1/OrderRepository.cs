using OrderApi.Data.Database;
using OrderApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Data.Repository.v1
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext orderContext) : base(orderContext)
        {
        }
    }
}

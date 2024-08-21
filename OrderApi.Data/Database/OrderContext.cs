using Microsoft.EntityFrameworkCore;
using OrderApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Data.Database
{
    public class OrderContext : DbContext
    {
        public virtual DbSet<Order> Order { get; set; }
    }
}

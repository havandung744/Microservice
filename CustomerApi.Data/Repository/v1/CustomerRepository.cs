﻿using CustomerApi.Data.Database;
using CustomerApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerApi.Data.Repository.v1
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(CustomerContext orderContext) : base(orderContext)
        {
        }

        public async Task<Customer> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await CustomerContext.Customer.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}

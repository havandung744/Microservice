using CustomerApi.Data.Database;
using CustomerApi.Domain.Entities;

namespace CustomerApi.Data.Repository.v1
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(CustomerContext orderContext) : base(orderContext)
        {
        }
    }
}

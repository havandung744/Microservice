using CustomerApi.Data.Repository.v1;
using CustomerApi.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApi.Service.v1.Query
{
    public class GetCustomerByIdCommand : IRequest<Customer>
    {
        public Guid Id { get; set; }
    }

    public class GetCustomerByIdCommandHandler : IRequestHandler<GetCustomerByIdCommand, Customer>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerByIdCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> Handle(GetCustomerByIdCommand request, CancellationToken cancellationToken)
        {
            return await _customerRepository.GetCustomerByIdAsync(request.Id, cancellationToken);
        }
    }
}

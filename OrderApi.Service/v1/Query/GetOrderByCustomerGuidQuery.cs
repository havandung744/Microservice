using MediatR;
using OrderApi.Data.Repository.v1;
using OrderApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Service.v1.Query
{
    public class GetOrderByCustomerGuidQuery : IRequest<List<Order>>
    {
        public Guid CustomerId { get; set; }
    }

    public class GetOrderByCustomerGuidQueryHandler : IRequestHandler<GetOrderByCustomerGuidQuery, List<Order>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderByCustomerGuidQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<Order>> Handle(GetOrderByCustomerGuidQuery request, CancellationToken cancellationToken)
        {
            return await _orderRepository.GetOrderByCustomerGuidAsync(request.CustomerId);
        }
    }
}

using MediatR;
using OrderApi.Data.Repository.v1;
using OrderApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Service.v1.Command
{
    public class CreateOrderCommand : IRequest<Order>
    {
        public Order Order { get; set; }
    }

    public class CreateOrderCommandHander : IRequestHandler<CreateOrderCommand, Order>
    {
        private readonly IOrderRepository _orderRepository;

        public CreateOrderCommandHander(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            return await _orderRepository.AddAsync(request.Order);
        }
    }
}

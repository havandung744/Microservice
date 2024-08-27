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
    public class UpdateOrderCommand : IRequest<Order>
    {
        public Order Order { get; set; }
    }

    public class UpdateOrderCommandHander : IRequestHandler<UpdateOrderCommand, Order>
    {
        private readonly IOrderRepository _orderRepository;

        public UpdateOrderCommandHander(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            return await _orderRepository.UpdateAsync(request.Order);
        }
    }
}

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
    public class GetOrdersCommand : IRequest<List<Order>>
    {
    }

    public class GetOrdersCommandHander : IRequestHandler<GetOrdersCommand, List<Order>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrdersCommandHander(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<Order>> Handle(GetOrdersCommand request, CancellationToken cancellationToken)
        {
            return _orderRepository.GetAll().ToList();
        }
    }
}

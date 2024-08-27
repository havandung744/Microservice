using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Data.Repository.v1;
using OrderApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Service.v1.Command
{
    public class DeleteOrderCommand : IRequest<int>
    {
        public string Id { get; set; }
        //public Order Order { get; set; }
    }

    public class DeleteOrderCommandHanler : IRequestHandler<DeleteOrderCommand, int>
    {

        protected readonly IOrderRepository _orderRepository;

        public DeleteOrderCommandHanler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<int> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {

            return await _orderRepository.DeleteAsync(request.Id);
        }
    }
}

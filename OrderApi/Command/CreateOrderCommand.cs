using MediatR;
using OrderApi.Domain.Entities;

namespace OrderApi.Command
{

    public class CreateOrderCommand : IRequest<Order>
    {
        public Order Order { get; set; }
    }
}

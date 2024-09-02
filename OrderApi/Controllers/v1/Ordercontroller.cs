using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Domain.Entities;
using OrderApi.Models.v1;
using OrderApi.Service.v1.Command;

namespace OrderApi.Controllers.v1
{
    [Route("v1/[controller]")]
    [ApiController]

    public class Ordercontroller : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public Ordercontroller(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("CreateOrder")]
        public async Task<ActionResult<Order>> CreateOrder(OrderModel orderModel)
        {
            try
            {
                return await _mediator.Send(new CreateOrderCommand
                {
                    Order = _mapper.Map<Order>(orderModel)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateOrder")]
        public async Task<ActionResult<Order>> UpdateOrder(OrderModel orderModel)
        {
            try
            {
                return await _mediator.Send(new UpdateOrderCommand
                {
                    Order = _mapper.Map<Order>(orderModel)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetOrders")]
        public async Task<ActionResult<List<Order>>> GetOrders()
        {
            try
            {
                return await _mediator.Send(new GetOrdersCommand());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteOrder")]
        public async Task<ActionResult<int>> DeleteOrder(DeleteOrderCommand deleteOrderCommand)
        {
            try
            {
                //return await _mediator.Send(deleteOrderCommand);
                return await _mediator.Send(deleteOrderCommand);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}

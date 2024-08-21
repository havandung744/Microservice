using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Domain.Entities;
using OrderApi.Models.v1;

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
        //[Route("GetLstCity")]
        public async Task<ActionResult<Order>> Order(OrderModel orderModel)
        {
            try
            {
                return await _mediator.Send(new OrderApi.Service.v1.Command.CreateOrderCommand
                {
                    Order = _mapper.Map<Order>(orderModel)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

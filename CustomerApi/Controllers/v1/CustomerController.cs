using AutoMapper;
using CustomerApi.Domain.Entities;
using CustomerApi.Models.v1;
using CustomerApi.Service.v1.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApi.Controllers.v1
{
    [Route("v1/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CustomerController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("CreateCustomer")]
        public async Task<ActionResult<Customer>> CreateCustomer(CreateCustomerModel createCustomerModel)
        {
            try
            {
                return await _mediator.Send(new CreateCustomerCommand
                {
                    Customer = _mapper.Map<Customer>(createCustomerModel)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

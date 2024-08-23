using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Service.v1.Services
{
    public class CustomerNameUpdateService : ICustomerNameUpdateService
    {
        private readonly IMediator _mediator;

        public CustomerNameUpdateService(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}

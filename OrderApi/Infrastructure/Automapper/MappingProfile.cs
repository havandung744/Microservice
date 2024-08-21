using AutoMapper;
using OrderApi.Domain.Entities;
using OrderApi.Models.v1;

namespace OrderApi.Infrastructure.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OrderModel, Order>().ReverseMap();
        }
    }
}

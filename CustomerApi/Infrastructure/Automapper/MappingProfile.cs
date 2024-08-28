using AutoMapper;
using CustomerApi.Domain.Entities;
using CustomerApi.Models.v1;
using OrderApi.Domain.Entities;

namespace CustomerApi.Infrastructure.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCustomerModel, Customer>().ReverseMap();
        }
    }
}

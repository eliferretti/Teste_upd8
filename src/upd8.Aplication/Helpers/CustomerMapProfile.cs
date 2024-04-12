using AutoMapper;
using upd8.Aplication.Dtos;
using upd8.Domain.Entity;

namespace upd8.Aplication.Helpers
{
    public class CustomerMapProfile : Profile
    {
        public CustomerMapProfile() 
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Customer, AddCustomerDto>().ReverseMap();
            CreateMap<Customer, FilterCustomerDto>().ReverseMap();
        }
    }
}

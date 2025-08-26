using AutoMapper;
using CepApi.Application.Abstraction.Domain.DTO;
using CepApi.Application.Abstraction.Domain.Models;

namespace CepApi.Application.Mapper
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressDTO>();
            CreateMap<AddressDTO, Address>();

            CreateMap<Login, LoginDTO>();
            CreateMap<LoginDTO, Login>();
        }
    }
}

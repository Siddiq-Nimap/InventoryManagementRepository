using AutoMapper;
using PMS.Models.Models;
using PMS.Models.Models.DTO;

namespace PMS.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Catagory, CatagoryDto>();
            Mapper.CreateMap<CatagoryDto, Catagory>();

            Mapper.CreateMap<Product, ProductDto>();
            Mapper.CreateMap<ProductDto, Product>();

            //Mapper.CreateMap<IEnumerable<Catagory> , IEnumerable<CatagoryDto>>();
            //Mapper.CreateMap<IEnumerable<CatagoryDto> , IEnumerable<Catagory>>();

            Mapper.CreateMap<LoginDto, Logins>();
           Mapper.CreateMap<Logins, LoginDto>();

            Mapper.CreateMap<SignUpDto, Logins>();
            Mapper.CreateMap<Logins, SignUpDto>();


        }
    }
}
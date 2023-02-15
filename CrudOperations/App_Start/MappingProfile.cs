using AutoMapper;
using DAL.DTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudOperations.App_Start
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
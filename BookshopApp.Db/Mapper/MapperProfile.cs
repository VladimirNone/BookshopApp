using AutoMapper;
using BookshopApp.Models;
using BookshopApp.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshopApp.Db.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<OrderState, OrderState>();

            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorDto, Author>();
            
            CreateMap<Product, ProductDto>()
                .ForMember(h => h.Author, g=>g.MapFrom(src => src.Author));
            CreateMap<ProductDto, Product>();

            CreateMap<Order, OrderDto>()
                .ForMember(h => h.Customer, g => g.MapFrom(src => src.Customer))
                .ForMember(h => h.State, g => g.MapFrom(src => src.State));
            CreateMap<OrderDto, Order>();
        }
    }
}

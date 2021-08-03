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

            CreateMap<User, UserPrivateDto>();
            
            CreateMap<Product, ProductDto>()
                .ForMember(h => h.Author, g=>g.MapFrom(src => src.Author));
            CreateMap<ProductDto, Product>();

            CreateMap<OrderedProduct, CartProductDto>()
                .ForMember(h => h.Product, g => g.MapFrom(src => src.Product));

            CreateMap<Order, OrderDto>()
                .ForMember(h => h.Customer, g => g.MapFrom(src => src.Customer))
                .ForMember(h => h.OrderedProducts, g => g.MapFrom(src => src.OrderedProducts))
                .ForMember(h => h.State, g => g.MapFrom(src => src.State))
                .ForMember(h => h.FinalAmount, g => g.MapFrom(src => Math.Round(src.FinalAmount, 2)));
            CreateMap<Order, CartDto>()
                .ForMember(h => h.OrderedProducts, g => g.MapFrom(src => src.OrderedProducts))
                .ForMember(h => h.FinalAmount, g => g.MapFrom(src => Math.Round(src.FinalAmount, 2)));
        }
    }
}

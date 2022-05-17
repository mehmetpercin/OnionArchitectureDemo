using Application.Commands.Product.Update;
using Application.Queries.Product;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product,ProductResponse>().ReverseMap();
            CreateMap<Product, UpdateProductCommand>().ReverseMap();
        }
    }
}

using Aspiria.Models;
using Aspiria.DTOs;
using AutoMapper;

namespace Aspiria.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // Entity → DTO
            CreateMap<Product, ProductDto>();

            // DTO → Entity
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
        }
    }
}
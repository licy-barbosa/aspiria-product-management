using Aspiria.Data;
using Aspiria.DTOs;
using Aspiria.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AspiriaApiTest
{
    public class DatabaseTest
    {
        protected AppDbContext BuildContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
            return new AppDbContext(options);
        }

        protected IMapper ConfigMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDto>();
                cfg.CreateMap<CreateProductDto, Product>();
                cfg.CreateMap<UpdateProductDto, Product>();
            });
            return config.CreateMapper();
        }
    }
}
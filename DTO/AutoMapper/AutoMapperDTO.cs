using AutoMapper;
using DataBase.Models;
using DataBase.Models.Carts;
using DTO.Models.Carts;
using DTO.Models.Categories;
using DTO.Models.Products;

namespace DTO.AutoMapper
{
    public static class AutoMapperDTO
    {
        public static readonly Mapper Mapper = new (new MapperConfiguration(GetMapperConfigurationExpression()));

        private static MapperConfigurationExpression GetMapperConfigurationExpression()
        {
            var cfg = new MapperConfigurationExpression();

            // Catergories
            cfg.CreateMap<CategoryDTO, Category>();
            cfg.CreateMap<Category, CategoryDTO>();

            // Products
            cfg.CreateMap<ProductDTO, Product>();
            cfg.CreateMap<Product, ProductDTO>();

            // Carts
            cfg.CreateMap<CartDTO, Cart>();
            cfg.CreateMap<Cart, CartDTO>();

            return cfg;
        }
    }
}
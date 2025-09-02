using AutoMapper;
using ECommerce.Core.DTOs.Products;
using ECommerce.Core.Entities.Product;

namespace ECommerce.API.Mapping
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ViewProductDto>().ReverseMap();

            CreateMap<AddProductDto, Product>().ReverseMap()
                .ForMember(x => x.Photo, op => op.Ignore());

            CreateMap<UpdateProductDto, Product>().ReverseMap()
                .ForMember(x => x.Photo, op => op.Ignore());
        }
    }
}

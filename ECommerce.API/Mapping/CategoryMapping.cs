using AutoMapper;
using ECommerce.Core.DTOs.Categories;
using ECommerce.Core.Entities.Product;

namespace ECommerce.API.Mapping
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
           CreateMap<AddCategoryDto, Category>().ReverseMap();
           CreateMap<UpdateCategoryDto, Category>().ReverseMap();
        }
    }
}

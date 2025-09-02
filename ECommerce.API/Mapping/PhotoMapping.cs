using AutoMapper;
using ECommerce.Core.DTOs.Images;
using ECommerce.Core.Entities.Product;

namespace ECommerce.API.Mapping
{
    public class PhotoMapping : Profile
    {
        public PhotoMapping()
        {
            CreateMap<Photo,ViewPhotoDto>().ReverseMap();
        }
    }
}

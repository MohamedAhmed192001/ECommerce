using AutoMapper;
using ECommerce.Core.DTOs.Orders;
using ECommerce.Core.Entities;
using ECommerce.Core.Entities.Order;

namespace ECommerce.API.Mapping
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<Order, OrderToReturnDTO>()
                .ForMember(d => d.deliveryMethod,
                o => o.
                MapFrom(s => s.deliveryMethod.Name))
                .ReverseMap();

            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
            CreateMap<ShippingAddress, ShipAddressDTO>().ReverseMap();
            CreateMap<Address, ShipAddressDTO>().ReverseMap();
        }
    }
}

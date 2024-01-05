using AutoMapper;
using Order.Contracts.V1.Request;
using Order.DataAccessLayer.Entities;

namespace Order.PresentationLayer.Mapping
{
    public class RequestToDomain : Profile
    {
        public RequestToDomain()
        {
            CreateMap<ProductModelRequest, Products>();
            CreateMap<OrderModelRequest, Orders>();
        }
    }
}

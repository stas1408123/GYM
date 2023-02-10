using AutoMapper;
using GYM.BLL.Models;
using GYM.DAL.Entities;

namespace GYM.BLL.Mapping
{
    public class OrderModelMapping : Profile
    {
        public OrderModelMapping()
        {
            CreateMap<OrderModel, OrderEntity>().ReverseMap();
        }
    }
}

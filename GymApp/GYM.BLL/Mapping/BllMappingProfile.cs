using AutoMapper;
using GYM.BLL.Models;
using GYM.DAL.Entities;

namespace GYM.BLL.Mapping
{
    public class BllMappingProfile : Profile
    {
        public BllMappingProfile()
        {
            CreateMap<CouchModel, CouchEntity>().ReverseMap();

            CreateMap<OrderModel, OrderEntity>().ReverseMap();

            CreateMap<VisitorModel, VisitorEntity>().ReverseMap();
        }
    }
}

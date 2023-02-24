using AutoMapper;
using GYM.API.Models;
using GYM.BLL.Models;

namespace GYM.API.Mapping
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<CouchViewModel, CouchModel>().ReverseMap();

            CreateMap<OrderViewModel, OrderModel>().ReverseMap();

            CreateMap<VisitorViewModel, VisitorModel>().ReverseMap();
        }
    }
}

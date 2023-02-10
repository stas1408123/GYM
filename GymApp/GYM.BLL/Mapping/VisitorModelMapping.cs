using AutoMapper;
using GYM.BLL.Models;
using GYM.DAL.Entities;

namespace GYM.BLL.Mapping
{
    public class VisitorModelMapping : Profile
    {
        public VisitorModelMapping()
        {
            CreateMap<VisitorModel, VisitorEntity>().ReverseMap();
        }
    }
}

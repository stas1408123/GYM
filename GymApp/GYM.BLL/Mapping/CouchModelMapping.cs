using AutoMapper;
using GYM.BLL.Models;
using GYM.DAL.Entities;

namespace GYM.BLL.Mapping
{
    public class CouchModelMapping : Profile
    {
        public CouchModelMapping()
        {
            CreateMap<CouchModel, CouchEntity>().ReverseMap();
        }
    }
}

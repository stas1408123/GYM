using AutoMapper;
using GYM.BLL.Models;
using GYM.DAL.Entities;
using GYM.DAL.Repositories.Abstractions;

namespace GYM.BLL.Services
{
    public class VisitorService : GenericService<VisitorEntity, VisitorModel>
    {
        public VisitorService(IRepository<VisitorEntity> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}

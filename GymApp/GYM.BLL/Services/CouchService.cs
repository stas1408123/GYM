using AutoMapper;
using GYM.BLL.Abstractions;
using GYM.BLL.Models;
using GYM.DAL.Entities;
using GYM.DAL.Repositories.Abstractions;
using System.Linq.Expressions;

namespace GYM.BLL.Services
{
    public class CouchService : GenericService<CouchEntity,CouchModel>
    {
        public CouchService(IRepository<CouchEntity> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}

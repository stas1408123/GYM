using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GYM.DAL.Entities;
using GYM.DAL.Repositories.Abstractions;

namespace GYM.DAL.Repositories
{
    public class VisitorRepository:IRepository<VisitorEntity>
    {
        public IEnumerable<VisitorEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public VisitorEntity Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VisitorEntity> Find(Func<VisitorEntity, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Create(VisitorEntity item)
        {
            throw new NotImplementedException();
        }

        public void Update(VisitorEntity item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}

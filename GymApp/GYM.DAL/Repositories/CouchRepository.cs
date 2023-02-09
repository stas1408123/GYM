using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GYM.DAL.Entities;
using GYM.DAL.Repositories.Abstractions;

namespace GYM.DAL.Repositories
{
    public class CouchRepository:IRepository<CouchEntity>
    {
        public IEnumerable<CouchEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public CouchEntity Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CouchEntity> Find(Func<CouchEntity, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Create(CouchEntity item)
        {
            throw new NotImplementedException();
        }

        public void Update(CouchEntity item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}

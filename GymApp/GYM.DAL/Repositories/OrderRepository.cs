using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GYM.DAL.Entities;
using GYM.DAL.Repositories.Abstractions;

namespace GYM.DAL.Repositories
{
    public class OrderRepository:IRepository<OrderEntity>
    {
        public IEnumerable<OrderEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public OrderEntity Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderEntity> Find(Func<OrderEntity, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Create(OrderEntity item)
        {
            throw new NotImplementedException();
        }

        public void Update(OrderEntity item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}

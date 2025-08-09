using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly OrderDBContext _dbContext;

        public BaseRepository(OrderDBContext dBContext) 
        {
            _dbContext = dBContext;
        }

        public List<T> Get()
        {
            return _dbContext.Set<T>().ToList();
        }

        public T Get<TId>(TId id)
        {
            return _dbContext.Set<T>().Find(new object[] { id });
        }

    }
}

using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> Get<TId>(TId id)
        {
            return await _dbContext.Set<T>().FindAsync(new object[] { id });
        }

        public void Add(T item)
        {
            _dbContext.Set<T>().Add(item);
        }

        public void Update(T item)
        {
            _dbContext.Set<T>().Update(item);
        }

        public void Delete(T item)
        {
            _dbContext.Set<T>().Remove(item);
        }

    }
}

using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly OrderDBContext _dbContext;

        public UserRepository(OrderDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<User?> GetUser(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
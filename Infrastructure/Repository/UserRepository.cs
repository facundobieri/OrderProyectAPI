using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<User?> GetUser(string email, string password)
        {
            return await _dbContext.Users
                .Where(x => x.IsActive)
                .FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _dbContext.Users
                .Where(u => u.IsActive)
                .ToListAsync();
        }

        public async Task<User?> GetById(int id)
        {
            return await _dbContext.Users
                .Where(u => u.IsActive)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _dbContext.Users
                .Where(u => u.IsActive)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> CreateUser(User user)
        {
            user.IsActive = true;
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task UpdateUser(User user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUser(int id)
        {
            // Baja lógica
            var user = await _dbContext.Users.FindAsync(id);
            if (user != null)
            {
                user.IsActive = false;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class OrderDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public OrderDBContext() { }
        public OrderDBContext(DbContextOptions<OrderDBContext> options) : base(options) { }
    }
}
